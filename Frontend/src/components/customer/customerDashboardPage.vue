<script setup>
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'
import BalanceLineChart from "./ModalsAndComponents/BalanceLineChart.vue";

const router = useRouter()
const accounts = ref([])
const selectedAccountId = ref('')
const transactions = ref([])
const totalCurrenciesBalance = {
  TRY: 0.00,
  USD: 0.00,
  EUR: 0.00
}

async function showTransactionList(){
  try {
    const response = await api.get(`/Transaction/account/${0}`)
    transactions.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

let totalVirtualCardLimits = ref('')

const totalBalance = ref('')

onMounted(async () => {
  await loadAccounts();
  await loadTotalBalance();
  await loadTotalCurrenciesBalance();
  await loadVirtualCards();
  await showTransactionList();
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount/user')
    accounts.value = response.data
  } catch (err) {
    if(err.status === 401){
      alert("Unauthorized")
    } else {
      alert(err.message)
    }
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

async function loadTotalCurrenciesBalance(){
  try{
    const response = await api.get(`/currency/user/currency-balance/${1}`)
    totalCurrenciesBalance.TRY = response.data;

    const response2 = await api.get(`/currency/user/currency-balance/${2}`)
    totalCurrenciesBalance.USD = response2.data;

    const response3 = await api.get(`/currency/user/currency-balance/${3}`)
    totalCurrenciesBalance.EUR = response3.data;
  } catch (err) {
    alert(err.message)
  }
}

async function loadVirtualCards() {
  try {
    const response = await api.get('/card/virtual-cards/user')

    totalVirtualCardLimits = response.data.reduce((totalLimit, card) => totalLimit + card.availableLimit, 0)
  } catch (err) {
    alert(err.message)
  }
}

async function logOut(){
  localStorage.removeItem('token')
  await router.push('/')
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
      await loadAccounts()
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

async function quickaction_deposit() {
  if(!selectedAccount.value) {
    alert("An account must be selected")
    return
  }
  const payload = {
    accountId: selectedAccountId.value,
    amount: 1000,
  }

  try {
    await api.post('/Transaction/deposit', payload)
    alert("Money has been deposited")
    await loadAccounts();
    await loadTotalBalance();
    await loadTotalCurrenciesBalance();
    await showTransactionList();

  } catch (err) {
      alert(err.message)
  }
}

async function quickaction_withdraw() {
  if(!selectedAccount.value) {
    alert("An account must be selected")
    return
  }
  const payload = {
    accountId: selectedAccountId.value,
    amount: 500,
  }

  try {
    await api.post('/Transaction/withdraw', payload)
    alert("Money has been withdrawn")
    await loadAccounts();
    await loadTotalBalance();
    await loadTotalCurrenciesBalance();
    await showTransactionList();

  } catch (err) {
    if (err.status === 400) {
      alert("You do not have enough funds")
    } else {
      alert(err.message)

    }
  }
}

async function quickaction_createcard() {
  if(!selectedAccount || selectedAccount.value.currencyType !== 'TRY'){
    alert("A TRY account must be selected")
    return
  }
  const password = Math.floor(1000 + Math.random() * 9000).toString()

  const payload = {
    onlineShopping: false,
    password: password,
    linkedAccountId: selectedAccountId.value
  }
  try{
    await api.post('/Card/debit-card', payload)
    await loadAccounts();
    alert('Debit card has been created')
  } catch (err) {
      alert(err.message)
  }
}
</script>

<template>
  <div class="topbar">
    <ul>
      <li>
        <a style="color: black">
          <button @click="logOut">
            <img style="width: 2.5vw; height: auto" src="../../assets/img/logout.png">
          </button>
        </a>
      </li>
      <li>
        <a href="/customer/profile" style="color: black">
          <img style="width: 2.5vw; height: auto" src="../../assets/img/profile.png">
        </a>
      </li>
    </ul>
  </div>

  <div class="wrapper">
    <div class="container">
      <div class="column flex-1">
        <div class="card ads" style="height: 140px" title="Project Title">Project Title (To be added)</div>
        <div class="card ads" style="height: 530px" title="Project Info">Project Info (To be added)</div>
        <div class="card blank" style="height: 100px">Blank</div>
      </div>

      <div class="column flex-2" >
        <div class="card" style="height: 600px; background-color: transparent; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="accounts">
          <div class="account" >
            <div style="display: flex; flex-direction: row; width: 100%; justify-content: space-between; padding-left: 50px; padding-right: 50px">
              <a class="title" style="margin-top: 10px">Accounts</a>
              <div style="display: flex; flex-direction: row">
                <button :disabled="!selectedAccount" @click="deleteAccount">
                  <img  style="height: 35px; width: auto; margin-left: 20px; object-fit: contain; aspect-ratio: 1/1" src="../../assets/img/trash.png">
                </button>

                <a href="/customer/create-account">
                  <img  style="height: 50px; width: auto; margin-left: 20px; object-fit: contain; aspect-ratio: 1/1" src="../../assets/img/plus.png">
                </a>
              </div>
            </div>

            <ul>
              <li v-for="account in accounts.slice(0,3)" :key="account.id"
                  :class="selectedAccountId === account.id.toString() ? 'selected-account' : 'unselected-account'"
                  @click="selectedAccountId = account.id.toString()">
                <a style="font-size: 20px">{{account.currencySymbol + account.balance}}</a>
                <a style="font-weight: normal">{{account.currencyType}} Account - {{account.id.toString().slice(0,3) + '-' + account.id.toString().slice(3)}}</a>
                <div class="bankcard-preview-wrapper bankcard-preview-scroll">
                  <a class="bankcard-preview" v-for="card in account.debitCards" :key="card.id">{{card.cardNumber.slice(-5)}}</a>
                </div>
              </li>
            </ul>
          </div>

          <div class="all-transactions-button">
            <a style="font-weight: bold" href="/customer/accounts">All Accounts</a>
          </div>
        </div>
        <div class="card" style="height: 240px; padding: 0px; background-color: transparent; " title="actions">
          <div class="actions">
            <router-link class="button" to="/customer/transfer-money">
              <button class="button">Transfer Money</button>
            </router-link>
            <router-link class="button" to="/customer/deposit-withdraw">
              <button class="button">Deposit / Withdraw</button>
            </router-link>
            <router-link class="button" to="/customer/cards">
              <button class="button">Cards</button>
            </router-link>
            <router-link class="button" to="/customer/currency-dashboard">
              <button class="button">Currencies</button>
            </router-link>
          </div>
        </div>
      </div>

      <div class="column flex-3">
        <div class="card ads" style="height: 140px">(To be added)</div>
        <div class="card blank">Blank</div>
        <div class="card" style="height: 200px" title="quick actions">
          <div style="margin-bottom: 15px">
            <a class="title">Quick Actions</a>
          </div>
          <div class="quick-actions">
            <div class="quick-action-item" title="quick action 1">
              <div class="quick-action-image-wrapper">
                <img @click="quickaction_deposit" class="quick-action-image" src="../../assets/img/deposit-quickaction.jpg">
              </div>
              <a class="quick-action-text">Deposit 1000</a>
            </div>
            <div class="quick-action-item" title="quick action 2">
              <div class="quick-action-image-wrapper">
                <img @click="quickaction_withdraw" class="quick-action-image" src="../../assets/img/withdraw-quickaction.png">
              </div>
              <a class="quick-action-text">Withdraw 500</a>
            </div>
            <div class="quick-action-item" title="quick action 3">
              <div class="quick-action-image-wrapper">
                <img @click="quickaction_createcard" class="quick-action-image" src="../../assets/img/createcard-quickaction.png">
              </div>
              <a class="quick-action-text">Create Debit Card</a>
            </div>
          </div>
        </div>
        <div class="card" style="height: 400px;">
            <BalanceLineChart/>
        </div>
      </div>

      <div class="column flex-4">
        <div class="card" style="height: 160px; display: flex; flex-direction: column; justify-content: center" title="Total Balance">
          <div style="display: flex; justify-content: space-between">
            <label class="title" style="font-weight: bold">Total Balance (in TRY)</label>
            <label class="title" style="font-weight: bold">₺{{totalBalance}}</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title">Total TRY</label>
            <label class="title" style="font-weight: bold">₺{{totalCurrenciesBalance.TRY}}</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title">Total USD</label>
            <label class="title" style="font-weight: bold">${{totalCurrenciesBalance.USD}}</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title">Total EUR</label>
            <label class="title" style="font-weight: bold">€{{totalCurrenciesBalance.EUR}}</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title">Total Virtual Card Limits</label>
            <label class="title" style="font-weight: bold">₺{{ totalVirtualCardLimits }}</label>
          </div>
        </div>
        <div class="card" style="height: 110px;">
          <div style="display: flex; justify-content: space-between">
            <label class="title" style="color: dimgray">USD ($)</label>
            <label class="title" style="font-weight: bold; color: dimgray">₺35</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title" style="color: dimgray">EUR (€)</label>
            <label class="title" style="font-weight: bold; color: dimgray">₺40</label>
          </div>
          <div style="display: flex; justify-content: space-between">
            <label class="title" style="color: dimgray">GBP (£)</label>
            <label class="title" style="font-weight: bold; color: dimgray">(To be added)</label>
          </div>
        </div>
        <div class="card" style="height: 550px; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="Transaction Summary">
          <div>
            <div style="margin-bottom: 20px">
              <a class="title">Transaction Summary</a>
            </div>
            <div class="transaction-summary-item" v-for="transaction in transactions.slice(-4).reverse()" :key="transaction.id">
              <div style="display: flex; flex-direction: row; gap: 10px">
                <img v-if="transaction.type === 'Deposit'" class="image" src="../../assets/img/deposit.png">
                <img v-else-if="transaction.type === 'Withdraw'" class="image" src="../../assets/img/withdraw.png">
                <img v-else-if="transaction.type === 'Money Transfer'" class="image" src="../../assets/img/transfer-money.png">
                <img v-else-if="transaction.type === 'Virtual Card Money Transfer'" class="image" src="../../assets/img/card-money-transfer.png">
                <img v-else class="image" src="../../assets/img/question-sign.png">


                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold;">{{transaction.type}}</a>
                  <a style="color: gray" v-if="transaction.type === 'Virtual Card Money Transfer'">to {{transaction.targetType}} - {{transaction.targetId}}</a>
                  <a style="color: gray" v-else-if="transaction.type === 'Deposit' || transaction.type === 'Withdraw'">{{transaction.sourceType}} - {{transaction.sourceId}}</a>
                  <a style="color: gray" v-else-if="transaction.type === 'Money Transfer'">to {{transaction.targetType}} - {{transaction.targetId}}</a>
                </div>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a style="font-weight: bold">{{transaction.targetCurrencySymbol + transaction.amount}}</a>
                <a style="color: gray">{{transaction.timestamp.toString().split('T')[0]}}</a>
              </div>

            </div>
          </div>
          <div class="all-transactions-button">
            <a style="font-weight: bold" href="/customer/transactions">All Transactions</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.wrapper {
  margin-top: 8vh;
  width: 100%;
  height: 90vh;
  display: flex;
  justify-content: center;
  box-sizing: border-box;
}

.container {
  display: flex;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
}

.flex-1 { flex: 350; }
.flex-2 { flex: 430; }
.flex-3 { flex: 580; }
.flex-4 { flex: 410; }

@media (max-width: 1120px) {
  .container {
    flex-wrap: wrap;
  }

  .column {
    flex: 1 1 250px;
    margin-bottom: 20px;
  }

  .flex-1 { flex: 350 1 250px; }
  .flex-2 { flex: 430 1 250px; }
  .flex-3 { flex: 580 1 250px; }
  .flex-4 { flex: 410 1 250px; }
}

@media (min-width: 1121px) {
  .container {
    flex-wrap: nowrap;
  }
  .flex-1 { flex: 350; }
  .flex-2 { flex: 430; }
  .flex-3 { flex: 580; }
  .flex-4 { flex: 410; }
}

.column {
  margin-left: 10px;
  margin-right: 10px;
  display: flex;
  flex-direction: column;
  gap: 20px;
  min-width: 0;
  min-height: unset;
  flex-shrink: 1;
}

.card {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 23px;
  min-height: 40px;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  overflow: hidden;
}

.ads {
  background: #daaeee;
  background: radial-gradient(circle, rgba(218, 174, 238, 1) 0%, rgba(148, 187, 233, 1) 100%);
}

.blank {
  background-color: dimgray;
  visibility: hidden;
  height: 60px;
}

.transaction-summary-item {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  gap: 20px;
  margin-bottom: 20px;
}

.actions {
  height: 100%;
  width: 100%;
  border-radius: 20px;
  display: grid;
  grid-template-columns: 1fr 1fr;
  justify-content: center;
  align-items: center;
  gap: 20px;
  .button {
    height: 100%;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 20px;
    font-size: 20px;
    cursor: pointer;
    text-decoration: none;
    background-color: white;
  }
}

.quick-actions {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  gap: 10px;
}

.quick-action-item {
  width: fit-content;
  border-radius: 14px;
  padding: 2px;
  background-color: #336ff3;
  position: relative;
}

.quick-action-image {
  width: 170px;
  object-fit: fill;
  border-radius: 12px;
  filter: brightness(80%);
  cursor: pointer;
}

.quick-action-image-wrapper {
  padding: 2px;
  background-color: white;
  border-radius: 12px;
}

.quick-action-text {
  position: absolute;
  top: 70%;
  left: 40%;
  transform: translate(-50%, -50%);
  color: white;
  font-weight: bold;
  font-size: 17px;
  z-index: 2;
  pointer-events: none;
}

.image {
  width: 40px;
  height: 40px;
  object-fit: contain;
  aspect-ratio: 1 / 1;
  background-color: lightgray;
  padding: 6px;
  border-radius: 10px;
}

.account {
  z-index: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.account ul {
  display: flex;
  flex-direction: column-reverse;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 85%;
}

.account li {
  display: flex;
  flex-direction: column;
  padding-left: 10px;
  align-items: flex-start;
  justify-content: center;
  height: 120px;
  border-radius: 15px;
  margin-top: 20px;
  cursor: pointer;
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
}

.account li.selected-account {
  background-color: lightgray;
}
.bankcard-preview-wrapper {
  overflow-x: auto;
  padding-bottom: 5px;
  width: 100%;
}

.bankcard-preview {
  background-image: linear-gradient(to right top, #0e4daa, #645fbb, #9674ca, #c38bd9, #eba4e8);
  display: flex;
  align-items: flex-end;
  padding-left: 3px;
  padding-bottom: 2px;
  justify-content: flex-start;
  min-width: 60px;
  height: 40px;
  margin-right: 5px;
  color: white;
  font-weight: bold;
  border-radius: 5px;
}

.bankcard-preview-scroll {
  display: flex;
  overflow-x: auto;
  scrollbar-width: thin;
  scrollbar-color: gray transparent;
}

.title {
  color: black;
  font-size: 20px;
}

.all-transactions-button {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 40px;
  border-radius: 15px;
  background-color: rgba(236,238,253,0.4);
}

.topbar{
  position: fixed;
  display: flex;
  flex-direction: row-reverse;
  top: 0;
  left:0;
  width: 100vw;
  height: 6vh;
  background: white;
  z-index: 0;
  overflow: hidden;
}

.topbar ul {
  display: flex;
  flex-direction: row-reverse;
  list-style: none;
  padding-right: 30px;
  align-items: center;
}

.topbar li {
  margin-left: 30px;
  max-width: 100%;
  overflow: hidden;
}

.topbar a {
  display: flex;
  font-size: 1.2vw; /* Ekran boyutuna göre küçülebilir */
}
</style>