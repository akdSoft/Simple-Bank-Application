<script setup>
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'
import CreateDebitCardModal from "./ModalsAndComponents/CreateDebitCardModal.vue";
import CreateVirtualCardModal from "./ModalsAndComponents/CreateVirtualCardModal.vue";
import VirtualCardMoneyTransferModal from "./ModalsAndComponents/VirtualCardMoneyTransferModal.vue";

const debitCards = ref([])
const virtualCards = ref([])

const showCreateDebitCardModal = ref(false)
const showCreateVirtualCardModal = ref(false)
const showVirtualCardMoneyTransferModal = ref(false)
const router = useRouter()

onMounted(async () => {
  await  loadCards();
})

async function loadCards(){
  try{
    const response = await api.get('/Card/debit-cards/user')
    debitCards.value = response.data

    const response2 = await api.get('/Card/virtual-cards/user')
    virtualCards.value = response2.data

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
        <a>
          <button @click="logOut">
            <img style="width: 2.5vw; height: auto" src="../../assets/img/logout.png">
          </button>
        </a>
      </li>
      <li>
        <a href="/customer/profile">
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
        <div class="card blank" style="height: 100px"></div>
      </div>

      <div class="column flex-2">
        <div class="card bankcards-wrapper" style="height: 860px;" title="Cards">
          <div class="bankcard-item-container">
            <div class="bankcards-header">
              <a class="title">Debit Cards</a>
              <a>
                <button @click="showCreateDebitCardModal = true">
                  <img class="image" src="../../assets/img/plus.png">
                </button>
                <CreateDebitCardModal v-if="showCreateDebitCardModal" @close="loadCards(); showCreateDebitCardModal = false"></CreateDebitCardModal>
              </a>
            </div>
            <div class="bankcard-item" v-for="card in debitCards" :key="card.id" style="gap: 30px">
              <div style="display: flex; align-items: center; gap: 10px;">
                <a class="bankcard-preview">{{card.cardNumber.slice(-5)}}</a>
                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold">{{card.cardholderNameAndSurname}}</a>
                  <a style="color: gray">{{ card.cardNumber }}</a>
                  <a style="color: gray">CVV {{ card.cvv }} / EXP {{card.expirationDate.slice(0,7)}}</a>
                </div>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a style="color: gray">Password: {{ card.password }}</a>
                <a style="color: gray">Online Shopping: {{ card.onlineShopping }}</a>
                <a style="color: gray">Linked Account: {{ card.linkedAccountId.toString().slice(0, 3) + '-' + card.linkedAccountId.toString().slice(3)}}</a>
              </div>
            </div>
          </div>
        </div>

      </div>

      <div class="column flex-3">
        <div class="card bankcard-item-wrapper" style="height: 860px; display: flex; flex-direction: column; background-color: transparent; justify-content: space-between; overflow-y: auto" title="Cards">
          <div class="bankcards-container">
            <div class="bankcards-header">
              <a class="title">Virtual Cards</a>
              <a>
                <button @click="showVirtualCardMoneyTransferModal = true">
                  <img  style="height: 50px; width: auto; margin-left: 20px; object-fit: contain; aspect-ratio: 1/1" src="../../assets/img/virtualcard-money-transfer.png">
                </button>
                <VirtualCardMoneyTransferModal v-if="showVirtualCardMoneyTransferModal" @close="loadCards(); showVirtualCardMoneyTransferModal = false"></VirtualCardMoneyTransferModal>

                <button @click="showCreateVirtualCardModal = true">
                  <img class="image" src="../../assets/img/plus.png">
                </button>
                <CreateVirtualCardModal v-if="showCreateVirtualCardModal" @close="loadCards(); showCreateVirtualCardModal = false"></CreateVirtualCardModal>
              </a>
            </div>

            <div class="bankcard-item" v-for="card in virtualCards" :key="card.id">
              <div style="display: flex; align-items: center; gap: 10px">
                <a class="bankcard-preview">{{card.cardNumber.slice(-5)}}</a>
                <div style="display: flex; flex-direction: column">
                  <a style="font-weight: bold">{{card.cardholderNameAndSurname}}</a>
                  <a style="color: gray">{{ card.cardNumber }}</a>
                  <a style="color: gray">CVV {{ card.cvv }} / EXP {{card.expirationDate.slice(0,7)}}</a>
                </div>
              </div>

              <div style="display: flex; flex-direction: column; align-items: flex-end">
                <a style="color: gray">Limit: ₺{{ card.availableLimit }}</a>
                <a style="color: gray">Online Shopping: {{ card.onlineShopping }}</a>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="column flex-4">
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

  .flex-1 { flex: 350 1 250px; }
  .flex-2 { flex: 535 1 250px; }
  .flex-3 { flex: 535 1 250px; }
  .flex-4 { flex: 350 1 250px; }
}

@media (min-width: 1121px) {
  .container {
    flex-wrap: nowrap;
  }
  .flex-1 { flex: 350; }
  .flex-2 { flex: 535; }
  .flex-3 { flex: 535; }
  .flex-4 { flex: 350; }
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

.bankcards-wrapper {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  background-color: transparent;
  overflow-y: auto
}

.bankcards-container {
  display: flex;
  flex-direction: column;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 95%;
}

.bankcards-header {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  .image {
    height: 50px;
    width: auto;
    margin-left: 20px;
    object-fit: contain;
    aspect-ratio: 1/1
  }
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
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
  font-size: 14px;
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

.bankcard-preview {
  background-image: linear-gradient(to right top, #0e4daa, #645fbb, #9674ca, #c38bd9, #eba4e8);
  display: flex;
  align-items: flex-end;
  padding-left: 3px;
  padding-bottom: 2px;
  justify-content: flex-start;
  min-width: 84px;
  height: 56px;
  margin-right: 5px;
  color: white;
  font-weight: bold;
  border-radius: 5px;
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