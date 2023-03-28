using LinqKit;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Tecnocim.Alia.Application.CommandHandlers;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Services;

public class ExtractorService : IExtractorService
{
    private readonly Alia.Domain.Repositories.IUnitOfWork _efUnitOfWork;
    private readonly Intermedia.Domain.Repositories.IUnitOfWork _intermediaUnitOfWork;
    private readonly ILogger<UploadFicheroCommandHandler> _logger;

    public ExtractorService(
           Alia.Domain.Repositories.IUnitOfWork efUnitOfWork,
           Intermedia.Domain.Repositories.IUnitOfWork intermediaUnitOfWork,
           ILogger<UploadFicheroCommandHandler> logger)
    {
        _efUnitOfWork = efUnitOfWork;
        _intermediaUnitOfWork = intermediaUnitOfWork;
        _logger = logger;
    }

    public async Task<bool> Extract(UploadFicheroResponse fichero)
    {
        // primero copiamos el fichero a la ubicación que usa el Extractor
        // el usuario IIS debe tener permisos de escritura en destino
        // TODO: refactorizarlo por objetos FILE y no por SHELL
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        cmd.StartInfo.Arguments = "/C copy \"C:\\FicherosUsuarios\\" + fichero.Nombre + "\" \"C:\\Users\\Administrator\\Documents\\HistorialBSV\\" + fichero.Nombre + "\"";
        _logger.LogInformation("PKK arguments: " + cmd.StartInfo.Arguments);
        _logger.LogInformation("PKK argument2s: " + "/C copy \"C:\\FicherosUsuarios\\" + fichero.Nombre + "\" \"C:\\Users\\Administrator\\Documents\\HistorialBSV\\" + fichero.Nombre + "\"");
        cmd.StartInfo.WorkingDirectory = @"C:\FicherosUsuarios";
        cmd.Start();
        cmd.WaitForExit();
        string respuesta = cmd.StandardOutput.ReadToEnd();
        _logger.LogInformation("PKK respuesta: " + respuesta);
        fichero.Estado = "COPIED";

        // ejecutamos el extractor
        var empresa = await _efUnitOfWork.EmpresaRepository.GetFirstOrDefault(x => x, x => x.EmpresaId == fichero.EmpresaId && !x.Deleted.HasValue, null, null, false);

        cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        cmd.StartInfo.Arguments = "/C python manage.py upload --fecha \"" + fichero.FechaContenido.ToString("yyyy-MM-dd") + "\" --CIF " + empresa.CIF + " --tipoDoc " + fichero.Origen + " --filename \"C:\\Users\\Administrator\\Documents\\HistorialBSV\\" + fichero.Nombre + "\"";
        _logger.LogInformation("PKK python arguments: " + "/C python manage.py upload --fecha \"" + fichero.FechaContenido.ToString("yyyy-MM-dd") + "\" --CIF " + empresa.CIF + " --tipoDoc " + fichero.Origen + " --filename \"C:\\Users\\Administrator\\Documents\\HistorialBSV\\" + fichero.Nombre + "\"");
        cmd.StartInfo.WorkingDirectory = @"C:\Extractor";
        cmd.Start();
        cmd.WaitForExit();
        respuesta = cmd.StandardOutput.ReadToEnd();
        _logger.LogInformation("PKK python respuesta: " + respuesta);
        if (respuesta.Contains('|'))
        {
            string resultado = respuesta.Split('|')[0].ToString();
            int extractorid = Convert.ToInt16(respuesta.Split('|')[1].ToString());
            fichero.Estado = resultado;
            fichero.ExtractorId = extractorid;
        }
        else
        {
            fichero.Estado = "ERROR EXEC";
        }

        return true;
    }

    public async Task<bool> Sincroniza(UploadFicheroResponse fichero)
    {
        // Jorge WIP

        // Copiar Documento
        var inter_documento = await _intermediaUnitOfWork.CoreDocumentoRepository.GetFirstOrDefault(x => x, x => x.ExtraccionId == fichero.ExtractorId, null, null, false);
        var ef_documento = new Tecnocim.Alia.Domain.Documento();
        ef_documento.RutaDocumento = inter_documento.Documento;
        ef_documento.Fecha = inter_documento.Fecha;
        ef_documento.ExtractorId = Convert.ToInt16(inter_documento.ExtraccionId);
        ef_documento.Origen = inter_documento.Origen;
        ef_documento.Status = inter_documento.Status;
        ef_documento.EmpresaId = fichero.EmpresaId;
        ef_documento.Created = DateTime.Now;
        _efUnitOfWork.DocumentoRepository.Insert(ef_documento);
        _efUnitOfWork.Commit();  //TODO: Jorge ojo con este commit, no deberías hacerlo sólo al final del método?
        _logger.LogInformation("PKK Documento ID: " + ef_documento.DocumentoId);

        fichero.DocumentoId = ef_documento.DocumentoId;

        // Copiar Analitica
        List<Tecnocim.Alia.Domain.Analitica> ef_analiticas = new List<Domain.Analitica>();
        var inter_analiticas = await _intermediaUnitOfWork.CoreAnaliticaRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        inter_analiticas.ForEach(x =>
        {
            var ef_analitica = new Tecnocim.Alia.Domain.Analitica();
            ef_analitica.DocumentoId = ef_documento.DocumentoId;
            ef_analitica.Magnitud = Convert.ToDecimal(x.Magnitud);
            ef_analitica.Cuenta = x.Cuenta;
            ef_analitica.Created = DateTime.Now;
            ef_analiticas.Add(ef_analitica);
        });
        _efUnitOfWork.AnaliticaRepository.InsertRange(ef_analiticas);
        _logger.LogInformation("PKK Analiticas copiadas: " + ef_analiticas.Count());

        // Copiar Contabilidad
        List<Tecnocim.Alia.Domain.Contabilidad> ef_contabilidades = new List<Domain.Contabilidad>();
        var inter_contabilidades = await _intermediaUnitOfWork.CoreContabilidadRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        inter_contabilidades.ForEach(x =>
        {
            var ef_contabilidad = new Tecnocim.Alia.Domain.Contabilidad();
            ef_contabilidad.DocumentoId = ef_documento.DocumentoId;
            ef_contabilidad.Magnitud = Convert.ToDecimal(x.Magnitud);
            ef_contabilidad.Codigo = string.IsNullOrEmpty(x.Codigo) ? "" : x.Codigo.ToString();
            ef_contabilidad.Concepto = x.Concepto;
            ef_contabilidad.Created = DateTime.Now;
            ef_contabilidades.Add(ef_contabilidad);
        });
        _efUnitOfWork.ContabilidadRepository.InsertRange(ef_contabilidades);
        _logger.LogInformation("PKK Contabilidades copiadas: " + ef_contabilidades.Count());

        // Copiar Ratios
        List<Tecnocim.Alia.Domain.Ratio> ef_ratios = new List<Domain.Ratio>();
        var inter_ratios = await _intermediaUnitOfWork.CoreRatioRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        inter_ratios.ForEach(x =>
        {
            var ef_ratio = new Tecnocim.Alia.Domain.Ratio();
            ef_ratio.DocumentoId = ef_documento.DocumentoId;
            ef_ratio.Magnitud = Convert.ToDecimal(x.Magnitud);
            ef_ratio.Concepto = x.Concepto;
            ef_ratio.Created = DateTime.Now;
            ef_ratios.Add(ef_ratio);
        });
        _efUnitOfWork.RatioRepository.InsertRange(ef_ratios);
        _logger.LogInformation("PKK Ratios copiados: " + ef_ratios.Count());

        // Copiar Pool
        List<Tecnocim.Alia.Domain.Pool> ef_pools = new List<Domain.Pool>();
        var inter_pools = await _intermediaUnitOfWork.CorePoolRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        inter_pools.ForEach(x =>
        {
            var ef_pool = new Tecnocim.Alia.Domain.Pool();
            ef_pool.DocumentoId = ef_documento.DocumentoId;
            ef_pool.Dispuesto = Convert.ToDecimal(x.Dispuesto);
            ef_pool.Concepto = x.Concepto;
            ef_pool.Cuenta = x.Cuenta;
            ef_pool.Created = DateTime.Now;
            ef_pools.Add(ef_pool);
        });
        _efUnitOfWork.PoolRepository.InsertRange(ef_pools);
        _logger.LogInformation("PKK Pools copiados: " + ef_pools.Count());


        // Copiar Cirbe
        List<Tecnocim.Alia.Domain.Cirbe> ef_cirbes = new List<Domain.Cirbe>();
        var inter_cirbes = await _intermediaUnitOfWork.CoreCirbeRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        inter_cirbes.ForEach(async x =>
        {
            var ef_cirbe = new Tecnocim.Alia.Domain.Cirbe();
            ef_cirbe.Operacion = x.Operacion;
            ef_cirbe.Dispuesto = Convert.ToDecimal(x.Dispuesto);
            ef_cirbe.Participantes = x.Participantes;
            ef_cirbe.Importes = Convert.ToDecimal(x.Importes);
            ef_cirbe.Demora = Convert.ToDecimal(x.Demora);
            ef_cirbe.Disponible = Convert.ToDecimal(x.Disponible);
            ef_cirbe.DocumentoId = ef_documento.DocumentoId;
            // RECHECK ID
            ef_cirbe.EquivalenciasEntidadId = Convert.ToInt16(x.EntidadId);

            var moneda = _efUnitOfWork.EquivalenciasMonedaRepository.GetFirstAsync(y => y.Tipo == x.MonedaId);
            ef_cirbe.EquivalenciasMonedaId = moneda.Result.Id;

            var natinterv = _efUnitOfWork.EquivalenciasNatintervRepository.GetFirstAsync(y => y.Tipo == x.NatIntervId);
            ef_cirbe.EquivalenciasNatintervId = natinterv.Result.Id;

            var plazo = _efUnitOfWork.EquivalenciasPlazoRepository.GetFirstAsync(y => y.Tipo == x.PlazoId);
            ef_cirbe.EquivalenciasPlazoId = plazo.Result.Id;

            var producto = _efUnitOfWork.EquivalenciasProductoRepository.GetFirstAsync(y => y.Tipo == x.ProductoId);
            ef_cirbe.EquivalenciasProductoId = producto.Result.Id;

            var situoper = _efUnitOfWork.EquivalenciasSituoperRepository.GetFirstAsync(y => y.Tipo == x.SituOperId);
            ef_cirbe.EquivalenciasSituoperId = situoper.Result.Id;

            var solcol = _efUnitOfWork.EquivalenciasSolcolRepository.GetFirstAsync(y => y.Tipo == x.SolColId);
            ef_cirbe.EquivalenciasSolcolId = solcol.Result.Id;

            var tipo = _efUnitOfWork.EquivalenciasTipoRepository.GetFirstAsync(y => y.Tipo == x.TipoId);
            ef_cirbe.EquivalenciasTipoId = tipo.Result.Id;

            ef_cirbe.Created = DateTime.Now;
            ef_cirbes.Add(ef_cirbe);
        });
        _efUnitOfWork.CirbeRepository.InsertRange(ef_cirbes);

        _logger.LogInformation("PKK Cirbes copiados: " + ef_cirbes.Count());
        // TO DO copiar Cirbe

        _efUnitOfWork.Commit();

        fichero.Estado = "SINCRO-FULL";


        // CARGAR EN EL RESPONSE LOS POSIBLES ERRORES DEL EXTRACTOR
        var errores = await _intermediaUnitOfWork.CoreExtraccionesErroreRepository.GetAsync(x => x.ExtraccionId == fichero.ExtractorId);
        // WIP

        if(errores is null)
        {
            
            return true;
        }

        foreach (var error in errores)
        {
            fichero.Errores = new List<ErroresResponse>();
            fichero.Errores.Add(new ErroresResponse
            {
                Id = error.Id,
                Mensaje = error.Mensaje,
                Traza = error.Traza,
                Bloqueo = error.Bloqueo
            });
        }

        //errores.ForEach(x =>
        //{
        //    fichero.Errores.Add(new ErroresResponse()
        //    {
        //        Id = x.Id,
        //        Mensaje = x.Mensaje,
        //        Traza = x.Traza,
        //        Bloqueo = x.Bloqueo
        //    });
        //});

        return true;
    }
}
