
<style src="vue-d3-network/dist/vue-d3-network.css"></style>
<script src="//d3js.org/d3.v3.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-p34f1UUtsS3wqzfto5wAAmdvj+osOnFyQFpp4Ua3gs/ZVWx6oOypYoCJhGGScy+8" crossorigin="anonymous"></script>
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>
<template>
  <div id="app">
      <div style="border-style: solid">
          <form>
              <b-modal v-if="isActive"
                       v-model="isActive"
                       modal-class="my-second-class"
                       centered
                       title="BootstrapVue">
                  <div class="buefy_card" v-if="isActive">
                      <div class="card-content">
                          <div>名稱:{{ input_name }}</div>
                          <div>ID:{{ input_ID }}</div>
                          <div>狀態:{{ input_svgAttrs }}</div>
                          <div v-if="input_Switch != null">裝置:{{ input_Switch }}</div>
                          <div v-if="input_ip != null">ip:{{ input_ip }}</div>
                          <div v-if="input_mac != null">mac:{{ input_mac }}</div>
                          <div v-if="input_cpu != null">cpu%:{{ input_cpu }}</div>
                          <div v-if="input_memory_used != null">
                              memory used:{{ input_memory_used }}
                          </div>
                          <div v-if="input_memory_total != null">
                              memory total:{{ input_memory_total }}
                          </div>
                          <div v-if="input_meory != null">meory%:{{ input_meory }}</div>
                          <div v-if="input_port != null">port#:{{ input_port }}</div>
                      </div>
                  </div>
              </b-modal>
              <div>
                  <svg class="bar-chart"></svg>
              </div>
              <div>
                  <!--浮<button @click="D3port" type="button">顯示狀態圖</button>-->
              </div>
              <div>
                  <select v-model="jackylo" @change="get_prot">
                      <option v-for="item in types" v-bind:value="item.value" :key="item">
                          {{ item.title }}
                      </option>
                  </select>
                  <!--           <input
              class="buttons"
              style="position: relative; left: 5px"
              value="輸入"
              src="reset-8.svg"
            /> -->
                  <p>Type: {{ jackylo }}</p>
              </div>

              <!--        <label>選取網路探索起始方法:</label>
        <div>
          <input
            type="radio"
            id="使用起始路由器(seed router)"
            name="drone"
            value="使用起始路由器(seed router)"
          />
          <label for="使用起始路由器(seed router)"
            >使用起始路由器(seed router)</label
          >
        </div>
        <div id="watermark"></div>
        <div>
          <input
            type="radio"
            id="使用IP範圍(IP range)"
            name="drone"
            value="使用IP範圍(IP range)"
          />
          <label for="使用IP範圍(IP range)">使用IP範圍(IP range)</label>
        </div>
        <div>
          <input
            type="radio"
            id="使用子網路(subnet)"
            name="drone"
            value="使用子網路(subnet)"
          />
          <label for="使用子網路(subnet)">使用子網路(subnet)</label>
        </div>

        <div>
          <input
            type="radio"
            id="使用網域服務(Active Directory)"
            name="drone"
            value="使用網域服務(Active Directory)"
          />
          <label for="使用網域服務(Active Directory)"
            >使用網域服務(Active Directory)</label
          >
        </div>
        <br />

        (IP address)：<input type="text" name="欄位名稱" /> <br /><br />
        <div>選取網路探索起始方法:</div>

        <div>
          <input type="radio" id="只用SNMP" name="drone1" value="只用SNMP" />
          <label for="只用SNMP">只用SNMP</label>
        </div>
        <div>
          <input
            type="radio"
            id="Telnet + SNMP"
            name="drone1"
            value="Telnet + SNMP"
          />
          <label for="Telnet + SNMP">Telnet + SNMP</label>
        </div>
        <div>
          <input
            type="radio"
            id="SSH + SNMP"
            name="drone1"
            value="SSH + SNMP"
          />
          <label for="SSH + SNMP">SSH + SNMP</label>
        </div>
        <div>
          <input
            type="radio"
            id="SSH/Telnet + SNMP"
            name="drone1"
            value="SSH/Telnet + SNMP"
          />
          <label for="SSH/Telnet + SNMP">SSH/Telnet + SNMP</label>
        </div>
        <br />
        <label>選取網路探索階層</label>
        <select>
          <option value="1">請選擇</option>
          <option value="2">第一層</option>
          <option value="3">第二層</option>
          <option value="4">第三層</option>
          <option value="5">第四層</option>
          <option value="6">第五層</option>
          <option value="7">第六層</option>
          <option value="8">第七層</option>
          <option value="9">第八層</option>
          <option value="10">第九層</option>
          <option value="11">第十層</option></select
        ><br />
        <input
          align="justify"
          style="position: relative; left: 100px"
          type="submit"
          alt="Login"
          value="輸入"
          width="100"
          height="30"
        />
        <input
          align="justify"
          style="position: relative; left: 110px"
          type="submit"
          alt="Login"
          name="drone1"
          value="離開"
        /> -->
          </form>
          <br />
          <!--<input type="file" ref="doc" @change="readFile()" />-->
          <div>{{ nodeAry }}</div>
          <div>{{ linkAry }}</div>
      </div>
    <div class="card_title"><b>發現設備列表</b></div>
    <div class="card_body">
      <div id="C_table">
        <div class="C_tr">
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
          <div class="C_td"></div>
        </div>
        <div class="C_tr">
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
          <div class="C_tv"></div>
        </div>
      </div>
      <!--  <input
        align="center"
        style="position: relative; top: 500px; left: 48%"
        type="submit"
        alt="Login"
        value="儲存"
        width="100"
        height="30"
      />

      <input
        align="justify"
        style="position: relative; top: 500px; left: 50%"
        type="submit"
        alt="Login"
        name="drone1"
        value="退出"
      /> -->
      </div>
    <d3-network
      :net-nodes="nodes"
      :net-links="links"
      :options="options"
      @node-click="onClick"
      @link-click="link_Click"
      @readFile="readFile"
    ></d3-network>
  </div>
</template>
<script>
import * as d3 from "d3";
import D3Network from "vue-d3-network";
const nodeIcon1 =
  '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.1.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M0 93.7l183.6-25.3v177.4H0V93.7zm0 324.6l183.6 25.3V268.4H0v149.9zm203.8 28L448 480V268.4H203.8v177.9zm0-380.6v180.1H448V32L203.8 65.7z"/></svg>';
const nodeIcon2 =
  '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.1.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M464 288h-416C21.5 288 0 309.5 0 336v96C0 458.5 21.5 480 48 480h416c26.5 0 48-21.5 48-48v-96C512 309.5 490.5 288 464 288zM320 416c-17.62 0-32-14.38-32-32s14.38-32 32-32s32 14.38 32 32S337.6 416 320 416zM416 416c-17.62 0-32-14.38-32-32s14.38-32 32-32s32 14.38 32 32S433.6 416 416 416zM464 32h-416C21.5 32 0 53.5 0 80v192.4C13.41 262.3 29.92 256 48 256h416c18.08 0 34.59 6.254 48 16.41V80C512 53.5 490.5 32 464 32z"/></svg>';
export default {
  name: "DropDownList",
  components: {
    D3Network,
  },

  data() {
    return {
      jackylo: "請選擇要顯示的port",

      types: [{ title: "請選擇要顯示的port" }],

      nodes: [
        /*{
          id: 1,
          name: "",
          _svgAttrs: "Close", //狀態(暫定)
          _labelClass: "data2",
          _color: "Gray", //正常 Green,不確定yellow,關閉 red
          svgSym:nodeIcon1
        },*/
      ],
      links: [
        // { sid: 1, tid: 1, name: "null", _color: "rebeccapurple" },
      ],
      props: {
        dataList: {
          type: Array,
          default() {
            return [{ name: "選項一" }, { name: "選項二" }];
          },
        },
        labelProperty: {
          type: String,
          default() {
            return "name";
          },
        },
      },
      directives: {
        dpl: {
          bind(el) {
            el.style.display = "none";
          },
        },
      },
      port: [],
      options: {
        force: 3000,
        nodeSize: 15,
        nodeLabels: true,
        linkWidth: 5,
      },
      timer: "",
      value: 0,
      isActive: false, //跳出式視窗判斷
      file: null, //開啟檔案
      content: null, //讀取內容
      nodeAry: null, //nodearray
      linkAry: null, //linkarray
      reloadcheck: false,
      names: [],
    };
  },
  methods: {
    /*    feadTestFile(){
      const file = this.loadFile('node-ip-all.csv');
      console.info(file);
      console.log(this.unicdoeToutf8(file));
      console.log(file);
    },
    loadfile(name){
      const xhr =Bew XMLHttpRequest()
      const okStatus = document.location.protocol==="file:"? 0 : 200
      xhr.open('get',name,false)
      xhr.overrideMimeTyoe('text/html;cgarset=utf-8')
    },*/
    get_preset() {
      var vm = this;
      d3.csv("山丘.李宗盛.csv", function (rows) {
        //console.log(rows);
        vm.types.push({ title: rows.csv, value: rows.csv });
      });
    },
    get() {
      this.value++;
      //console.log(this.value);
      var rows;
      var vm = this;
      var vae;
      var i = 0;
      d3.csv("地球上最浪漫的一首歌.黃鴻升.csv", function (loadedRows) {
        //console.log(loadedRows.csv);
        //console.log(vm.names[i]);
        vae = loadedRows;
        if (vae.csv == "node-ip-all.csv") {
          //console.log(loadedRows);
          doSomethingWithRows(vae);
        }
        if (
          vae.csv != "gateway_hexthop.csv" &&
          vae.csv != "node-ip-all.csv" &&
          vae.csv != ""
        ) {
          vm.names.push(vae.csv);
        }
        //doSomethingWithRows();
      });

      function doSomethingWithRows(vae) {
        //console.log(rows);
        vae = vae;
        if (vae.csv == "node-ip-all.csv") {
          d3.csv("node-ip-all.csv", function (loadedRows) {
            //console.log(vm.names);
            rows = loadedRows;
            i++;
            //console.log(rows);
            if (rows.icon == "nodeIcon1") {
              vm.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: rows.name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: nodeIcon1,
              }); //讀取nodes
            }
            if (rows.icon == "nodeIcon2") {
              vm.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: rows.name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: nodeIcon2,
              }); //讀取nodes
            }
            if (rows.icon == "null") {
              vm.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: rows.name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: null,
              }); //讀取nodes
            }

            d3.csv("gateway_hexthop.csv", function (rows2) {
              //console.log(vm.nodes.length);
              for (let i = 0; i < vm.nodes.length; i++) {
                for (let j = 0; j < vm.nodes.length; j++) {
                  if (rows2.name == vm.nodes[j].name) {
                    //console.log(this.nodes[j].name);
                    vm.links.push({
                      //將讀取資料轉為物件
                      sid: j + 1,
                      tid: j + 1,
                      name: rows2.name,
                      _color: "red",
                    }); //讀取links
                  }
                }
                for (let j = 0; j < vm.nodes.length; j++) {
                  if (rows2.parent == vm.nodes[j].name) {
                    vm.links[vm.links.length - 1].tid = j + 1;
                  }
                }
              }
            });
          });
        }
      }
      //https://stackoverflow.com/questions/34498596/vuejs-csv-filereader
      //https://stackoverflow.com/questions/9491885/csv-to-array-in-d3-js
    },
    get_prop() {
      var i;
      var vm = this;
      var y = 0;
      //console.log(this.names);
      //console.log(this.nodes);
      d3.csv("prop.csv", function (rows) {
        vm.nodes[y].Switch = rows.installation;
        vm.nodes[y].ip = rows.ip;
        
        vm.nodes[y].memory = rows.memory;
        vm.nodes[y].mac = rows.mac;
        vm.nodes[y].cpu = rows.cpu;
        vm.nodes[y].port = rows.port;
        y++;
      });
    },
    get_prot() {
      var vm = this;
      let svgWidth = 600, //svg寬
        svgHeight = 150; //svg高
      let textarray = [];
      let svg = d3
        .select("svg")
        .attr("width", svgWidth)
        .attr("height", svgHeight);
      let dataset = [];
      dataset = [];
      let svgx = 0;
      let svgy = 30;
      let fix = 0;
      console.log(this.jackylo);
      if (this.reloadcheck == true) {
        window.location.reload();
      } else {
        this.reloadcheck = true;
      }
      d3.csv(this.jackylo, function (rows) {
        get_portrun(rows);
      });
      function get_portrun(rows) {
        vm.port.push({
          port_name: rows.port_name,
          on_off: rows.on_off,
        }); //讀取nodes
        if (svgx == 12) {
          svgy = svgy + 40;
          svgx = 0;
        }
        if (svgx == 9) {
          fix = 4;
        }
        if (rows.on_off == 1) {
          svg
            .append("circle")
            .attr("cx", 30 + svgx * 34)
            .attr("cy", svgy)
            .attr("r", 10)
            .attr("fill", "#2ca02c");
          svg
            .append("text")
            .attr("x", 25 + svgx * 34 - fix)
            .attr("y", svgy + 25)
            .style("fill", "steelblue")
            .style("font-size", "15px")
            .style("font-weight", "bold")
            .text(rows.port_name);
        } else if (rows.on_off == 2) {
          svg
            .append("circle")
            .attr("cx", 30 + svgx * 34)
            .attr("cy", svgy)
            .attr("r", 10)
            .attr("fill", "#7f7f7f");
          svg
            .append("text")
            .attr("x", 25 + svgx * 34 - fix)
            .attr("y", svgy + 25)
            .style("fill", "steelblue")
            .style("font-size", "15px")
            .style("font-weight", "bold")
            .text(rows.port_name);
        }
        svgx++;
        //console.log(vm.port.length);
      }
      for (let i = 0; i < this.port.length; i++) {
        if (i == 12) {
          svgy = svgy + 40;
          svgx = 0;
        }
        if (this.port[i].IF == 1) {
          dataset.push(this.port[i].ID);
          svg
            .append("circle")
            .attr("cx", 30 + svgx * 34)
            .attr("cy", svgy)
            .attr("r", 10)
            .attr("fill", "#2ca02c");
        } else if (this.port[i].IF == 2) {
          dataset.push(this.port[i].ID);
          svg
            .append("circle")
            .attr("cx", 30 + svgx * 34)
            .attr("cy", svgy)
            .attr("r", 10)
            .attr("fill", "#7f7f7f");
        }
        textarray[i] = [svgx, svgy];
        svgx++;
      }
      let text = svg
        .selectAll("text")
        .data(dataset)
        .enter()
        .append("text")
        .text((d) => d)
        .attr("x", (d, i) => 25 + 34 * textarray[i][0])
        .attr("y", (d, i) => textarray[i][1] + 25)
        .attr("fill", "#A64C38");
    },

    readFile() {
      this.file = this.$refs.doc.files[0];
      const reader = new FileReader();
      if (this.file.name.includes("node-ip-all.csv")) {
        //讀取txt
        reader.onload = (res) => {
          this.content = res.target.result; //讀取內容
          this.content = this.content.replace(/ /g, ""); //刪除空白
          this.nodes = this.$options.data().nodes; //nodes初始化
          var nodeCSV = d3.csvParseRows(this.content); //csv讀取
          //console.log(nodeCSV);
          var nodeprop = nodeCSV.map(function (d) {
            //將讀取資料轉為物件
            return {
              name: d[0],
              icon: d[2],
            };
          });
          //console.log(nodeprop);
          this.nodeAry = nodeprop;
          //console.log(this.nodeAry[1].icon);
          var i;
          for (i = 1; i < this.nodeAry.length; i++) {
            if (this.nodeAry[i].icon == "nodeIcon1") {
              this.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: this.nodeAry[i].name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: nodeIcon1,
              }); //讀取nodes
            }

            if (this.nodeAry[i].icon == "nodeIcon2") {
              this.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: this.nodeAry[i].name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: nodeIcon2,
              }); //讀取nodes
            }

            if (this.nodeAry[i].icon == "null") {
              this.nodes.push({
                //將讀取資料轉為物件
                id: i,
                name: this.nodeAry[i].name,
                _svgAttrs: "open",
                _labelClass: "data2",
                _color: "Green",
                Switch: null,
                ip: null,
                mac: null,
                cpu: null,
                memory_used: null,
                memory_total: null,
                meory: null,
                port: null,
                _size: 30,
                svgSym: null,
              }); //讀取nodes
            }
          }
          this.nodes[0].x = 300;
          this.nodes[0].pageX = 300;
          this.nodes[0].y = 50;
          this.nodes[0].pageY = 50;

          this.nodes[1].x = 300;
          this.nodes[1].pageX = 300;
          this.nodes[1].y = 100;
          this.nodes[1].pageY = 100;

          this.nodes[2].x = 300;
          this.nodes[2].pageX = 300;
          this.nodes[2].y = -200;
          this.nodes[2].pageY = -200;
        };
        reader.readAsText(this.file);
      } else if (this.file.name.includes("gateway_hexthop.csv")) {
        //讀取csv
        reader.onload = (res) => {
          this.content = res.target.result; //讀取內容
          this.content = this.content.replace(/ /g, ""); //刪除空白
          //this.links = this.$options.data().links;
          this.linkAry = this.content.split(","); //逗點切割陣列
          var table = d3.csvParse(this.content); //d3讀取csv
          for (let i = 0; i < table.length; i++) {
            for (let j = 0; j < this.nodes.length; j++) {
              if (table[i].name == this.nodes[j].name) {
                //console.log(this.nodes[j].name);
                this.links.push({
                  //將讀取資料轉為物件
                  sid: j + 1,
                  tid: j + 1,
                  name: table[i].name,
                  _color: "red",
                }); //讀取links
              }
            }
            for (let j = 0; j < this.nodes.length; j++) {
              if (table[i].parent == this.nodes[j].name) {
                this.links[this.links.length - 1].tid = j + 1;
              }
            }
          }
          this.nodes[0].x = 300;
          this.nodes[0].pageX = 300;
          this.nodes[0].y = 50;
          this.nodes[0].pageY = 50;
          this.nodes[1].x = 300;
          this.nodes[1].pageX = 300;
          this.nodes[1].y = -100;
          this.nodes[1].pageY = -100;
          this.nodes[2].x = 300;
          this.nodes[2].pageX = 300;
          this.nodes[2].y = -200;
          this.nodes[2].pageY = -200;
        };
        reader.readAsText(this.file);
      } else if (this.file.name.includes("-prop.csv")) {
        //讀取csv
        reader.onload = (res) => {
          this.content = res.target.result; //讀取內容
          this.content = this.content.replace(/ /g, ""); //刪除空白
          var parsedCSV = d3.csvParseRows(this.content); //csv讀取
          var prop = parsedCSV.map(function (d) {
            //將讀取資料轉為物件
            return {
              name: d[0],
              attribute: d[1],
            };
          });
          for (let i = 0; i < this.nodes.length; i++) {
            if (prop[1].attribute == this.nodes[i].name) {
              (this.nodes[i].Switch = prop[0].name),
                (this.nodes[i].ip = prop[1].attribute),
                (this.nodes[i].mac = prop[2].attribute),
                (this.nodes[i].cpu = prop[3].attribute),
                (this.nodes[i].memory_used = prop[4].attribute),
                (this.nodes[i].memory_total = prop[5].attribute),
                (this.nodes[i].meory = prop[6].attribute),
                (this.nodes[i].port = prop[7].attribute);
            }
          }
        };
        reader.readAsText(this.file);
      } else if (this.file.name.includes("_interface_port.csv")) {
        //讀取csv
        reader.onload = (res) => {
          this.content = res.target.result; //讀取內容
          this.content = this.content.replace(/ /g, ""); //刪除空白
          let portCSV = d3.csvParseRows(this.content); //csv讀取
          let portAry = portCSV[0];
          let portAry2 = portCSV[1];
          console.log(portAry);
          for (let i = 0; i < portAry.length; i++) {
            this.port.push({
              //將讀取資料轉為物件
              ID: portAry[i],
              IF: portAry2[i],
            }); //讀取links
          }
        };
        reader.readAsText(this.file);
      } else {
        this.content = "check the console for file output";
        reader.onload = (res) => {
          console.log(res.target.result);
        };
        reader.onerror = (err) => console.log(err);
        reader.readAsText(this.file);
      }
    },
    D3port() {
      if (this.reloadcheck == true) {
        window.location.reload();
      } else {
        this.reloadcheck = true;
        let svgWidth = 500, //svg寬
          svgHeight = 120; //svg高
        let textarray = [];
        let svg = d3
          .select("svg")
          .attr("width", svgWidth)
          .attr("height", svgHeight);
        let dataset = [];
        dataset = [];
        let svgx = 0;
        let svgy = 30;
        for (let i = 0; i < this.port.length; i++) {
          if (i == 12) {
            svgy = svgy + 40;
            svgx = 0;
          }
          if (this.port[i].IF == 1) {
            dataset.push(this.port[i].ID);
            svg
              .append("circle")
              .attr("cx", 30 + svgx * 34)
              .attr("cy", svgy)
              .attr("r", 10)
              .attr("fill", "#2ca02c");
          } else if (this.port[i].IF == 2) {
            dataset.push(this.port[i].ID);
            svg
              .append("circle")
              .attr("cx", 30 + svgx * 34)
              .attr("cy", svgy)
              .attr("r", 10)
              .attr("fill", "#7f7f7f");
          }
          textarray[i] = [svgx, svgy];
          svgx++;
        }
        let text = svg
          .selectAll("text")
          .data(dataset)
          .enter()
          .append("text")
          .text((d) => d)
          .attr("x", (d, i) => 25 + 34 * textarray[i][0])
          .attr("y", (d, i) => textarray[i][1] + 25)
          .attr("fill", "#A64C38");
      }
    },

    onClick(event, node) {
      //console.log(event)
      //console.log(JSON.stringify(node))
      this.isActive = true; //彈跳視窗判別
      this.input_ID = node.id;
      this.input_name = node.name;
      this.input_Switch = node.Switch;
      this.input_ip = node.ip;
      this.input_mac = node.mac;
      this.input_cpu = node.cpu;
      this.memory_used = node.memory_used;
      this.input_memory_used = node.memory_used;
      this.input_memory_total = node.memory_total;
      this.input_meory = node.meory;
      this.input_port = node.port;
      this.input_svgAttrs = node._svgAttrs; //點擊node事件
    },
    link_Click(event, link) {
      this.isActive = true;
      this.input_name = link.name;
      this.input_ID = link.id; //點擊link事件
    },
  },

  mounted() {
    this.timer = setTimeout(this.get, 1000);
    this.timer = setTimeout(this.get_prop, 5000);
    this.timer = setTimeout(this.get_preset, 50);
  },
  beforeDestroy() {
    clearTimeout(this.timer);
  },
};
</script>
<style scoped>
.card {
  width: 1000px;
  height: 10px;
  background: white;
  border: 1px solid #cccccc;
  border-radius: 6px;
  box-shadow: 0 0 4px 0 rgba(0, 0, 0, 0.2);
}
.card_title {
  text-align: center;
  background: white;
  padding: 16px;
  border-bottom: 1px solid #cccccc;
  color: rgb(0, 0, 0);
  font-size: 40px;
  line-height: 26px;
  font-weight: bold;
}
.card_body {
  padding: 16px;
  color: #555555;
  font-size: 16px;
  line-height: 22px;
}
</style><!--浮水印CSS!--> 
<style type="text/css">
#watermark {
  color: #8ee1b149;
  font-size: 80pt;
  -webkit-transform: rotate(-45deg);
  -moz-transform: rotate(-45deg);
  position: absolute;
  width: 100%;
  height: 100%;
  margin: 0;
  z-index: -99;
  left: 100px;
  top: 280px;
}
</style>

<style card-class>
.buefy_card {
  margin-top: 10%;
  margin-left: 100%;
  float: right;
  background: rgba(255, 255, 255, 0);
  border-radius: 15px;
  border: thick double #32a1ce;
}
.card-content {
  height: 500px;
  width: 400px;
  font-size: 17px;
  float: right;
  background: white;
  background-color: #ffffff00;
}
</style>
<style card-class>
.card-class {
  background: rgba(0, 0, 0, 0);
  color: rgba(255, 255, 255, 0);
}
</style>
<style type="text/css">
#a {
  width: 100%;
}
</style>
<style>
#C_table {
  border: 1px solid #0066cc;
  padding: 5px;
  -webkit-border-radius: 5px;
  -moz-border-radius: 5px;
  border-radius: 5px;
  text-align: center;
  margin: auto;
  display: table;
}
.C_tr {
  display: table-row;
}
.C_td {
  background: #0066cc;
  color: white;
  width: 150px;
  border: 1px solid #cccccc;
  display: table-cell;
}
.C_tv {
  width: 150px;
  background: #efefef;
  border: 1px solid #cccccc;
  display: table-cell;
}
</style>
<style css_table>
td {
  border: 1px solid #333;
}

thead,
tfoot {
  text-align: center;

  background-color: rgba(87, 54, 235, 0.253);
  border: 1px solid #333;
  color: #333;
}
</style>
<style scoped>
.card {
  width: 1000px;
  margin: 10px;
  background: white;
  border: px solid #cccccc;
  border-radius: 6px;
  box-shadow: 0 0 4px 0 rgba(0, 0, 0, 0.2);
}
.card_title {
  text-align: center;
  background: white;
  padding: 16px;
  border-bottom: px solid #cccccc;
  color: rgb(0, 0, 0);
  font-size: 40px;
  line-height: 26px;
  font-weight: bold;
}
.card_body {
  padding: 16px;
  color: #555555;
  font-size: 16px;
  line-height: 22px;
}
</style>
