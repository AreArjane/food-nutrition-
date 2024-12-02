import React, { useEffect, useState } from 'react';
import apiClient from '../../api'; // Import the API client for Sub1's API
import AddFoodItem from '../AddFoodItem/AddFoodItem'; // Import the AddFoodItem component for editing

const Menu = ({ foodList }) => {
  const [apiFoodItems, setApiFoodItems] = useState([]); // Stores food items fetched from API
  const [error, setError] = useState(null); // Stores error message if something goes wrong
  const [loading, setLoading] = useState(true); // Tracks loading state
  const [pagination, setPagination] = useState({
    pageNumber: 1,
    pageSize: 10,
    totalItems: 0, // Total number of items (useful for pagination)
  });
  const [editingFoodItem, setEditingFoodItem] = useState(null); // Stores food item to be edited

  // Fetch food items from the API (if no foodList is passed as a prop)
  useEffect(() => {
    if (!foodList) {
      const fetchFoodItems = async () => {
        try {
          const response = await apiClient.get(`/Foods?pagenumber=${pagination.pageNumber}&pagesize=${pagination.pageSize}`);
          const { data, totalItems } = response.data;

          // Manipulate data if necessary
          const manipulatedData = data.map((item) => ({
            ...item,
            foodCategoryDescription: item.foodCategory?.description || 'Unknown category',
            foodCategoryCode: item.foodCategory?.code || 'N/A',
          }));

          setApiFoodItems(manipulatedData);
          setPagination((prev) => ({ ...prev, totalItems }));
        } catch (err) {
          setError('There was an error fetching food items.');
          console.error('Error fetching food items:', err);
        } finally {
          setLoading(false);
        }
      };

      fetchFoodItems();
    }
  }, [pagination.pageNumber, pagination.pageSize, foodList]); // Dependency array includes foodList to avoid unnecessary fetch

  // Function to handle deletion of a food item
  const handleDelete = async (foodId) => {
    try {
      await apiClient.delete(`/Foods/${foodId}`);
      setApiFoodItems(prevItems => prevItems.filter(item => item.foodId !== foodId));
    } catch (err) {
      setError('Failed to delete food item.');
      console.error('Error deleting food item:', err);
    }
  };

  // Function to handle editing of a food item
  const handleEdit = (foodItem) => {
    setEditingFoodItem(foodItem);
  };

  // If loading, show a loading message
  if (loading && !foodList) return <div>Loading food items...</div>;

  // If there was an error, show the error message
  if (error) return <div>{error}</div>;

  // Determine whether to render API data or prop data
  const itemsToRender = foodList || apiFoodItems;

  return (
    <div className="menu-container">
      <h1>Menu</h1>
      <table className="food-table">
        <thead>
          <tr>
            <th>Food ID</th>
            <th>Description</th>
            <th>Category</th>
            <th>Category Code</th>
            <th>Type</th>
            <th>Published</th>
            <th>Actions</th> {/* New column for actions */}
          </tr>
        </thead>
        <tbody>
          {itemsToRender.length > 0 ? (
            itemsToRender.map((item, index) => (
              <tr key={item.foodId || index}>
                <td>{item.foodId || item.name}</td>
                <td>{item.description}</td>
                <td>{item.foodCategoryDescription || 'N/A'}</td>
                <td>{item.foodCategoryCode || 'N/A'}</td>
                <td>{item.dataType || 'N/A'}</td>
                <td>{item.publicationDate || 'N/A'}</td>
                <td>
                  <button onClick={() => handleEdit(item)} className="btn btn-warning btn-sm">Edit</button>
                  <button onClick={() => handleDelete(item.foodId)} className="btn btn-danger btn-sm">Delete</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7" className="text-center">No food items available.</td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Pagination Controls (if needed) */}
      {!foodList && (
        <div className="pagination-controls">
          <button
            onClick={() =>
              setPagination((prev) => ({
                ...prev,
                pageNumber: prev.pageNumber > 1 ? prev.pageNumber - 1 : 1,
              }))
            }
            disabled={pagination.pageNumber <= 1}
          >
            Previous
          </button>
          <span>
            Page {pagination.pageNumber} of {Math.ceil(pagination.totalItems / pagination.pageSize)}
          </span>
          <button
            onClick={() =>
              setPagination((prev) => ({
                ...prev,
                pageNumber: prev.pageNumber + 1,
              }))
            }
            disabled={pagination.pageNumber * pagination.pageSize >= pagination.totalItems}
          >
            Next
          </button>
        </div>
      )}

      {/* Edit Food Item Modal or Component */}
      {editingFoodItem && (
        <AddFoodItem
          onSubmit={async (foodData) => {
            // API call to update the food item
            try {
              await apiClient.put(`/Foods/${editingFoodItem.foodId}`, foodData);
              setApiFoodItems((prevItems) =>
                prevItems.map((item) =>
                  item.foodId === editingFoodItem.foodId ? { ...item, ...foodData } : item
                )
              );
              setEditingFoodItem(null); // Close the edit form
            } catch (err) {
              setError('Failed to update food item.');
              console.error('Error updating food item:', err);
            }
          }}
        />
      )}
    </div>
  );
};

export default Menu;
