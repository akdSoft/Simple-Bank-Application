<script setup>
import {ref, onMounted, computed} from "vue";
import axios from "axios";
import '../../assets/dashboard.css'

const transactions = ref([])
const accounts = ref([])
const selectedAccountId = ref(0)

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

async function showTransactionList(){
  try {
    const response = await axios.get(`http://localhost:5280/api/Transaction/account/${selectedAccountId.value}`, {withCredentials: true})
    transactions.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

onMounted(async () => {
  await loadAccounts();
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
    <h2 class="dashboard-title">Transaction History</h2>

    <table v-if="transactions.length > 0">
      <tbody>
      <tr>
        <th>Id</th>
        <th>Account Id</th>
        <th>User Id</th>
        <th>Amount</th>
        <th>Type</th>
        <th>Related Account ID</th>
      </tr>

      <tr v-for="transaction in transactions" :key="transaction.id">
        <td>{{transaction.id}}</td>
        <td>{{transaction.accountId}}</td>
        <td>{{transaction.userId}}</td>
        <td>{{transaction.amount}}</td>
        <td>{{transaction.type}}</td>
        <td>{{transaction.relatedAccountId}}</td>
      </tr>
      </tbody>
    </table>

    <div style="display: flex; flex-direction: row; gap: 5px">
      <button class="dashboard-button" @click="showTransactionList">Show Transaction List</button>

      <select class="select" id="account" v-model="selectedAccountId">
        <option class="select-items" :value="0">--Filter by Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}}
        </option>
      </select>
    </div>

    <router-link to="/customer/dashboard">
      <button class="dashboard-button">Return</button>
    </router-link>
  </div>
</div>
</template>

<style scoped>

</style>