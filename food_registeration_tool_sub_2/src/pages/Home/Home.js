import React from 'react'; // Import React library
import foodImage from '../../assets/images/food-image.jpg'; // Import the food image asset

// Define the Home component
const Home = () => {
  return (
    <div className="home-container">
      <img src={foodImage} alt="Delicious food" className="home-image" />
      <h2 className="text-center">Welcome to the Food App!</h2>
      <p className="text-center">Platform designed to help you find and share delicious recipes.</p>
      <p className="text-center">Whether you're a beginner or an experienced cook, we have something for everyone.</p>
       
      <p className="text-center">Sign up today to get started and share your food experiences!</p>
    </div>
  );
};


export default Home; // Export the Home component for use in other parts of the application
