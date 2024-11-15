import React, { useState, useEffect } from 'react';
import AddFoodForm from './AddFoodForm';
import FoodTable from './FoodTable';
import FoodGrid from './FoodGrid';

const FoodList = () => {
  const [foods, setFoods] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [view, setView] = useState('table');

  // Log rendering for debugging
  console.log("Rendering FoodList component");

  // Function to add new food
  const handleAddFood = (newFood) => {
    setFoods([...foods, newFood]);
  };

  // Simulating data fetching
  useEffect(() => {
    console.log("Fetching data...");
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const fetchedFoods = [
          { id: 1, name: 'Apple', calories: 95 },
          { id: 2, name: 'Banana', calories: 105 },
          { id: 3, name: 'Carrot', calories: 25 }
        
        ];
        setFoods(fetchedFoods);
      } catch (err) {
        setError('Failed to fetch food data. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Food Registration</h2>

      {/* Show loading or error messages */}
      {loading ? (
        <p className="loading">Loading...</p>
      ) : error ? (
        <p className="error">{error}</p>
      ) : (
        <div>
          {/* Add Food Form */}
          <AddFoodForm onSubmit={handleAddFood} />

          {/* Toggle between Table and Grid View */}
          <div className="mb-4">
            <button
              className={`btn ${view === 'table' ? 'btn-secondary' : 'btn-primary'}`}
              onClick={() => setView(view === 'table' ? 'grid' : 'table')}
            >
              {view === 'table' ? 'Switch to Grid View' : 'Switch to Table View'}
            </button>
          </div>

          {/* Display food items in selected view */}
          {foods.length > 0 ? (
            view === 'table' ? (
              <FoodTable foods={foods} />
            ) : (
              <FoodGrid foods={foods} />
            )
          ) : (
            <p className="mt-4">No food records available. Please add some food items.</p>
          )}
        </div>
      )}
    </div>
  );
};

export default FoodList;
















