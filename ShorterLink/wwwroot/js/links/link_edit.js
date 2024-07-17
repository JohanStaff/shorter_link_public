$(function () { 
var alertText = $("#alertText");
const alert = function (message) {
    alertText.html(message);
}

let saveButton = $("#saveButton");
let hashUrlInput = $("#hashUrlInput");
let maskUrlInput = $("#maskUrlInput");
let activeCheckbox = $("#activeCheckbox");

saveButton.on("click", function() {
	$.ajax({
		type: "POST",
		url: "/links/edit/" + hashUrlInput.val() + "/save",
		data: { alias: maskUrlInput.val(), active: activeCheckbox.is(":checked") },
		success: function (response) {
			let parsed = JSON.parse(response);

			if(parsed.response_code == 200) {
				window.location = parsed.redirect;
				return;
			} else {
				alert(parsed.message);
			}
		}
	});
});
});