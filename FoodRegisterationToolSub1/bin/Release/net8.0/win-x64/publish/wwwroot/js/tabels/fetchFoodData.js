let pagenumber = 1; 
let pagesize = 20; 
let isLoading = false; 

async function fetchFoodData() {
    if (isLoading) return;
    isLoading = true;

    const foodTableBody = document.querySelector("#foodTableBody");
    const loadingMessage = document.querySelector("#loadingMessage");
    const loadMoreButton = document.querySelector("#loadMoreButton");

    try {
        loadingMessage.style.display = "block";
        loadMoreButton.disabled = true; 

        const response = await fetch(`http://localhost:5072/foodapi/Foods?pagenumber=${pagenumber}&pagesize=${pagesize}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });
        const data = await response.json();

        loadingMessage.style.display = "none";


        if (data) {
            data.data.forEach((fq) => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${fq.foodId}</td>
                    <td>${fq.dataType}</td>
                    <td>${fq.description}</td>
                    <td>${fq.publicationDate}</td>
                    <td>${fq.foodCategory ? fq.foodCategory.description : "N/A"}</td>
                    <td>${fq.foodCategory ? fq.foodCategory.code : "N/A"}</td>
                `;

                foodTableBody.append(row);
            });
            pagenumber++;
            
        } else {
            const noDataRow = document.createElement("tr");
            noDataRow.innerHTML = `<td colspan="6" class="text-center">No data available</td>`;
            foodTableBody.append(noDataRow);
            loadMoreButton.style.display = "none";
            console.error("No DATA AVALIABLE");
        }
    } catch (error) {
        loadingMessage.style.color = "red";
        loadingMessage.textContent = "Error fetching data: " + error.message;
    } finally {
        isLoading = false;
        loadMoreButton.disabled = false;
    }
}
document.querySelector("#loadMoreButton").addEventListener("click", fetchFoodData);

fetchFoodData();
