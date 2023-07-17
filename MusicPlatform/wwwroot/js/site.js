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
                favoriteButton.innerHTML = "Remove from favorites";
            } else {
                // Request failed
                favoriteButton.style.backgroundColor = "black";
                favoriteButton.innerHTML = "Add to favorites";
            }
        })
        .catch(error => {
            console.error('An error occurred while adding song to favorites:', error);
        });
}

function getComments(id) {

    const container = document.querySelector('.comment-section');
    const apiUrl = '/api/songs/getcomments';
    // Request body
    const requestBody = {
        id: id
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
                return response.json();
            } else {
                // Request failed
                console.error('Failed to get comments');
            }
        }).then(data => {
            // Clear existing comments

            container.innerHTML = '';
            // Iterate over the comments and generate HTML elements
            data.forEach(comment => {
                const pUser = document.createElement('p');
                pUser.textContent = 'User: ' + comment.username;

                const textarea = document.createElement('textarea');
                textarea.classList.add('form-control');
                textarea.rows = 3;
                textarea.readOnly = true;
                textarea.textContent = comment.text;
                const hr = document.createElement('hr');
                container.appendChild(pUser);
                container.appendChild(textarea);
                container.appendChild(hr);
            });
        })
        .catch(error => {
            console.error('An error occurred while showing comments:', error);
        });

}

function addComment(userId, songId) {
    // API endpoint URL
    const apiUrl = '/api/songs/comment';
    const comment = document.getElementById('commentText').value;
    // Request body
    const requestBody = {
        userId: userId,
        songId: songId,
        comment: comment
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
                alert('added comment');
            } else {
                // Request failed
                console.error('Failed to add song to favorites.');
            }
        })
        .catch(error => {
            console.error('An error occurred while adding song to favorites:', error);
        });
}
function addFan(userId, artistId) {
    // API endpoint URL
    const apiUrl = '/api/artist/fans';
 
    // Request body
    const requestBody = {
        userId: userId,
        artistId: artistId
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
                alert('added to fanbase');
            } else {
                // Request failed
                console.error('Failed to add song to favorites.');
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
                favoriteButton.innerHTML = "Remove from favorites";
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