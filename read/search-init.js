document.addEventListener("DOMContentLoaded", function () {
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
});
