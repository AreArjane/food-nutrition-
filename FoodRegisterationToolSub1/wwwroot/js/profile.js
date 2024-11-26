


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

    /*document.querySelector('#cancelUpdate').addEventListener('click', function(event) {
        event.preventDefault();
        toggleView('profileInfo', 'main-content');
    })*/
});

function toggleView(hideId, showId) {

    hideId.style.display = 'none';
    showId.style.display = 'block';
}


function fetchProfileInfo(userId) {
    fetch(`http://localhost:5072/api/SuperUser/${userId}/profile`)
    .then(response => response.json())
    .then(data => {
        const profileInfoDiv = document.querySelector("#profile-content");
        profileInfoDiv.innerHTML = `
        <form id="personalInfoForm:${userId}">
            <label for="firstName">First Name</label>
            <input type="text" name="firstName" id="firstNameInput">
            <br>
            <label for="phoneNr">Phone Number</label>
            <input type="text" name="phoneNr" id="phoneNrInput">
            <br>
            <label for="dateOfBirth">Date of Birth</label>
            <input type="text" name="dateOfBirth" id="dateOfBirthInput">
            <br>
            <label for="email">E-Mail</label>
            <input type="text" name="email" id="emailInput">
            <br>
            <button type="submit" id="editPersonalInforForm">Edit Profile</button> 
            <br>
            <button type="button" id="cancelUpdate">Cancel</button>
        </form>
        `;

      
        const firstNameInput = document.querySelector('input[name="firstName"]');
        const phoneNrInput = document.querySelector('input[name="phoneNr"]');
        const dateOfBirthInput = document.querySelector('input[name="dateOfBirth"]');
        const emailInput = document.querySelector('input[name="email"]');

        firstNameInput.value = data.firstName;
        phoneNrInput.value = data.phoneNr;
        dateOfBirthInput.value = data.dateOfBirth;
        emailInput.value = data.email;

        document.querySelector('#cancelUpdate').addEventListener('click', function(event) {
            event.preventDefault();
            toggleView(profileInfoDiv, document.querySelector('#main-content'));
        });
    })
    .catch(error => console.error('Error fetching profile data:', error));
}