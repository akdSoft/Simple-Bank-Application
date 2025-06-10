<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'

const accountId = ref('')

async function deleteAccount(){
  if(!accountId.value){
    alert("All fields must be selected and completed correctly")
    return
  }
  try {
    const response = await  api.delete(`/BankAccount/${accountId.value}`)
    alert('Account has been deleted')
  } catch (err) {
    if(err.status === 404) {
      alert('Make sure user exists')
    } else if (err.status === 405) {
      alert("All fields must be selected and completed correctly")
    }
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