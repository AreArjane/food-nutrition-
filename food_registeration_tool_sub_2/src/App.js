import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Header from './components/Header/Header';
import Footer from './components/footer/footer';
import AuthForm from './components/AuthForm/AuthForm';
import Home from './pages/Home/Home';
import Error from './pages/Error/Error';
import FoodAppPage from './pages/FoodAppPage/FoodAppPage'; // Correct import of FoodAppPage
import Menu from './components/Menu/Menu';
import About from './components/About/About';
import Contact from './components/Contact/Contact';
import AddFoodItem from './components/AddFoodItem/AddFoodItem';
import './styles/global.css';
import './styles/variables.css';

function App() {
  const [foodList, setFoodList] = useState([]); // Lokal state for matvarer

  // Funksjon for å håndtere matvare som blir lagt til
  const handleAddFood = (newFood) => {
    console.log('Adding food:', newFood); // Logg for testing
    setFoodList((prev) => [...prev, newFood]); // Oppdater state med ny matvare
  };

  return (
    <div className="App">
      <Header />
      <main>
        <Routes>
          {/* Home page */}
          <Route path="/" element={<Home />} />

          {/* Authentication page */}
          <Route path="/auth" element={<AuthForm />} />
          
          {/* Food-related pages */}
          <Route path="/menu" element={<Menu foodList={foodList} />} />
          <Route path="/add-food" element={<AddFoodItem onSubmit={handleAddFood} />} />
          <Route path="/food-app" element={<FoodAppPage />} /> {/* Add the new route for food app */}

          {/* Other informational pages */}
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />

          {/* Catch-all route for error page */}
          <Route path="*" element={<Error />} />
        </Routes>
      </main>
      <Footer />
    </div>
  );
}

export default App;
