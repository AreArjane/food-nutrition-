import React, { useEffect, useState } from 'react';

const NutrientManager = () => {
    const [nutrients, setNutrients] = useState([]);
    const [newNutrient, setNewNutrient] = useState({ name: '', unit: '', number: '', rank: '' });
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    // Fetch nutrients from API
    const fetchNutrients = async () => {
        setLoading(true);
        setError('');
        try {
            const response = await fetch('http://localhost:5072/nutrientapi/Nutrients');
            if (!response.ok) throw new Error('Network response was not ok');
            const data = await response.json();
            setNutrients(data);
        } catch (err) {
            setError('Error fetching data: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchNutrients();
    }, []);

    // Handle input changes
    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewNutrient({ ...newNutrient, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        console.log(newNutrient);  // Sjekk hva som blir sendt til serveren
    
        try {
            const response = await fetch('http://localhost:5072/nutrientapi/Nutrients', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newNutrient),
            });
    
            if (!response.ok) {
                throw new Error('Failed to add nutrient');
            }
    
            // Oppdater listen over næringsstoffer
            fetchNutrients();
            setNewNutrient({ name: '', unit: '', number: '', rank: '' }); // Tilbakestill skjemaet
        } catch (error) {
            setError(error.message); // Sett error state for å vise feilmeldingen
        }
    };

    // Delete a nutrient
    const handleDelete = async (id) => {
        setError('');
        try {
            const response = await fetch(`http://localhost:5072/nutrientapi/Nutrients/${id}`, { method: 'DELETE' });
            if (!response.ok) throw new Error('Failed to delete nutrient');
            await fetchNutrients(); // Refresh the list
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div>
            <h2>Nutrient Manager</h2>
            {error && <div className="error-message">{error}</div>}
            {loading && <div>Loading...</div>}
            <form onSubmit={handleSubmit}>
                <input type="text" name="name" placeholder="Name" value={newNutrient.name} onChange={handleChange} required />
                <input type="text" name="unit" placeholder="Unit" value={newNutrient.unit} onChange={handleChange} required />
                <input type="number" name="number" placeholder="Number" value={newNutrient.number} onChange={handleChange} required />
                <input type="number" name="rank" placeholder="Rank" value={newNutrient.rank} onChange={handleChange} required />
                <button type="submit">Add Nutrient</button>
            </form>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Unit</th>
                        <th>Number</th>
                        <th>Rank</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {nutrients.map((nutrient) => (
                        <tr key={nutrient.id}>
                            <td>{nutrient.name}</td>
                            <td>{nutrient.unit}</td>
                            <td>{nutrient.number}</td>
                            <td>{nutrient.rank}</td>
                            <td>
                                <button onClick={() => handleDelete(nutrient.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default NutrientManager;
