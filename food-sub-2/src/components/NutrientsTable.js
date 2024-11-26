import React, { useState, useEffect } from 'react';
import './tables.css';

const NutrientsTable = () => {
    const [nutrients, setNutrients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(1);

    const fetchNutrientData = async () => {
        try {
            setLoading(true);
            const response = await fetch(`http://localhost:5072/nutrientsapi/Nutrients?page=${page}&pageSize=10`);
            const data = await response.json();
            setNutrients((prevNutrients) => [...prevNutrients, ...data.Data]);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching nutrient data:', error);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchNutrientData();
    }, [page]);

    const loadMore = () => {
        setPage((prevPage) => prevPage + 1);
    };

    return (
        <div className="table-container">
            <h1 className="text-center mb-4">Nutrients Information</h1>
            <table className="table table-striped table-bordered">
                <thead className="table-dark">
                    <tr>
                        <th>Nutrient ID</th>
                        <th>Name</th>
                        <th>Unit Name</th>
                        <th>Nutrient NBR</th>
                        <th>Rank</th>
                    </tr>
                </thead>
                <tbody>
                    {nutrients.map((nutrient) => (
                        <tr key={nutrient.NutrientId}>
                            <td>{nutrient.NutrientId}</td>
                            <td>{nutrient.Name}</td>
                            <td>{nutrient.UnitName}</td>
                            <td>{nutrient.NutrientNbr}</td>
                            <td>{nutrient.Rank}</td>
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

export default NutrientsTable;
