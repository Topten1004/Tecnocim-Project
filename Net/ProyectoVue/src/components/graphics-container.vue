<template>  
  <div class="row align-items-start">
          <div class="col-6">
            <div class="row">
              <div class="col mb-2">
                <img src="../img/ant-design_radar-chart-outlined.svg" />
                <span>Flujos de tesorería</span>
              </div>
              <div class="col-12">
                <div id="chart_div" class="dvChart">
                  <ChartRender></ChartRender>
                </div>
              </div>
            </div>
            <div class="col-12">
              <div class="section-footer"></div>
            </div>
          </div>
          <div class="col-4">
            <div class="row">
              <div class="col-12 mb-2">
                <img src="../img/wpf_future.svg" />
                <span>Previsionales</span>
              </div>
              <div class="col-12">
                <div class="dvSection"></div>
              </div>
            </div>
            <div class="col-12">
              <div class="section-footer"></div>
            </div>
          </div>
          <div class="col-2">
            <div class="row">
              <div class="col-12 mb-2">
                <img src="../img/icon-park-outline_bank-card-one.svg" />
                <span>Administración</span>
              </div>
              <div class="col-12">
                <div id="chart_adm" class="dvChart"></div>
              </div>
            </div>
            <div class="col-12">
              <div class="section-footer"></div>
            </div>
          </div>
        </div>
        <div class="row align-items-start mt-5">
          <div class="col-4">
            <div class="row">
              <div class="col-12 mb-2">
                <img src="../img/bxs_network-chart.svg" />
                <span>Ciber por entidades</span>
              </div>
              <div class="col-12">
                <div id="chart_ciber" class="dvChart"></div>
              </div>
            </div>
            <div class="col-12">
              <div class="section-footer"></div>
            </div>
          </div>
          <div class="col">
            <div class="row">
              <div class="col-12 mb-2">
                <img src="../img/icon-park-outline_bank-card-one.svg" />
                <span>Cuentas Anuales y Ratios</span>
              </div>
              <div class="col-12">
                <div class="dvSection">
                  <div class="row th">
                    <div class="col">Concepto</div>
                    <div class="col">Años</div>
                    <div class="col">Enero</div>
                    <div class="col">Febrero</div>
                    <div class="col">Marzo</div>
                    <div class="col">Abril</div>
                    <div class="col">Mayo</div>
                    <div class="col">Junio</div>
                    <div class="col">Julio</div>
                    <div class="col">Agosto</div>
                    <div class="col">Septimbre</div>
                    <div class="col">Ratio X</div>
                    <div class="col"></div>
                  </div>
                  <div class="row td">
                    <div class="col">2021</div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                  </div>
                  <div class="row td">
                    <div class="col">2021</div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                  </div>
                  <div class="row td">
                    <div class="col">2021</div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                  </div>
                  <div class="row td">
                    <div class="col">2021</div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                  </div>
                  <div class="row td">
                    <div class="col">2021</div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                    <div class="col"></div>
                  </div>
                </div>
              </div>
              <div class="col-12">                
              </div>
            </div>
          </div>
        </div>
</template>

<script>
export default {
  name: 'GraphicsContainer',
  components: {}
  }
  </script>