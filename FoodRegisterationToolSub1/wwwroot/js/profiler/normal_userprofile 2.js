
/**
 * DOM function ensure the content of html element are loaded before to apply any function
 * Dynamic allocation of main-content and profile-conetent section of the html element
 * call two function @fetchProfileInfo  --> return normaluser profile data
 * @logoutUser --> clean the session data and return the user to the loging page
 * 
 */

document.addEventListener("DOMContentLoaded", function() {
    const userId = window.currentUserID;
    const personalInfo = document.querySelector("#personalInfo");
    const userType = personalInfo.getAttribute("data-user-type");
    
    console.log(userId);

    personalInfo.addEventListener("click", function(event) {
        event.preventDefault();
        const profilecontent = document.querySelector('#profile-content');
        const maincontent = document.querySelector('#main-content');

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
    fetch(`http://localhost:5072/api/NormalUser/${userId}/profile`)
    .then(response => response.json())
    .then(data => {
        const profileInfoDiv = document.querySelector("#profile-content");
        profileInfoDiv.innerHTML = `
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
                <label class="label" for="lastName">Last Name</label>
                <div class="control">
                <input class="input" type="text" name="lastName" id="lastNameInput">
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

      
        const firstNameInput = document.querySelector('input[name="firstName"]');
        const phoneNrInput = document.querySelector('input[name="phoneNr"]');
        const LastName = document.querySelector('input[name="lastName"]');
        const emailInput = document.querySelector('input[name="email"]');

        firstNameInput.value = data.firstName;
        phoneNrInput.value = data.phoneNr;
        LastName.value = data.lastName;
        emailInput.value = data.email;

        document.querySelector('#cancelUpdate').addEventListener('click', function(event) {
            event.preventDefault();
            toggleView(profileInfoDiv, document.querySelector('#main-content'));
        });
    })
    .catch(error => console.error('Error fetching profile data:', error));
}

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
