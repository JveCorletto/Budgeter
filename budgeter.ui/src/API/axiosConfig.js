import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7136/API/", // Cambia esto por la URL de tu backend
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosInstance;