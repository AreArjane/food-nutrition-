import React, { useEffect, useState } from 'react';
import axios from 'axios';

const FoodList = () => {
    const [foodItems, setFoodItems] = useState([]); // Tilstand for matvarer
    const [error, setError] = useState(null); // Tilstand for feil
    const [loading, setLoading] = useState(true); // Tilstand for lasting

    useEffect(() => {
        const fetchData = async () => {
            console.log('Fetching food items...'); // Logg når forespørselen starter
            try {
                const response = await axios.get('http://localhost:5072/foodapi/Foods'); // API-endepunkt
                console.log('Response received:', response.data); // Logg responsen
                setFoodItems(response.data); // Oppdater tilstanden med dataene
            } catch (error) {
                console.error('Error fetching food items:', error); // Logg feilen
                setError('Det oppstod en feil under henting av matvarer: ' + (error.response ? error.response.data : error.message)); // Sett feilmelding
            } finally {
                setLoading(false); // Sett lasting til false uansett hva
            }
        };

        fetchData();
    }, []); // Tom array sørger for at effekten bare kjører én gang

    // Hvis komponenten laster, vis en melding
    if (loading) {
        return <div>Loading...</div>;
    }

    // Hvis det oppstod en feil, vis feilmeldingen
    if (error) {
        return <div>{error}</div>;
    }

    // Hvis ingen matvarer finnes, vis en melding
    if (foodItems.length === 0) {
        return <div>Ingen matvarer tilgjengelig.</div>;
    }

    // Render listen over matvarer
    return (
        <div>
            <h2>Food List</h2>
            <ul>
                {foodItems.map(item => (
                    <li key={item.id}> {/* Bruker id som nøkkel */}
                        <h3>{item.name}</h3> {/* Anta at 'name' er en egenskap i dataene */}
                        <p>{item.description}</p> {/* Beskrivelse av maten */}
                        <p>Price: ${item.price}</p> {/* Anta at 'price' er tilgjengelig */}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default FoodList;