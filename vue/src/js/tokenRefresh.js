import store from "../store/index"
import axios from '../api/instance';
import path from '../api/path'

const startTokenRefreshTimer = (tokenRefreshTimer) => {
    tokenRefreshTimer = setInterval(() => {
        this.refreshToken();
    }, 30 * 60 * 1000);
};

const refreshToken = () => {
    let formData = new window.FormData();
    formData.append("refresh_token", store.getters.getRefresh_token);
    axios.post(path.baseUrl + path.login + '/RefreshToken', formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    }).then(res => {
        this.$store.commit('setToken', res.data.data);
    }).catch(err => {
        console.log(err);
    })
};

const destroyTokenRefreshTimer = (tokenRefreshTimer) => {
    clearInterval(tokenRefreshTimer);
};

export {
    startTokenRefreshTimer,
    refreshToken,
    destroyTokenRefreshTimer
};