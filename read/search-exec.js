document.addEventListener("DOMContentLoaded", () => {
    let searchQuery = new URLSearchParams(window.location.search).get("q"); // Get search term from URL
    console.log("Search query:", searchQuery);

    if (!searchQuery) {
        console.warn("No search query provided.");
        return;
    }

    fetch("full.xml") // Load XML file
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.text();
        })
        .then(text => {
            let parser = new DOMParser();
            let xmlDoc = parser.parseFromString(text, "application/xml"); // Parse as XML
            console.log("Parsed XML document:", xmlDoc);

            let matchingSpans = xmlDoc.querySelectorAll(`span[data-root='${searchQuery}']`);
            console.log("Matching spans found:", matchingSpans.length);

            if (matchingSpans.length === 0) {
                console.warn("No matching spans found.");
                displayNoResults(searchQuery);
                return;
            }

            let matchingParagraphs = new Map(); // Map to store unique paragraphs by reference

            matchingSpans.forEach(span => {
                let parentP = span.closest("p"); // Find closest <p> ancestor
                if (parentP && !matchingParagraphs.has(parentP)) {
                    let clonedP = parentP.cloneNode(true); // Clone paragraph
                    let clonedSpans = clonedP.querySelectorAll(`span[data-root='${searchQuery}']`);

                    clonedSpans.forEach(clonedSpan => clonedSpan.classList.add("match")); // Highlight ALL matches

                    matchingParagraphs.set(parentP, clonedP); // Store unique paragraphs
                }
            });

            console.log("Matching <p> elements found:", matchingParagraphs.size);

            if (matchingParagraphs.size === 0) {
                console.warn("No matching <p> elements found.");
                displayNoResults(searchQuery);
                return;
            }

            displayResults(searchQuery, Array.from(matchingParagraphs.values())); // Convert Map to array
        })
        .catch(error => console.error("Error loading XML:", error));
});

function displayResults(query, paragraphs) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = ""; // Clear previous results

    let matchCount = paragraphs.length;

    let summary = document.createElement("p");
    summary.className = "book";
    summary.textContent = `Root ${query}: ${matchCount} matching lines`;
    document.title = `HIERO | Root ${query}`;
    resultsContainer.appendChild(summary);

    paragraphs.forEach(p => {
        resultsContainer.appendChild(p);
    });

    console.log("Results displayed.");
}

function displayNoResults(query) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book">Root ${query}: 0 matches</p>`;
    document.title = `HIERO | Root ${query}`;
}