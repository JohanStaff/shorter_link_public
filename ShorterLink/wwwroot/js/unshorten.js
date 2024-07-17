$(function() {
let shorterButton = $("#unshortenButton");
let linkTextField = $("#linkInput");
let resultText = $("#resultText");
let copyButton = $("#copyButton");

const displayResult = function(text) {
    resultText.html(text);
}

shorterButton.on("click", function () { 
    if(linkTextField.val().length < 4) {
        displayResult("Please, enter a valid link!");
        return;
    }

    $.ajax({
        type: "GET",
        url: "/shorter/unshorten?hash_url=" + linkTextField.val(),
        success: function (response) {
            displayResult(`<a href="${response}">${response}</a>`); 
//            copyButton.removeClass("d-none");
        },
        error: function () {
            displayResult("An error occured, please try again later");
        }
    });
});
});