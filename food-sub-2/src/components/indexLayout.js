import React from 'react';
import Header from './Header';
import Footer from './Footer';
import './Layout.css';
const IndexLayout = ({ children }) => {
    return (
        <div>
            <Header />
            <main role="main">{children}</main>
            <Footer />
        </div>
    );
};

export default IndexLayout;
