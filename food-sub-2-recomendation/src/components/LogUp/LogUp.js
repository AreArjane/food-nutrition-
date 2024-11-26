import React from 'react';
import './LogUp.css';

const LogUp = () => {
    return (
        <div className="logup-container">
            <h1>Select User Type</h1>
            <div className="user-types">
                <img
                    src="https://res.cloudinary.com/dnwuimgi4/image/upload/v1731605768/sigupwelcome_x6xkzk.svg"
                    className="highlight"
                    alt="Signup Highlight"
                />
                <a href="/normaluser" className="user-type-button">NormalUser</a>
                <a href="/superuser" className="user-type-button">SuperUser</a>
                <a href="/adminuser" className="user-type-button">AdminUser</a>
            </div>
        </div>
    );
};

export default LogUp;
