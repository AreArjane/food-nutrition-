import React from 'react';

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

export default CategoryList;