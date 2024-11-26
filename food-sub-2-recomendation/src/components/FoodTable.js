import React, { useState, useEffect } from 'react';
import './tables.css';

const FoodTable = () => {
    const [foods, setFoods] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(1);

    const fetchFoodData = async () => {
        try {
            setLoading(true);
            const response = await fetch('http://localhost:5072/foodapi/Foods?pagenumber=2&pagesize=10', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            const data = await response.json();
            setFoods((prevFoods) => [...prevFoods, ...data.Data]);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching food data:', error);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchFoodData();
    }, [page]);

    const loadMore = () => {
        setPage((prevPage) => prevPage + 1);
    };

    return (
        <div className="table-container">
            <h1 className="text-center mb-4">Food Information</h1>
            <table className="table table-striped table-bordered">
                <thead className="table-dark">
                    <tr>
                        <th>Food ID</th>
                        <th>Data Type</th>
                        <th>Description</th>
                        <th>Publication Date</th>
                        <th>Category</th>
                        <th>Category Code</th>
                    </tr>
                </thead>
                <tbody>
                    {foods.map((food) => (
                        <tr key={food.FoodId}>
                            <td>{food.FoodId}</td>
                            <td>{food.DataType}</td>
                            <td>{food.Description}</td>
                            <td>{food.PublicationDate}</td>
                            <td>{food.FoodCategory?.Description || 'N/A'}</td>
                            <td>{food.FoodCategory?.Code || 'N/A'}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {loading && <div className="loading">Loading data...</div>}
            {!loading && (
                <button onClick={loadMore} className="btn btn-primary mx-auto d-block">
                    Load More
                </button>
            )}
        </div>
    );
};

export default FoodTable;
