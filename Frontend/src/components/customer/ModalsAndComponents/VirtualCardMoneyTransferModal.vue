<script setup>
import {onMounted, ref} from "vue";
import axios from "axios";

const amount = ref('')
const accounts = ref([])
const selectedAccountId = ref('')
const virtualCards = ref([])
const selectedCardId = ref('')

const transferType = ref('')

onMounted(async () => {
  await  loadAccounts();
  await loadCards();
})

async function loadAccounts(){
  try{
    const response = await axios.get('http://localhost:5280/api/BankAccount/user', {withCredentials: true},)
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

async function loadCards(){
  try{
    const response = await axios.get('http://localhost:5280/api/Card/virtual-cards/user', {withCredentials: true},)
    virtualCards.value = response.data

  } catch (err) {
    alert(err.message)
  }
}

async function transferFromAccountToCard(){
  const payload = {
    fromAccountOrCardId: selectedAccountId.value,
    targetAccountOrCardId: selectedCardId.value,
    amount: amount.value
  }
  try{
    const response = await axios.post('http://localhost:5280/api/Card/virtual-card/transfer-from-account', payload, {withCredentials: true})

    selectedAccountId.value = '';
    selectedCardId.value = '';
    amount.value = '';

    if (response.status === 200 && response.data === true){
      alert('money transferred')
    } else {
      alert('unexpected response: ' + response.status)
    }
  } catch (err) {
    alert(err.message)
  }
}

async function transferFromCardToAccount(){
  const payload2 = {
    fromAccountOrCardId: selectedCardId.value,
    targetAccountOrCardId: selectedAccountId.value,
    amount: amount.value
  }
  try{
    const response2 = await axios.post('http://localhost:5280/api/Card/virtual-card/transfer-from-card', payload2, {withCredentials: true})

    selectedAccountId.value = '';
    selectedCardId.value = '';
    amount.value = '';

    if (response2.status === 200 && response2.data === true){
      alert('money transferred')
    } else {
      alert('unexpected response: ' + response2.status)
    }
  } catch (err) {
    alert(err.message)
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
        <option class="select-items" value="">--Choose an Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}}
        </option>
      </select>

      <label>Choose a virtual card</label>

      <select class="select"  id="card" v-model="selectedCardId">
        <option class="select-items" value="">--Choose a virtual card--</option>
        <option class="select-items" v-for="card in virtualCards" :key="card.id" :value="card.id">
          {{card.cardNumber}}
        </option>
      </select>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter amount:</label>
        <input class="input" type="number" v-model="amount">
      </div>

      <div style="margin: 10px">
        <button v-if="transferType === 'fromAccount'" @click="transferFromAccountToCard">Transfer</button>
        <button v-else-if="transferType === 'fromCard'" @click="transferFromCardToAccount">Transfer</button>
        <button v-else @click="alert('Choose transfer type')">Transfer</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>

</style>