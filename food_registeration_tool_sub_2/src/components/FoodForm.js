import React, { useState } from 'react';

const FoodForm = () => {
    const [foodName, setFoodName] = useState('');

    const handleChange = (event) => {
        setFoodName(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        console.log("Food Name Submitted: ", foodName);
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                <label htmlFor="foodName">Food Name</label>
                <input
                    type="text"
                    className="form-control"
                    id="foodName"
                    value={foodName}
                    onChange={handleChange}
                />
            </div>
            <button type="submit" className="btn btn-primary">Submit</button>
        </form>
    );
};

export default FoodForm;


