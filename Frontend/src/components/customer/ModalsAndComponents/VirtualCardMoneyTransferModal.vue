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

</style>