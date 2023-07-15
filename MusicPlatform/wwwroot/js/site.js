// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initializeDatabase() {

    var event = new Event('DatabaseInitialized');
    fetch('/api/database')
        .then(response => {
            if (response.ok) {


                document.dispatchEvent(event);
            } else {
                // Handle error
                console.error('Error initializing database:', response.statusText);
            }
        })
        .catch(error => {
            // Handle error
            console.error('Error initializing database:', error);
        });
}


function updateProgressBar(value) {
    var progressBar = document.getElementById("progress-bar");
    progressBar.setAttribute("aria-valuenow", value);
    progressBar.style.width = value + "%";
}

function enrichData() {
    var event = new Event('DataEnriched');
    fetch('/api/artist')
        .then(response => {
            if (response.ok) {
                document.dispatchEvent(event);
            } else {
                // Handle error
                console.error('Error enriching data:', response.statusText);
            }
        })
        .catch(error => {
            // Handle error
            console.error('Error enriching data:', error);
        });
 }