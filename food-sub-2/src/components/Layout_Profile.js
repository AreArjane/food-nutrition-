import React from 'react';
import './Layout_Profile.css';

const Layout_Profile = ({ children }) => {
    return (
        <div className="layout-container">
            <nav className="navbar is-white is-spaced">
                <div className="container">
                    <div className="navbar-brand">
                        <h1 className="title is-5 has-text-white">UserID: {window.currentUserID || 'Guest'}</h1>
                        <button
                            className="navbar-burger"
                            aria-label="menu"
                            aria-expanded="false"
                            data-target="navbar-menu"
                        >
                            <span aria-hidden="true"></span>
                            <span aria-hidden="true"></span>
                            <span aria-hidden="true"></span>
                        </button>
                    </div>
                    <div id="navbar-menu" className="navbar-menu">
                        <div className="navbar-start">
                            <a className="navbar-item">Home</a>
                            <a className="navbar-item">Personal Information</a>
                            <a className="navbar-item">Security</a>
                            <a className="navbar-item">Management</a>
                            <a className="navbar-item">Document Order</a>
                            <a className="navbar-item" id="logout">Logout</a>
                        </div>
                    </div>
                </div>
            </nav>
            <main>{children}</main>
            <footer className="footer">Hello from footer!</footer>
        </div>
    );
};

export default Layout_Profile;
