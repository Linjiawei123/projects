<template>
  <div class="login">
    <div class="form">
      <h3>用户登录</h3>
      <div class="input">
        <el-input v-model="Account" placeholder="请输入账号" prefix-icon="User" />
      </div>
      <div class="input">
        <el-input v-model="Password" placeholder="请输入密码" prefix-icon="Hide" type="password" show-password />
      </div>
      <el-button class="but" type="success" @click="login">登录</el-button>
    </div>
  </div>
</template>

<script>
import axios from '../api/instance';
import path from '../api/path'
import md5 from '../js/md5'
export default {
  name: 'LoginView',
  data() {
    return {
      Account: 'admin',
      Password: '123456'
    }
  },
  methods: {
    login() {
      axios.post(path.baseUrl + path.login, {
        account: this.Account,
        password: md5.hexMD5(this.Password)
      }).then(res => {
        this.$store.commit('setToken', res.data.data.access_token);
        setTimeout(() => {
          this.$router.push('/home');
        })
      }).catch(err => {
        console.log(err);
      })
    }
  }
}
</script>
<style scoped>
.login {
  width: 100%;
  height: 100%;
  background-color: bisque;
  background-image: url('../assets/login.png');
  background-size: 100% 100%;
  background-repeat: no-repeat;
  position: relative;
}

.form {
  width: 400px;
  height: 250px;
  background-color: rgb(230, 230, 230);
  padding: 10px;
  border-radius: 10px;
  box-shadow: 5px 5px 5px #888888;
  position: absolute;
  top: 40%;
  right: 10%;
}

.input {
  margin: 15px 0px;
}

.but {
  width: 100%;
}
</style>
