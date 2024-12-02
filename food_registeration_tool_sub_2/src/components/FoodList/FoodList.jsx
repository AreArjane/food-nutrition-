import React, { useEffect, useState } from 'react';
import { getFoodItems } from '../../services/foodService';  // Import the function to fetch food items

const FoodList = () => {
  const [foodItems, setFoodItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchFoodItems = async () => {
      try {
        const data = await getFoodItems(); // Use getFoodItems from foodService
        setFoodItems(data);
      } catch (err) {
        setError('Could not fetch food items.');
        console.error('Error fetching food items:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchFoodItems();
  }, []); // Fetch food items on first render

  if (loading) return <div>Loading food items...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1>Food Menu</h1>
      <ul>
        {foodItems.map((item) => (
          <li key={item.foodId}>
            <h3>{item.name}</h3>
            <p>{item.description}</p>
            <p>Price: ${item.price}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FoodList;
