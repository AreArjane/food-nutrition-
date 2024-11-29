const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');
const foodRoutes = require('./routes/foodRoutes');
const categoryRoutes = require('./routes/categoryRoutes');

const app = express();
const PORT = process.env.PORT || 3000;

// Koble til MongoDB
mongoose.connect('mongodb://localhost:27017/foodmenu', { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('MongoDB connected'))
    .catch(err => console.error(err));

// Middleware
app.use(cors());
app.use(bodyParser.json());

// Ruter
app.use('/api/food-items', foodRoutes);
app.use('/api/categories', categoryRoutes); // Sørg for at denne linjen er til stede

// Start server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});