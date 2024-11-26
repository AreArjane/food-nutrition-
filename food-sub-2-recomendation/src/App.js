import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import IndexLayout from './components/indexLayout';
import Home from './pages/Home';
import Nutrient from './pages/Nutrient';
import Food from './pages/Food';
import Meals from './pages/Meals';
import Privacy from './pages/Privacy';

import Layout_Profile from './components/Layout_Profile';
import Login from './components/Login/Login';
import LogUp from './components/LogUp/LogUp';
import Profile from './pages/Profile';

import FoodTable from './components/FoodTable';
import NutrientsTable from './components/NutrientsTable';

const App = () => {
    return (
        <Router>
            <IndexLayout>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/nutrientapi" element={<NutrientsTable />} />
                    <Route path="/foodapi" element={<FoodTable />} />
                    <Route path="/meals" element={<Meals />} />
                    <Route path="/privacy" element={<Privacy />} />

                    <Route path="/login" element={<Login />} />
                    <Route path="/logup" element={<LogUp />} />
                    <Route path="/profile" element={<Profile />} />
                </Routes>
            </IndexLayout>
        </Router>
    );
};

export default App;
