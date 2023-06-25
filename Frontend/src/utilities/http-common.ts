import axios from "axios";

export const apiClient = axios.create({
  baseURL: "https://localhost:44312/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export const setAuthToken = (token: any) => {
  if (token) {
    apiClient.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  } else {
    delete apiClient.defaults.headers.common["Authorization"];
  }
};
