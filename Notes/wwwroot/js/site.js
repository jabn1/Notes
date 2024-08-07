// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SelectNote(id) {
    document.getElementById('NewNoteParentId').value = id;
}


function CopyTextFromElement(id) {
    var copyText = document.getElementById(id);

    // Select the text field
    copyText.select();
    copyText.setSelectionRange(0, 99999); // For mobile devices

    // Copy the text inside the text field
    navigator.clipboard.writeText(copyText.value);

}

function onDrop(e, fileInputId) {
    e.preventDefault();
    let fileInput = document.getElementById(fileInputId);
    fileInput.files = e.dataTransfer.files;
    const event = new Event('change', { bubbles: true });
    fileInput.dispatchEvent(event);
}

function onPaste(e, fileInputId) {
    let fileInput = document.getElementById(fileInputId);
    fileInput.files = e.clipboardData.files;
    const event = new Event('change', { bubbles: true });
    fileInput.dispatchEvent(event);
}