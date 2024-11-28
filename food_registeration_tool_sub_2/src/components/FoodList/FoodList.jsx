import React, { useEffect, useState } from 'react';
import axios from 'axios';

const FoodList = () => {
    // State to hold food items fetched from the API
    const [foodItems, setFoodItems] = useState([]);
    // State to handle errors
    const [error, setError] = useState(null);
    // State to track loading status
    const [loading, setLoading] = useState(true);

    // Fetch food items when the component is mounted
    useEffect(() => {
        axios.get('/foodapi/Foods') // API endpoint to fetch food items
            .then(response => {
                setFoodItems(response.data);  // Update state with fetched data
                setLoading(false);  // Turn off loading indicator
            })
            .catch(error => {
                console.error('Error fetching food items:', error);
                setError('There was an error fetching the food items.');  // Set error message
                setLoading(false);  // Turn off loading indicator even if there's an error
            });
    }, []);  // Empty dependency array ensures this runs only once when component is mounted

    // If the component is still loading, display a loading message
    if (loading) {
        return <div>Loading...</div>;
    }

    // If there was an error fetching the data, show the error message
    if (error) {
        return <div>{error}</div>;
    }

    // Render the list of food items if no errors and not loading
    return (
        <div>
            <h2>Food List</h2>
            <ul>
                {foodItems.map(item => (
                    <li key={item.id}> {/* Displaying food item by its id */}
                        <h3>{item.name}</h3> {/* Assuming 'name' is a property in the food data */}
                        <p>{item.description}</p> {/* Display description (if available in your data) */}
                        <p>Price: ${item.price}</p> {/* Assuming 'price' is available */}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default FoodList;