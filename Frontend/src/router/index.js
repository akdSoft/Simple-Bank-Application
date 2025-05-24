import {createRouter, createWebHistory} from "vue-router";
import Login from '../components/Login.vue';
import Register from '../components/Register.vue';
import HomePage from "../components/HomePage.vue";
import adminDashboard from "../components/admin/adminDashboard.vue";
import DeleteAccount from "../components/admin/DeleteAccount.vue";
import DeleteUser from "../components/admin/DeleteUser.vue";
import ListAllAccounts from "../components/admin/ListAllAccounts.vue";
import ListAllTransactions from "../components/admin/ListAllTransactions.vue";
import ListAllUsers from "../components/admin/ListAllUsers.vue";
import customerDashboard from "../components/customer/customerDashboard.vue";
import Profile from "../components/customer/Profile.vue";
import CreateAccount from "../components/customer/CreateAccount.vue";
import TransferMoney from "../components/customer/TransferMoney.vue";
import DepositWithdraw from "../components/customer/DepositWithdraw.vue";
import TransactionHistory from "../components/customer/TransactionHistory.vue";

const routes = [
    {path: '/', component: HomePage},
    {path: '/login', component: Login},
    {path: '/register', component: Register},
    {path: '/admin/dashboard', component: adminDashboard},
    {path: '/admin/delete-account', component: DeleteAccount},
    {path: '/admin/delete-user', component: DeleteUser},
    {path: '/admin/list-all-accounts', component: ListAllAccounts},
    {path: '/admin/list-all-transactions', component: ListAllTransactions},
    {path: '/admin/list-all-users', component: ListAllUsers},
    {path: '/customer/dashboard', component: customerDashboard},
    {path: '/customer/profile', component: Profile},
    {path: '/customer/create-account', component: CreateAccount},
    {path: '/customer/transfer-money', component: TransferMoney},
    {path: '/customer/deposit-withdraw', component: DepositWithdraw},
    {path: '/customer/transaction-history', component: TransactionHistory}
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router