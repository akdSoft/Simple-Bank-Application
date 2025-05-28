<script setup>
import {ref} from "vue";
import axios from "axios";

const userId = ref('')

async function deleteUser(){
  try {
    const response = await  axios.delete(`http://localhost:5280/api/User/${userId.value}`, {withCredentials: true})
    if(response.status === 204 || response.status === 404){
      alert('user deleted')
    }
    else{
      alert('unexpected situation')
    }
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">Delete User</h2>

      <div style="display: flex; flex-direction: column">
        <label>User Id</label>
        <input v-model="userId">
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