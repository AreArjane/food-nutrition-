import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5072/foodapi', // Your backend URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Fetch all food items
export const getFoodItems = async () => {
  try {
    const response = await apiClient.get('/Foods');  // Use apiClient
    return response.data;
  } catch (error) {
    console.error("Error fetching food items:", error);
    throw error;
  }
};

// Create a new food item
export const createFoodItem = async (foodItem) => {
  try {
    const response = await apiClient.post('/Foods', foodItem);  // Use apiClient
    return response.data;
  } catch (error) {
    console.error("Error creating food item:", error);
    throw error;
  }
};

// Update an existing food item
export const updateFoodItem = async (id, foodItem) => {
  try {
    const response = await apiClient.put(/Foods/${id}, foodItem);  // Use apiClient
    return response.data;
  } catch (error) {
    console.error("Error updating food item:", error);
    throw error;
  }
};

// Delete a food item
export const deleteFoodItem = async (id) => {
  try {
    await apiClient.delete(/Foods/${id});  // Use apiClient
  } catch (error) {
    console.error("Error deleting food item:", error);
    throw error;
  }
};
