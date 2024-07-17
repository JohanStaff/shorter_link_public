$(function() {
let shorterButton = $("#shorterButton");
let linkTextField = $("#linkInput");
let resultText = $("#resultText");

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
        url: "/shorter/shorter?source=" + linkTextField.val(),
        success: function (response) {
            displayResult(response);    
        },
        error: function () {
            displayResult("An error occured, please try again later");
        }
    });
});
});