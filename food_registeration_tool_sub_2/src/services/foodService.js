// services/foodService.js
import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://your-backend-api-url/api',
  headers: { 'Content-Type': 'application/json' },
});
// src/services/foodService.js
export const getFoods = async () => {
    try {
      const response = await fetch('your-api-endpoint'); // Replace with your actual API URL
      const data = await response.json();
      return data; // Assuming data is an array of food items
    } catch (error) {
      console.error('Error fetching foods:', error);
      return []; // Return an empty array on error
    }
  };
  