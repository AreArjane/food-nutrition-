// App.js
import React from 'react';
import './App.css'; // Looks for App.css in the same folder as App.js

import FoodList from './components/FoodList';

function App() {
  return (
    <div className="App">
      <h1>Food List App</h1>
      <FoodList />
    </div>
  );
}

export default App;


