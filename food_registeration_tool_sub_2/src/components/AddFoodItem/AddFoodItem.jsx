import React, { useState } from 'react'; // Import React and useState hook
import { createFoodItem } from '../../services/foodService'; // Import the createFoodItem function for API interaction

// Define the AddFoodItem component
const AddFoodItem = ({ onFoodAdded }) => { 
    // State variables to manage form inputs and messages
    const [name, setName] = useState(''); // State for food name
    const [description, setDescription] = useState(''); // State for food description
    const [price, setPrice] = useState(''); // State for food pric
    const [message, setMessage] = useState(''); // State for success/error messages

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault(); // Prevent default form submission behavior

        // Basic validation for form inputs
        if (!name.trim() || !description.trim() || isNaN(price) || price <= 0) {
            setMessage('Please fill out all fields with valid values.');
            return;
        }

        try {
            // Create a new food object with parsed values
            const newFood = { 
                name, 
                description, 
                price: parseFloat(price) // Ensure the price is stored as a number
            };

            // Call the service function to save the food item
            await createFoodItem(newFood);

            // Display success message and reset form fields
            setMessage('Food item created successfully!');
            setName('');
            setDescription('');
            setPrice('');

            // Notify the parent component about the new food item
            if (onFoodAdded) onFoodAdded();
        } catch (err) {

            // Log the error for debugging
            console.error(err);
            setMessage('Could not create food item.'); // Display error message
        }
    };

    return (
        <div className="add-food-item-container">
            <h2>Add Your Food</h2>
            {message && (
                <p className={`message ${message.includes('Could not') ? 'error' : 'success'}`}>
                    {message}
                </p>
            )}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name</label>
                    <input
                        type="text"
                        placeholder="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Description</label>
                    <textarea
                        placeholder="Description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Price</label>
                    <input
                        type="number"
                        placeholder="Price"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default AddFoodItem; // Export the component for use in other parts of the application
