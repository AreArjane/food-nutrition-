import React from 'react';

const FoodTable = ({ foods }) => {
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
              <button className="btn btn-danger btn-sm">Delete</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default FoodTable;





