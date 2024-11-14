import React, { useState } from 'react';

const FoodForm = ({ onSubmit }) => {
  const [name, setName] = useState('');
  const [calories, setCalories] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (name && calories) {
      onSubmit({ name, calories });  // Passing the new food data to the parent (FoodList)
      setName('');
      setCalories('');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="mt-4">
      <div className="mb-3">
        <label htmlFor="foodName" className="form-label">Food Name</label>
        <input 
          type="text" 
          id="foodName" 
          className="form-control" 
          value={name} 
          onChange={(e) => setName(e.target.value)} 
          required 
        />
      </div>
      <div className="mb-3">
        <label htmlFor="calories" className="form-label">Calories</label>
        <input 
          type="number" 
          id="calories" 
          className="form-control" 
          value={calories} 
          onChange={(e) => setCalories(e.target.value)} 
          required 
        />
      </div>
      <button type="submit" className="btn btn-primary">Add Food</button>
    </form>
  );
};

export default FoodForm;



