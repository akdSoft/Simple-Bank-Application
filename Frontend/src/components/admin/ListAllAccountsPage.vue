<script setup>
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'

const accounts = ref([])
const selectedAccountId = ref('')
const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})
async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount')
    response.data.sort((a, b) => a.userId - b.userId)
    accounts.value = response.data
  } catch (err) {
    if(err.status === 401){
      alert("Unauthorized")
    } else {
      alert(err.message)
    }
  }
}

const router = useRouter()

onMounted(async () => {
  await loadAccounts()
})

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

async function logOut(){
  localStorage.removeItem('token')
  await router.push('/')
}
</script>

<template>
  <div class="topbar">
    <ul>
      <li>
        <a>
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
        <div class="card ads" style="height: 530px" title="Project Info">Project Info (To be added)</div>
        <div class="card blank" style="height: 100px">Blank</div>
      </div>

      <div class="column flex-2">
        <div class="card accounts-wrapper" title="accounts">
          <div class="accounts-container">
            <div class="accounts-header">
              <a class="title" style="margin-top: 10px">Accounts</a>
              <div style="display: flex; flex-direction: row">
                <button :disabled="!selectedAccount" @click="deleteAccount">
                  <img class="image" src="../../assets/img/trash.png">
                </button>
              </div>
            </div>

            <ul>
              <li v-for="account in accounts" :key="account.id"
                  :class="selectedAccountId === account.id.toString() ? 'selected-account' : ''"
                  @click="selectedAccountId = account.id.toString()">
                <div style="display: flex; flex-direction: row; align-items: center; gap: 15px">
                  <img class="image" v-if="account.accountType === 'SavingsAccount'" src="../../assets/img/savings-account.png">
                  <img class="image" v-else-if="account.accountType === 'CheckingAccount'" src="../../assets/img/checking-account.png">

                  <div style="display: flex; flex-direction: column">
                    <a >{{ account.userName + '' +  account.userSurname}}</a>
                    <a style="color: gray">{{ account.currencyType + ' - ' + account.accountType}}</a>
                    <div class="bankcard-preview-wrapper bankcard-preview-scroll">
                      <a class="bankcard-preview" v-for="card in account.debitCards" :key="card.id">{{card.cardNumber.slice(-5)}}</a>
                    </div>
                  </div>
                </div>
                <div style="display: flex; flex-direction: column; align-items: flex-end">
                  <a >Balance: {{ account.currencySymbol + account.balance }}</a>
                  <a style="color: gray">Debit Cards: {{ account.debitCards.length }}</a>
                  <a style="color: gray">ID: {{ account.id.toString().slice(0, 3) + '-' + account.id.toString().slice(3)}}</a>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <div class="column flex-3">
        <div class="card return" title="Return">
          <a class="title">Return to the homepage</a>
          <a href="/admin/dashboard">
            <img class="image" src="../../assets/img/return.png">
          </a>
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

@media (max-width: 1120px) {
  .container {
    flex-wrap: wrap;
  }

  .column {
    flex: 1 1 250px;
    margin-bottom: 20px;
  }

  .flex-1 { flex: 100 1 250px; }
  .flex-2 { flex: 310 1 250px; }
  .flex-3 { flex: 100 1 250px; }
}

@media (min-width: 1121px) {
  .container {
    flex-wrap: nowrap;
  }
  .flex-1 { flex: 100; }
  .flex-2 { flex: 310; }
  .flex-3 { flex: 100; }
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

.accounts-wrapper {
  height: 860px;
  background-color: transparent;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  overflow-y: auto
}

.accounts-container {
  z-index: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.accounts-header {
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: space-between;
  padding-left: 50px;
  padding-right: 50px;
  .image {
    height: 35px;
    width: auto;
    margin-left: 20px;
    object-fit: contain;
    aspect-ratio: 1/1
  }
}

.accounts-container ul {
  display: flex;
  flex-direction: column-reverse;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 85%;
}

.accounts-container li {
  display: flex;
  flex-direction: row;
  padding-left: 10px;
  padding-right: 10px;
  align-items: center;
  justify-content: space-between;
  height: 120px;
  border-radius: 15px;
  margin-top: 20px;
  cursor: pointer;
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
  .image {
    width: 70px;
    height: 70px;
  }
}


.accounts-container li.selected-account {
  background-color: lightgray;
}

.bankcard-preview-wrapper {
  overflow-x: auto;
  padding-bottom: 5px;
  width: 350px;
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

.return {
  height: 80px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  .image {
    height: 50px;
    width: auto;
    margin-left: 20px;
    object-fit: contain;
    aspect-ratio: 1/1;
  }
}

.title {
  color: black;
  font-size: 20px;
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
  font-size: 1.2vw;
}
</style>