// src/components/FoodList.js
import React, { useEffect, useState } from 'react';
import { getAllFoods, deleteFood } from '../services/foodService';
import Alert from './additionals/Alert';

const FoodList = () => {
  // Declare the foods state
  const [foods, setFoods] = useState([]); // Initialize foods as an empty array
  const [alert, setAlert] = useState(null);
  const [filter, setFilter] = useState('');

  useEffect(() => {
    getAllFoods()
      .then((data) => setFoods(data))
      .catch((error) => {
        console.error('Feil ved henting av matvarer:', error);
        setAlert({ message: 'Kunne ikke hente matvarer', type: 'danger' });
      });
  }, []);

  const handleDelete = (id) => {
    deleteFood(id)
      .then(() => {
        setFoods(foods.filter((food) => food.id !== id)); // Update the foods state
        setAlert({ message: 'Matvare slettet!', type: 'success' });
      })
      .catch((error) => {
        console.error('Feil ved sletting:', error);
        setAlert({ message: 'Kunne ikke slette matvaren', type: 'danger' });
      });
  };

  const filteredFoods = foods.filter((food) =>
    food.name.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <div>
      {alert && <Alert message={alert.message} type={alert.type} />}
      <input
        type="text"
        placeholder="Søk etter matvarer"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
        className="form-control mb-3"
      />
      <ul className="list-group">
        {filteredFoods.map((food) => (
          <li key={food.id} className="list-group-item d-flex justify-content-between align-items-center">
            <span>{food.name} - {food.calories} kalorier</span>
            <button onClick={() => handleDelete(food.id)} className="btn btn-danger btn-sm">
              Slett
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FoodList;

