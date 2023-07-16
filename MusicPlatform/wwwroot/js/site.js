// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const favoriteButton = document.getElementById('favorite-button');


favoriteButton.addEventListener('click', function () {
    const user = favoriteButton.getAttribute('data-user');
    const song = favoriteButton.getAttribute('data-song');
    addToFavorites(song, user);
});


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

function checkFavorites(id, userId) {
    // API endpoint URL
    const apiUrl = '/api/songs/check';

    // Request body
    const requestBody = {
        id: id,
        userId: userId
    };

    // Fetch options
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    };

    // Make the API request
    fetch(apiUrl, options)
        .then(response => {
            if (response.ok) {
                // Request successful
                favoriteButton.style.backgroundColor = "green";
            } else {
                // Request failed
                favoriteButton.style.backgroundColor = "black";
            }
        })
        .catch(error => {
            console.error('An error occurred while adding song to favorites:', error);
        });
}


function addToFavorites(id, userId) {
    // API endpoint URL
    const apiUrl = '/api/songs';

    // Request body
    const requestBody = {
        id: id,
        userId: userId
    };

    // Fetch options
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    };

    // Make the API request
    fetch(apiUrl, options)
        .then(response => {
            if (response.ok) {
                // Request successful
                favoriteButton.style.backgroundColor = "green";
            } else {
                // Request failed
                console.error('Failed to add song to favorites.');
            }
        })
        .catch(error => {
            console.error('An error occurred while adding song to favorites:', error);
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