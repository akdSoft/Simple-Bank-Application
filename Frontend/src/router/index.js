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
import {jwtDecode} from "jwt-decode";

const routes = [
    {path: '/', component: HomePage},
    {path: '/login', component: LoginPage},
    {path: '/register', component: RegisterPage},
    {path: '/admin/dashboard', component: AdminDashboardPage, meta: { requiresAuth: true}},
    {path: '/admin/delete-account', component: DeleteAccountPage, meta: { requiresAuth: true }},
    {path: '/admin/delete-user', component: DeleteUserPage, meta: { requiresAuth: true }},
    {path: '/admin/list-all-accounts', component: ListAllAccountsPage, meta: { requiresAuth: true }},
    {path: '/admin/list-all-cards', component: ListAllCardsPage, meta: { requiresAuth: true }},
    {path: '/admin/list-all-transactions', component: ListAllTransactionsPage, meta: { requiresAuth: true }},
    {path: '/admin/list-all-users', component: ListAllUsersPage, meta: { requiresAuth: true }},
    {path: '/admin/currency-dashboard', component: CurrencyDashboardPage, meta: { requiresAuth: true }},
    {path: '/customer/dashboard', component: CustomerDashboardPage, meta: { requiresAuth: true }},
    {path: '/customer/profile', component: ProfilePage, meta: { requiresAuth: true }},
    {path: '/customer/cards', component: CardsPage, meta: { requiresAuth: true }},
    {path: '/customer/create-account', component: CreateAccountPage, meta: { requiresAuth: true }},
    {path: '/customer/transfer-money', component: TransferMoneyPage, meta: { requiresAuth: true }},
    {path: '/customer/deposit-withdraw', component: DepositWithdrawPage, meta: { requiresAuth: true }},
    {path: '/customer/transaction-history', component: TransactionHistoryPage, meta: { requiresAuth: true }}
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token')

    if (to.meta.requiresAuth) {
        if (!token) {
            return next('/login')
        }

        try {
            const decodedToken = jwtDecode(token)

            const role = decodedToken.role
            const isAdminRoute = to.path.startsWith('/admin')
            const isCustomerRoute = to.path.startsWith('/customer')

            if(isAdminRoute && role !== 'admin') {
                return next('/login')
            }

            if(isCustomerRoute && role !== 'customer') {
                return next('login')
            }

            return next()
        } catch (err) {
            alert('Invalid token: ' + err)
        }
    }
    return next()
})

export default router