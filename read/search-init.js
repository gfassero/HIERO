document.addEventListener("DOMContentLoaded", function () {

    // PREPARE POPUP FOR INITATING SEARCH

    let popup = document.createElement("div");
    popup.className = "popup";
    document.body.appendChild(popup);

    document.body.addEventListener("click", function (event) {
        let span = event.target.closest("span[data-root]");
        if (span) {
            let dataRoot = span.getAttribute("data-root");

            // Create popup content
            popup.innerHTML = `Search root:
                <a href="search.html?q=${dataRoot}" target="_blank">${dataRoot}</a>
            `;

            // Position popup near clicked word
            popup.style.left = `${event.pageX + 10}px`;
            popup.style.top = `${event.pageY + 10}px`;
            popup.style.display = "block";
        } else {
            // Hide popup if clicking elsewhere
            popup.style.display = "none";
        }
    });



    // HIGHLIGHT MATCHES WHEN RETURNING FROM SEARCH

    let searchQuery = new URLSearchParams(window.location.search).get("q"); // Get search term from URL
    console.log("Search query:", searchQuery);

    if (!searchQuery) {
        console.warn("No search query provided.");
        return;
    }

    let matchingSpans = document.getElementById("translation").querySelectorAll(`span[data-root='${searchQuery}']`); // Select from the current document
    console.log("Matching spans found:", matchingSpans.length);

    if (matchingSpans.length === 0) {
        console.warn("No matching spans found.");
        return;
    }

    matchingSpans.forEach(span => {
        span.classList.add("match"); // Highlight the matching span
    });

    console.log("Matches highlighted.");

});
