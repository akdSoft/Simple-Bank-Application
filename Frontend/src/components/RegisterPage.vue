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
  <div class="wrapper">
    <div class="homePage-card">
      <div class="homePage-title">
        <a class="title">Register Page</a>
      </div>

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

      <div class="homepage-button">
        <a href="/" style="width: 40%">
          <div class="card" style="height: 60px; width: 140px; display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 16px">Home Page</p>
          </div>
        </a>

        <a href="/login" style="width: 40%">
          <div class="card" style="height: 60px; width: 140px; display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 16px">Login Page</p>
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
  padding-top: 30px;
  padding-bottom: 30px;
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