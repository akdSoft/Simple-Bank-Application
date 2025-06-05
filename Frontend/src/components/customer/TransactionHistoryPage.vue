<script setup>
import {ref, onMounted, computed} from "vue";
import api from '../../api/axiosInstance.js'
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
    const response = await api.get(`/Transaction/account/${selectedAccountId.value}`)
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
    const response = await api.get('/BankAccount/user')
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
<div class="dashboard-wrapper">
  <div class="dashboard-card" style="max-width: max-content">
    <h2 class="dashboard-title">Transaction History</h2>

    <table class="custom-table" v-if="transactions.length > 0">
      <tbody>
      <tr>
        <th class="custom-table-h">Id</th>
        <th class="custom-table-h">Source Type</th>
        <th class="custom-table-h">Source Id</th>
        <th class="custom-table-h">Target Type</th>
        <th class="custom-table-h">Target Id</th>
        <th class="custom-table-h">Amount</th>
        <th class="custom-table-h">User Id</th>
        <th class="custom-table-h">Type</th>
        <th class="custom-table-h">Timestamp</th>
      </tr>

      <tr v-for="transaction in transactions" :key="transaction.id">
        <td class="custom-table-h">{{transaction.id}}</td>
        <td class="custom-table-h">{{transaction.sourceType}}</td>
        <td class="custom-table-h">{{transaction.sourceId}}</td>
        <td class="custom-table-h">{{transaction.targetType}}</td>
        <td class="custom-table-h">{{transaction.targetId}}</td>
        <td class="custom-table-h">{{transaction.amount}}</td>
        <td class="custom-table-h">{{transaction.userId}}</td>
        <td class="custom-table-h">{{transaction.type}}</td>
        <td class="custom-table-h">{{transaction.timestamp}}</td>
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