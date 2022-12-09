import Vue from "vue";
import App from "./App.vue";
import Buefy from "buefy";
import "buefy/dist/buefy.css";
Vue.config.productionTip = false;
Vue.use(Buefy);
/*const example = new Vue({
  data: () => ({ file: null, content: null,strAry:null }),
  template: `
    <div style="border-style:solid">
      <input type="file" ref="doc" @change="readFile()" />
      <div>{{content}}</div>
    </div>
  `,
  methods: {
    readFile() {
      this.file = this.$refs.doc.files[0];
      const reader = new FileReader();
      if (this.file.name.includes(".txt")) {
        reader.onload = (res) => {
          this.content = res.target.result;
          var strAry =  this.content.split(" ");
          // 輸出 ["a", "b", "c", "d", "e"]
          console.log(strAry);
        };
        reader.onerror = (err) => console.log(err);
        reader.readAsText(this.file);
      } else {
        this.content = "check the console for file output";
        reader.onload = (res) => {
          console.log(res.target.result);
        };
        reader.onerror = (err) => console.log(err);
        reader.readAsText(this.file);
      }
    }
  }
}).$mount("#app2");*/
new Vue({
  render: (h) => h(App)
  //data{}
}).$mount("#app");
