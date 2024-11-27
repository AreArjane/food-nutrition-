document.getElementById("searchButton").addEventListener("click", async function() {
    const query = document.querySelector("#query").value;
    if (!query) {
        alert("Please enter a search query.");
        return;
    }

    // Fetch data from the search API
    const response = await fetch(`/api/search?query=${encodeURIComponent(query)}`);
    const data = await response.json();

    const resultsDiv = document.getElementById("results");
    resultsDiv.innerHTML = ""; // Clear previous results

    // Display results
    if (data.Foods.length || data.Meal.length) {
        data.Foods.forEach(food => {
            resultsDiv.innerHTML += `<div><strong>Food:</strong> ${food.Description} (Type: ${food.DataType}, Published: ${food.PublicationDate})</div>`;
        });
        data.Meal.forEach(meal => {
            resultsDiv.innerHTML += `<div><strong>Meal:</strong> ${meal.Name}</div>`;
        });
    } else {
        resultsDiv.innerHTML = "<p>No results found</p>";
    }
});