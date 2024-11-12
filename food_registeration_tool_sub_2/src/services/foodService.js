// src/services/foodService.js
import axios from 'axios';

const API_URL = 'https://localhost:5001/api/foods'; // Bytt til din backend-URL

export const getAllFoods = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Feil ved henting av matvarer:', error);
    throw error;
  }
};

export const addFood = async (food) => {
  try {
    const response = await axios.post(API_URL, food);
    return response.data;
  } catch (error) {
    console.error('Feil ved oppretting av mat:', error);
    throw error;
  }
};

// Du kan også lage oppdatering og sletting
export const deleteFood = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error('Feil ved sletting av mat:', error);
    throw error;
  }
};
