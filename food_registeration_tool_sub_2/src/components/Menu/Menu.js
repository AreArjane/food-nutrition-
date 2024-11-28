import React, { useEffect, useState } from 'react';
import { getFoodItems } from '../../services/foodService';

const Menu = () => {
  const [foodItems, setFoodItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchFoodItems = async () => {
      try {
        const data = await getFoodItems();
        console.log("Fetched food items:", data); // Log the fetched data
        setFoodItems(data);
      } catch (err) {
        setError('Could not pick up food list.');
        console.error("Error fetching food list:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchFoodItems();
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

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

export default Menu;