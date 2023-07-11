// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initializeDatabase() {

    var event = new Event('DatabaseInitialized');
    fetch('/api/database')
        .then(response => {
            if (response.ok) {

                info.hidden = true;

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

function getFeatured() {

    fetch('/api/artist')
        .then(response => response.json())
        .then(data => {
            const container = document.getElementById('content');

            data.forEach(item => {
                // Create a <div> element with the class "card"
                const card = document.createElement('div');
                card.className = 'card';
                card.style.width = '18rem';

                // Create an <img> element with the class "card-img-top" and set the src attribute
                const image = document.createElement('img');
                image.className = 'card-img-top';
                image.src = item.imageUrl;
                image.alt = 'Card image cap';

                // Create a <div> element with the class "card-body"
                const cardBody = document.createElement('div');
                cardBody.className = 'card-body';

                // Create an <h5> element with the class "card-title" and set the text content
                const title = document.createElement('h5');
                title.className = 'card-title';
                title.textContent = item.name;

                // Create a <p> element with the class "card-text" and set the text content
                const text = document.createElement('p');
                text.className = 'card-text';
                text.textContent = '';

                // Create an <a> element with the classes "btn" and "btn-primary" and set the href attribute and text content
                const link = document.createElement('a');
                link.className = 'btn btn-primary';
                link.href = item.url;
                link.textContent = 'Go somewhere';

                // Append the elements to build the card structure
                card.appendChild(image);
                cardBody.appendChild(title);
                cardBody.appendChild(text);
                cardBody.appendChild(link);
                card.appendChild(cardBody);
                console.log(card);
                // Add the card to the container
                container.appendChild(card);
            });
        })
        .catch(error => {
            // Handle error
            console.error('Error creating content', error);
        });
    
}