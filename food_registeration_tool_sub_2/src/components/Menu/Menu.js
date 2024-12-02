import React, { useEffect, useState } from 'react';
import apiClient from '../../api'; // Import the API client for Sub1's API

// Menu component accepts a foodList prop and can also fetch food items from an API
const Menu = ({ foodList }) => {
  const [apiFoodItems, setApiFoodItems] = useState([]); // Stores food items fetched from API
  const [error, setError] = useState(null); // Stores error message if something goes wrong
  const [loading, setLoading] = useState(true); // Tracks loading state
  const [pagination, setPagination] = useState({
    pageNumber: 1,
    pageSize: 10,
    totalItems: 0, // Total number of items (useful for pagination)
  });

  // Fetch food items from the API (if no foodList is passed as a prop)
  useEffect(() => {
    if (!foodList) {
      const fetchFoodItems = async () => {
        try {
          const response = await apiClient.get(
            '/Foods?pagenumber=${pagination.pageNumber}&pagesize=${pagination.pageSize}'
          );
          const { data, pagenumber, pagesize, totalItems } = response.data;

          // Manipulate data if necessary
          const manipulatedData = data.map((item) => {
            const foodCategoryDescription = item.foodCategory?.description || 'Unknown category';
            const foodCategoryCode = item.foodCategory?.code || 'N/A';

            return {
              ...item,
              foodCategoryDescription,
              foodCategoryCode,
            };
          });

          setApiFoodItems(manipulatedData);
          setPagination({
            ...pagination,
            totalItems,
          });
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
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6" className="text-center">No food items available.</td>
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
    </div>
  );
};

export default Menu;
