<script setup>
import axios from "axios";
import {ref} from "vue";

const selectedAccountType = ref('')

async function createAccount(){
  if(!selectedAccountType.value) return;

  try{
    const response = await axios.post('http://localhost:5280/api/BankAccount', {accountType: selectedAccountType.value}, {withCredentials: true})
    if (response.status === 200){
      alert("account created")
    }
    else{
      alert("unexpected situation")
    }
  }
  catch (err) {
    alert(err.message)
  }
}
</script>

<template>
<div class="dashboard-wrapper">
  <div class="dashboard-card">
    <h2 class="dashboard-title">Create Account</h2>
    <h2 class="dashboard-title">{{selectedAccountType}}</h2>

    <div style="display: flex; flex-direction: column">
      <label>Account Type</label>

      <select class="select" v-model="selectedAccountType">
        <option class="select-items" value="">--Select an Account Type--</option>
        <option class="select-items" value="SavingsAccount">Savings Account</option>
        <option class="select-items" value="CheckingAccount">Checking Account</option>
      </select>
    </div>

    <button class="dashboard-button" @click="createAccount()">Create Account</button>

    <router-link to="/customer/dashboard">
      <button class="dashboard-button">Return</button>
    </router-link>
  </div>
</div>
</template>

<style scoped>

</style>