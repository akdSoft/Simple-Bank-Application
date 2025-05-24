<script>
import axios from 'axios';
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
      try{
        const payload = {
          username: this.username,
          password: this.password
        }

        const response = await axios.post('http://localhost:5280/api/Auth/login', payload, {withCredentials: true})
        if(response.status === 200){
          if(response.data === 'logged in as admin'){
            this.$router.push('/admin/dashboard')
          }
          else{
            this.$router.push('/customer/dashboard')
          }
        }
        else{
          alert('unexpected situation')
        }
      } catch (err){
        alert('error')
        console.log(err.message);
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
        <input class="input" type="text" v-model="username" required>

        <label>Password:</label>
        <input  class="input" type="text" v-model="password" required>

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