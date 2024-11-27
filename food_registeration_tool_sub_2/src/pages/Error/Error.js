import React from 'react';
import { Link } from 'react-router-dom';


const Error = () => {
  return (
    <div className="error-container">
      <h1>Oops! Something went wrong.</h1>
      <p>We couldn't find the page you were looking for.</p>
      <Link to="/" className="error-link">Go back to Home</Link>
    </div>
  );
};

export default Error;