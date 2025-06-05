<script setup>
import {computed, onMounted, ref} from "vue";
import api from '../../api/axiosInstance.js'
import {useRouter} from "vue-router";

const router = useRouter()

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
    const response = await api.put('/User', payload)
    if(response.status === 200){
      alert('user updated')
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
    const response = await api.delete('/User')
    if(response.status === 204){
      alert('user deleted')
      router.push('/')
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
    const response = await api.get('/User/me')
    user.value = response.data
  } catch (err) {
    alert(err.message)
  }
})
</script>

<template>
<div class="dashboard-wrapper">
  <div class="dashboard-card">
    <h2 class="dashboard-title">Profile</h2>

    <div style="display: flex; flex-direction: column">
      <label>Name:</label>
      <input class="input" v-model="user.name">

      <label>Surname:</label>
      <input class="input" v-model="user.surname">

      <label>Username:</label>
      <input class="input" v-model="user.username">

      <label>Password:</label>
      <input class="input" v-model="user.password">

      <label>Email:</label>
      <input class="input" v-model="user.email">

      <label>User ID:</label>
      <input class="input" :value="user.id" readonly>
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