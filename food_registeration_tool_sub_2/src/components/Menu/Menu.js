import React, { useEffect, useState } from 'react';
import { getFoodItems } from '../../services/foodService'; // Import the service function to fetch food items

// Define the Menu component
const Menu = () => {
    // State variables
  const [foodItems, setFoodItems] = useState([]); // Stores the list of food items
  const [loading, setLoading] = useState(true); // Tracks the loading state
  const [error, setError] = useState(null); // Tracks any error messages

  // Fetch food items when the component mounts
  useEffect(() => {
    const fetchFoodItems = async () => {
      try {
        // Call the service function to fetch food items
        const data = await getFoodItems();
        console.log("Fetched food items:", data); // Log the fetched data
        setFoodItems(data); // Update the state with fetched data
      } catch (err) {
        // Handle errors and set error message
        setError('Could not pick up food list.');
        console.error("Error fetching food list:", err); // Log the error for debugging
      } finally {
        // Stop the loading state
        setLoading(false);
      }
    };

    fetchFoodItems(); // Invoke the fetch function
  }, []); // Dependency array ensures this runs only once on mount

  // Show loading message while fetching data
  if (loading) return <p>Loading...</p>;

  // Show error message if fetching fails
  if (error) return <p>{error}</p>;

  // Render the food menu
  return (
    <div className="menu-container">
      <h1>Food Menu</h1>
      <ul>
        {foodItems.map((item) => (
          <li key={item.id}>
            <h2>{item.name}</h2>
            <p>{item.description}</p>
            <p>Price: {item.price} NOK</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Menu; // Export the Menu component
