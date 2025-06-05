<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'

const accountId = ref('')

async function deleteAccount(){
  try {
    const response = await  api.delete(`/BankAccount/${accountId.value}`)
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
        <input class="input" v-model="accountId">
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