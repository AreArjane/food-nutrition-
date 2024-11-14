import React, { useState, useEffect } from 'react';
import FoodTable from './FoodTable'; // Komponent for å vise matvarer i en tabell
import AddFoodForm from './AddFoodForm'; // Komponent for å legge til nye matvarer
import FoodGrid from './FoodGrid'; // Komponent for å vise matvarer i et rutenett

const FoodList = () => {
  const [foods, setFoods] = useState([]); // State for matvarer
  const [loading, setLoading] = useState(true); // State for loading
  const [error, setError] = useState(null); // State for feil
  const [view, setView] = useState('table'); // State for visningstype (tabell eller rutenett)

  // Funksjon for å håndtere legging av ny mat
  const handleAddFood = (newFood) => {
    setFoods([...foods, newFood]);
  };

  // Hent statisk data for debugging
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);  // Sett loading til true mens data lastes
      setError(null);  // Nullstill feilstatus ved ny lasting
      try {
        // Simulerer en API-henting med statisk data
        const fetchedFoods = [
          { id: 1, name: 'Apple', calories: 95 },
          { id: 2, name: 'Banana', calories: 105 },
          { id: 3, name: 'Carrot', calories: 25 }
        ];
        console.log(fetchedFoods);  // Logg hentet data
        setFoods(fetchedFoods);  // Sett statisk data til foods
      } catch (err) {
        console.error('Error fetching foods:', err);
        setError('Failed to fetch food data. Please try again later.');
      } finally {
        setLoading(false);  // Sett loading til false når data er hentet eller feil oppstår
      }
    };

    fetchData();
  }, []); // Tom array sikrer at data kun hentes ved første rendering

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Food Registration</h2>

      {/* Vist mens data lastes */}
      {loading ? (
        <p>Loading...</p> // Kan også legge til en spinner her om ønskelig
      ) : error ? (
        <p className="text-red-500">{error}</p> // Feilmelding hvis henting mislykkes
      ) : (
        <div>
          {/* Skjema for å legge til nye matvarer */}
          <AddFoodForm onSubmit={handleAddFood} />

          {/* Knapp for å bytte visning mellom tabell og rutenett */}
          <div className="mb-4">
            <button
              className={`btn ${view === 'table' ? 'btn-secondary' : 'btn-primary'}`}
              onClick={() => setView(view === 'table' ? 'grid' : 'table')}
            >
              {view === 'table' ? 'Switch to Grid View' : 'Switch to Table View'}
            </button>
          </div>

          {/* Hvis matvarer finnes, vis enten tabell eller rutenett */}
          {foods.length > 0 ? (
            view === 'table' ? (
              <FoodTable foods={foods} /> // Vist som tabell
            ) : (
              <FoodGrid foods={foods} /> // Vist som rutenett
            )
          ) : (
            <p className="mt-4">No food records available. Please add some food items.</p>
          )}
        </div>
      )}
    </div>
  );
};

export default FoodList;














