import axios from "axios";

export const httpClient = axios.create({
    headers: {
        "Content-type": "application/json"
    },
    baseURL: "https://localhost:5001/api"
})