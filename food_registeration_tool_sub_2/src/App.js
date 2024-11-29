import React from 'react';
import { Route, Routes } from 'react-router-dom';
//import Footer from './components/footer/Footer';
import Header from './components/Header/Header';
import AuthForm from './components/AuthForm/AuthForm';
import Home from './pages/Home/Home';
import Error from './pages/Error/Error';
import FoodAppPage from './pages/FoodAppPage/FoodAppPage'; 
import Menu from './components/Menu/Menu';
import About from './components/About/About';
import Contact from './components/Contact/Contact';
import AddFoodItem from './components/AddFoodItem/AddFoodItem';
import FoodList from './components/FoodList/FoodList'; // Importer FoodList
import './styles/global.css';
import './styles/variables.css';

function App() {
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
          <Route path="/menu" element={<Menu />} />
          <Route path="/add-food" element={<AddFoodItem />} />
          <Route path="/food-list" element={<FoodList />} /> {/* Legg til FoodList-ruten */}
          <Route path="/food-app" element={<FoodAppPage />} />
          {/* Other informational pages */}
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
          {/* Catch-all route for error page */}
          <Route path="*" element={<Error />} />
        </Routes>
      </main>
      {/* <Footer /> */}
    </div>
  );
}

export default App;