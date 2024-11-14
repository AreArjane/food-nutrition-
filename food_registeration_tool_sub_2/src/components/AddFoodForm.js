import React, { useState } from 'react';

const AddFoodForm = ({ onSubmit }) => {
  const [name, setName] = useState('');
  const [calories, setCalories] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (name && calories) {
      onSubmit({ id: Date.now(), name, calories: parseInt(calories) });
      setName('');
      setCalories('');
    } else {
      alert('Please fill out both fields!');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Add New Food Item</h3>
      <input
        type="text"
        placeholder="Food name"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <input
        type="number"
        placeholder="Calories"
        value={calories}
        onChange={(e) => setCalories(e.target.value)}
      />
      <button type="submit">Add Food</button>
    </form>
  );
};

export default AddFoodForm;
