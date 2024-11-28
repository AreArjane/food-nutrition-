import React, { useState } from 'react';
import { createFoodItem } from '../../services/foodService';

const AddFoodItem = ({ onFoodAdded }) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!name.trim() || !description.trim() || isNaN(price) || price <= 0) {
            setMessage('Please fill out all fields with valid values.');
            return;
        }

        try {
            const newFood = { 
                name, 
                description, 
                price: parseFloat(price)
            };
            await createFoodItem(newFood);
            setMessage('Food item created successfully!');
            setName('');
            setDescription('');
            setPrice('');

            // Notify the parent about the new food item
            if (onFoodAdded) onFoodAdded();
        } catch (err) {
            console.error(err);
            setMessage('Could not create food item.');
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

export default AddFoodItem;
