<script setup>
import {computed, onMounted, ref} from "vue";
import axios from "axios";

const amount = ref('')
const accounts = ref([])
const selectedAccountId = ref('')

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

async function deposit(){
  const payload = {
    accountId: selectedAccountId.value,
    amount: amount.value,
  }

  try {
    const response = await axios.post('http://localhost:5280/api/Transaction/deposit', payload, {withCredentials: true})
    if (response.status === 200){
      await loadAccounts()
      alert("successfully deposited")
    }
  } catch (err) {
    alert(err.message)
  }
}

async function withdraw(){
  const payload = {
    accountId: selectedAccountId.value,
    amount: amount.value,
  }
  try {
    const response = await axios.post('http://localhost:5280/api/Transaction/withdraw', payload, {withCredentials: true})
    if (response.status === 200){
      await loadAccounts()
      alert("successfully withdrew")
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
    <h2 class="dashboard-title">Deposit/Withdraw</h2>

    <div style="display: flex; flex-direction: column">
      <label>Select An Account</label>

      <select class="select" v-model="selectedAccountId">
        <option class="select-items" >--Select an Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id">
          {{account.id}}
        </option>
      </select>

      <label>Balance</label>
      <input :value="selectedAccount.balance" readonly>

      <label>Amount</label>
      <input v-model="amount">
    </div>

    <button class="dashboard-button" @click="deposit">Deposit</button>
    <button class="dashboard-button" @click="withdraw">Withdraw</button>

    <router-link to="/customer/dashboard">
      <button class="dashboard-button">Return</button>
    </router-link>
  </div>
</div>
</template>

<style scoped>

</style>