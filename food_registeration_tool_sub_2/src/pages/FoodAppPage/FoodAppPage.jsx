import React, { useState } from 'react';
import AddFoodItem from '../../components/AddFoodItem/AddFoodItem';  // Correct import for AddFoodItem
import FoodList from '../../components/FoodList/FoodList';           // Correct import for FoodList

const FoodAppPage = () => {
  const [refreshList, setRefreshList] = useState(false);

  // This function will toggle the state, triggering a re-fetch of food items
  const handleFoodAdded = () => {
    setRefreshList((prev) => !prev); // This will cause the FoodList to re-render
  };

  return (
    <div className="food-app-page">
      <h2>Manage Food Items</h2>
      <AddFoodItem onFoodAdded={handleFoodAdded} /> {/* Pass handler to AddFoodItem */}
      <FoodList key={refreshList} /> {/* Dynamically refresh the list when key changes */}
    </div>
  );
};

export default FoodAppPage;