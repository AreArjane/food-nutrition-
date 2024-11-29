import React, { useEffect, useState } from 'react';
import { getFoodItems } from '../../services/foodService'; // Importer tjenesten som henter matvarer

const Menu = () => {
  const [foodItems, setFoodItems] = useState([]); // Tilstand for å lagre matvarer
  const [loading, setLoading] = useState(true); // Tilstand for lasting
  const [error, setError] = useState(null); // Tilstand for feil

  useEffect(() => {
    const fetchFoodItems = async () => {
      try {
        const data = await getFoodItems(); // Hent matvarer fra tjenesten
        console.log("Fetched food items:", data); // Logg de hentede dataene
        setFoodItems(data); // Oppdater tilstanden med hentede data
      } catch (err) {
        setError('Could not pick up food list.'); // Sett feilmelding
        console.error("Error fetching food list:", err); // Logg feilen
      } finally {
        setLoading(false); // Sett lasting til false uansett hva
      }
    };

    fetchFoodItems(); // Kall funksjonen for å hente matvarer
  }, []); // Tom array for å kjøre effekten bare én gang ved montering

  // Vis lastingstekst
  if (loading) return <p>Loading...</p>;
  // Vis feilmelding hvis det oppstod en feil
  if (error) return <p>{error}</p>;

  // Render listen over matvarer
  return (
    <div className="menu-container">
      <h1>Food Menu</h1>
      <ul>
        {foodItems.map((item) => (
          <li key={item.id}> {/* Bruker id som nøkkel */}
            <h2>{item.name}</h2> {/* Navn på maten */}
            <p>{item.description}</p> {/* Beskrivelse av maten */}
            <p>Price: {item.price} NOK</p> {/* Pris på maten */}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Menu;