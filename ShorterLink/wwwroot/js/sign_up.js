$(function() {
var alertText = $("#alertText");
const alert = function (message) {
    alertText.html(message);
}

let emailInput = $("#emailInput");
let usernameInput = $("#usernameInput");
let passwordInput = $("#passwordInput");
let submitButton = $("#submit");
let signUpForm = $("#signUpForm");

signUpForm.submit(function (e) { 
    e.preventDefault();

    if(passwordInput.val().length < 4 || emailInput.val().length < 5 || usernameInput.val().length < 2) {
        alert("Please, fill in the fields");
        return;
    }

    $.ajax({
        method: "POST",
        url: "/user/register",
        data: { username: usernameInput.val(), email: emailInput.val(), password: passwordInput.val() },
        success: function(response) {
            let parsed = JSON.parse(response);

            if(parsed.response_code == 200) {
                window.location = parsed.redirect;
                return;
            }
            alert(parsed.message);
        },
        error: function() {
        }
    });
});
});