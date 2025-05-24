<script setup>
import {ref} from "vue";
import axios from "axios";

const accountId = ref('')

async function deleteAccount(){
  try {
    const response = await  axios.delete(`http://localhost:5280/api/BankAccount/${accountId.value}`, {withCredentials: true})
    if(response.status === 204 || response.status === 404){
      alert('account deleted')
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
      <h2 class="dashboard-title">Delete Accounts</h2>

      <div style="display: flex; flex-direction: column">
        <label>Account Id</label>
        <input v-model="accountId">
      </div>

      <button class="dashboard-button" @click="deleteAccount">Delete</button>

      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>