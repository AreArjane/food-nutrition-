import React, { useEffect, useState } from 'react';

const NutrientManager = () => {

    // State to store the list of nutrients
    const [nutrients, setNutrients] = useState([]);

    // State to handle new nutrient input form
    const [newNutrient, setNewNutrient] = useState({ name: '', unit: '', number: '', rank: '' });

    // State to handle error messages
    const [error, setError] = useState('');

    // State to manage loading state
    const [loading, setLoading] = useState(false);

    // Fetch nutrients from API
    const fetchNutrients = async () => {
        setLoading(true); // Show loading state
        setError(''); // Clear any previous errors
        try {
            const response = await fetch('http://localhost:5072/nutrientapi/Nutrients');
            if (!response.ok) throw new Error('Network response was not ok'); // Handle network errors
            const data = await response.json();
            setNutrients(data); // Update state with fetched nutrients
        } catch (err) {
            setError('Error fetching data: ' + err.message); // Set error message in case of failure
        } finally { 
            setLoading(false);  // Hide loading state
        }
    };

    // Fetch nutrients on component mount
    useEffect(() => {
        fetchNutrients();
    }, []);

    // Handle input changes
    const handleChange = (e) => {
        const { name, value } = e.target; // Destructure input name and value
        setNewNutrient({ ...newNutrient, [name]: value }); // Update form state
    };

    // Handle form submission to add a new nutrient
    const handleSubmit = async (e) => {
        e.preventDefault(); // Prevent default form submission
    
        console.log(newNutrient);  // Sjekk hva som blir sendt til serveren
    
        try {
            const response = await fetch('http://localhost:5072/nutrientapi/Nutrients', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', // Specify content type
                },
                body: JSON.stringify(newNutrient), // Send nutrient data as JSON
            });
    
            if (!response.ok) {
                throw new Error('Failed to add nutrient'); // Handle errors
            }
    
            // Refresh the list of nutrients after successful addition
            fetchNutrients();

            // Reset the form inputs
            setNewNutrient({ name: '', unit: '', number: '', rank: '' }); // Tilbakestill skjemaet
        } catch (error) {
            setError(error.message); // Set error state to display the message
        }
    };

    // Handle deletion of a nutrient
    const handleDelete = async (id) => {
        setError(''); // Clear any previous errors
        try {
            const response = await fetch(`http://localhost:5072/nutrientapi/Nutrients/${id}`, { method: 'DELETE' });
            if (!response.ok) throw new Error('Failed to delete nutrient'); // Handle deletion errors
            await fetchNutrients(); // Refresh the list of nutrients after deletion
        } catch (err) {
            setError(err.message); // Set error state to display the message
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
