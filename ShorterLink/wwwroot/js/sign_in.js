$(function() {
var alertText = $("#alertText");
const alert = function (message) {
    alertText.html(message);
}

let emailInput = $("#emailInput");
let passwordInput = $("#passwordInput");
let submitButton = $("#submit");

submitButton.on("click", function() {
    if(passwordInput.val().length < 4 || emailInput.val().length < 5) {
        alert("Please, fill in the data in the fields");
        return;
    }

    $.ajax({
        method: "POST",
        url: "/user/authentificate",
        data: { email: emailInput.val(), password: passwordInput.val() },
        success: function(response) {
            let responseValue = JSON.parse(response);

            if(responseValue.response_code == 403) {
                alert(responseValue.message);
                return;
            }
            if(responseValue.response_code == 200) {
                window.location = `${responseValue.redirect}`;
                return;
            }
        },
        error: function() {
        }
    });
});
});