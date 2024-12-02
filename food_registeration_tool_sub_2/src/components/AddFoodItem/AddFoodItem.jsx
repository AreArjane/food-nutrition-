import React, { useState } from 'react';

const AddFoodItem = ({ onSubmit }) => {
  const [foodData, setFoodData] = useState({ name: '', description: '', price: '' });
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFoodData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const validateForm = () => {
    const { name, description, price } = foodData;
    if (!name || !description || !price) {
      setError('All fields are required.');
      return false;
    }
    if (isNaN(price) || Number(price) <= 0) {
      setError('Price must be a positive number.');
      return false;
    }
    setError(null);
    return true;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!validateForm()) return;

    if (typeof onSubmit === 'function') {
      onSubmit(foodData);
      setFoodData({ name: '', description: '', price: '' });
    } else {
      console.error('onSubmit is not a function.');
    }
  };

  return (
    <div className="add-food-item">
      <h2>Add Food Item</h2>
      <form onSubmit={handleSubmit}>
        {/* Food Name */}
        <div className="form-group">
          <label htmlFor="name">Food Name</label>
          <input
            type="text"
            id="name"
            name="name"
            value={foodData.name}
            onChange={handleChange}
            className="form-control"
            placeholder="Enter food name"
          />
        </div>
        
        {/* Description */}
        <div className="form-group">
          <label htmlFor="description">Description</label>
          <input
            type="text"
            id="description"
            name="description"
            value={foodData.description}
            onChange={handleChange}
            className="form-control"
            placeholder="Enter food description"
          />
        </div>

        {/* Price */}
        <div className="form-group">
          <label htmlFor="price">Price</label>
          <input
            type="number"
            id="price"
            name="price"
            value={foodData.price}
            onChange={handleChange}
            className="form-control"
            placeholder="Enter price"
          />
        </div>

        {/* Error Message */}
        {error && <p className="text-danger mt-2">{error}</p>}

        {/* Submit Button */}
        <button type="submit" className="btn btn-primary mt-3">
          Add Food
        </button>
      </form>
    </div>
  );
};

export default AddFoodItem;
