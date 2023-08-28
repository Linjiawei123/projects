import { createStore } from 'vuex'
import storage from './storage.js'
import router from '../router/index.js'

export default createStore({
  state: {
    token: '',
    refresh_token: '',
    userInfo: {},
    menuActiveIndex: '',
    tags: [
      {
        name: '首页',
        url: '/home/main'
      }
    ],
    isTagsShow: false
  },
  getters: {
    getToken(state) {
      return state.token || storage.getStr("token") || "";
    },
    getUserInfo(userInfo) {
      return userInfo;
    },
    getRefresh_token(state){
      return state.refresh_token || storage.getStr("refresh_token") || "";
    }
  },
  mutations: {
    setToken(state, data) {
      state.token = data.access_token;
      state.refresh_token = data.refresh_token;
      storage.setStr('token', data.access_token);
      storage.setStr('refresh_token', data.refresh_token);
    },
    delToken(state) {
      state.token = "";
      storage.remove("token");
      state.tags = [{
        name: '首页',
        url: '/home/main'
      }];
      state.menuActiveIndex = '';
    },
    setUserInfo(state, userInfo) {
      state.userInfo = userInfo;
    },
    menuIndex(state, menuId) {
      state.menuActiveIndex = menuId;
    },
    pushtags(state, val) {
      state.menuActiveIndex = val.id;
      let result = state.tags.findIndex(item => item.name === val.name);
      result === -1 ? state.tags.push(val) : '';
    },
    closeTab(state, val) {
      let result = state.tags.findIndex(item => item.name === val.name);
      state.tags.splice(result, 1);
    },
    cleartagsview(state, val) {
      state.tags = [{
        name: '首页',
        url: '/home/main'
      }]
      if (val !== "/home/main") {
        router.push({ path: "/home/main" });
      }
    },
    changeisshow(state) {
      state.isTagsShow = !state.isTagsShow;
    }
  },
  actions: {
    // removeToken: (context) => {
    // context.commit('setToken')
    // }
  },
  modules: {
  }
})
