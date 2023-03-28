<template>
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>

        <form @submit.prevent="SendFile" class="form">
          <div class="moda-container upload">
            <div class="modal-titulo">
              <img src="../img/doc.png" />
              <span><a>CARGA DE DOCUMENTAOS FINACIEROS </a></span>
              <a class="close"><img src="../img/close.png" /></a>
            </div>
            <div class="modal-detail">
              <div class="status row">
                <div class="st col">
                  <div
                    class="st-1"
                    v-bind:class="{ activo: CurrentStep == 1 }"
                  ></div>
                  <span>CARGA DOCUMENTOS</span>
                </div>
                <div class="st col">
                  <div
                    class="st-2"
                    v-bind:class="{ activo: CurrentStep == 2 }"
                  ></div>
                  <span>GESTIÓN DE DOCUMENTOS </span>
                </div>
                <div class="st col">
                  <div
                    class="st-3"
                    v-bind:class="{ activo: CurrentStep == 3 }"
                  ></div>
                  <span>CONTRACTOS</span>
                </div>
                <div class="st col">
                  <div
                    class="st-4"
                    v-bind:class="{ activo: CurrentStep == 4 }"
                  ></div>
                  <span>CREANDO POOL</span>
                </div>
              </div>

              <div class="step-1">
                <div class="txt-h-i">
                  <div class="txt-img"></div>
                  <div class="txt-info">
                    <div class="txt-titulo">CARGA DE DOCUMENTOS</div>
                    <div class="txt-detail">
                      Por favor, adjunta los documentos financieros
                      correspondientes 2020, 2021 y el último balance del año en
                      curso.
                    </div>
                  </div>
                </div>

                <div class="up-docs">
                  <div class="txt-docs">Balance sumas y saldos</div>
                  <input type="date" required v-model="fecha" />
                  <button type="button" v-bind:onclick="LoadFile">
                    ADJUNTAR
                  </button>
                  <div
                    class="check-docs"
                    v-bind:class="{
                      ok: fileName.length > 0 && fecha.length > 0,
                    }"
                  ></div>
                  <div class="up-docs-st">{{ fileName }}</div>
                </div>

                <div class="form-group">
                  <input
                    style="display: none"
                    type="file"
                    id="file1"
                    name="file1"
                    @change="onFileChange"
                  />
                </div>
              </div>

              <div class="step-2">
                <div class="txt-h-i">
                  <div class="txt-img"></div>
                  <div class="txt-info">
                    <div class="txt-titulo">GESTIÓN DE DOCUMENTOS</div>
                    <div class="txt-detail">
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                      Mattis auctor suspendisse aliquet in.
                    </div>
                  </div>
                </div>

                <div class="row docHeader">
                  <div class="col col-2">TIPO</div>
                  <div class="col col-5">DOCUMENTO</div>
                  <div class="col center">FECHA DE CARGA</div>
                  <div class="col col-1">ACCIÓN</div>
                </div>
                <div
                  class="docDetailts row"
                  v-for="documento in Object.values(this.Documentos)"
                  v-bind:key="documento"
                >
                  <div class="col col-2">{{ documento.origen }}</div>
                  <div class="col col-5">{{ Split(documento.documento) }}</div>
                  <div class="col center">{{ documento.fecha }}</div>
                  <div class="col col-1">
                    <div
                      v-bind:class="{
                        docOk: documento.status == true,
                        docKo: documento.status == false,
                      }"
                    ></div>
                    <div
                      class="docDel"
                      v-on:click="DeleteDoc(documento.id)"
                      v-bind:data-id="documento.id"
                    ></div>
                  </div>
                </div>
              </div>

              <div class="step-3">
                <div class="txt-h-i">
                  <div class="txt-img"></div>
                  <div class="txt-info">
                    <div class="txt-titulo">completar CONTRATOS</div>
                    <div class="txt-detail">
                      Se han detectado 8 cuentas de financiación, por favor
                      complete la información de los contratos.
                    </div>
                  </div>
                </div>

                <div class="docHeader row hc">
                  <div class="col col-2 "># CUENTA</div>
                  <div class="col col-2">OTRA CUENTA </div>
                  <div class="col col-2 center">DISPUESTO</div>
                  <div class="col">CONCEPTO</div>
                  <div class="col col-2 center">ACCIÓN</div>
                </div>
                <div class="dvOverflow">
                  <div class="docDetailts row" v-for="contrato in Contratos[0]" v-bind:key="contrato"  v-bind:class="{ con_activo: contrato.contrato != null }" v-bind:id="contrato.id">
                    <div class="col col-2 ">{{ contrato.cuenta }}</div>
                    <div class="col col-2">{{ contrato.contrato == null ? "" : contrato.contrato.cuenta_complementaria }}</div>
                    <div class="col col-2 center">{{ contrato.dispuesto }}</div>
                    <div class="col">{{ contrato.concepto }}</div>
                    <FormPool v-bind:contrato="contrato" v-bind:contratos="Contratos"  @click="handleClick"></FormPool>
                  </div>
                </div>
              </div>

              <div class="step-4">
                <div class="txt-h-i">
                  <div class="txt-img"></div>
                  <div class="txt-info">
                    <div class="txt-titulo">Pool Creado</div>
                    <div class="txt-detail">
                      Se ha completado la creación del pool
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <div class="col">
                <a
                  class="st noshow"
                  v-bind:class="{ send_l: CurrentStep == 3 }"
                  v-on:click="SecondStep"
                  >Anterior</a
                >
                <a
                  class="st noshow"
                  v-bind:class="{ send_l: CurrentStep == 4 }"
                  v-on:click="ThirdStep"
                  >Anterior</a
                >
              </div>
              <div class="col">
                <a
                  class="st noshow"
                  v-on:click="SendFile"
                  v-bind:class="{
                    send:
                      fileName.length > 0 &&
                      fecha.length > 0 &&
                      Documentos.length <= 0 &&
                      CurrentStep == 1,
                  }"
                  >cargar documentos</a
                >
                <a
                  class="st noshow"
                  v-bind:class="{
                    send:
                      Documentos.length > 0 &&
                      Contratos.length >= 0 &&
                      CurrentStep == 2,
                  }"
                  v-on:click="ThirdStep"
                >
                  agregar contrato</a
                >
                <a
                  class="st noshow st-gopool"
                  v-bind:class="{ send: CurrentStep == 3 }"
                  v-on:click="FourthStep"
                  style="width: 231px; background-position: 207px"
                >
                  Generar pool bancario</a
                >
                <a
                  class="st noshow st-gopool"
                  v-bind:class="{ send: CurrentStep == 4 }"
                  v-on:click="PoolBancario"
                  style="width: 231px; background-position: 207px"
                >
                  POOL BANCARIO</a
                >
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>



</template>

<script>
const $ = require("jquery");
window.$ = $;

import Navbar from "../components/navbar.vue";
import Sidebar from "../components/sidebar.vue";
import FormPool from "@/components/form-pool.vue";


export default {
  name: "UploadApp",
  components: { Sidebar, Navbar, FormPool},
  data() {
    return {      
      CurrentStep: 1,
      currentEntidad: "",
      fileName: "",
      fecha: "",
      CIF: "",
      tipoDoc: "BSS",
      Documentos: [],
      Contratos: [],
    };
  },
  mounted() {
    this.FirstStep();
  },
  methods: {
    getOther(data) {
      return "00" + data;
    },
    onFileChange(event) {
      var fileData = event.target.files[0];
      this.fileName = fileData.name;
    },
    Split(data) {
      var datas = data.split("/");
      return datas[datas.length - 1];
    },
    LoadFile() {
      document.getElementById("file1").click();
    },
    async FirstStep() {
      this.fecha="";
      //this.fileName="";
      this.OcultarStep();
      $(".step-2").show();
      this.CurrentStep = 1;
      await this.axios
        .get("/core/documentos/", {
          params: { empresa__CIF: localStorage.getItem("empresa") },
        })
        .then((r) => {
          this.Documentos = r.data;
          if (Object.keys(this.Documentos).length > 0) {
            this.SecondStep();
          } else {
            $(".step-1").show();
            $(".step-2").hide();
          }
        })
        .catch((err) => {
          console.log(err.response);
        });
    },
    async SecondStep() {
      this.OcultarStep();
      $(".step-2").show();
      this.CurrentStep = 2;
    },
    async ThirdStep() {
      this.OcultarStep();
      $(".step-3").show();
      this.CurrentStep = 3;

      await this.axios
        .get("/core/pool/", {
          params: { CIF: localStorage.getItem("empresa") },
        })
        .then((r) => {          
          this.Contratos = Object.values(r.data);
        })
        .catch((err) => {
          console.log(err.response);
        });
    },
    async FourthStep() {
      this.OcultarStep();
      $(".step-4").show();
      this.CurrentStep = 4;
    },
    async DeleteDoc(id) {
      this.$Block();
      await this.axios
        .delete("/core/documentos/" + id)
        .then(() => {
          this.FirstStep();
          this.$UnBlock();
        })
        .catch((err) => {
          this.$UnBlock();
          console.log(err.response);
        });
    },
    async SendFile() {
      this.$Block();
      this.CIF = localStorage.getItem("empresa");
      var fileExcel = document.getElementById("file1");
      var form = new FormData();
      form.append("fecha", this.fecha);
      form.append("CIF", this.CIF);
      form.append("tipoDoc", this.tipoDoc);
      form.append("file", fileExcel.files[0]);

      await this.axios
        .post("/sources/upload/", form)
        .then(() => {
          this.$UnBlock();
          this.FirstStep();
        })
        .catch((err) => {
          this.$UnBlock();
          console.log(err.response);
        });
    },
    OcultarStep() {
      $(".step-1").hide();
      $(".step-2").hide();
      $(".step-3").hide();
      $(".step-4").hide();
      this.CurrentStep = 0;
    },
    PoolBancario() {      
      this.$router.push("/Pool");
    },
    handleClick(id){
      this.ThirdStep();      
      console.log("se ha modificado este contrato"+id);
    }
  },
};
</script>
