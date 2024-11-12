// src/components/Navbar.js
import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav>
      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/about">About</Link></li>
        <li><Link to="/foodlist">Food List</Link></li> {/* Legg til lenke til Food List */}
      </ul>
    </nav>
  );
}

export default Navbar;
