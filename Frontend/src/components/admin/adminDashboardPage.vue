<script setup>

import {useRouter} from "vue-router";
import {computed, onMounted, ref} from "vue";
import api from "../../api/axiosInstance.js";

const router = useRouter()
const accounts = ref([])
const cards = ref([])
const users = ref([])
const transactions = ref([])

async function showTransactionList(){
  try {
    const response = await api.get(`/Transaction`)
    transactions.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

onMounted(async () => {
  await loadAccounts();
  await loadCards();
  await loadUsers();
  await showTransactionList();
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount')
    accounts.value = response.data
  } catch (err) {
    if(err.status === 401){
      alert("Unauthorized")
    } else {
      alert(err.message)
    }
  }
}

async function loadCards() {
  try {
    const response = await api.get('/Card/debit-cards')
    const response2 = await api.get('/Card/virtual-cards')

    cards.value = response.data.concat(response2.data)

  } catch (err) {
    alert(err.message)
  }
}

async function loadUsers() {
  try {
    const response = await api.get('/User')
    users.value = response.data

  } catch (err) {
    alert(err.message)
  }
}

async function logOut(){
  localStorage.removeItem('token')
  await router.push('/')
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
    </ul>
  </div>

  <div class="wrapper">
    <div class="container">
      <div class="column flex-1">
        <div class="card ads" style="height: 140px" title="Project Title">Project Title (To be added)</div>
        <div class="card ads" style="height: 520px" title="Project Info">Project Info (To be added)</div>
        <div class="card blank" style="height: 220px">Blank</div>
      </div>

      <div class="column flex-2">
        <div class="card" style="height: 800px; background-color: transparent; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="accounts">
          <div class="account">
            <div style="display: flex; flex-direction: row; width: 100%; justify-content: space-between; padding-left: 50px; padding-right: 50px">
              <a class="title" style="margin-top: 10px">Accounts</a>
            </div>

            <ul>
              <li v-for="account in accounts.slice(-3)" :key="account.id">
                <div style="display: flex; flex-direction: column; width: fit-content;">
                  <a style="font-size: 20px; width: fit-content">{{account.currencySymbol + account.balance}}</a>
                  <a style="font-weight: normal; width: fit-content;">{{account.currencyType}} Account - {{account.id}}</a>
                  <div class="bankcard-preview-wrapper bankcard-preview-scroll" style="width: 260px;">
                    <a class="bankcard-preview"  v-for="card in account.debitCards" :key="card.id">{{card.cardNumber.slice(-5)}}</a>
                  </div>
                </div>

                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold; white-space: pre-line">{{ account.userName }}</a>
                  <a style="font-weight: bold; white-space: pre-line">{{ account.userSurname }}</a>
                </div>
              </li>
            </ul>
          </div>
          <a href="/admin/list-all-accounts" style="font-weight: bold" class="show-all-button">All Acccounts</a>
        </div>
        <div class="card" style="height: 100px; padding: 0px; background-color: transparent; " title="actions">
          <div class="actions" >
            <router-link class="button" to="/admin/delete-user">
              <button class="button">Delete User</button>
            </router-link>
            <router-link class="button" to="/admin/delete-account">
              <button class="button">Delete Account</button>
            </router-link>

          </div>
        </div>
      </div>

      <div class="column flex-3">
        <div class="card ads" style="height: 140px">(To be added)</div>
        <div class="card blank">Blank</div>

        <div class="card bankcard-item-wrapper" style="height: 680px; display: flex; flex-direction: column; background-color: transparent; justify-content: space-between; overflow-y: auto" title="Cards">
          <div class="bankcard-item-container">
            <a class="title">All Cards</a>
            <div class="bankcard-item" v-for="card in cards.slice(-3)" :key="card.id">
                <div style="display: flex; align-items: center; gap: 10px">
                  <a class="bankcard-preview" style="min-width: 72px; height: 48px">{{card.cardNumber.slice(-5)}}</a>
                  <a style="font-weight: bold; white-space: pre-line">{{card.type.replace(" ", "\n")}}</a>
                </div>

                <a v-if="card.type === 'Virtual Card'" style="font-weight: bold; white-space: pre-line">Limit: ₺{{ card.availableLimit }}</a>

                <div>
                  <a style="font-weight: bold; white-space: pre-line">{{ card.cardholderNameAndSurname.replace(" ", "\n") }}</a>
                </div>
            </div>
          </div>
          <a href="/admin/list-all-cards" style="font-weight: bold" class="show-all-button">
            All Cards
          </a>
        </div>
      </div>

      <div class="column flex-4">
        <div class="card" style="height: 450px; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="Users">
          <div>
            <div style="margin-bottom: 20px">
              <a class="title">Users</a>
            </div>
            <div class="transaction-summary-item" v-for="user in users.slice(-4).reverse()" :key="user.id">
              <div style="display: flex; flex-direction: row; gap: 10px">
                <img  class="image" src="../../assets/img/user.png">

                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold;">{{user.name}}</a>
                  <a style="font-weight: bold;">{{user.surname}}</a>
                </div>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a >{{user.username}}</a>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a>Bank Accounts: {{user.bankAccounts.length}}</a>
              </div>

            </div>
          </div>

          <a href="/admin/list-all-users" style="font-weight: bold" class="show-all-button">
            All Users
          </a>
        </div>

        <div class="card" style="height: 450px; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="Transaction Summary">
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
          <a href="/admin/list-all-transactions" style="font-weight: bold" class="show-all-button">
            All Transactions
          </a>
        </div>
      </div>
    </div>
  </div>


  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">admin page</h2>

      <router-link to="/admin/list-all-users">
        <button class="dashboard-button">List All Users</button>
      </router-link>

      <router-link to="/admin/delete-user">
        <button class="dashboard-button">Delete User</button>
      </router-link>

      <router-link to="/admin/list-all-accounts">
        <button class="dashboard-button">List All Accounts</button>
      </router-link>

      <router-link to="/admin/delete-account">
        <button class="dashboard-button">Delete Account</button>
      </router-link>

      <router-link to="/admin/currency-dashboard">
        <button class="dashboard-button">Currency Dashboard</button>
      </router-link>

      <router-link to="/admin/list-all-cards">
        <button class="dashboard-button">List All Cards</button>
      </router-link>

      <router-link to="/admin/list-all-transactions">
        <button class="dashboard-button">List All Transactions</button>
      </router-link>


      <button class="dashboard-button" @click="logOut">Log Out</button>
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

@media (max-width: 1120px) {
  .container {
    flex-wrap: wrap;
  }

  .column {
    flex: 1 1 250px;
    margin-bottom: 20px;
  }

  .flex-1 { flex: 320 1 250px; }
  .flex-2 { flex: 430 1 250px; }
  .flex-3 { flex: 460 1 250px; }
  .flex-4 { flex: 470 1 250px; }
}

@media (min-width: 1121px) {
  .container {
    flex-wrap: nowrap;
  }
  .flex-1 { flex: 320; }
  .flex-2 { flex: 430; }
  .flex-3 { flex: 460; }
  .flex-4 { flex: 470; }
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
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  z-index: 0;
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

.bankcard-item-wrapper {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.bankcard-item-container {
  display: flex;
  flex-direction: column;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 75%;
}

.bankcard-item {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  height: 120px;
  padding-left: 10px;
  padding-right: 10px;
  border-radius: 15px;
  margin-top: 20px;
  cursor: pointer;
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
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
  flex-direction: row;
  padding-left: 10px;
  padding-right: 10px;
  align-items: center;
  justify-content: space-between;
  height: 120px;
  width: 100%;
  border-radius: 15px;
  margin-top: 20px;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
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

.show-all-button {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 40px;
  border-radius: 15px;
  background-color: #dbdbff;
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