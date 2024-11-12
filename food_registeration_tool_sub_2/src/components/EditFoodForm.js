// src/components/EditFoodForm.js
import React, { useState, useEffect } from 'react';
import foodService from '../services/foodService';

const EditFoodForm = ({ foodId, onUpdate }) => {
  const [food, setFood] = useState({ name: '', description: '', calories: 0 });

  useEffect(() => {
    const fetchFood = async () => {
      const data = await foodService.getFoodById(foodId);
      setFood(data);
    };
    fetchFood();
  }, [foodId]);

  const handleChange = (e) => {
    setFood({ ...food, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await foodService.updateFood(foodId, food);
    onUpdate(); // Trigger a refresh or update the state in FoodList
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Name:
        <input type="text" name="name" value={food.name} onChange={handleChange} />
      </label>
      <label>
        Description:
        <textarea name="description" value={food.description} onChange={handleChange}></textarea>
      </label>
      <label>
        Calories:
        <input type="number" name="calories" value={food.calories} onChange={handleChange} />
      </label>
      <button type="submit">Update Food</button>
    </form>
  );
};

export default EditFoodForm;