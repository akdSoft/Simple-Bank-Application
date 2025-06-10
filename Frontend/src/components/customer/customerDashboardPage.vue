<script setup>
import '../../assets/dashboard.css'
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'
import BalanceLineChart from "./ModalsAndComponents/BalanceLineChart.vue";

const router = useRouter()
const accounts = ref([])
const selectedAccountId = ref('')

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

const totalBalance = ref('')

onMounted(async () => {
  await  loadAccounts();
  await loadTotalBalance();
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount/user')
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

async function loadTotalBalance(){
  try{
    const response = await api.get('/user/me')
    totalBalance.value = response.data.totalBalanceInTRY;
  } catch (err) {
    alert(err.message)
  }
}

async function logOut(){
  try{
    const response = await api.post('/Auth/logout')
    if(response.data === 'logged out'){
      router.push('/')
    }
    else{
      alert('Unexpected situation')
    }
  } catch (err) {
    alert(err.message)
  }
}

async function deleteAccount(){
  if(!selectedAccount.value){
    alert('Choose a Valid Account')
    return
  }

  try {
    const response = await api.delete(`/BankAccount/${selectedAccountId.value}`)
    if (response.status === 204) {
      alert('Account has been deleted')
    }
  } catch (err) {
    if(err.status === 404 || err.status === 405){
      alert('Choose a Valid Account')
    }
    else{
      alert(err.message)
    }
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">customer page</h2>

      <select class="select" id="account" v-model="selectedAccountId">
        <option class="select-items" value="">--Choose an Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}} - {{account.currencyType}} Account
        </option>
      </select>

      <div style="display: flex; flex-direction: column; margin-bottom: 30px">
        <label>Balance of Selected Account</label>
        <input class="input" v-if="selectedAccount" :value="selectedAccount.balance + ' ' + selectedAccount.currencySymbol" readonly>
        <input class="input" v-else  readonly>

        <label>Total Balance (in TRY)</label>
        <input class="input" :value="totalBalance + ' ₺'" readonly>

        <button class="dashboard-button" @click="deleteAccount">Delete Selected Account</button>
      </div>

      <div style="height: 400px">
        <BalanceLineChart />
      </div>


      <router-link to="/customer/profile">
        <button class="dashboard-button">Profile</button>
      </router-link>

      <router-link to="/customer/cards">
        <button class="dashboard-button">Cards</button>
      </router-link>

      <router-link to="/customer/create-account">
        <button class="dashboard-button">Create Account</button>
      </router-link>

      <router-link to="/customer/transfer-money">
        <button class="dashboard-button">Transfer Money</button>
      </router-link>

      <router-link to="/customer/deposit-withdraw">
        <button class="dashboard-button">Deposit/Withdraw</button>
      </router-link>

      <router-link to="/customer/transaction-history">
        <button class="dashboard-button">Transaction History</button>
      </router-link>

      <button class="dashboard-button" @click="logOut">Log Out</button>

    </div>
  </div>

</template>

<style scoped>

</style>