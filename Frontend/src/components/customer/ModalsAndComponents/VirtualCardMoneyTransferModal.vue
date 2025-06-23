<script setup>
import {computed, onMounted, ref} from "vue";
import api from '../../../api/axiosInstance.js';

const amount = ref('')
const accounts = ref([])

const selectedAccountId = ref('')
const selectedAccount = computed(() => {
  if(!selectedAccountId.value) return 0
  return accounts.value.find(acc => acc.id === parseInt(selectedAccountId.value))
})

const virtualCards = ref([])

const selectedCardId = ref('')
const selectedCard = computed(() => {
  if(!selectedCardId.value) return 0
  return virtualCards.value.find(acc => acc.id === parseInt(selectedCardId.value))
})

const transferType = ref('')

onMounted(async () => {
  await  loadAccounts();
  await loadCards();
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount/user')
    accounts.value = response.data.filter(account => account.currencyType === 'TRY')
  } catch (err) {
    alert(err.message)
  }
}

async function loadCards(){
  try{
    const response = await api.get('/Card/virtual-cards/user')
    virtualCards.value = response.data

  } catch (err) {
    alert(err.message)
  }
}

async function transferFromAccountToCard(){
  if(!selectedAccount.value || !selectedCard.value || !amount.value){
    alert("All fields must be selected and completed correctly")
    return
  }

  const payload = {
    fromAccountOrCardId: selectedAccountId.value,
    targetAccountOrCardId: selectedCardId.value,
    amount: amount.value
  }
  try{
    const response = await api.post('/transaction/transfer/account-to-virtualcard', payload)

    selectedAccountId.value = '';
    selectedCardId.value = '';
    amount.value = '';

    alert('Money has been transferred')
  } catch (err) {
    if(err.status === 400){
      alert("All fields must be selected and completed correctly")
    }
  }
}

async function transferFromCardToAccount(){
  if(!selectedAccount.value || !selectedCard.value || !amount.value){
    alert("All fields must be selected and completed correctly")
    return
  }

  const payload2 = {
    fromAccountOrCardId: selectedCardId.value,
    targetAccountOrCardId: selectedAccountId.value,
    amount: amount.value
  }
  try{
    const response2 = await api.post('/transaction/transfer/virtualcard-to-account', payload2)

    selectedAccountId.value = '';
    selectedCardId.value = '';
    amount.value = '';

    alert('Money has been transferred')
  } catch (err) {
    if(err.status === 400){
      alert("All fields must be selected and completed correctly")
    }
  }
}

function handleTransfer(){
  if(transferType.value === 'fromAccount') {
    transferFromAccountToCard()
  } else if (transferType.value === 'fromCard') {
    transferFromCardToAccount()
  } else {
    alert("All fields must be selected and completed correctly")
  }
}
</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')">
    <div class="modal-content">
      <h2 class="dashboard-title">Transfer</h2>

      <div>
        <input type="radio" id="fromAccount" value="fromAccount" v-model="transferType">
        <label for="fromAccount">From Account to Card</label>
      </div>

      <div>
        <input type="radio" id="fromCard" value="fromCard" v-model="transferType">
        <label for="fromCard">From Card to Account</label>
      </div>

      <label>Choose an account</label>

      <select class="select"  id="account" v-model="selectedAccountId">
        <option class="select-items" value="">--Choose a TRY Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}} - {{account.currencyType}} Account
        </option>
      </select>

      <div>
        <label style="margin-right: 10px">Balance:</label>
        <input class="input" v-if="selectedAccount" :value="selectedAccount.balance + ' ' + selectedAccount.currencySymbol" readonly>
        <input class="input" v-else readonly>
      </div>

      <label>Choose a virtual card</label>

      <select class="select"  id="card" v-model="selectedCardId">
        <option class="select-items" value="">--Choose a virtual card--</option>
        <option class="select-items" v-for="card in virtualCards" :key="card.id" :value="card.id">
          {{card.cardNumber}}
        </option>
      </select>

      <div>
        <label style="margin-right: 10px">Available Limit:</label>
        <input class="input" :value="selectedCard.availableLimit" readonly>
      </div>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter amount:</label>
        <input class="input" type="number" v-model="amount">
      </div>

      <div style="margin: 10px">
        <button @click="handleTransfer">Transfer</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>
.dashboard-wrapper{
  display: flex;
  justify-content: center;
  align-items: center;
  /*width: 100%;*/
  /*height: 100vh;*/
}

.dashboard-card {
  background-color: #1e1e1e;
  color: white;
  padding: 2rem;
  border-radius: 1rem;
  width: 100%;
  max-width: 500px;
  text-align: center;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

.dashboard-title {
  font-weight: bold;
  margin-bottom: 1rem;
  text-transform: uppercase;
  font-size: 24px;
}

.dashboard-button {
  background: transparent;
  color: white;
  border: 1px solid white;
  font-size: 1rem;
  padding: 0.75rem 1.5rem;
  margin: 0.5rem 0;
  width: 100%;
  cursor: pointer;
  transition: 0.3s ease-in-out;
  border-radius: 0.5rem;
}

.dashboard-button:hover {
  background-color: white;
  color: #1e1e1e;
}

.input {
  border: 1px solid white;
  padding: 10px;
  margin: 0.2rem 0;
  border-radius: 10px;
  background-color: #3b3b3b;
  font-size: small;
  font-weight: bold;
  text-align: center;
}

.select {
  background: transparent;
  border: 1px solid white;
  font-size: 1rem;
  padding: 0.75rem 1.5rem;
  margin: 0.5rem 0;
  width: 100%;
  cursor: pointer;
  transition: 0.3s ease-in-out;
  border-radius: 0.5rem;
}

.select-items {
  background-color: #212121;
  top: 100%;
  left: 0;
  right: 0;
  z-index: 99;
}

.debit-or-virtual-card {
  background-color: blue;
  color: white;
  padding: 2rem;
  border-radius: 1rem;
  max-height: 120px;
  min-width: 250px;
  text-align: center;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
  margin: 0 auto;
}
.debit-or-virtual-card-number {
  top:100px;
  left:20px;
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  word-spacing: 1px;
}
.debit-or-virtual-card-name {
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  left: 20px;
  top:130px
}
.debit-or-virtual-card-expdate {
  font-family: 'Courier New', Courier, monospace;
  top:130px;
  left:160px
}
.debit-or-virtual-card-cvv {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  top:135px;
  left:120px;
  font-size: 17px;
  color:white;
}
.debit-or-virtual-card-type {
  top:100px;
  left:20px;
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  word-spacing: 1px;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0,0,0,0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background-color: #6c6868;
  padding: 2rem;
  border-radius: 1rem;
  max-width: 500px;
  width: 90%;
}

.custom-table {
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
  text-align: center;
  font-size: 14px;
}

.custom-table-h {
  border: 1px solid #ccc;
  padding: 8px;
}
</style>