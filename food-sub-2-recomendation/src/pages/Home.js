import React from 'react';
import './/Home.css';

const Home = () => {
    return (
        <div>
            <div className="gif-wrapper">
                <img
                    src="https://res.cloudinary.com/dnwuimgi4/image/upload/v1731588454/index_ffcn0m.gif"
                    alt="Animated Design"
                    className="animated-gif"
                />
                <div className="search-overlay">
                    <input
                        type="text"
                        id="query"
                        className="search-input"
                        placeholder="Enter food or meal name"
                        aria-label="Search query"
                    />
                    <button id="searchButton" className="search-button" type="button">
                        Search
                    </button>
                </div>
            </div>
            <div id="results" className="results-section" style={{ overflowX: 'auto' }}>
                {/* JavaScript will populate search results here */}
            </div>
        </div>
    );
};

export default Home;
