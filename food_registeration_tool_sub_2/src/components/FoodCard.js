// src/components/FoodCard.js
import React from 'react';

const FoodCard = ({ food }) => (
  <div className="max-w-sm rounded overflow-hidden shadow-lg bg-white p-4">
    <div className="px-6 py-4">
      <h5 className="font-bold text-xl mb-2">{food.name}</h5>
      <p className="text-gray-700 text-base">Calories: {food.calories}</p>
      <button className="mt-4 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700">More Info</button>
    </div>
  </div>
);

export default FoodCard;
