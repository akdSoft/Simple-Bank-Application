<script>
import api from '../api/axiosInstance.js';

export default {
  data(){
    return {
      name: '',
      surname: '',
      username: '',
      password: '',
      email: ''
    }
  },
  methods: {
    async register() {
      if(!this.name || !this.surname || !this.username || !this.password || !this.email){
        alert('All fields must be selected and completed correctly')
        return
      }
      try {
        const payload = {
          name: this.name,
          surname: this.surname,
          username: this.username,
          password: this.password,
          email: this.email
        }

        const response = await api.post('/Auth/register', payload)
        alert ('User has been created')

        this.name = ''
        this.surname = ''
        this.username = ''
        this.password = ''
        this.email = ''
      } catch (err){
        if(err.status === 400){
          const errors = err.response.data.errors;

          let message = ''
          for(const field in errors){
            message += `${field}: ${errors[field]}\n}`
          }
          alert(message)
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
      <h2 class="dashboard-title">Register Page</h2>

      <form @submit.prevent="register" style="display: flex; flex-direction: column">
        <label >Name:</label>
        <input class="input" type="text" v-model="name" required>

        <label>Surname:</label>
        <input class="input" type="text" v-model="surname" required>

        <label>Username:</label>
        <input class="input" type="text" v-model="username" required>

        <label>Password:</label>
        <input class="input" type="text" v-model="password" required>

        <label>Email:</label>
        <input class="input" type="text" v-model="email" required>

        <button class="dashboard-button" type="submit">Register</button>
      </form>

      <router-link to="/login">
        <button class="dashboard-button">Go to Login Page</button>
      </router-link>

      <router-link to="/">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>

  </div>

</template>

<style scoped>

</style>