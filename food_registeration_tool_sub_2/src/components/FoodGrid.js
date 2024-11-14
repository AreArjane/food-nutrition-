import React from 'react';

const FoodGrid = ({ foods }) => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4">
      {foods.map((food) => (
        <div key={food.id} className="max-w-sm rounded overflow-hidden shadow-lg bg-white p-4">
          <div className="px-6 py-4">
            <h5 className="font-bold text-xl mb-2">{food.name}</h5>
            <p className="text-gray-700 text-base">Calories: {food.calories}</p>
          </div>
        </div>
      ))}
    </div>
  );
};

export default FoodGrid;





