<template>
  <nav class="sidebar">
    <div class="brand">
      <router-link to="#">
        <img src="../img/logo.png" />
      </router-link>
    </div>
    <div>
      <ul class="list-unstyled components mb-5">
        <li>
          <router-link to="/Upload">
            <div class="sd-ico-doc"></div>
            <div>DOCUMENTOS</div>
          </router-link>
        </li>
        <li>
          <a href="#" @click="toggle('pool')">
            <div class="sd-ico-pool"></div>
            <div>POOL DE ENDEUDAMIENTO</div>
          </a>
        </li>
        <div id="pool" class="ms-3">
          <li>
            <router-link to="/Pool">
              <div class="sd-ico-pool"></div>
              <div>POOL BANCARIO</div>
            </router-link>
          </li>
          <li>
            <router-link to="/Pool/Cirbe">
              <img src="../img/pie-chart.png" alt="" style="width: 35px; margin-left: -7px;" class="mt-0">
              <div style="margin-top: 10px;">CIRBE</div>
            </router-link>
          </li>
        </div>
        <li>
          <router-link to="/Ratios">
            <div class="sd-ico-ratio"></div>
            <div>CUENTAS ANUALES Y RATIOS</div>
          </router-link>
        </li>
        <li>
          <router-link to="/Deuda">
            <div class="sd-ico-projection"></div>
            <div>SERVICIO DE DEUDA</div>
          </router-link>
        </li>
        <li>
          <router-link to="/Balance">
            <div class="sd-ico-projection"></div>
            <div>BALANCE P y G</div>
          </router-link>
        </li>
        <li>
          <router-link to="/Analitica">
            <div class="sd-ico-ratio"></div>
            <div>ANALÍTICA</div>
          </router-link>
        </li>
        <li>
          <router-link to="/Entidades">
            <div class="sd-ico-projection"></div>
            <div>ENTIDADES</div>
          </router-link>
        </li>
        <li>
          <router-link to="#">
            <div class="sd-ico-save-report"></div>
            <div>INFORMES GUARDADOS</div>
          </router-link>
        </li>
        <li>
          <router-link to="#">
            <div class="sd-ico-save-settings"></div>
            <div>AJUSTES</div>
          </router-link>
        </li>
      </ul>
      <ul class="list-unstyled components mb-5">
        <li>
          <router-link to="/logout">
            <div class="sd-ico-exit"></div>
            <div>SALIR DE LA APLICACIÓN</div>
          </router-link>
        </li>
      </ul>

      <a href="http://www.aliaconsultores.com" target="_blank">
        www.aliaconsultores.com
      </a>
    </div>
  </nav>
</template>

<script>
const $ = require("jquery");

export default {
  name: "SideBar",
  components: {},
  mounted() {
    // Chapuzas por no hacer un layout
    const showPool = JSON.parse(localStorage.getItem('show-pool'));
    if(showPool) {
      $('#pool').show();
    } else {
      $('#pool').hide();
    }
  },
  methods: {
    toggle(id) {
      $('#' + id)
        .slideToggle(400, () => localStorage.setItem('show-' + id, JSON.stringify(!$('#' + id).is(':hidden'))));
    }
  }
};
</script>
