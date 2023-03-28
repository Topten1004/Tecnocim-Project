<template>
  <nav class="sidebar">
    <div class="brand">
      <a href="#">
        <img src="../img/logo.png" />
      </a>
    </div>
    <div>
      <ul class="list-unstyled components mb-5">
        <li>
          <a href="/Upload">
            <div class="sd-ico-doc"></div>
            <div>CARGA DE DOCUMENTO</div>
          </a>
        </li>
        <li>
          <a href="/Pool">
            <div class="sd-ico-pool"></div>
            <div>POOL DE ENDEUDAMIENTO</div>
          </a>
        </li>
        <li>
          <a href="/Ratios">
            <div class="sd-ico-ratio"></div>
            <div>CUENTAS ANUALES Y RATIOS</div>
          </a>
        </li>
        <li>
          <a href="/Deuda">
            <div class="sd-ico-projection"></div>
            <div>SERVICIO DE DEUDA</div>
          </a>
        </li>
        <li>
          <a href="/Balance">
            <div class="sd-ico-projection"></div>
            <div>BALANCE P y G</div>
          </a>
        </li>
        <li>
          <a href="#">
            <div class="sd-ico-save-report"></div>
            <div>INFORMES GUARDADOS</div>
          </a>
        </li>
        <li>
          <a href="#">
            <div class="sd-ico-save-settings"></div>
            <div>AJUSTES</div>
          </a>
        </li>
      </ul>
      <ul class="list-unstyled components mb-5">
        <li>
          <a href="/logout">
            <div class="sd-ico-exit"></div>
            <div>SALIR DE LA APLICACIÓN</div>
          </a>
        </li>
      </ul>

      <a href="http://www.aliaconsultores.com" target="_blank">
        www.aliaconsultores.com
      </a>
    </div>
  </nav>
</template>

<script>
export default {
  name: "SideBar",
  components: {},
};
</script>
