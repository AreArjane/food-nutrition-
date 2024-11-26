import React from 'react';
import { Link } from 'react-router-dom';
import './Header.css';

const Header = () => {
    return (
        <header className="header-container">
            <div className="label-section">
                <Link to="/">Home</Link>
                <Link to="/nutrientapi">Nutrient</Link>
                <Link to="/foodapi">Food</Link>
                <Link to="/meals">Meals</Link>
                <Link to="#">In Norwegian</Link>
            </div>
            <div className="login-section">
                <Link to="/login">Login</Link>
                <Link to="/set">SetUp</Link>
            </div>
        </header>
    );
};

export default Header;
