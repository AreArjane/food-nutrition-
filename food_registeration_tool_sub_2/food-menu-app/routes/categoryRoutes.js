const express = require('express');
const Category = require('../models/Category');
const router = express.Router();

// Opprett en kategori
router.post('/', async (req, res) => {
    const category = new Category(req.body);
    try {
        await category.save();
        res.status(201).send(category);
    } catch (error) {
        res.status(400).send(error);
    }
});

// Les alle kategorier
router.get('/', async (req, res) => {
    try {
        const categories = await Category.find();
        res.status(200).send(categories);
    } catch (error) {
        res.status(500).send(error);
    }
});

// Oppdater en kategori
router.patch('/:id', async (req, res) => {
    try {
        const category = await Category.findByIdAndUpdate(req.params.id, req.body, { new: true });
        res.status(200).send(category);
    } catch (error) {
        res.status(400).send(error);
    }
});

// Slett en kategori
router.delete('/:id', async (req, res) => {
    try {
        await Category.findByIdAndDelete(req.params.id);
        res.status(204).send();
    } catch (error) {
        res.status(500).send(error);
    }
});

module.exports = router;