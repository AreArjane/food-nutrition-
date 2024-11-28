
/**
 * @brief DeQue Operating class work like to store the cached data for better simulant
 * 
 * @return item         -> is the data of the deque operation class
 * @return addFront     -> it add the element to the front to the array with the time complexity given O(n) it shift all element to the front and add new one at first place of the array.
 * @return addRear      -> add the element at the end of the array this has the time cpplexity of O(1)
 * @return removeFront  -> remove the first element of the array with the time complexity of O(n) due to shifting the element
 * @return removeRear   -> remove the last element of the array with the time complexity of O(1) its very fast
 * @return front()      -> return the first element of the array
 * @return rear()       -> returtn the last element of the array
 * @return isEmpty()    -> check if the array is empty
 * @return size()       -> Return the number of element in the deque array
 * @return update(index, element) -> updates the element of the specific index given with the time complexity of O(1)
 * @return getAll()     -> return all element of the array 
 * 
 * @note For further developing the Deque operation its better to use the circular algorithms as the most browser has 5M as the maks storage for cahing. where the circulal deque 
 * relay on the fixed data array and it make it faster for the front operation like add and remove when using modular artmstic operation instead of shifting the element
 */
class DequeOperation {
    items;
    constructor() { 
        this.items = [];
    }

    addFront(element) {
        this.items.unshift(element);
    }

    addRear(element) { 
        this.items.push(element);
    }

    removeFront() { 
        if (this.isEmpty()) {return "Deque is empty"; }

        return this.items.shift();
    }

    removeRear() { 
        if(this.isEmpty()) return "Deque is empty";

        return this.items.pop();
    }

    front() { 
        if(this.isEmpty()) return "Depeque are empty";
        return this.items[0];
    }
    rear() {
        if(this.isEmpty()) return "Depequ are empty";
        return this.items[this.items.length - 1];
    }

    isEmpty() { 
        return this.items.length === 0;
    }

    size() { 
        return this.items.length;
    }

    clear() {
        this.items = [];
    }

    update(index, elemet) { 
        if(index >= 0 && index < this.items.length) {
            this.items[index] = elemet;
        }
    }

    getAll() {
        return this.items;
    }
}

/**
 * DOM function ensure the content of html element are loaded before to apply any function
 * Dynamic allocation of main-content and profile-conetent section of the html element
 * call two function @fetchProfileInfo  --> return superuser profile data
 * @logoutUser --> clean the session data and return the user to the loging page
 * 
 */
document.addEventListener("DOMContentLoaded", function() {
    const userId        = window.currentUserID;
    const personalInfo  = document.querySelector("#personalInfo");
    const userType      = personalInfo.getAttribute("data-user-type");
    
    console.log(userId);

    personalInfo.addEventListener("click", function(event) {
        event.preventDefault();
        const profilecontent    = document.querySelector('#profile-content');
        const maincontent       = document.querySelector('#main-content');

        toggleView(maincontent, profilecontent);

        fetchProfileInfo(userId);
    });

        logoutUser();
});
/**
 * Dynamic toogle function set the Bulma contene is-hidden to add and remove 
 * for dynamic allocation. 
 */
function toggleView(hideId, showId) {

    hideId.classList.add("is-hidden");
    showId.classList.remove("is-hidden");
}


function fetchProfileInfo(userId) {
    fetch(`http://localhost:5072/api/SuperUser/${userId}/profile`)
    .then(response => response.json())
    .then(data => {
        const profileInfoDiv        =      document.querySelector("#profile-content");
        profileInfoDiv.innerHTML    = `
         <form id="personalInfoForm:${userId}" class="box">
                <div class="field">
                <label class="label" for="firstName">First Name</label>
                <div class="control">
                <input class="input" type="text" name="firstName" id="firstNameInput">
                </div>
                </div>
                <div class="field">
                <label class="label" for="phoneNr">Phone Number</label>
                <div class="control">
                <input class="input" type="text" name="phoneNr" id="phoneNrInput">
                </div>
                </div>
                <div class="field">
                <label class="label" for="dateOfBirth">DateofBirth</label>
                <div class="control">
                <input class="input" type="text" name="dateOfBirth" id="dateOfBirthInput">
                </div>
                </div>
                <div class="field">
                <label class="label" for="email">E-Mail</label>
                <div class="control">
                <input class="input" type="text" name="email" id="emailInput">
                </div>
                </div>
                <div class="field is-grouped">
                <div class="control">
                <button class="button is-primary" type="submit" id="editPersonalInforForm">Edit Profile</button>
                </div>
                <div class="control">
                <button class="button is-light" type="button" id="cancelUpdate">Cancel</button>
                </div>
                </div>
                </form>
        `;

      
        const firstNameInput    =    document.querySelector('input[name="firstName"]');
        const phoneNrInput      =    document.querySelector('input[name="phoneNr"]');
        const dateOfBirthInput  =    document.querySelector('input[name="dateOfBirth"]');
        const emailInput        =    document.querySelector('input[name="email"]');

        firstNameInput.value         =   data.firstName;
        phoneNrInput.value           =   data.phoneNr;
        dateOfBirthInput.value       =   data.dateOfBirth;
        emailInput.value             =   data.email;

        document.querySelector('#cancelUpdate').addEventListener('click', function(event) {
            event.preventDefault();
            toggleView(profileInfoDiv, document.querySelector('#main-content'));
        });
    })
    .catch(error => console.error('Error fetching profile data:', error));
} 


/**
 * Preform session clean with help of controller /Auth/logout
 * @returns Redirect URl to the Loging page
 */
function logoutUser() {

    const logout = document.querySelector("#logout");
    if(!logout) return;


    logout.addEventListener("click", async function(event) { 
    event.preventDefault();
    const FToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

    if(!FToken) {console.error("Anti Frogery token not found it");}

    try {

    const response = await fetch("http://localhost:5072/Auth/logout", {
        method : 'POST',
        headers : {
            'Content-Type' : 'application/json',
            'RequestVerificationToken' : FToken
        },
    });

    const data = await response.json();
    if(data.success) {
        window.location.href = data.redirectUrl;
    } else {
        console.log("Failed fetching data or there not responding data");
    }

} catch(error) {
    console.error('Error :', error);
}


});
}
/********************************************************** */
class API {
    base_ip;
    constructor(){
        this.base_ip = "http://localhost:5072";
    }
API_ENDPOINT = {

    GET_ALL_FOODS       : (page, pagesize, startwith) => `${this.base_ip}/foodapi/Foods?pagenumber=${page}&pagesize=${pagesize}&foodstartwith=${startwith}`,
    GET_SINGLE_FOOD     : (id) => `${base_ip}/food/${id}`,
    GET_ALL_NUTRIENT    : (page, pagesize, startwith) => `${this.base_ip}/nutrientapi/Nutrients?pagenumber=${page}&pagesize=${pagesize}&nutrientstartwith=${startwith}`,
    GET_SINGLE_NUTRIENT : (id) => `${base_ip}/nutrientapi/nutrient/${id}`,
    GET_SINGLE_MEALS    : (id) => `${base_ip}/api/${id}/Details`,

    GET_ALL_USERS       : `${this.base_ip}/AdimUser/allnromaluser`

};

}

/***************************************************************Main Process class for the All Users Profile with CRUD*****************************************************************************************************/
class superuserJS  {

    //tab area
    #tabs;
    #tabdescription;
    #activeModelType
    
    #maincontent;
    //pagination area
    #paginationcontainer;
    #paginationSearchButton;
    #previous;
    #next;

    #paginationcontent;
    #paginationcontentbutton;
    
    #generateInnerHtml;

    //data processing
    #dataContent;
    #metaData;
    #pageCach;

    constructor() {
        this.#tabs;
        this.#tabdescription = "";
        this.#maincontent            = [];
        this.#paginationcontainer    = [];
        this.#paginationcontent = null;
        this.#dataContent = new Map();
        this.#metaData = { pageSize : 10};
        this.#paginationcontentbutton = new Map([[0, "Create"], [1, "Update"], [2, "Delete"]]);
        this.#generateInnerHtml = "";
        this.#pageCach = new DequeOperation();


    }

   



/*****************************************************************Setter and getters******************************************************************************************************************************************* */
/**
 * 
 * @param {string } modelsType
 * Track the navbar on the left side, based on change the tags string on the top of the page 
 */
    setTabs(modelsType) { 
        switch(modelsType) {

            case 'dashboard':
                this.#tabs           =   ['static', 'search'];
                this.#tabdescription =   "Filter by"; 
                this.#paginationcontent = '';                               break;
            case 'customer':
                this.#tabs           = ['Name', 'PhoneNr'];
                this.#tabdescription = "Filter by"; 
                break;
            case 'foods':
                this.#tabs           = ['Name', 'Publication Date'];
                this.#tabdescription = "Filter by";                         break;
            case 'foodscategories':
                this.#tabs           = ['Name', 'Code'];  
                this.#tabdescription = "Filter by";                         break;
            case 'nutrients':
                this.#tabs           = ['Name', 'Rank', 'Unit'];   
                this.#tabdescription = "Filter by";                         break;
            case 'foodsnutrients':
                this.#tabs           = ['amount'];       
                this.#tabdescription = "Filter by";                         break;
            case 'meals':
                this.#tabs           = ['Name', 'Date publication'];
                this.#tabdescription = "Filter by";                         break;
            case 'manageuser':
                this.#tabs = [''];       
                this.#tabdescription  = "Action";                           break;
            case 'managefoods':
                this.#tabs = [''];
                this.#tabdescription = "Action";                            break;
            case 'managemeals':
                this.#tabs = [''];
                this.#tabdescription = "Action";                            break;
            default:
                this.#tabs = [''];  
                this.#tabdescription = "";                                  break;
        }
    }

/**
 * Getts and setts of the Class
 * Tabdescription            ->  is string display along with the tag display the name Filter By or Action
 * MainContent               ->  is the statics data display and hero welcome page ofr the superuser profile
 * setPaginationCotnainer    ->  is the the container of the search assoicated with the get method for each navbar action
 * setPaginationSerachButton ->  is the button in the pagnation conainer provide the GET method to API URL
 * setPaginationContent      ->  is the content of the pagination container display with paragraf 
 */
    getTabedescription() {
        return this.#tabdescription;
    }

    setMainContent(element) {
        this.#maincontent = Array.isArray(element) ? element : Array.from(element); 
    }
    setPaginationContainer(element) {
        this.#paginationcontainer = Array.isArray(element) ? element : Array.from(element);
    }
    setPaginationContent(pcontent) {
        if (!pcontent || !(pcontent instanceof HTMLElement)) {
            console.error('Invalid pagination content element:', pcontent);
        } else {
            console.log('Setting pagination content to:', pcontent);
            this.#paginationcontent = pcontent;
        }
    }
    setPaginationsearchButton(button) { 
        this.#paginationSearchButton = button;
    }
  
    getAllContent() { 
        return Array.from(this.#paginationcontent);
    }

    getPaginationButton() { 
        return Array.from(this.#paginationcontentbutton);
    }

    getmetaData() { 
        return this.#metaData;
    }
    getAllDataContent() { 
        return Array.from(this.#dataContent.entries());
    }
    getDataContentByKey() { 
        return this.#dataContent.get(key) || null;
    }

    getCachedPage(pagenumber) { 
        console.log("HERE IS FROM CACH", this.#pageCach.getAll());
        return this.#pageCach.getAll().find((page) => page.metaData.currentPage === pagenumber) || null;
    }

    setPrevious(previous) { 
        this.#previous = previous;
    }
    setNext(next) { 
        this.#next = next;
    }

    setActiveModelType(modeltype) { 
        this.#activeModelType = modeltype;
    }
    getActiveModelType(modeltype) { 
        return this.#activeModelType;
    }

/*****************************************************************FUNCTIONS******************************************************************************************************************************************************/

/**Update the GET method section data and set the previous and next buttom equal to the pagenumber of the API
 * User can navigate between the pages the pagesize are fixed here to the 10 element for each page. If there 100 element in database, means we have 10 pages. 
 */
    PaginationUpdate(modeltype) {
        const updatePage = (newPage) =>  {
            this.#metaData.currentPage = newPage;
            this.fetchGetDataDecision(modeltype, newPage, this.#metaData.pageSize);
            //this.InnerHtmlContent(modeltype);
        }
        this.#previous.addEventListener('click', () => {
            if(this.#metaData.currentPage > 1) {
                updatePage(this.#metaData.currentPage - 1);
            }});

        this.#next.addEventListener('click', () => {
            if(this.#metaData.currentPage < this.#metaData.totalPages) { 
                updatePage(this.#metaData.currentPage + 1);
            }});
    }
    /**
     * 
     * @param {*} mode
     * Mode is the string model type of the tags where the user click on navbar in the right side
     * Ensuring display the content based on the nav click. 
     */

    updateVisibility(mode) {

        switch(true) {
            case ['dashboard'].includes(mode): 
            this.#maincontent.forEach((element) => element.classList.remove('is-hidden'));
            this.#paginationcontainer.forEach((element)=> element.classList.add('is-hidden'));                  break;

            case ['customer', 'foods', 'foodscategories', 'nutrients', 'foodsnutrients', 'meals'].includes(mode):
                this.#maincontent.forEach((element)=>element.classList.add('is-hidden'));
                this.#paginationcontainer.forEach((element)=> element.classList.remove('is-hidden'));           
                break;

            default:
                this.#maincontent.forEach((element) => element.classList.remove('is-hidden'));
                this.#paginationcontainer.forEach((element)=> element.classList.add('is-hidden'));              break;
        }
    }
    /**
     * Next task function to process the data and manipulated after success fetching from the API. It put on the class variabel metadata and datacontent.
     * @param {*} jsonData data after success from API service
     * @param {*} pagenumber represent the pagenumber with the database element divided into the pagesize
     * @param {*} modelType The model type of the API where we need it to manage the data based on what it is FOOD, Nutrients, FOODNutrient or FooodCategories.
     * @returns 
     */

    processFetchingData(jsonData, pagenumber, modelType) { 
        if(!jsonData || typeof jsonData !== 'object' || !Array.isArray(jsonData.data)) {
            console.error("Invalid data format");
            return;
        }
        if (!this.#dataContent) {
            this.#dataContent = new Map();
        } else {
            this.#dataContent.clear();   
        }

        this.#metaData = { 
            totalCount  : jsonData.totalCount,
            totalPages  : jsonData.totalPages,
            currentPage : jsonData.currentPage,
            pageSize    : jsonData.pageSize
        };
         
        jsonData.data.forEach((item, key) => {
            const globalIndex = key +(pagenumber - 1) * jsonData.pageSize;
            this.#dataContent.set(globalIndex, item);
            console.log(this.#dataContent);
        });
        this.InnerHtmlContent(modelType);
        this.#pageCach.addRear({
            modelType,
            pagenumber,
            metaData: {...this.#metaData},
            dataContent: new Map(this.#dataContent),
        });
    }
    /**
     * Next Tasks function fetch the data from the API service after the models given in the GETdataLink. 
     * @param {*} link is the link to be fetched 
     * @param {*} mode the mode the type of the data, here it use it to passed to the process data function
     * @param {*} pageNumber represent the pagenumber assoicated with the datafetched to passed to the process function.
     * @returns 
     */
    async fetchGetlink(link, mode, pageNumber) { 
        console.log(`Fetching link: ${link} for mode: ${mode} on page: ${pageNumber}`);

        //const url = new URL(link);
        //const param = new URLSearchParams(url.search);
        const cachpage = this.getCachedPage(1);
       /*if(cachpage) {
            //console.log(`page ${param.get("pagenumber")} loaded from cach`);
            this.#metaData = cachpage.metadata;
            this.#dataContent = cachpage.datacontent;
            this.processFetchingData(
                { ...cachpage.metaData, data: Array.from(cachpage.dataContent.values()) },
                pagenumber
            );
            return;
        } else {*/

    try {

    const response = await fetch(link, {
                    method: 'GET',
                    headers : {'Content-Type' : 'application/json'}});
    if(response.ok) {
        const data = await response.json();
        console.log("From fetching link",data);
        this.processFetchingData(data, pageNumber, mode);
        return data; 
    } else { 
            console.log("An error ocurred during detching data. Check API call");
            return null;

    }} catch(error) {
        console.error("Error: ", error);
        return null; 
        }
    }
    /**
     * Function Decide the link to be fetched depend on the model given 
     * @param {*} mode represnet the button been clicked on the nav bar section on the right side
     * @param {*} page represent the pagenumber of the database to be fetched   
     * @param {*} pagesize represent the amount of the lement to be fetched, this is crucial when the superuser will decide how many food or how many record need to be fetched.
     * @param {*} startwith the filter method given to the API to based on the name of the record registered in the API database 
     * @param {*} id is the ID of the varoius API database models when the superuser fetching in order to make CRUD operations.
     * @returns 
     */

    async fetchGetDataDecision(mode, page, pagesize, startwith, id) {
        if(this.#activeModelType !== mode) { console.log(`skiping fetching modeType ${mode}a s its not an active model type`);return;}

           switch(mode) {
                case 'customer':
                    await this.fetchGetlink(`http://localhost:5072/AdminUser/allnormaluser?pagenumber=${page}&pagesize=${pagesize}`, mode, page);          break;
                case 'foods':
                    await this.fetchGetlink(`http://localhost:5072/foodapi/Foods?pagenumber=${page}&pagesize=${pagesize}`, mode, page);                     break;
                case 'nutrients':
                    await this.fetchGetlink(`http://localhost:5072/nutrientapi/Nutrients?pagenumber=${page}&pagesize=${pagesize}`, mode, page);             break;
                case 'foodscategories':
                    await this.fetchGetlink(`http://localhost:5072/foodcategory/FoodCategories?pagenumber=${page}&pagesize=${pagesize}`, mode, page);       break;
                case 'foodsnutrients':
                    break;
                case 'meals':
                    break;
                case 'singlefoods':
                    await this.fetchGetlink(GET_SINGLE_FOOD(id));                               break;
        
                case 'singlenutrient':
                    await this.fetchGetlink(GET_SINGLE_NUTRIENT(id));                           break;
        
                case 'singlemeals':
                    await this.fetchGetlink(GET_SINGLE_MEALS(id));                              break;
            }
    
    }
    /**
     * Function been processed after the dataprocess function done with applying the data to the metadata and datacontent. 
     * It fill up the container named pagination in the html document with the paragarafe dividev with tcontainer for each paragrafe containes the data from the API service.
     * @param {*} modelType 
     * @returns 
     */

    InnerHtmlContent(modelType) { 
        if (!this.#paginationcontent || !this.#paginationcontent.innerHTML) {console.error("Pagination content element is not properly set.");return;}

        this.#paginationcontent.innerHTML = '';
        
        if (this.#dataContent.size === 0) {this.#paginationcontent.innerHTML = `<p class="notification is-warning">No data available to display.</p>`;return;}
        

        //this.#paginationcontentbutton = this.#tabs.map((tab, index)=> tab);
        //var count = 0;var count_1 = 0;var count_2 = 0;var count_3 = 0;
        switch(modelType) {
            case 'customer': 

                for(let [key, item] of this.#dataContent.entries()){
                    console.log("Here from inner html",key, item);
            
                    this.#paginationcontent.innerHTML += `
                        <div class="box" id="${item.userId}">
                        <p><strong>User with Id:</strong> ${item.userId} and user type of : ${item.userType === 0 ? "NormalUser" : "SuperUser"}</p>
                        <p>Has Email registered<strong>${item.email}</strong> and phone number <strong>${item.phoneNr}</strong> 
                        FirstName : ${item.firstName} and LastName : ${item.lastName}</p>
                        <p>The home address ${item.homeAddress === null ? "was not provided" : item.homeAddress} where the ZIP ${item.postalCode === null ? "Not provided also" : item.postalCode}</p>
                        </div>`; 
                }
                break;
            

            case 'foods':
                
                for(let [key, item] of this.#dataContent.entries()){
                    console.log("Here from inner html",key, item);
                    const foodCategoryDescription = item.foodCategory?.description || "Unknown category";
                    const foodCategoryCode = item.foodCategory?.code || "N/A";

                
                    this.#paginationcontent.innerHTML += `
                        <div class="box" id="${item.foodId}">
                        <p><strong>Food with Id:</strong> ${item.foodId} and type of : ${item.dataType}</p>
                        <p>is ${item.description}, been published at ${item.publicationDate}</p>
                        <p>adherence to the categories of ${foodCategoryDescription} with the code given ${foodCategoryCode}</p>
                        </div>`; 
                }
                  
                break;
            
            case 'nutrients':
   
                for(let [key, item] of this.#dataContent.entries()){
                    console.log("Here from inner html",key, item);
                    
                    this.#paginationcontent.innerHTML += `
                        <div class="box" id="${item.id}">
                        <p><strong>Nutrients with Id:</strong> ${item.id} and know by the name: ${item.name}</p>
                        <p>has a ${item.unitName} unit name and has an NBR value of ${item.nutrientNbr}</p>
                        <p>Given the rank of ${item.rank}</p>
                        </div>`;
                } 
                
                break;

            case 'foodscategories': 
                
                for(let [key, item] of this.#dataContent.entries()){
                    console.log("Here from inner html",key, item);
                    
                    this.#paginationcontent.innerHTML += `
                        <div class="box" id="${item.id}">
                        <p><strong>Category with Id:</strong> ${item.id} and code : ${item.code}</p>
                        <p>Its Name is <strong>${item.description}</strong></p>`; 
                }
                break;

            case 'foodnutrients': break;
            case 'meals': break;

        }

    }

    
          

    updateTags(listSelector) {
        const listTags = document.querySelector(listSelector);
        if(!listTags) {console.error(`List container cannot be found it ${lisTags}`); return;}

        listTags.innerHTML = '';

        const ul = document.createElement('ul');
        this.#tabs.forEach((tab, index) => {
            const li = document.createElement('li');
            if(index === 0) {li.classList.add('is-active');}

            li.id = `tab-${index + 1}`;
            const a = document.createElement('a');
            a.textContent = tab;

            a.addEventListener('click', () => {
                this.handleTabClick(li, tab, index);

            });
            li.append(a);
            ul.append(li);
        });
        listTags.append(ul);
        
    }

    handleTabClick(clickedTab, tabName, tabIndex) {
        const allTabs = clickedTab.parentElement.querySelectorAll('li');
        allTabs.forEach((tab) => tab.classList.remove('is-active'));

        clickedTab.classList.add('is-active');

        console.log(`tab clicked: ${tabName} Index ${tabIndex}`);
    }

    cachPageData(pageNumber, metadata, contentdata) {

        this.#pageCach.addRear({
            pageNumber,
            metadata : {...metadata},
            contentdata: new Map(contentdata),
        });
    } 


}


/********************************************************************************************************** */
const superuser = new superuserJS();

document.querySelector('.models-container').addEventListener('click', (event) => {
    //define the element
    const tabdescription = document.querySelector("#tab-lists-description");
    const maincontent = document.querySelectorAll('.main-content-1');
    const paginationcontent = document.querySelectorAll('.pagination-container');
    const searchbutton = document.querySelector("#search-button-pagination");
    const paginationNext = document.querySelector('#pagination-next');
    const paginationPrevious = document.querySelector('#pagination-previous');
    
    const listItems = event.target.closest('li[data-model]');
    
    if(listItems) {
        const modelType = listItems.getAttribute('data-model');

        superuser.setActiveModelType(modelType);
        /**HERE IS THE CONTENT */
        const pcontent = document.querySelector('#pagination-content');
        console.log('pcontent:', pcontent); 
        superuser.setPaginationContent(pcontent);

        document.querySelectorAll('.models-lists li a').forEach((items) => {
            items.classList.remove('is-active');        });

            listItems.querySelector('a').classList.add('is-active');

            superuser.setTabs(modelType);
            tabdescription.textContent = superuser.getTabedescription();
            superuser.updateTags('#tab-lists');

            //setting the element
            superuser.setMainContent(maincontent);
            superuser.setPaginationContainer(paginationcontent);
            superuser.updateVisibility(modelType);
            superuser.setNext(paginationNext);
            superuser.setPrevious(paginationPrevious);

            //call the functions
            superuser.setPaginationsearchButton(searchbutton);
            console.log(modelType);

            superuser.fetchGetDataDecision(modelType, 1 , 10).then(() => {
                superuser.PaginationUpdate(modelType);
        
        });      
    }
});

window.addEventListener('DOMContentLoaded', () => {
    const activeModelItem = document.querySelector('.models-lists li a.is-active');
    const tabdescription = document.querySelector("#tab-lists-description");
    if (activeModelItem) {
        const modelType = activeModelItem.parentElement.getAttribute('data-model');

        superuser.setTabs(modelType);
        tabdescription.textContent = superuser.getTabedescription();
        superuser.updateTags('#tab-lists');
    }
});
/**
 * Special function to fetch the static data and added to the fronted page of the superuser 
 * The sattic data contains how many record there is in the databse registered. This could further developed to give like news data content and information.
 */
function AppendStatic() {
    document.addEventListener("DOMContentLoaded", async function(event) {

        event.preventDefault();

        const staticElement = document.querySelector(".static-element");

        try {
        const response = await fetch("http://localhost:5072/Static/StaticResult", { 
            method: 'GET',
            headers : {"Content-Type": 'application/json'}
        });

        if(response.ok)  {

            const data = await response.json();

            const staticounter = data.staticCounter;

            Object.entries(staticounter).forEach(([index, static]) => {

                const article   = document.createElement('article');
                const pTitle    = document.createElement('p');
                const pSubtitle = document.createElement('p');

                article.className = 'tile is-child box';
                
                pTitle.className = 'title';
                pTitle.textContent = `${static}`;

                pSubtitle.className = 'subtitle';
                pSubtitle.textContent = `${index.charAt(0).toUpperCase() + index.slice(1)}`;

                article.append(pTitle);
                article.append(pSubtitle);
                staticElement.append(article);
                
            });

        } else { 

            console.error('Erorr : ', error);
        }} catch(error) {
            console.error("Error Fetching data: ", error);

        }
    });

}AppendStatic();
/**********************************************************************END of the main class of SuperUser Profile******************************************************************************************************** */




document.addEventListener('DOMContentLoaded', () => {
    // Handle Navbar Burger
    const navbarBurger = document.querySelector('.navbar-burger[data-target="navbar-menu"]');
    const navbarMenu = document.getElementById('navbar-menu');

    if (navbarBurger && navbarMenu) {
        navbarBurger.addEventListener('click', () => {
            navbarBurger.classList.toggle('is-active');
            navbarMenu.classList.toggle('is-active');
        });
    }
    //Burger functionn given with the Bulma CSS to handle the mobil view for the navbar section.
    const sidebarBurger = document.querySelector('.sidebar-burger[data-target="sidebar-menu"]');
    const sidebarMenu = document.getElementById('sidebar-menu');

    if (sidebarBurger && sidebarMenu) {
        sidebarBurger.addEventListener('click', () => {
            sidebarBurger.classList.toggle('is-active');
            sidebarMenu.classList.toggle('is-hidden');
        });
    }

    // Ensure Sidebar Visibility on Resize
    const handleResize = () => {
        if (window.innerWidth >= 1024) { // Desktop view
            if (sidebarMenu) {
                sidebarMenu.classList.remove('is-hidden');
            }
        } else { // Mobile view
            if (!sidebarBurger.classList.contains('is-active') && sidebarMenu) {
                sidebarMenu.classList.add('is-hidden');
            }
        }
    };

    window.addEventListener('resize', handleResize);
    handleResize(); // Run on load
});






