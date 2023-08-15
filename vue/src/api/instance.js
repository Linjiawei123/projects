import axios from "axios";
import store from '../store'
//import qs from "querystring"
import { ElMessage } from 'element-plus'

const instance = axios.create({
    timeout: 5000
})

instance.interceptors.request.use(
    config => {
        config.headers.Authorization =  'Bearer ' + store.getters.getToken;
        return config;
    },
    error => {
        Promise.reject(error)
    }
)

instance.interceptors.response.use(
    response => {
        return response.status == 200 ? response : Promise.reject(response.data.message)
    },
    error => {
        console.log(error.response);
        if (error.response.status) {
            switch (error.response.status) {
                // 401: 未登录
                // 未登录则跳转登录页面，并携带当前页面的路径
                // 在登录成功后返回当前页面，这一步需要在登录页操作。                
                case 401:
                    this.$router.replace({
                        path: '/login',
                        query: {
                            // redirect: this.$router.currentRoute.fullPath
                        }
                    });
                    break;

                // // 403 token过期
                // // 登录过期对用户进行提示
                // // 清除本地token和清空vuex中token对象
                // // 跳转登录页面                
                // case 403:
                //     ElMessage({
                //         message: '登录过期，请重新登录',
                //         duration: 3000,
                //         showclose: true,
                //         type:'error'
                //     });
                //     this.$store.commit('delToken');
                //     // 跳转登录页面，并将要浏览的页面fullPath传过去，登录成功后跳转需要访问的页面 
                //     setTimeout(() => {
                //         this.$router.replace({
                //             path: '/',
                //             query: {
                //                 redirect: this.$router.currentRoute.fullPath
                //             }
                //         });
                //     }, 1000);
                //     break;
                // 403 无操作权限               
                case 403:
                    ElMessage({
                        message: '无此操作权限',
                        duration: 3000,
                        showclose: true,
                        type:'error'
                    });
                    break;

                // 404请求不存在
                case 404:
                    ElMessage({
                        message: '网络请求不存在',
                        duration: 3000,
                        showclose: true,
                        type:'error'
                    });
                    break;
                // 其他错误，直接抛出错误提示
                default:
                    ElMessage({
                        message: error.response.data.message,
                        duration: 3000,
                        showclose: true,
                        type:'error'
                    });
            }
            return Promise.reject(error.response);
        }
    }
)


export default instance;