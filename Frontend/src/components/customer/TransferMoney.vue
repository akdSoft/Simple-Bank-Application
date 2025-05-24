<script setup>

import {computed, onMounted, ref} from "vue";
import axios from "axios";

const amount = ref('')
const targetAccountId = ref('')
const accounts = ref([])
const selectedAccountId = ref('')

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

async function transfer(){
  const payload = {
    fromAccountId: selectedAccountId.value,
    targetAccountId: targetAccountId.value,
    amount: amount.value
  }
  try {
    const response = await axios.post('http://localhost:5280/api/Transaction/transfer', payload, {withCredentials: true})
    if (response.status === 200){
      alert('successfully transferred')
      await loadAccounts()
    }
    else{
      alert('unexpected situation')
    }
  } catch (err) {
    alert(err.message)
  }
}

onMounted(async () => {
  await loadAccounts()
})

async function loadAccounts(){
  try{
    const response = await axios.get('http://localhost:5280/api/BankAccount/user', {withCredentials: true},)
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">Transfer Money</h2>

      <div style="display: flex; flex-direction: column">
        <label>Select An Account</label>

        <select v-model="selectedAccountId">
          <option>--Select an Account--</option>
          <option v-for="account in accounts" :key="account.id">
            {{account.id}}
          </option>
        </select>

        <label>Balance</label>
        <input :value="selectedAccount.balance" readonly>

        <label>Target Account ID:</label>
        <input v-model="targetAccountId">

        <label>Amount</label>
        <input v-model="amount">
      </div>

      <button class="dashboard-button" @click="transfer">Transfer</button>

      <router-link to="/customer/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>