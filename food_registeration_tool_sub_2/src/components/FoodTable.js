import React from 'react';
import { deleteFood } from '../services/foodService'; // Add the deleteFood function to your service

const FoodTable = ({ foods, onFoodDeleted }) => {
  const handleDelete = async (id) => {
    try {
      await deleteFood(id); // Call the delete service function
      onFoodDeleted(id); // Callback to remove the deleted food from the UI (if you're managing state in a parent component)
    } catch (error) {
      console.error('Error deleting food:', error);
    }
  };

  return (
    <table className="table table-striped mt-4">
      <thead>
        <tr>
          <th>Name</th>
          <th>Calories</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {foods.map(food => (
          <tr key={food.id}>
            <td>{food.name}</td>
            <td>{food.calories}</td>
            <td>
              <button className="btn btn-danger btn-sm" onClick={() => handleDelete(food.id)}>
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default FoodTable;
