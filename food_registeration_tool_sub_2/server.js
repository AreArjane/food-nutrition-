const express = require('express');
const cors = require('cors');  // Importer cors
const app = express();
const port = 5072;

// Bruk CORS-middleware
app.use(cors());

// Middleware for å parse JSON-forespørsler
app.use(express.json());

// Simulert database (array)
let nutrients = [
    { id: 1, name: 'Vitamin A', unit: 'mg', number: 900, rank: 1 },
    { id: 2, name: 'Vitamin C', unit: 'mg', number: 90, rank: 2 },
];

// Håndter GET-forespørsel for å hente næringsstoffene
app.get('/nutrientapi/Nutrients', (req, res) => {
    res.json(nutrients);
});

// Håndter POST-forespørsel for å legge til næringsstoff
app.post('/nutrientapi/Nutrients', (req, res) => {
    console.log(req.body);  // Sjekk hva som kommer fra frontend

    const { name, unit, number, rank } = req.body;

    // Sjekk om alle nødvendige felt er sendt
    if (!name || !unit || !number || !rank) {
        return res.status(400).json({ error: 'All fields are required' });
    }

    // Opprett nytt næringsstoff og legg det til i arrayen
    const newNutrient = { id: Date.now(), name, unit, number, rank };
    nutrients.push(newNutrient);

    // Send tilbake det nye næringsstoffet som respons
    return res.status(201).json(newNutrient);
});

// Start serveren
app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
