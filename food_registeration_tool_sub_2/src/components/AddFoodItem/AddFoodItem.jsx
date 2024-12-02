import React, { useState } from 'react';

const AddFoodItem = ({ onSubmit }) => {
  const [foodData, setFoodData] = useState({ name: '', description: '', price: '' });
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);
  const [loading, setLoading] = useState(false);

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

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) return;

    setLoading(true);
    setSuccess(false);

    try {
      // Assuming onSubmit is an async function
      await onSubmit(foodData);
      setSuccess(true);
      setFoodData({ name: '', description: '', price: '' });
    } catch (error) {
      setError('Failed to add food item. Please try again.');
    } finally {
      setLoading(false);
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
            required
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
            required
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
            required
            min="0.01"
            step="0.01"
          />
        </div>

        {/* Error and Success Messages */}
        {error && <p className="text-danger mt-2">{error}</p>}
        {success && <p className="text-success mt-2">Food item added successfully!</p>}

        {/* Submit Button */}
        <button type="submit" className="btn btn-primary mt-3" disabled={loading}>
          {loading ? 'Adding...' : 'Add Food'}
        </button>
      </form>
    </div>
  );
};

export default AddFoodItem;
