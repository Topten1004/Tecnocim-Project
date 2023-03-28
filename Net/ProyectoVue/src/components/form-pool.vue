<template>
  <div
    class="col-2 cc-action"
    v-bind:data-id="frmId"
    v-on:click="ShowForm(frmId)"
  ></div>

  <form @submit.prevent="SaveContrato" v-bind:name="frmId">
    <div class="moda-container frmContrato" v-bind:data-id="frmId">
      <div class="modal-titulo">
        <img src="../img/doc.png" />
        <span><a>COMPLETANDO CONTRATO DE LA CUENTA #{{ Cuenta }}</a></span>
        <a class="close" v-on:click="close()"><img src="../img/close.png" /></a>
      </div>
      <div class="content">
        <div class="row">
          <div class="col-12 danger">
            <div id="alert" v-bind:data-alertid="frmId" class="alert alert-danger alert-dismissible fade show" role="alert" data-bs-dismiss="alert" aria-label="Close" style="cursor:pointer;display: none">
              
          </div>
          </div>
          <div class="col-6">
            <label for="s-entidad" class="form-label">seleccione entidad</label>
            <select
              class="form-control"
              name="entidad"
              id="s-entidad"
              v-model="Entidad"
              required
            >
              <option value="">---</option>
              <option
                v-for="Entidad in Entidades"
                v-bind:value="parseInt(Object.keys(Entidad)[0])"
                v-bind:key="Entidad"
              >
                {{ Object.values(Entidad)[0] }}
              </option>
            </select>
          </div>
          <div class="col-6">
            <label for="s-producto" class="form-label">tipo de producto</label>
            <select
              class="form-control"
              name="producto"
              id="s-producto"
              v-model="Producto"
              required
            >
              <option value="">---</option>
              <option
                v-for="Producto in Productos"
                v-bind:value="Object.values(Producto)[0]"
                v-bind:key="Producto"
              >
                {{ Object.values(Producto)[0] }}
              </option>
            </select>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <label for="s-concepto" class="form-label">concepto</label>
            <input
              class="form-control"
              type="text"
              id="s-concepto"
              name="s-concepto"
              v-model="Concepto"
              required
            />
          </div>
        </div>
        <div class="row">
          <div class="col-3">
            <label for="s-importe" class="form-label"
              >importe inicial (euros)</label
            >
            <input
              class="form-control"
              type="number"
              id="s-importe"
              name="s-importe"
              v-model="Importe"
              v-bind:min="Dispuesto"
              step="any"
              placeholder="..."
              required
            />
          </div>
          <div class="col-3">
            <label for="s-dispuesto" class="form-label"
              >dispuesto (euros)</label
            >
            <input
              class="form-control"
              type="text"
              readonly="readonly"
              id="s-dispuesto"
              name="s-dispuesto"
              placeholder="..."
              v-bind:value="$FormatNumeroNoSign(Dispuesto)"
              required
            />
          </div>
          <div class="col-3">
            <label for="s-frmpago" class="form-label">forma de pago</label>
            <select
              class="form-control"
              name="s-frmpago"
              id="s-frmpago"
              v-model="FormaPago"
              required
            >
              <option value="">...</option>
              <option value="1">MENSUAL</option>
              <option value="3">TRIMESTRAL</option>
              <option value="6">SEMESTRAL</option>
              <option value="12">ANUAL</option>
            </select>
          </div>
          <div class="col-3">
            <label for="s-frmpago" class="form-label">Carencia</label>
            <input
              class="form-control"
              type="number"
              id="s-carencia"
              name="s-carencia"
              v-model="Carencia"
              step="any"
              placeholder="..."
              required
            />
          </div>
        </div>
        <div class="row">
          <div class="col-3">
            <label for="s-inicio" class="form-label">fecha de inicio</label>
            <input
              class="form-control"
              type="date"
              id="s-inicio"
              name="s-inicio"
              placeholder="..."
              v-model="FechaInicio"
            />
          </div>
          <div class="col-4">
            <label for="s-fin" class="form-label">fecha de fin</label>
            <input
              class="form-control"
              type="date"
              id="s-fin"
              name="s-fin"
              placeholder="..."
              v-model="FechaFin"
            />
          </div>

          <div class="col-5">
            <label for="s-entidad" class="form-label">Seleccione Cuenta Asociada</label>
            <select
              class="form-control"
              name="id_cuenta"
              id="s-id-cuenta"
              v-model="id_cuenta"              
            >
              <option value="">---</option>
              <option
                v-for="_cuenta in Cuentas"
                v-bind:value="_cuenta.id"
                v-bind:key="_cuenta.id"
                v-bind:class="_cuenta.enuso"
              >
                {{ _cuenta.descripcion}}
              </option>
            </select>
          </div>


        </div>
        <div class="row">
          <div class="col-3">
            <label for="s-precio" class="form-label">precio </label>
            <input
              class="form-control"
              type="number"
              min="0"
              max="100"
              step="any"
              id="s-precio"
              name="s-precio"
              placeholder="..."
              v-model="Precio"
            />
          </div>

          <div class="col-3">
            <label for="s-digitalizada" class="form-label"
              >póliza digitalizada</label
            >
            <select
              class="form-control"
              name="s-digitalizada"
              id="s-digitalizada"
              v-model="Poliza"
            >
              <option value="false">No</option>
              <option value="true">Si</option>
            </select>
          </div>
          <div class="col-3">
            <label for="s-minimis" class="form-label">minimis</label>
            <select
              class="form-control"
              name="s-minimis"
              id="s-minimis"
              v-model="Minimis"
            >
              <option value="false">No</option>
              <option value="true">Si</option>
            </select>
          </div>
        </div>
        <div class="row">
          <div class="col-2">
            <label for="s-valoracion" class="form-label valoration"
              >valoración</label
            >
          </div>
          <div class="col-10">
            <p class="clasificacion" v-bind:class="frmId">
              <input
                id="radio1"
                type="radio"
                name="valoracion"
                value="5"
                v-model="Valoracion"
              />
              <label for="radio1">★</label>
              <input
                id="radio2"
                type="radio"
                name="valoracion"
                value="4"
                v-model="Valoracion"
              />
              <label for="radio2">★</label>
              <input
                id="radio3"
                type="radio"
                name="valoracion"
                value="3"
                v-model="Valoracion"
              />
              <label for="radio3">★</label>
              <input
                id="radio4"
                type="radio"
                name="valoracion"
                value="2"
                v-model="Valoracion"
              />
              <label for="radio4">★</label>
              <input
                id="radio5"
                type="radio"
                name="valoracion"
                value="1"
                v-model="Valoracion"
              />
              <label for="radio5">★</label>
            </p>
          </div>
        </div>

        <div class="row">
          <div class="col-12">
            <label for="s-nota" class="form-label">nota del consultor</label>
            <textarea
              class="form-control"
              name="nota"
              id="nota"
              cols="30"
              rows="2"
              v-model="Nota"
            ></textarea>
          </div>
        </div>

        <div class="row">
          <div class="col-12 frm-button">
            <button
              type="button"
              class="btn btn-secondary cancel"
              v-on:click="close()"
            >
              cancelar
            </button>
            <button type="submit" class="btn btn-primary save">guardar</button>
          </div>
        </div>
      </div>
    </div>
  </form>
</template>
<script>
const $ = require("jquery");
window.$ = $;

export default {
  name: "NavBar",
  emits: { click: null },
  components: {},
  data() {
    return {
      frmId: "",
      filter: "",
      Entidades: [],
      Productos: [],
      Cuenta: "",
      Entidad: "",
      Producto: "",
      Dispuesto: "",
      Concepto: "",
      Importe: "",
      Disponible: "",
      FormaPago: "",
      FechaInicio: "",
      FechaFin: "",
      Precio: "",
      Cirbe: "",
      Poliza: "",
      Minimis: "",
      Valoracion: "",
      Nota: "",
      Carencia: "",
      Id: "",
      id_cuenta:"",
      CurrentContrato: this.contrato,
      OtrosContractos: this.contratos,
      Cuentas:[]
    };
  },
  props: {
    id: Number,
    contrato: Object,
    contratos: Object
  },
  mounted() {
    this.frmId = this.Newid();
    this.LoadEntidad();
    this.LoadProducto();
    this.LoadContrato();
  },
  methods: {
    Newid() {
      let s4 = () => {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
      };
      return (
        s4() +
        s4() +
        "-" +
        s4() +
        "-" +
        s4() +
        "-" +
        s4() +
        "-" +
        s4() +
        s4() +
        s4()
      );
    },
    ShowForm(id) {
      this.LoadContrato();
      this.$BlockPop();
      $(".frmContrato[data-id=" + id + "]").show();
    },
    close() {
      this.$UnBlockPop();
      $(".frmContrato").hide();
    },
    async LoadEntidad() {
      this.axios.get("/complement/entidad/?id=0").then((r) => {
        this.Entidades = r.data;
        localStorage.setItem("Entidades", JSON.stringify(r.data));
      });
    },
    async LoadProducto() {
      this.axios.get("/complement/producto").then((r) => {
        this.Productos = r.data;
        localStorage.setItem("Productos", JSON.stringify(r.data));
      });
    },
    async LoadContrato() {
      var CurrentContrato = { ...this.contrato };
      var CurrentContratos = { ...this.contratos };
      var _contratos = Object.values(CurrentContratos[0]);

      console.log(CurrentContrato);

      this.Cuentas = [];
      this.Cuenta = CurrentContrato.cuenta;

if(!this.Cuenta.startsWith("52"))
{
       for (let i = 0; i < _contratos.length; i ++) {          
          if(_contratos[i].cuenta.startsWith('52')  )
          {

            var enUso =  _contratos[i].contrato == null ? "": "used";
            if (CurrentContrato.contrato != null && CurrentContrato.contrato.cuenta_complementaria == _contratos[i].cuenta) {
              this.id_cuenta = _contratos[i].id;
              enUso = "";
            }

            this.Cuentas.push( {id : _contratos[i].id,descripcion:  _contratos[i].cuenta+  " " + _contratos[i].concepto , enuso: enUso });
           
          }   
       }
      }
     
      this.Concepto = CurrentContrato.concepto;
      this.Dispuesto = CurrentContrato.dispuesto;
      this.Id = CurrentContrato.id;
      if (CurrentContrato.contrato != null) {
        this.Importe = CurrentContrato.contrato.limite;
        this.Disponible = CurrentContrato.contrato.limite;
        this.FormaPago = CurrentContrato.contrato.periodificacion;
        this.FechaInicio = CurrentContrato.contrato.inicio;
        this.FechaFin = CurrentContrato.contrato.vencimiento;
        this.Precio = CurrentContrato.contrato.precio;
        this.Poliza = CurrentContrato.contrato.digitalizada;
        this.Minimis = CurrentContrato.contrato.minimis;
        this.Valoracion = CurrentContrato.contrato.valoracion;
        this.Nota = CurrentContrato.contrato.notas;
        this.Producto = CurrentContrato.contrato.producto;
        this.Entidad = CurrentContrato.contrato.entidad;
        this.Carencia = CurrentContrato.contrato.carencia;
      } else {
        this.Importe =
          this.Disponible =
          this.FormaPago =
          this.FechaInicio =
          this.FechaFin =
          this.Precio =
          this.Poliza =
          this.Minimis =
          this.Valoracion =
          this.Nota =
          this.Producto =
          this.Entidad =
          this.id_cuenta =
            "";
      }
    },
    getEntidad() {
      return ("0000" + this.Entidad).slice(-4);
    },
    async SaveContrato() {
      var form = new FormData();

      form.append("inicio", this.FechaInicio);
      form.append("vencimiento", this.FechaFin);
      form.append("entidad", this.getEntidad());
      form.append("producto", this.Producto);
      form.append("limite", this.Importe);
      form.append("precio", this.Precio);
      form.append("moneda", "EUR");
      form.append("minimis", this.Minimiss ? "True" : "False");
      form.append("digitalizada", this.Poliza ? "True" : "False");
      form.append("valoracion", 5);
      form.append("periodificacion", this.FormaPago);
      form.append("carencia", this.Carencia);
      form.append("notas", this.Nota);
      form.append("id_cuenta" ,this.id_cuenta);      
      form.append("id_pool" ,this.Id);      

console.log("carencia");
console.log(this.Carencia);

      //.post("/core/contrato/?id_pool=" + this.Id, form)

      await this.axios
        .post("/core/contrato/", form)
        .then(() => {          
          this.$UnBlockPop();
          this.$emit("click", this.Id);
          $(".frmContrato").hide();
        })
        .catch((err) => {
          console.log(err.response);
          $("[alertid="+ this.frmId+"]").html(err.response);
          $("[alertid="+ this.frmId+"]").show();
        });
    },
  },
};
</script>
