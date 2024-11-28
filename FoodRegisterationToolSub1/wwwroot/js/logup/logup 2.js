const userTypeMapping = {
    "NormalUser": 0,
    "SuperUser": 1,
    "AdminUser": 2
};
 let userTypeValue = null;

document.addEventListener("DOMContentLoaded", function(){

    
    
document.querySelectorAll('.user-type-button').forEach(button => { 
    button.addEventListener('click', function (event) {
        event.preventDefault();

        document.querySelectorAll('.user-type-button').forEach( btn => btn.classList.remove('selected'));
        this.classList.add('selected');


        const usertype = this.getAttribute('data-user-type');
        console.log(usertype);
        userTypeValue = userTypeMapping[usertype];
        console.log(userTypeValue);
        //document.querySelector("#userType").value = userTypeValue;
        
        
        const logupform = document.querySelector("#LogUpForm");

        if(userTypeValue === 0)  {
            

            if(logupform) {
            logupform.style.display = 'block';
            console.log("#LogUpForm is set to display: block");

            // Populate the form with updated fields
            logupform.innerHTML = `
            <h2>Login</h2>
            <form id="LogingFormElement" style="display: block;">
                
                <div>
                    <input type="text" id="firstname" name="firstname" placeholder="Write your first name" required />
                </div>
                <div>
                    <input type="text" id="lastname" name="lastname" placeholder="Write your last name" required />
                </div>
                <div>
                    <input type="text" id="phonenr" name="phonenr" placeholder="Write your phone number" required />
                </div>
                <div>
                    <input type="email" id="email" name="email" placeholder="Write your email address" required />
                </div>
                <div>
                    <input type="password" id="password" name="password" placeholder="Enter your password" required />
                </div>
                <input type="hidden" id="userType" name="userType" value="${userTypeValue}" />
                <button type="button" onClick="submitLoginForm()">Login</button>
            </form>
            <div id="errorMessages" style="color:red; display: none;"></div>
            `;
        } else {
            console.log("Element #logupform not founden") }
        } else if(userTypeValue === 1) { 
            if(document.querySelector("#LogingFormElement")) {
            document.querySelector("#LogingFormElement").style.display = 'none';}

            logupform.style.display = 'block';

            logupform.innerHTML = `
            <h2>Login</h2>
            <form id="LogingFormElement" style="display: block;">
                
                <div>
                    <input type="text" id="firstname" name="firstname" placeholder="Write your first name" required />
                </div>
                <div>
                    <input type="text" id="dateofbirth" name="dateofbirth" placeholder="Write your dateofbirth" required />
                </div>
                <div>
                    <input type="text" id="phonenr" name="phonenr" placeholder="Write your phone number" required />
                </div>
                <div>
                    <input type="email" id="email" name="email" placeholder="Write your email address" required />
                </div>
                <div>
                    <input type="password" id="password" name="password" placeholder="Enter your password" required />
                </div>
                <input type="hidden" id="userType" name="userType" value="${userTypeValue}" />
                <button type="button" onClick="submitLoginForm()">Login</button>
            </form>
            <div id="errorMessages" style="color:red; display: none;"></div>
            `;
        }
    

    });
});

});

/******************************************************************************Functions****************************************************************************************************/
document.addEventListener("DOMContentLoaded", function () {
    async function submitLoginForm() { 
        console.log(userTypeValue);


        if(userTypeValue === 0) {

        const firstname = document.querySelector("#firstname").value;
        const lastname = document.querySelector("#lastname").value;
        const phonenr = document.querySelector("#phonenr").value;
        const email = document.querySelector("#email").value;
        const password = document.querySelector("#password").value;
        const userType = document.querySelector("#userType").value;
    
        
        if (!isNorwegianAlphabet(firstname, lastname) || !isOnlyNumbers(phonenr) || !isValidEmail(email)) {
            displayErrors("One or more fields contain invalid input. Check and try again.");
            return;
        }
        
    
     
        const formdata = new FormData();
        formdata.append("firstname", firstname);
        formdata.append("lastname", lastname);
        formdata.append("phonenr", phonenr);
        formdata.append("email", email);
        formdata.append("password", password);
        formdata.append("usertype", userType);

        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!emailPattern.test(email)) {
            console.error("Invalid Email format:", email);
            displayErrors("Invalid Email format.");
            return; 
        }
    
        
        for (const [key, value] of formdata.entries()) {
            console.log(`${key}: ${value}`);
        }
    
        try {
            const response = await fetch("/set/s", {
                method: "POST",
                body: formdata
            });
            
            if(response.redirected) {
                window.location.href = response.url;
                return;
            }
            
            const data = await response.json();

            
            if (response.ok) {
                console.log("Registration successful:", data.message);
                alert("User registration successful");
            } else {
                console.error("Registration failed:", data);
                displayErrors(data.errors || data.error || "An unknown error occurred.");
            }
        } catch (error) {
            console.error("Error submitting form:", error);
            displayErrors("An error occurred while submitting the form.");
        } } 
        
        if(userTypeValue === 1) {

            const firstname = document.querySelector("#firstname").value;
            const dateofbirth = document.querySelector("#dateofbirth").value;
            const phonenr = document.querySelector("#phonenr").value;
            const email = document.querySelector("#email").value;
            const password = document.querySelector("#password").value;
            const userType = document.querySelector("#userType").value;
        
            
            if (!isNorwegianAlphabet(firstname) || !isOnlyNumbers(phonenr) || !isValidEmail(email)) {
                displayErrors("One or more fields contain invalid input. Check and try again.");
                return;
            }

        const formdata = new FormData();
        formdata.append("firstname", firstname);
        formdata.append("phonenr", phonenr);
        formdata.append("email", email);
        formdata.append("password", password);
        formdata.append("dateofbirth", dateofbirth);
        formdata.append("usertype", userType);

        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!emailPattern.test(email)) {
            console.error("Invalid Email format:", email);
            displayErrors("Invalid Email format.");
            return; 
        }
    
        
        for (const [key, value] of formdata.entries()) {
            console.log(`${key}: ${value}`);
        }
    
        try {
            const response = await fetch("/set/s", {
                method: "POST",
                body: formdata
            });



            if(response.redirected) {
                window.location.href = response.url;
                return;
            }

            
            const data = await response.json();

            
    
           
            if (response.ok) {
                console.log("Registration successful:", data.message);
                alert("User registration successful");
            } else {
                console.error("Registration failed:", data);
                displayErrors(data.errors || data.error || "An unknown error occurred.");
            }
        } catch (error) {
            console.error("Error submitting form:", error);
            displayErrors("An error occurred while submitting the form.");
        }


        }
    }

    function displayErrors(errors) {
        const errorContainer = document.querySelector("#errorMessages");
        errorContainer.style.display = 'block';
        if (Array.isArray(errors)) {
            errorContainer.innerHTML = errors.map(error => `<p>${error}</p>`).join('');
        } else {
            errorContainer.innerHTML = `<p>${errors}</p>`;
        }
    
    }

window.submitLoginForm = submitLoginForm;
});

/**************************************************************Validation functions***********************************************************************************************************/

function isNorwegianAlphabet(...args) {

    const norwegianAlphabetRegex = /^[a-zA-ZæøåÆØÅ]+$/;
    return args.map(arg => norwegianAlphabetRegex.test(arg));
}


function isOnlyNumbers(...args) {

    const onlyNumbersRegex = /^[0-9]+$/;
    return args.map(arg => onlyNumbersRegex.test(arg));
}


function isValidEmail(input) {
    
    const emailRegex = /^[a-zA-Z0-9]+[a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(input);
}

function isValidNorwegianAddress(input) {
    const norwegianAddressRegex = /^[a-zA-ZæøåÆØÅ0-9\s]+$/;
    return norwegianAddressRegex.test(input);
}





/**************************************************************GSAP Animation movement***********************************************************************************************************/

document.addEventListener("DOMContentLoaded", function () {

    const highlight = document.querySelector(".highlight");
    const buttons = document.querySelectorAll(".user-type-button");
    let lastClicked = null;

   

    buttons.forEach((button) => {
       
        button.addEventListener("mouseenter", () => {
            gsap.to(highlight, {
                scale: 1.5,
                width: button.offsetWidth * 2,
                height: button.offsetHeight,
                x: button.offsetLeft,
                y: button.offsetTop,
                duration: 0.3,
                ease: "power2.out",
                
            });
        });

        

        button.addEventListener("mouseleave", () => {

            if(lastClicked) {
            

            gsap.to(highlight, { 
                scale: 1.5,
                width: lastClicked.offsetWidth,
                height: lastClicked.height * 1.5,
                x: lastClicked.offsetLeft,
                y: lastClicked.offsetTop + lastClicked.offsetHeight / 2,
                duration: 0.3,
                ease: "power2.out",

            });
        } else {

            gsap.to(highlight, {
                scale: 1.5,
                width: button.offsetWidth, 
                height: button.offsetHeight * 1.5, 
                x: button.offsetLeft,
                y: button.offsetTop + button.offsetHeight / 2, 
                duration: 0.3,
                ease: "power2.out",
            });
        }
        });

       
        button.addEventListener("click", () => {
            lastClicked = button;
            gsap.to(highlight, {
                scale: 1.5,
                width: button.offsetWidth,
                height: button.offsetHeight,
                x: button.offsetLeft,
                y: button.offsetTop - 20,
                duration: 0.1,
                ease: "power2.out",
            });
        });
    });
});

/**********************************************************************************************************************************************************************************/