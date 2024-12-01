import React from 'react'; // Import React library

// Define the CategoryList component
// Props: 
// - categories: an array of category objects
// - onSelectCategory: a callback function to handle category selection
const CategoryList = ({ categories, onSelectCategory }) => {
    return (
        <div className="category-list">
            <h2>Kategorier</h2>
            <ul>
                {categories.map((category) => (
                    <li key={category.id} onClick={() => onSelectCategory(category)}>
                        {category.name}
                    </li>
                ))}
            </ul>
        </div>
    );
};
 
export default CategoryList; // Export the CategoryList component for use in other parts of the application
