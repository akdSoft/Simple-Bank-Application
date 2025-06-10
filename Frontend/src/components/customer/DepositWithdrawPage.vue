<script setup>
import {computed, onMounted, ref} from "vue";
import api from '../../api/axiosInstance.js'

const amount = ref('')
const accounts = ref([])
const selectedAccountId = ref('')

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

async function deposit(){
  if(!selectedAccount.value || !amount.value) {
    alert("All fields must be selected and completed correctly")
    return
  }
  const payload = {
    accountId: selectedAccountId.value,
    amount: amount.value,
  }

  try {
    const response = await api.post('/Transaction/deposit', payload)
    alert("Money has been deposited")
    await loadAccounts()
  } catch (err) {
    if (err.status === 400){
      alert("All fields must be selected and completed correctly")
    }
    else{
      alert(err.message)
    }
  }
}

async function withdraw(){
  if(!selectedAccount.value || !amount.value) {
    alert("All fields must be selected and completed correctly")
    return
  }
  const payload = {
    accountId: selectedAccountId.value,
    amount: amount.value,
  }
  try {
    const response = await api.post('/Transaction/withdraw', payload)
    alert("Money has been withdrawn")
    await loadAccounts()
  } catch (err) {
    if (err.status === 400){
      alert("All fields must be selected and completed correctly")
    }
    else{
      alert(err.message)

    }
  }
}

onMounted(async () => {
  await loadAccounts()
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount/user')
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
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}} - {{account.currencyType}} Account
        </option>
      </select>

      <label>Balance</label>
      <input class="input" v-if="selectedAccount" :value="selectedAccount.balance + ' ' + selectedAccount.currencySymbol" readonly>
      <input class="input" v-else readonly>

      <label>Amount</label>
      <input class="input" v-model="amount">
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