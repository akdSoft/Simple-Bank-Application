import axios from "axios";

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5280/api',
    withCredentials: true
})

export default axiosInstance