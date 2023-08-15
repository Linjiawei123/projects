import { createStore } from 'vuex'
import storage from './storage.js'
import router from '../router/index.js'

export default createStore({
  state: {
    token: "",
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
    }
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
      storage.setStr('token', token);
      // console.log('store、localstorage保存token成功！');
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
