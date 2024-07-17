$(function() {
let shorterButton = $("#shorterButton");
let linkTextField = $("#linkInput");
let resultText = $("#resultText");
let copyButton = $("#copyButton");

const displayResult = function(text) {
    resultText.html(text);
}
const copyToClipboard = function copyToClipboard(text){
    let textArea = document.createElement("textarea");
    textArea.value = text;
    textArea.select();
    textArea.setAttribute("readonly", "");
    document.body.appendChild(textArea);
    document.execCommand("copy");
    document.body.removeChild(textArea);
}

copyButton.on("click", function() {
    copyToClipboard(resultText.val());
});
shorterButton.on("click", function () { 
    if(linkTextField.val().length < 4) {
        displayResult("Please, enter a valid link!");
        return;
    }

    $.ajax({
        type: "GET",
        url: "/shorter/shorter?source=" + linkTextField.val(),
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