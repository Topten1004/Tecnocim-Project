using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tecnocim.Alia.Intermedia.Domain;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure
{
    public partial class SmartdebtIntermediaContext : DbContext
    {
        public SmartdebtIntermediaContext()
        {
        }

        public SmartdebtIntermediaContext(DbContextOptions<SmartdebtIntermediaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthGroup> AuthGroups { get; set; } = null!;
        public virtual DbSet<AuthGroupPermission> AuthGroupPermissions { get; set; } = null!;
        public virtual DbSet<AuthPermission> AuthPermissions { get; set; } = null!;
        public virtual DbSet<AuthtokenToken> AuthtokenTokens { get; set; } = null!;
        public virtual DbSet<CoreAnalitica> CoreAnaliticas { get; set; } = null!;
        public virtual DbSet<CoreCirbe> CoreCirbes { get; set; } = null!;
        public virtual DbSet<CoreCirbePersonal> CoreCirbePersonals { get; set; } = null!;
        public virtual DbSet<CoreCirbeReal> CoreCirbeReals { get; set; } = null!;
        public virtual DbSet<CoreContabilidad> CoreContabilidads { get; set; } = null!;
        public virtual DbSet<CoreContrato> CoreContratos { get; set; } = null!;
        public virtual DbSet<CoreCrudo> CoreCrudos { get; set; } = null!;
        public virtual DbSet<CoreDocumento> CoreDocumentos { get; set; } = null!;
        public virtual DbSet<CoreEmpresa> CoreEmpresas { get; set; } = null!;
        public virtual DbSet<CoreExtraccione> CoreExtracciones { get; set; } = null!;
        public virtual DbSet<CoreExtraccionesErrore> CoreExtraccionesErrores { get; set; } = null!;
        public virtual DbSet<CorePool> CorePools { get; set; } = null!;
        public virtual DbSet<CoreRatio> CoreRatios { get; set; } = null!;
        public virtual DbSet<CoreUser> CoreUsers { get; set; } = null!;
        public virtual DbSet<CoreUserGroup> CoreUserGroups { get; set; } = null!;
        public virtual DbSet<CoreUserUserPermission> CoreUserUserPermissions { get; set; } = null!;
        public virtual DbSet<DjangoAdminLog> DjangoAdminLogs { get; set; } = null!;
        public virtual DbSet<DjangoContentType> DjangoContentTypes { get; set; } = null!;
        public virtual DbSet<DjangoMigration> DjangoMigrations { get; set; } = null!;
        public virtual DbSet<DjangoSession> DjangoSessions { get; set; } = null!;
        public virtual DbSet<EquivalenciasEntidad> EquivalenciasEntidads { get; set; } = null!;
        public virtual DbSet<EquivalenciasMonedum> EquivalenciasMoneda { get; set; } = null!;
        public virtual DbSet<EquivalenciasNatinterv> EquivalenciasNatintervs { get; set; } = null!;
        public virtual DbSet<EquivalenciasPersonal> EquivalenciasPersonals { get; set; } = null!;
        public virtual DbSet<EquivalenciasPlazo> EquivalenciasPlazos { get; set; } = null!;
        public virtual DbSet<EquivalenciasProducto> EquivalenciasProductos { get; set; } = null!;
        public virtual DbSet<EquivalenciasReal> EquivalenciasReals { get; set; } = null!;
        public virtual DbSet<EquivalenciasSituoper> EquivalenciasSituopers { get; set; } = null!;
        public virtual DbSet<EquivalenciasSolcol> EquivalenciasSolcols { get; set; } = null!;
        public virtual DbSet<EquivalenciasTipo> EquivalenciasTipos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:IntermediaConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("db_owner");

            modelBuilder.Entity<AuthGroup>(entity =>
            {
                entity.ToTable("auth_group");

                entity.HasIndex(e => e.Name, "auth_group_name_a6ea08ec_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<AuthGroupPermission>(entity =>
            {
                entity.ToTable("auth_group_permissions");

                entity.HasIndex(e => e.GroupId, "auth_group_permissions_group_id_b120cbf9");

                entity.HasIndex(e => new { e.GroupId, e.PermissionId }, "auth_group_permissions_group_id_permission_id_0cd325b0_uniq")
                    .IsUnique()
                    .HasFilter("([group_id] IS NOT NULL AND [permission_id] IS NOT NULL)");

                entity.HasIndex(e => e.PermissionId, "auth_group_permissions_permission_id_84c5c92e");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_group_id_b120cbf9_fk_auth_group_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_permission_id_84c5c92e_fk_auth_permission_id");
            });

            modelBuilder.Entity<AuthPermission>(entity =>
            {
                entity.ToTable("auth_permission");

                entity.HasIndex(e => e.ContentTypeId, "auth_permission_content_type_id_2f476e4b");

                entity.HasIndex(e => new { e.ContentTypeId, e.Codename }, "auth_permission_content_type_id_codename_01ab375a_uniq")
                    .IsUnique()
                    .HasFilter("([content_type_id] IS NOT NULL AND [codename] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codename)
                    .HasMaxLength(100)
                    .HasColumnName("codename");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.AuthPermissions)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_permission_content_type_id_2f476e4b_fk_django_content_type_id");
            });

            modelBuilder.Entity<AuthtokenToken>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__authtoke__DFD83CAEA287FB7A");

                entity.ToTable("authtoken_token");

                entity.HasIndex(e => e.UserId, "UQ__authtoke__B9BE370E8FB198D2")
                    .IsUnique();

                entity.Property(e => e.Key)
                    .HasMaxLength(40)
                    .HasColumnName("key");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AuthtokenToken)
                    .HasForeignKey<AuthtokenToken>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authtoken_token_user_id_35299eff_fk_core_user_id");
            });

            modelBuilder.Entity<CoreAnalitica>(entity =>
            {
                entity.ToTable("core_analitica");

                entity.HasIndex(e => new { e.Cuenta, e.DocumentoId }, "core_analitica_cuenta_documento_id_e8cb609c_uniq")
                    .IsUnique()
                    .HasFilter("([cuenta] IS NOT NULL AND [documento_id] IS NOT NULL)");

                entity.HasIndex(e => e.DocumentoId, "core_analitica_documento_id_b4d67c09");

                entity.HasIndex(e => e.ExtraccionId, "core_analitica_extraccion_id_22277380");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(100)
                    .HasColumnName("cuenta");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Magnitud).HasColumnName("magnitud");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CoreAnaliticas)
                    .HasForeignKey(d => d.DocumentoId)
                    .HasConstraintName("core_analitica_documento_id_b4d67c09_fk_core_documento_id");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreAnaliticas)
                    .HasForeignKey(d => d.ExtraccionId)
                    .HasConstraintName("core_analitica_extraccion_id_22277380_fk_core_extracciones_id");
            });

            modelBuilder.Entity<CoreCirbe>(entity =>
            {
                entity.ToTable("core_cirbe");

                entity.HasIndex(e => e.ContratoId, "core_cirbe_contrato_id_c47d0abf");

                entity.HasIndex(e => e.DocumentoId, "core_cirbe_documento_id_81c0b1ef");

                entity.HasIndex(e => new { e.DocumentoId, e.EntidadId, e.Operacion }, "core_cirbe_documento_id_entidad_id_operacion_0e3a295c_uniq")
                    .IsUnique()
                    .HasFilter("([documento_id] IS NOT NULL AND [entidad_id] IS NOT NULL AND [operacion] IS NOT NULL)");

                entity.HasIndex(e => e.EntidadId, "core_cirbe_entidad_id_8d77ee1e");

                entity.HasIndex(e => e.ExtraccionId, "core_cirbe_extraccion_id_c5e6a71e");

                entity.HasIndex(e => e.MonedaId, "core_cirbe_moneda_id_15019613");

                entity.HasIndex(e => e.NatIntervId, "core_cirbe_natInterv_id_5ce465bc");

                entity.HasIndex(e => e.PlazoId, "core_cirbe_plazo_id_e164e81a");

                entity.HasIndex(e => e.ProductoId, "core_cirbe_producto_id_f66fb75f");

                entity.HasIndex(e => e.SituOperId, "core_cirbe_situOper_id_f77490f0");

                entity.HasIndex(e => e.SolColId, "core_cirbe_solCol_id_a758a609");

                entity.HasIndex(e => e.TipoId, "core_cirbe_tipo_id_e3d3c3b1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContratoId).HasColumnName("contrato_id");

                entity.Property(e => e.Demora).HasColumnName("demora");

                entity.Property(e => e.Disponible).HasColumnName("disponible");

                entity.Property(e => e.Dispuesto).HasColumnName("dispuesto");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.EntidadId).HasColumnName("entidad_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Importes).HasColumnName("importes");

                entity.Property(e => e.MonedaId)
                    .HasMaxLength(3)
                    .HasColumnName("moneda_id");

                entity.Property(e => e.NatIntervId)
                    .HasMaxLength(3)
                    .HasColumnName("natInterv_id");

                entity.Property(e => e.Operacion)
                    .HasMaxLength(100)
                    .HasColumnName("operacion");

                entity.Property(e => e.Participantes).HasColumnName("participantes");

                entity.Property(e => e.PlazoId)
                    .HasMaxLength(3)
                    .HasColumnName("plazo_id");

                entity.Property(e => e.ProductoId)
                    .HasMaxLength(30)
                    .HasColumnName("producto_id");

                entity.Property(e => e.SituOperId)
                    .HasMaxLength(3)
                    .HasColumnName("situOper_id");

                entity.Property(e => e.SolColId)
                    .HasMaxLength(3)
                    .HasColumnName("solCol_id");

                entity.Property(e => e.TipoId)
                    .HasMaxLength(3)
                    .HasColumnName("tipo_id");

                entity.HasOne(d => d.Contrato)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.ContratoId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("core_cirbe_contrato_id_c47d0abf_fk_core_contrato_id");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.DocumentoId)
                    .HasConstraintName("core_cirbe_documento_id_81c0b1ef_fk_core_documento_id");

                entity.HasOne(d => d.Entidad)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.EntidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_entidad_id_8d77ee1e_fk_equivalencias_entidad_id");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.ExtraccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_extraccion_id_c5e6a71e_fk_core_extracciones_id");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.MonedaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_moneda_id_15019613_fk_equivalencias_moneda_tipo");

                entity.HasOne(d => d.NatInterv)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.NatIntervId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_natInterv_id_5ce465bc_fk_equivalencias_natinterv_tipo");

                entity.HasOne(d => d.Plazo)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.PlazoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_plazo_id_e164e81a_fk_equivalencias_plazo_tipo");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_producto_id_f66fb75f_fk_equivalencias_producto_tipo");

                entity.HasOne(d => d.SituOper)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.SituOperId)
                    .HasConstraintName("core_cirbe_situOper_id_f77490f0_fk_equivalencias_situoper_tipo");

                entity.HasOne(d => d.SolCol)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.SolColId)
                    .HasConstraintName("core_cirbe_solCol_id_a758a609_fk_equivalencias_solcol_tipo");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.CoreCirbes)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_cirbe_tipo_id_e3d3c3b1_fk_equivalencias_tipo_tipo");
            });

            modelBuilder.Entity<CoreCirbePersonal>(entity =>
            {
                entity.ToTable("core_cirbe_personal");

                entity.HasIndex(e => e.CirbeId, "core_cirbe_personal_cirbe_id_f5cae3eb");

                entity.HasIndex(e => new { e.CirbeId, e.PersonalId }, "core_cirbe_personal_cirbe_id_personal_id_2cc868d3_uniq")
                    .IsUnique()
                    .HasFilter("([cirbe_id] IS NOT NULL AND [personal_id] IS NOT NULL)");

                entity.HasIndex(e => e.PersonalId, "core_cirbe_personal_personal_id_810fa3e7");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CirbeId).HasColumnName("cirbe_id");

                entity.Property(e => e.PersonalId).HasColumnName("personal_id");

                entity.HasOne(d => d.Cirbe)
                    .WithMany(p => p.CoreCirbePersonals)
                    .HasForeignKey(d => d.CirbeId)
                    .HasConstraintName("core_cirbe_personal_cirbe_id_f5cae3eb_fk_core_cirbe_id");

                entity.HasOne(d => d.Personal)
                    .WithMany(p => p.CoreCirbePersonals)
                    .HasForeignKey(d => d.PersonalId)
                    .HasConstraintName("core_cirbe_personal_personal_id_810fa3e7_fk_equivalencias_personal_id");
            });

            modelBuilder.Entity<CoreCirbeReal>(entity =>
            {
                entity.ToTable("core_cirbe_real");

                entity.HasIndex(e => e.CirbeId, "core_cirbe_real_cirbe_id_b2d79410");

                entity.HasIndex(e => new { e.CirbeId, e.RealId }, "core_cirbe_real_cirbe_id_real_id_c32a8eb4_uniq")
                    .IsUnique()
                    .HasFilter("([cirbe_id] IS NOT NULL AND [real_id] IS NOT NULL)");

                entity.HasIndex(e => e.RealId, "core_cirbe_real_real_id_562fe58a");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CirbeId).HasColumnName("cirbe_id");

                entity.Property(e => e.RealId).HasColumnName("real_id");

                entity.HasOne(d => d.Cirbe)
                    .WithMany(p => p.CoreCirbeReals)
                    .HasForeignKey(d => d.CirbeId)
                    .HasConstraintName("core_cirbe_real_cirbe_id_b2d79410_fk_core_cirbe_id");

                entity.HasOne(d => d.Real)
                    .WithMany(p => p.CoreCirbeReals)
                    .HasForeignKey(d => d.RealId)
                    .HasConstraintName("core_cirbe_real_real_id_562fe58a_fk_equivalencias_real_id");
            });

            modelBuilder.Entity<CoreContabilidad>(entity =>
            {
                entity.ToTable("core_contabilidad");

                entity.HasIndex(e => new { e.Codigo, e.Concepto, e.DocumentoId }, "core_contabilidad_codigo_concepto_documento_id_5f4fc24f_uniq")
                    .IsUnique()
                    .HasFilter("([codigo] IS NOT NULL AND [concepto] IS NOT NULL AND [documento_id] IS NOT NULL)");

                entity.HasIndex(e => e.DocumentoId, "core_contabilidad_documento_id_65aa8cf0");

                entity.HasIndex(e => e.ExtraccionId, "core_contabilidad_extraccion_id_a178d9ed");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .HasColumnName("codigo");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(100)
                    .HasColumnName("concepto");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Magnitud).HasColumnName("magnitud");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CoreContabilidads)
                    .HasForeignKey(d => d.DocumentoId)
                    .HasConstraintName("core_contabilidad_documento_id_65aa8cf0_fk_core_documento_id");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreContabilidads)
                    .HasForeignKey(d => d.ExtraccionId)
                    .HasConstraintName("core_contabilidad_extraccion_id_a178d9ed_fk_core_extracciones_id");
            });

            modelBuilder.Entity<CoreContrato>(entity =>
            {
                entity.ToTable("core_contrato");

                entity.HasIndex(e => e.EntidadId, "core_contrato_entidad_id_4f1c0d58");

                entity.HasIndex(e => e.MonedaId, "core_contrato_moneda_id_c491d93e");

                entity.HasIndex(e => e.ProductoId, "core_contrato_producto_id_8ce93721");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carencia).HasColumnName("carencia");

                entity.Property(e => e.Digitalizada).HasColumnName("digitalizada");

                entity.Property(e => e.EntidadId).HasColumnName("entidad_id");

                entity.Property(e => e.Inicio)
                    .HasColumnType("date")
                    .HasColumnName("inicio");

                entity.Property(e => e.Limite).HasColumnName("limite");

                entity.Property(e => e.Minimis).HasColumnName("minimis");

                entity.Property(e => e.MonedaId)
                    .HasMaxLength(3)
                    .HasColumnName("moneda_id");

                entity.Property(e => e.Notas)
                    .HasMaxLength(1200)
                    .HasColumnName("notas");

                entity.Property(e => e.Periodificacion).HasColumnName("periodificacion");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.ProductoId)
                    .HasMaxLength(30)
                    .HasColumnName("producto_id");

                entity.Property(e => e.Valoracion).HasColumnName("valoracion");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("date")
                    .HasColumnName("vencimiento");

                entity.HasOne(d => d.Entidad)
                    .WithMany(p => p.CoreContratos)
                    .HasForeignKey(d => d.EntidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_contrato_entidad_id_4f1c0d58_fk_equivalencias_entidad_id");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.CoreContratos)
                    .HasForeignKey(d => d.MonedaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_contrato_moneda_id_c491d93e_fk_equivalencias_moneda_tipo");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CoreContratos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_contrato_producto_id_8ce93721_fk_equivalencias_producto_tipo");
            });

            modelBuilder.Entity<CoreCrudo>(entity =>
            {
                entity.ToTable("core_crudos");

                entity.HasIndex(e => new { e.Cuenta, e.DocumentoId }, "core_crudos_cuenta_documento_id_75c622a2_uniq")
                    .IsUnique()
                    .HasFilter("([cuenta] IS NOT NULL AND [documento_id] IS NOT NULL)");

                entity.HasIndex(e => e.DocumentoId, "core_crudos_documento_id_b2eadb24");

                entity.HasIndex(e => e.ExtraccionId, "core_crudos_extraccion_id_4a14a29d");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(100)
                    .HasColumnName("concepto");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(100)
                    .HasColumnName("cuenta");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Magnitud).HasColumnName("magnitud");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CoreCrudos)
                    .HasForeignKey(d => d.DocumentoId)
                    .HasConstraintName("core_crudos_documento_id_b2eadb24_fk_core_documento_id");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreCrudos)
                    .HasForeignKey(d => d.ExtraccionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("core_crudos_extraccion_id_4a14a29d_fk_core_extracciones_id");
            });

            modelBuilder.Entity<CoreDocumento>(entity =>
            {
                entity.ToTable("core_documento");

                entity.HasIndex(e => e.EmpresaId, "core_documento_empresa_id_334a5c15");

                entity.HasIndex(e => e.ExtraccionId, "core_documento_extraccion_id_25a48674");

                entity.HasIndex(e => new { e.Origen, e.Fecha, e.EmpresaId }, "core_documento_origen_fecha_empresa_id_ef6bb5e6_uniq")
                    .IsUnique()
                    .HasFilter("([origen] IS NOT NULL AND [fecha] IS NOT NULL AND [empresa_id] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Documento)
                    .HasMaxLength(100)
                    .HasColumnName("documento");

                entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Origen)
                    .HasMaxLength(9)
                    .HasColumnName("origen");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<CoreEmpresa>(entity =>
            {
                entity.ToTable("core_empresa");

                entity.HasIndex(e => e.Cif, "UQ__core_emp__C1F8DC5EF630E798")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cif)
                    .HasMaxLength(12)
                    .HasColumnName("CIF");

                entity.Property(e => e.ConfigFile)
                    .HasMaxLength(100)
                    .HasColumnName("configFile");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(50)
                    .HasColumnName("contacto");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono).HasColumnName("telefono");
            });

            modelBuilder.Entity<CoreExtraccione>(entity =>
            {
                entity.ToTable("core_extracciones");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fechahora).HasColumnName("fechahora");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(2)
                    .HasColumnName("resultado");

                entity.Property(e => e.Ruta)
                    .HasMaxLength(255)
                    .HasColumnName("ruta");

                entity.Property(e => e.RutaUnmerged)
                    .HasMaxLength(255)
                    .HasColumnName("ruta_unmerged");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<CoreExtraccionesErrore>(entity =>
            {
                entity.ToTable("core_extracciones_errores");

                entity.HasIndex(e => e.ExtraccionId, "core_extracciones_errores_extraccion_id_8b7a4f33");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bloqueo).HasColumnName("bloqueo");

                entity.Property(e => e.Campo)
                    .HasMaxLength(50)
                    .HasColumnName("campo");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(255)
                    .HasColumnName("mensaje");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(50)
                    .HasColumnName("tabla");

                entity.Property(e => e.Traza).HasColumnName("traza");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreExtraccionesErrores)
                    .HasForeignKey(d => d.ExtraccionId)
                    .HasConstraintName("core_extracciones_errores_extraccion_id_8b7a4f33_fk_core_extracciones_id");
            });

            modelBuilder.Entity<CorePool>(entity =>
            {
                entity.ToTable("core_pool");

                entity.HasIndex(e => e.ContratoId, "core_pool_contrato_id_6b37be15");

                entity.HasIndex(e => new { e.Cuenta, e.DocumentoId }, "core_pool_cuenta_documento_id_1137e5a8_uniq")
                    .IsUnique()
                    .HasFilter("([cuenta] IS NOT NULL AND [documento_id] IS NOT NULL)");

                entity.HasIndex(e => e.DocumentoId, "core_pool_documento_id_8b3590e6");

                entity.HasIndex(e => e.ExtraccionId, "core_pool_extraccion_id_c0ccd6e2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(250)
                    .HasColumnName("concepto");

                entity.Property(e => e.ContratoId).HasColumnName("contrato_id");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(50)
                    .HasColumnName("cuenta");

                entity.Property(e => e.Dispuesto).HasColumnName("dispuesto");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CorePools)
                    .HasForeignKey(d => d.DocumentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_pool_documento_id_8b3590e6_fk_core_documento_id");
            });

            modelBuilder.Entity<CoreRatio>(entity =>
            {
                entity.ToTable("core_ratio");

                entity.HasIndex(e => new { e.Concepto, e.DocumentoId }, "core_ratio_concepto_documento_id_a3a5922e_uniq")
                    .IsUnique()
                    .HasFilter("([concepto] IS NOT NULL AND [documento_id] IS NOT NULL)");

                entity.HasIndex(e => e.DocumentoId, "core_ratio_documento_id_e057fc30");

                entity.HasIndex(e => e.ExtraccionId, "core_ratio_extraccion_id_65e801a7");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(100)
                    .HasColumnName("concepto");

                entity.Property(e => e.DocumentoId).HasColumnName("documento_id");

                entity.Property(e => e.ExtraccionId).HasColumnName("extraccion_id");

                entity.Property(e => e.Magnitud).HasColumnName("magnitud");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CoreRatios)
                    .HasForeignKey(d => d.DocumentoId)
                    .HasConstraintName("core_ratio_documento_id_e057fc30_fk_core_documento_id");

                entity.HasOne(d => d.Extraccion)
                    .WithMany(p => p.CoreRatios)
                    .HasForeignKey(d => d.ExtraccionId)
                    .HasConstraintName("core_ratio_extraccion_id_65e801a7_fk_core_extracciones_id");
            });

            modelBuilder.Entity<CoreUser>(entity =>
            {
                entity.ToTable("core_user");

                entity.HasIndex(e => e.Email, "UQ__core_use__AB6E61641E6DE127")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsStaff).HasColumnName("is_staff");

                entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");

                entity.Property(e => e.LastLogin).HasColumnName("last_login");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<CoreUserGroup>(entity =>
            {
                entity.ToTable("core_user_groups");

                entity.HasIndex(e => e.GroupId, "core_user_groups_group_id_fe8c697f");

                entity.HasIndex(e => e.UserId, "core_user_groups_user_id_70b4d9b8");

                entity.HasIndex(e => new { e.UserId, e.GroupId }, "core_user_groups_user_id_group_id_c82fcad1_uniq")
                    .IsUnique()
                    .HasFilter("([user_id] IS NOT NULL AND [group_id] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CoreUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_user_groups_group_id_fe8c697f_fk_auth_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoreUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_user_groups_user_id_70b4d9b8_fk_core_user_id");
            });

            modelBuilder.Entity<CoreUserUserPermission>(entity =>
            {
                entity.ToTable("core_user_user_permissions");

                entity.HasIndex(e => e.PermissionId, "core_user_user_permissions_permission_id_35ccf601");

                entity.HasIndex(e => e.UserId, "core_user_user_permissions_user_id_085123d3");

                entity.HasIndex(e => new { e.UserId, e.PermissionId }, "core_user_user_permissions_user_id_permission_id_73ea0daa_uniq")
                    .IsUnique()
                    .HasFilter("([user_id] IS NOT NULL AND [permission_id] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.CoreUserUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_user_user_permissions_permission_id_35ccf601_fk_auth_permission_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoreUserUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("core_user_user_permissions_user_id_085123d3_fk_core_user_id");
            });

            modelBuilder.Entity<DjangoAdminLog>(entity =>
            {
                entity.ToTable("django_admin_log");

                entity.HasIndex(e => e.ContentTypeId, "django_admin_log_content_type_id_c4bce8eb");

                entity.HasIndex(e => e.UserId, "django_admin_log_user_id_c564eba6");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionFlag).HasColumnName("action_flag");

                entity.Property(e => e.ActionTime).HasColumnName("action_time");

                entity.Property(e => e.ChangeMessage).HasColumnName("change_message");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ObjectRepr)
                    .HasMaxLength(200)
                    .HasColumnName("object_repr");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.DjangoAdminLogs)
                    .HasForeignKey(d => d.ContentTypeId)
                    .HasConstraintName("django_admin_log_content_type_id_c4bce8eb_fk_django_content_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DjangoAdminLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("django_admin_log_user_id_c564eba6_fk_core_user_id");
            });

            modelBuilder.Entity<DjangoContentType>(entity =>
            {
                entity.ToTable("django_content_type");

                entity.HasIndex(e => new { e.AppLabel, e.Model }, "django_content_type_app_label_model_76bd3d3b_uniq")
                    .IsUnique()
                    .HasFilter("([app_label] IS NOT NULL AND [model] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppLabel)
                    .HasMaxLength(100)
                    .HasColumnName("app_label");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .HasColumnName("model");
            });

            modelBuilder.Entity<DjangoMigration>(entity =>
            {
                entity.ToTable("django_migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.App)
                    .HasMaxLength(255)
                    .HasColumnName("app");

                entity.Property(e => e.Applied).HasColumnName("applied");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DjangoSession>(entity =>
            {
                entity.HasKey(e => e.SessionKey)
                    .HasName("PK__django_s__B3BA0F1F78B5579F");

                entity.ToTable("django_session");

                entity.HasIndex(e => e.ExpireDate, "django_session_expire_date_a5c62663");

                entity.Property(e => e.SessionKey)
                    .HasMaxLength(40)
                    .HasColumnName("session_key");

                entity.Property(e => e.ExpireDate).HasColumnName("expire_date");

                entity.Property(e => e.SessionData).HasColumnName("session_data");
            });

            modelBuilder.Entity<EquivalenciasEntidad>(entity =>
            {
                entity.ToTable("equivalencias_entidad");

                entity.HasIndex(e => e.Codigo, "UQ__equivale__40F9A2063A9E3357")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .HasColumnName("codigo");
            });

            modelBuilder.Entity<EquivalenciasMonedum>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F9564899F92E15");

                entity.ToTable("equivalencias_moneda");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasNatinterv>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F9564838553E89");

                entity.ToTable("equivalencias_natinterv");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasPersonal>(entity =>
            {
                entity.ToTable("equivalencias_personal");

                entity.HasIndex(e => e.Tipo, "equivalencias_personal_tipo_f9e1c24a_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasPlazo>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F95648ACBE0C35");

                entity.ToTable("equivalencias_plazo");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasProducto>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F95648C64300AD");

                entity.ToTable("equivalencias_producto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(30)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasReal>(entity =>
            {
                entity.ToTable("equivalencias_real");

                entity.HasIndex(e => e.Tipo, "equivalencias_real_tipo_0e1c74ae_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasSituoper>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F95648F46F811B");

                entity.ToTable("equivalencias_situoper");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasSolcol>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F95648217F8442");

                entity.ToTable("equivalencias_solcol");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EquivalenciasTipo>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__equivale__E7F9564853966501");

                entity.ToTable("equivalencias_tipo");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("tipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
