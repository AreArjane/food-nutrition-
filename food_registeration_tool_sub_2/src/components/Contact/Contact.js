import React, { useState } from 'react'; // Import React and useState hook for managing state

// Define the Contact component
const Contact = () => {
    // State variables to manage the form inputs 
    const [name, setName] = useState(''); // State for the user's name
    const [email, setEmail] = useState(''); // State for the user's email
    const [message, setMessage] = useState(''); // State for the user's message

    // Handle form submission
    const handleSubmit = (e) => {
      e.preventDefault(); // Prevent the default form submission behavior
      console.log("Form submitted with data:", { name, email, message }); // Log the submitted data for debugging

      // Clear the form fields after submission
      setName('');
      setEmail('');
      setMessage('');
    };
  
    return (
      <div className="contact-container">
        <h3 className="text-center">Contact Us</h3>
        <form onSubmit={handleSubmit} className="contact-form">
          <div className="form-group">
            <label htmlFor="name">Name:</label>
            <input
              type="text"
              id="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="email">Email:</label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="message">Message:</label>
            <textarea
              id="message"
              value={message}
              onChange={(e) => setMessage(e.target.value)}
            />
          </div>
          <button type="submit" className="submit-button">Send</button>
        </form>
      </div>
    );
  };
  
  export default Contact; // Export the Contact component for use in other parts of the application
