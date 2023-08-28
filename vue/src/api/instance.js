import axios from "axios";
import store from '../store'
import router from "@/router";
//import qs from "querystring"
import { ElMessage } from 'element-plus'

const instance = axios.create({
    timeout: 5000
})

instance.interceptors.request.use(
    config => {
        config.headers.Authorization = 'Bearer ' + store.getters.getToken;
        return config;
    },
    error => {
        Promise.reject(error)
    }
)

instance.interceptors.response.use(
    response => {
        if (response.status == 200 && response.data.status === true)
            return response;
        else if (response.status == 200 && response.data.status === false)
            ElMessage({
                message: response.data.message,
                duration: 3000,
                showclose: true,
                type: 'error'
            });
        else
            return Promise.reject(response.data.message)
    },
    error => {
        if (error.response.status) {
            switch (error.response.status) {
                // 401: 未登录
                // 未登录则跳转登录页面，并携带当前页面的路径
                // 在登录成功后返回当前页面，这一步需要在登录页操作。                
                case 401:
                    router.replace({
                        path: '/',
                        query: {
                            redirect: router.currentRoute.fullPath
                        }
                    });
                    break;
                // 403 无操作权限               
                case 403:
                    ElMessage({
                        message: '无此操作权限',
                        duration: 3000,
                        showclose: true,
                        type: 'error'
                    });
                    break;

                // 404请求不存在
                case 404:
                    ElMessage({
                        message: '网络请求不存在',
                        duration: 3000,
                        showclose: true,
                        type: 'error'
                    });
                    break;
                // 502服务器错误
                case 502:
                    ElMessage({
                        message: '服务器错误',
                        duration: 3000,
                        showclose: true,
                        type: 'error'
                    });
                    break;
                // 其他错误，直接抛出错误提示
                default:
                    ElMessage({
                        message: error.response.data.message,
                        duration: 3000,
                        showclose: true,
                        type: 'error'
                    });
            }
            return Promise.reject(error.response);
        }
    }
)


export default instance;