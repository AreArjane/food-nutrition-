// services/foodService.js
import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://your-backend-api-url/api', // Ensure this is the correct URL
  headers: { 'Content-Type': 'application/json' },
});

// Function to delete food item by ID
export const deleteFood = async (id) => {
  try {
    await apiClient.delete(`/foods/${id}`); // Replace with your actual API endpoint for deleting a food item
    console.log('Food deleted successfully');
  } catch (error) {
    console.error('Error deleting food:', error);
    throw error; // Rethrow error to be handled in the component
  }
};
