import { createApp } from 'vue'
import './css/all.css'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from "axios"
import 'video.js/dist/video-js.css'
import locale from 'element-plus/lib/locale/lang/zh-cn'
import { isHasRgihts } from './js/isHasRgihts'


const app = createApp(App)
app.config.globalProperties.$axios = axios

app.use(store)

app.use(router)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}

app.use(ElementPlus, { locale });

app.config.globalProperties.$isHasRgihts = isHasRgihts

app.mount('#app')
