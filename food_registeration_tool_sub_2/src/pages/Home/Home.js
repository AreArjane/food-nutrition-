import React from 'react';
import foodImage from '../../assets/images/food-image.jpg';
import foodImage1 from '../../assets/images/food-image1.jpg';
import foodImage2 from '../../assets/images/food-image2.png';
import foodImage3 from '../../assets/images/food-image3.jpg';
import foodImage4 from '../../assets/images/food-image4.jpg';

const Home = () => {
  return (
    <div className="home-container">
      {/* Gallery Section */}
      <div className="image-gallery">
      <img src={foodImage} alt="Delicious food" className="gallery-image" />
        <img src={foodImage1} alt="Food 1" className="gallery-image" />
        <img src={foodImage2} alt="Food 2" className="gallery-image" />
        <img src={foodImage3} alt="Food 3" className="gallery-image" />
        <img src={foodImage4} alt="Food 4" className="gallery-image" />
      </div>

      {/* Welcome Description */}
      
      <h2 className="text-center">Welcome to the Food App!</h2>
      <p className="text-center">Platform designed to help you find and share delicious recipes.</p>
      <p className="text-center">Whether you're a beginner or an experienced cook, we have something for everyone.</p>
      <p className="text-center">Sign up today to get started and share your food experiences!</p>
    </div>
  );
};

export default Home;
