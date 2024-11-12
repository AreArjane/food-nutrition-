// src/components/header/Header.js
import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => (
  <nav className="navbar navbar-expand-lg navbar-light bg-light">
    <Link className="navbar-brand" to="/">Mat App</Link>
    <div className="collapse navbar-collapse">
      <ul className="navbar-nav mr-auto">
        <li className="nav-item">
          <Link className="nav-link" to="/">Matliste</Link>
        </li>
        <li className="nav-item">
          <Link className="nav-link" to="/add-food">Legg til Mat</Link>
        </li>
      </ul>
    </div>
  </nav>
);

export default Header;
