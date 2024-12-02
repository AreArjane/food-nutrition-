import React, { useEffect, useState } from 'react';

const NutrientManager = () => {
    // State for å lagre næringsstoffene
    const [nutrients, setNutrients] = useState([]);

    // State for ny næringsstoff input
    const [newNutrient, setNewNutrient] = useState({ name: '', unit: '', number: '', rank: '' });

    // State for feilmeldinger
    const [error, setError] = useState('');

    // State for å håndtere innlasting
    const [loading, setLoading] = useState(false);

    // State for paginering
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(20); // Fast størrelse for hver side

    // Fetch funksjon for næringsstoffene
    const fetchNutrients = async () => {
        setLoading(true);
        setError('');
        
        try {
            const response = await fetch(`http://localhost:5072/nutrientapi/Nutrients?pagenumber=${pageNumber}&pagesize=${pageSize}`);
            
            if (!response.ok) throw new Error('Failed to fetch nutrients');
            
            const data = await response.json();
            
            if (data && data.length > 0) {
                setNutrients((prevNutrients) => [...prevNutrients, ...data]);
                setPageNumber(pageNumber + 1); // Øk pageNumber for neste henting
            } else {
                setError('No nutrients available');
            }
        } catch (err) {
            setError('Error fetching data: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    // Hent næringsstoffer når komponenten monteres
    useEffect(() => {
        fetchNutrients();
    }, []);

    // Håndtere input endringer
    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewNutrient({ ...newNutrient, [name]: value });
    };

    // Håndtere innsending av nytt næringsstoff
    const handleSubmit = async (e) => {
        e.preventDefault();

        // Validering før innsending
        if (!newNutrient.name || !newNutrient.unit || !newNutrient.number || !newNutrient.rank) {
            setError('All fields are required!');
            return;
        }

        try {
            const response = await fetch('http://localhost:5072/nutrientapi/create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newNutrient),
            });

            if (!response.ok) {
                throw new Error('Failed to add nutrient');
            }

            // Etter vellykket innsending, hent oppdaterte næringsstoffer
            fetchNutrients();

            // Nullstill input
            setNewNutrient({ name: '', unit: '', number: '', rank: '' });
            setError('');
        } catch (error) {
            setError(error.message);
        }
    };

    // Håndtere sletting av næringsstoff
    const handleDelete = async (id) => {
        setError('');
        try {
            const response = await fetch(`http://localhost:5072/nutrientapi/delete/${id}`, { method: 'DELETE' });
            if (!response.ok) throw new Error('Failed to delete nutrient');
            fetchNutrients();
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
                <input
                    type="text"
                    name="name"
                    placeholder="Name"
                    value={newNutrient.name}
                    onChange={handleChange}
                    required
                />
                <input
                    type="text"
                    name="unit"
                    placeholder="Unit"
                    value={newNutrient.unit}
                    onChange={handleChange}
                    required
                />
                <input
                    type="number"
                    name="number"
                    placeholder="Number"
                    value={newNutrient.number}
                    onChange={handleChange}
                    required
                />
                <input
                    type="number"
                    name="rank"
                    placeholder="Rank"
                    value={newNutrient.rank}
                    onChange={handleChange}
                    required
                />
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
                    {nutrients.length > 0 ? (
                        nutrients.map((nutrient) => (
                            <tr key={nutrient.id}>
                                <td>{nutrient.name}</td>
                                <td>{nutrient.unit}</td>
                                <td>{nutrient.number}</td>
                                <td>{nutrient.rank}</td>
                                <td>
                                    <button onClick={() => handleDelete(nutrient.id)}>Delete</button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="5">No nutrients available</td>
                        </tr>
                    )}
                </tbody>
            </table>

            {/* Load more button for paginated data */}
            <button onClick={fetchNutrients} disabled={loading}>
                Load More Nutrients
            </button>
        </div>
    );
};

export default NutrientManager;
