import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Importer Routes riktig
import Navbar from './components/Navbar';  // Importer Navbar-komponenten
import FoodList from './components/FoodList';  // Make sure this import is correct
import AddFood from './components/AddFood';
import Home from './components/Home';
import About from './components/About';

function App() {
  return (
    <Router>
      {/* Navbar vises på toppen av hver side */}
      <Navbar />
      <AddFood> </AddFood>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<About />} />
        <Route path="/foodlist" element={<FoodList />} /> {/* Rute for FoodList */}
      </Routes>
    </Router>
  );
}

export default App;