const request = require('supertest');
const app = require('../app'); // Juster banen hvis nødvendig

describe('User CRUD Operations', () => {
    test('Sample test - true is true', () => {
        expect(true).toBe(true);

    test('Create User - Success', async () => {
        const response = await request(app).post('/users').send({
            username: 'testuser',
            email: 'test@example.com',
            password: 'password123'
        });
        expect(response.status).toBe(201);
        userId = response.body.id; // Lagre ID-en for videre tester
    });

    test('Create User - Username Taken', async () => {
        const response = await request(app).post('/users').send({
            username: 'testuser',
            email: 'new@example.com',
            password: 'password123'
        });
        expect(response.status).toBe(400);
        expect(response.body.message).toBe('Username already exists');
    });

    test('Read User - Success', async () => {
        const response = await request(app).get(`/users/${userId}`);
        expect(response.status).toBe(200);
        expect(response.body.username).toBe('testuser');
    });

    test('Read User - Not Found', async () => {
        const response = await request(app).get('/users/invalid-id');
        expect(response.status).toBe(404);
        expect(response.body.message).toBe('User not found');
    });

    test('Update User - Success', async () => {
        const response = await request(app).put(`/users/${userId}`).send({ email: 'updated@example.com' });
        expect(response.status).toBe(200);
    });

    test('Update User - Invalid Data', async () => {
        const response = await request(app).put(`/users/${userId}`).send({ email: 'invalid-email' });
        expect(response.status).toBe(400);
        expect(response.body.message).toBe('Invalid email format');
    });

    test('Delete User - Success', async () => {
        const response = await request(app).delete(`/users/${userId}`);
        expect(response.status).toBe(204);
    });

    test('Delete User - Not Found', async () => {
        const response = await request(app).delete('/users/invalid-id');
        expect(response.status).toBe(404);
        expect(response.body.message).toBe('User not found');
        
    });
});
});
