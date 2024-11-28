import axios from "axios";

const API_URL = "http://lokalhost:5072/foodapi/Foods"; // Change to your backend URL

// Get all food items
export const getFoodItems = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching food items:", error);
    throw error;
  }
};

// Create a new food item
export const createFoodItem = async (foodItem) => {
  try {
    const response = await axios.post(API_URL, foodItem);
    return response.data;
  } catch (error) {
    console.error("Error creating food item:", error);
    throw error;
  }
};

// Update an existing food item
export const updateFoodItem = async (id, foodItem) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, foodItem);
    return response.data;
  } catch (error) {
    console.error("Error updating food item:", error);
    throw error;
  }
};

// Delete a food item
export const deleteFoodItem = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error("Error deleting food item:", error);
    throw error;
  }
};