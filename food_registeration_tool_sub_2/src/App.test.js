// Import necessary modules from '@testing-library/react' for testing
import { render, screen } from '@testing-library/react';

// Import the App component to be tested
import App from './App';

// Define a test to check if a specific text exists in the App component
test('renders learn react link', () => {

  // Render the App component in a test environment
  render(<App />);

  // Search for an element containing the text "learn react"
  // The text is not case-sensitive due to the regex flag /i
  const linkElement = screen.getByText(/learn react/i);

  // Expect the element to be present in the document
  expect(linkElement).toBeInTheDocument();
});
