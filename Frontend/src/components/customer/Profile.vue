<script setup>
import {computed, onMounted, ref} from "vue";
import axios from "axios";

const user = ref({
  name: '',
  surname: '',
  username: '',
  password: '',
  email: '',
  id: 0
})



async function updateUser(){
  const payload = {
    name: user.value.name,
    surname: user.value.surname,
    username: user.value.username,
    password: user.value.password,
    email: user.value.email
  }
  try{
    const response = await axios.put('http://localhost:5280/api/User', payload, {withCredentials: true})
    if(response.status === 200){
      alert('user updated')
      this.$router.push('/')
    }
    else{
      alert('unexpected situation')
    }
  } catch (err) {
    alert(err.message)
  }
}

async function deleteUser(){
  try{
    const response = await axios.delete('http://localhost:5280/api/User', {withCredentials: true})
    if(response.status === 204){
      alert('user deleted')
    }
    else{
      alert('unexpected situation')
    }
  } catch (err) {
    alert(err.message)
  }
}

onMounted(async () => {
  try{
    const response = await axios.get('http://localhost:5280/api/User/me', {withCredentials: true})
    user.value = response.data
  } catch (err) {
    alert(err.message)
  }
})
</script>

<template>
  <h3>name {{user.name}}</h3>
<div class="dashboard-wrapper">
  <div class="dashboard-card">
    <h2 class="dashboard-title">Profile</h2>

    <div style="display: flex; flex-direction: column">
      <label>Name:</label>
      <input v-model="user.name">

      <label>Surname:</label>
      <input v-model="user.surname">

      <label>Username:</label>
      <input v-model="user.username">

      <label>Password:</label>
      <input v-model="user.password">

      <label>Email:</label>
      <input v-model="user.email">

      <label>User ID:</label>
      <input :value="user.id" readonly>
    </div>

    <button class="dashboard-button" @click="updateUser">Update</button>
    <button class="dashboard-button" @click="deleteUser">Delete User</button>

    <router-link to="/customer/dashboard">
      <button class="dashboard-button">Return</button>
    </router-link>
  </div>
</div>
</template>

<style scoped>

</style>