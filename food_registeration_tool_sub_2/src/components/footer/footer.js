import React from 'react';
import logo from '../../logo.svg'; // Importer logo.svg
import { Link } from 'react-router-dom';


const Footer = () => {
  return (
    <footer className="footer">
      <div className="logo">
        <Link to="/">
          <img src={logo} className="logoImage" alt="Food App Logo" />
        </Link>
        </div>
      <div className="content">
        <p>&copy; 2024 Food App. All rights reserved.</p>
        <nav>
          <ul>
            <li><Link to="/">Home</Link></li>
            <li><a href="/about">About</a></li>
            <li><a href="/contact">Contact</a></li>
            <li><a href="/privacy">Privacy Policy</a></li>
            <li><a href="/terms">Terms of Service</a></li>
          </ul>
        </nav>
      </div>
    </footer>
  );
};

export default Footer;