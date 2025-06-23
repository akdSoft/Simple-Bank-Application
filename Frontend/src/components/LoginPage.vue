<script>
import api from '../api/axiosInstance.js';
import {jwtDecode} from "jwt-decode";
export default{
  computed: {
    register() {
      return register
    }
  },
  data(){
    return{
      username: '',
      password: ''
    }
  },
  methods: {
    async login(){
      if(!this.username || !this.password){
        alert('All fields must be selected and completed correctly')
        return
      }
      try{
        const payload = {
          username: this.username,
          password: this.password
        }

        const response = await api.post('/Auth/login', payload)

        const token = response.data.token
        localStorage.setItem('token', token)


        if(response.status === 200){

          try {
            const decodedToken = jwtDecode(token)

            const role = decodedToken.role

            if(role === 'admin') {
              this.$router.push('/admin/dashboard')
            } else if (role === 'customer') {
              this.$router.push('/customer/dashboard')
            } else {
              alert('Invalid token: ' + err)
            }
          } catch (err) {
            alert('Invalid token: ' + err)
          }
        }
      } catch (err){
        if(err.status === 400){
          const errors = err.response.data.errors;

          let message = ''
          for(const field in errors){
            message += `${field}: ${errors[field]}\n`
          }
          alert(message)

        } else if (err.status === 401){
          alert('Incorrect username or password')
        } else {
          alert(err.message)
        }
      }
    }
  }
}
</script>

<template>
  <div class="wrapper">
    <div class="homePage-card">
      <div class="homePage-title">
        <a class="title">Login Page</a>
      </div>

      <form @submit.prevent="login" style="display: flex; flex-direction: column">
        <label>Username:</label>
        <input class="input" type="text" v-model="username" >

        <label>Password:</label>
        <input  class="input" type="text" v-model="password" >

        <button class="dashboard-button" type="submit">Login</button>

      </form>

      <div class="homepage-button">
        <a href="/" style="width: 40%">
          <div class="card" style="height: 60px; width: 140px;  display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 18px">HOME PAGE</p>
          </div>
        </a>

        <a href="/register" style="width: 40%">
          <div class="card" style="height: 60px; width: 140px;  display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 18px">REGISTER</p>
          </div>
        </a>
      </div>
    </div>
  </div>

</template>

<style scoped>
.wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.homePage-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  padding-top: 90px;
  padding-bottom: 90px;
  background-color: transparent;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 450px;
  height: 550px;
  border-radius: 20px;
}

.homePage-title {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.homepage-button {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  gap: 60px;
}

.card {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 23px;
  min-height: 40px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.title {
  color: black;
  font-size: 28px;
  font-weight: bold;
}

</style>