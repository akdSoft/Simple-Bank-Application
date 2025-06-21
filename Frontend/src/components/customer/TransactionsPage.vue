<script setup>
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'
import CreateVirtualCardModal from "./ModalsAndComponents/CreateVirtualCardModal.vue";
import VirtualCardMoneyTransferModal from "./ModalsAndComponents/VirtualCardMoneyTransferModal.vue";


const transactions = ref([])
async function showTransactionList(){
  try {
    const response = await api.get(`/Transaction/account/${0}`)
    transactions.value = response.data
  } catch (err) {
    alert(err.message)
  }
}



const router = useRouter()

onMounted(async () => {
  await showTransactionList();
})


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

      <div class="column flex-2">
        <div class="card transaction-item-wrapper" style="height: 860px; background-color: transparent; display: flex; flex-direction: column; justify-content: space-between; overflow-y: auto" title="accounts">
          <div class="transaction-item-container">
            <div>
              <a class="title">Transactions</a>
            </div>

            <div class="transaction-item" v-for="transaction in transactions.reverse()" :key="transaction.id">
              <div style="display: flex; align-items: center; gap: 10px">
                <img v-if="transaction.type === 'Deposit'" class="image" src="../../assets/img/deposit.png">
                <img v-else-if="transaction.type === 'Withdraw'" class="image" src="../../assets/img/withdraw.png">
                <img v-else-if="transaction.type === 'Money Transfer'" class="image" src="../../assets/img/transfer-money.png">
                <img v-else-if="transaction.type === 'Virtual Card Money Transfer'" class="image" src="../../assets/img/card-money-transfer.png">
                <img v-else class="image" src="../../assets/img/question-sign.png">

                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold">{{ transaction.type }}</a>
                  <a style="color: gray">{{ 'from ' + transaction.sourceType + ' (' + transaction.sourceCurrencySymbol + ') - ' + transaction.sourceId.toString().slice(0, 3) + '-' + transaction.sourceId.toString().slice(3) }}</a>
                  <a v-if="transaction.targetType === 'Account'" style="color: gray">{{ 'to ' + transaction.targetType + ' (' + transaction.targetCurrencySymbol + ') - ' + transaction.targetId.toString().slice(0, 3) + '-' + transaction.targetId.toString().slice(3) }}</a>
                  <a v-else-if="transaction.targetType === 'VirtualCard' || transaction.targetType === 'DebitCard'" style="color: gray">{{ 'to ' + transaction.targetType + ' (' + transaction.targetCurrencySymbol + ') - ' + transaction.targetId }}</a>
                </div>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-start">
                <a style="color: gray">{{ 'Amount: ' + transaction.sourceCurrencySymbol + transaction.amount }}</a>
                <a style="color: gray">{{ 'Current Total Balance: ' + '₺' + transaction.currentBalance }}</a>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a>{{ transaction.userName + ' ' + transaction.userSurname }}</a>
                <a style="color: gray">{{ transaction.timestamp.split('.')[0].replace('T', ' ') }}</a>
              </div>
            </div>
          </div>
        </div>




      </div>

      <div class="column flex-3">
        <div class="card" style="height: 80px; display: flex; flex-direction: row; align-items: center; justify-content: space-between" title="Return">
          <a class="title">Return to the homepage</a>
          <a href="/customer/dashboard">
            <img  style="height: 50px; width: auto; margin-left: 20px; object-fit: contain; aspect-ratio: 1/1" src="../../assets/img/return.png">
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

.transaction-item-wrapper {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.transaction-item-container {
  display: flex;
  flex-direction: column;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 95%;
}

.transaction-item {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  height: 120px;
  padding-left: 10px;
  padding-right: 10px;
  border-radius: 15px;
  margin-top: 20px;
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
  font-size: 14px;
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

.image {
  width: 60px;
  height: 40px;
  object-fit: contain;
  aspect-ratio: 1 / 1;
  background-color: lightgray;
  padding: 6px;
  border-radius: 10px;
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
  font-size: 1.2vw; /* Ekran boyutuna göre küçülebilir */
}
</style>