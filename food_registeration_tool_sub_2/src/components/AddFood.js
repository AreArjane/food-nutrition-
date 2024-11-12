import React, { useState } from 'react';

const AddFood = () => {
  const [food, setFood] = useState('');

  const handleChange = (e) => setFood(e.target.value);
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Food added:', food);
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="text" value={food} onChange={handleChange} placeholder="Enter food name" />
      <button type="submit">Add Food</button>
    </form>
  );
};

export default AddFood;
