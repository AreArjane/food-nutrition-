import axios from 'axios';
/**
 * Service API fetching from SUB-1
 * Provides : 
 * fetchFoods -> return all foods from the API 
 */
const apiClient = axios.create({
    baseURL : 'http://localhost:5073',
    headers: {
        'Content-Type': 'application/json',
    },
});

/** */
export const fetchFoods = async (pageNumber, pageSize, search = '') => {
    const response = await apiClient.get('/foods', {
        params: { pageNumber, pageSize, foodstartwith: search },
    });
    return response.data;
};
// GET: Fetch food details by ID
export const fetchFoodDetails = async (id) => {
    const response = await apiClient.get(`/food/${id}`);
    return response.data;
};
// POST: Create a new food item
export const createFood = async (foodData) => {
    const response = await apiClient.post('/food', foodData);
    return response.data;
};
// PUT: Update a food item
export const updateFood = async (id, foodData) => {
    const response = await apiClient.put(`/food/${id}`, foodData);
    return response.data;
};
// DELETE: Delete a food item
export const deleteFood = async (id) => {
    const response = await apiClient.delete(`/food/${id}`);
    return response.data;
};