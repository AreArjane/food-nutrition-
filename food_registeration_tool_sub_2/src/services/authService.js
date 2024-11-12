// src/services/authService.js
import axios from 'axios';

const API_URL = '/api/auth/';

const register = async (username, password) => {
  try {
    const response = await axios.post(`${API_URL}register`, { username, password });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data.message || 'Registration failed');
  }
};

const login = async (username, password) => {
  try {
    const response = await axios.post(`${API_URL}login`, { username, password });
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
    }
    return response.data;
  } catch (error) {
    throw new Error(error.response.data.message || 'Login failed');
  }
};
const authService = {
  register,
  login,
  logout,
};

export default authService;