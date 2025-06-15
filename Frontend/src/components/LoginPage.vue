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
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">Login Page</h2>

      <form @submit.prevent="login" style="display: flex; flex-direction: column">
        <label>Username:</label>
        <input class="input" type="text" v-model="username" >

        <label>Password:</label>
        <input  class="input" type="text" v-model="password" >

        <button class="dashboard-button" type="submit">Login</button>

        <router-link to="/register">
          <button class="dashboard-button">Go to Register Page</button>
        </router-link>

        <router-link to="/">
          <button class="dashboard-button">Return</button>
        </router-link>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>