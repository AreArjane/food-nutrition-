import React from 'react'; // Import React library
import { Link } from 'react-router-dom'; // Import Link for navigation

// Define the Error component
const Error = () => {
  return (
    <div className="error-container">
      <h1>Oops! Something went wrong.</h1>
      <p>We couldn't find the page you were looking for.</p>
      <Link to="/" className="error-link">Go back to Home</Link>
    </div>
  );
};

export default Error; // Export the Error component for use in other parts of the application
