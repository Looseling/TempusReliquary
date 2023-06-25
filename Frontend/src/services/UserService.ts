import axios, { AxiosResponse } from "axios";

const API_URL = "https://localhost:44312/api";

export const register = async (
  username: string,
  email: string,
  password: string
): Promise<any> => {
  const res: AxiosResponse<any> = await axios.post(`${API_URL}/register`, {
    username,
    email,
    password,
  });
  return res.data;
};

export const login = async (email: string, password: string): Promise<any> => {
  const res: AxiosResponse<any> = await axios.post(`${API_URL}/login`, {
    email,
    password,
  });
  return res.data;
};
