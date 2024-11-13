document.querySelector("#searchButton").addEventListener("click", async function() {
    const query = document.querySelector("#query").value.trim();

    if (!query) {
        alert("Please enter a valid search query.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5072/api/search?query=${encodeURIComponent(query)}`);
        
        if (!response.ok) {
            alert("Error fetching data.");
            return;
        }

        const data = await response.json();

        const resultsDiv = document.querySelector("#results");
        resultsDiv.innerHTML = ""; // Clear any previous results

        // Check if there are results in Foods or Meal
        if (data.foods.length || data.meal.length) {
            // Create a table structure
            let tableHtml = `
            
                <table border="1" cellpadding="5" cellspacing="0">
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Name</th>
                            <th>Details</th>
                        </tr>
                    </thead>
                    <tbody>
                    
            `;

            // Add rows for each food item
            data.foods.forEach(food => {
                tableHtml += `
                    <tr>
                        <td>${food.dataType}</td>
                        <td>${food.description}</td>
                        <td>${food.publicationDate}</td>
                    </tr>
                `;
            });

            // Add rows for each meal item
            data.meal.forEach(meal => {
                tableHtml += `
                    <tr>
                        <td>Meal</td>
                        <td>${meal.Name}</td>
                        <td>-</td>
                    </tr>
                `;
            });

            tableHtml += `
                    </tbody>
                </table>
            `;

            // Insert the table into the results div
            resultsDiv.innerHTML = tableHtml;
        } else {
            resultsDiv.innerHTML = "<p>No results found</p>";
        }
    } catch (error) {
        console.error("Error fetching data:", error);
    }
});
