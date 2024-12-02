import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5072/foodapi', // Point to Sub1's API
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;
