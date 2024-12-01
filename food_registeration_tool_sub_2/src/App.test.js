// Importerer nødvendige moduler fra '@testing-library/react' for testing
import { render, screen } from '@testing-library/react';

// Importerer App-komponenten som skal testes
import App from './App';

// Definerer en test for å sjekke om en bestemt tekst finnes i App-komponenten
test('renders learn react link', () => {

  // Render App-komponenten i et testmiljø
  render(<App />);

  // Søker etter et element som inneholder teksten "learn react"
  // Teksten er ikke case-sensitiv på grunn av regex-flagget /i
  const linkElement = screen.getByText(/learn react/i);

  // Forventer at elementet skal finnes i dokumentet
  expect(linkElement).toBeInTheDocument();
});
