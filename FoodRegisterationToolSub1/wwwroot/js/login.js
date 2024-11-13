const userTypeMapping = {
    "NormalUser": 0,
    "SuperUser": 1,
    "AdminUser": 2
};
document.addEventListener("DOMContentLoaded", function(){
document.querySelectorAll('.user-type-button').forEach(button => { 
    button.addEventListener('click', function (event) {
        event.preventDefault();

        document.querySelectorAll('.user-type-button').forEach( btn => btn.classList.remove('selected'));
        this.classList.add('selected');


        const usertype = this.getAttribute('data-user-type');
        console.log(usertype);
        const userTypeValue = userTypeMapping[usertype];
        console.log(userTypeValue);
        //document.querySelector("#userType").value = userTypeValue;
        document.querySelector("#LoginForm").style.display = 'block';
        
        

    });
});




});


async function submitLoginForm() { 

    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    const usertype = document.querySelector("#userType").value;

    const formData = new FormData();
    formData.append('email', email);
    formData.append('password', password);
    formData.append('userType', usertype);

    try { 
        const response = await fetch('/Auth/verify', {
            method: 'POST',
            headers : { 
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: formData.toString()
        });

        const data = await response.json();

        if(data.seccess) { 
            window.location.href = data.redirectUrl;
        } else { 
            document.querySelector("#errorMessages").innerText = data.errorMessage;
            document.querySelector("#errorMessages").style.display = 'block';
        }
    } catch(error) { 
        console.error("Error: ", error);
    }
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