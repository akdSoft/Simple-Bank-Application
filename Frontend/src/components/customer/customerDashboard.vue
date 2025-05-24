<script setup>
import axios from 'axios';
import '../../assets/dashboard.css'
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";

const router = useRouter()
const accounts = ref([])
const selectedAccountId = ref('')

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

const totalBalance = computed(() => {
  let total = 0
  accounts.value.forEach(acc => {
    total += acc.balance
  })
  return total
})

onMounted(async () => {
  try{
    const response = await axios.get('http://localhost:5280/api/BankAccount/user', {withCredentials: true},)
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
})


async function logOut(){
  try{
    const response = await axios.post('http://localhost:5280/api/Auth/logout', {},{withCredentials: true})
    if(response.data === 'logged out'){
      router.push('/')
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
      <h2 class="dashboard-title">customer page</h2>

      <select id="account" v-model="selectedAccountId">
        <option value="">--Choose an Account--</option>
        <option v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}}
        </option>
      </select>

      <div style="display: flex; flex-direction: column">
        <label>Balance of Selected Account</label>
        <input :value="selectedAccount.balance" readonly>

        <label>Total Balance</label>
        <input :value="totalBalance" readonly>
      </div>

      <router-link to="/customer/profile">
        <button class="dashboard-button">Profile</button>
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

      <button class="dashboard-button" @click="logOut">Log Out</button>
    </div>
  </div>
</template>

<style scoped>

</style>