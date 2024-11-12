// src/pages/HomePage.js
import React from 'react';
import { Link } from 'react-router-dom';

function HomePage() {
  return (
    <div>
      <h1>Welcome to the Food Registration Tool</h1>
      <p>
        Here you can register and manage your food items.
      </p>
      <div>
        <Link to="/food-form">Add New Food</Link>
        <Link to="/food-list">View Food List</Link>
      </div>
    </div>
  );
}

export default HomePage;