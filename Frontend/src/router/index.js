import {createRouter, createWebHistory} from "vue-router";
import LoginPage from '../components/LoginPage.vue';
import RegisterPage from '../components/RegisterPage.vue';
import HomePage from "../components/HomePage.vue";
import AdminDashboardPage from "../components/admin/adminDashboardPage.vue";
import DeleteAccountPage from "../components/admin/DeleteAccountPage.vue";
import DeleteUserPage from "../components/admin/DeleteUserPage.vue";
import ListAllAccountsPage from "../components/admin/ListAllAccountsPage.vue";
import ListAllTransactionsPage from "../components/admin/ListAllTransactionsPage.vue";
import ListAllUsersPage from "../components/admin/ListAllUsersPage.vue";
import CustomerDashboardPage from "../components/customer/customerDashboardPage.vue";
import ProfilePage from "../components/customer/ProfilePage.vue";
import CreateAccountPage from "../components/customer/CreateAccountPage.vue";
import TransferMoneyPage from "../components/customer/TransferMoneyPage.vue";
import DepositWithdrawPage from "../components/customer/DepositWithdrawPage.vue";
import TransactionHistoryPage from "../components/customer/TransactionHistoryPage.vue";
import CardsPage from "../components/customer/CardsPage.vue";
import ListAllCardsPage from "../components/admin/ListAllCardsPage.vue";
import CurrencyDashboardPage from "../components/admin/CurrencyDashboardPage.vue";

const routes = [
    {path: '/', component: HomePage},
    {path: '/login', component: LoginPage},
    {path: '/register', component: RegisterPage},
    {path: '/admin/dashboard', component: AdminDashboardPage},
    {path: '/admin/delete-account', component: DeleteAccountPage},
    {path: '/admin/delete-user', component: DeleteUserPage},
    {path: '/admin/list-all-accounts', component: ListAllAccountsPage},
    {path: '/admin/list-all-cards', component: ListAllCardsPage},
    {path: '/admin/list-all-transactions', component: ListAllTransactionsPage},
    {path: '/admin/list-all-users', component: ListAllUsersPage},
    {path: '/admin/currency-dashboard', component: CurrencyDashboardPage},
    {path: '/customer/dashboard', component: CustomerDashboardPage},
    {path: '/customer/profile', component: ProfilePage},
    {path: '/customer/cards', component: CardsPage},
    {path: '/customer/create-account', component: CreateAccountPage},
    {path: '/customer/transfer-money', component: TransferMoneyPage},
    {path: '/customer/deposit-withdraw', component: DepositWithdrawPage},
    {path: '/customer/transaction-history', component: TransactionHistoryPage}
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router