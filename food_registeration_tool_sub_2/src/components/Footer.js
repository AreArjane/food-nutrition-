// src/components/Footer.js
import React from 'react';

function Footer() {
  return (
    <footer>
      <div>
        &copy; {new Date().getFullYear()} Food Registration Tool
      </div>
    </footer>
  );
}

export default Footer;