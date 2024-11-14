import React, { useState } from 'react';

const AddFoodForm = ({ onSubmit }) => {
  const [food, setFood] = useState({ name: '', calories: '' });

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(food);
    setFood({ name: '', calories: '' }); // Clear form after submit
  };

  return (
    <form onSubmit={handleSubmit} className="mb-4">
      <input
        type="text"
        placeholder="Food Name"
        value={food.name}
        onChange={(e) => setFood({ ...food, name: e.target.value })}
        className="p-2 border mb-2"
      />
      <input
        type="number"
        placeholder="Calories"
        value={food.calories}
        onChange={(e) => setFood({ ...food, calories: e.target.value })}
        className="p-2 border mb-2"
      />
      <button type="submit" className="btn btn-primary">Add Food</button>
    </form>
  );
};

export default AddFoodForm;
