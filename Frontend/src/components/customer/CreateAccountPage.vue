<script setup>
import {onMounted, ref} from "vue";
import api from '../../api/axiosInstance.js'

const selectedAccountType = ref('')
const currencies = ref([])
const selectedCurrencyId = ref('')

async function createAccount(){
  if(!selectedAccountType.value || !selectedCurrencyId.value){
    alert('All fields must be selected and completed correctly')
    return
  }

  const payload = {
    accountType: selectedAccountType.value,
    currencyId: selectedCurrencyId.value
  }

  try{
    const response = await api.post('/BankAccount', payload)
    alert("Account has been created")
  }
  catch (err) {
    if(err.status === 400){
      alert("All fields must be selected and completed correctly")
    } else {
      alert(err.message)
    }
  }
}

onMounted(async () => {
  await loadCurrencies();
})

async function loadCurrencies(){
  try{
    const response = await api.get('/Currency')
    currencies.value = response.data
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
<div class="dashboard-wrapper">
  <div class="dashboard-card">
    <h2 class="dashboard-title">Create Account</h2>

    <div style="display: flex; flex-direction: column">
      <label>Account Type</label>

      <select class="select" v-model="selectedAccountType">
        <option class="select-items" value="">--Select an Account Type--</option>
        <option class="select-items" value="SavingsAccount">Savings Account</option>
        <option class="select-items" value="CheckingAccount">Checking Account</option>
      </select>

      <label>Currency</label>

      <select class="select" v-model="selectedCurrencyId">
        <option class="select-items" value="">--Select a Currency--</option>
        <option class="select-items" v-for="currency in currencies" :key="currency.id" :value="currency.id">
          {{currency.name}}
        </option>
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