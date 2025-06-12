<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'

const userId = ref('')

async function deleteUser(){
  if(!userId.value){
    alert("All fields must be selected and completed correctly")
    return
  }

  try {
    const response = await api.delete(`/User/${userId.value}`)
    alert('User has been deleted')
  } catch (err) {
    if(err.status === 404){
      alert('Make sure user exists')
    } else {
      alert(err.message)
    }
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">Delete User</h2>

      <div style="display: flex; flex-direction: column">
        <label>User Id</label>
        <input class="input" v-model="userId">
      </div>

      <button class="dashboard-button" @click="deleteUser">Delete</button>

      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>