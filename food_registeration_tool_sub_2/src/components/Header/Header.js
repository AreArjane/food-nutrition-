import React from 'react';
import logo from '../../logo.svg'; // Importer logo.svg
import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <header className="header">
      <div className="logo">
        <Link to="/">
          <img src={logo} className="logoImage" alt="Food App Logo" />
        </Link>
      </div>
      <nav className="nav">
        <ul>
        <li><Link to="/">Home</Link></li>
          <li><Link to="/menu">Menu</Link></li>
          <li><Link to="/about">About</Link></li>
          <li><Link to="/contact">Contact</Link></li>
          <li><Link to="/auth" className="loginBtn">Login</Link></li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;