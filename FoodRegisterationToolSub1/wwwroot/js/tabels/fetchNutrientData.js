let pagenumber = 1; 
let pagesize = 20; 
let isLoading = false; 

async function fetchNutrientData() {
    if (isLoading) return;
    isLoading = true;

    const nutrientTableBody = document.querySelector("#nutrientTableBody");
    const loadingMessage = document.querySelector("#loadingMessage");
    const loadMoreButton = document.querySelector("#loadMoreButton");

    try {
        loadingMessage.style.display = "block";
        loadMoreButton.disabled = true; 

        const response = await fetch(`http://localhost:5072/nutrientapi/Nutrients?pagenumber=${pagenumber}&pagesize=${pagesize}`, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });
        const data = await response.json();

        loadingMessage.style.display = "none";


        if (data && data.data.length > 0) {
            data.data.forEach((nq) => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${nq.id}</td>
                    <td>${nq.name}</td>
                    <td>${nq.unitName}</td>
                    <td>${nq.nutrientNbr}</td>
                    <td>${nq.rank}</td>
                    
                `;

                nutrientTableBody.append(row);
            });
            pagenumber++;
            pagesize += 20;
        } else {
            const noDataRow = document.createElement("tr");
            noDataRow.innerHTML = `<td colspan="6" class="text-center">No data available</td>`;
            nutrientTableBody.append(noDataRow);
            loadMoreButton.style.display = "none";
        }
    } catch (error) {
        loadingMessage.style.color = "red";
        loadingMessage.textContent = "Error fetching data: " + error.message;
    } finally {
        isLoading = false;
        loadMoreButton.disabled = false;
    }
}
document.querySelector("#loadMoreButton").addEventListener("click", fetchNutrientData);

fetchNutrientData();
