let bookFiles = {
    "Gen": "01 Gen",
    "Exo": "02 Exo",
    "Lev": "03 Lev",
    "Num": "04 Num",
    "Deu": "05 Deu",
    "Jos": "06 Jos",
    "Jdg": "07 Jdg",
    "Rut": "08 Rut",
    "1Sa": "09 1Sa",
    "2Sa": "10 2Sa",
    "1Ki": "11 1Ki",
    "2Ki": "12 2Ki",
    "1Ch": "13 1Ch",
    "2Ch": "14 2Ch",
    "Ezr": "15 Ezr",
    "Neh": "16 Neh",
    "Est": "17 Est",
    "Job": "18 Job",
    "Psa": "19 Psa",
    "Pro": "20 Pro",
    "Ecc": "21 Ecc",
    "Sng": "22 Sng",
    "Isa": "23 Isa",
    "Jer": "24 Jer",
    "Lam": "25 Lam",
    "Ezk": "26 Ezk",
    "Dan": "27 Dan",
    "Hos": "28 Hos",
    "Jol": "29 Jol",
    "Amo": "30 Amo",
    "Oba": "31 Oba",
    "Jon": "32 Jon",
    "Mic": "33 Mic",
    "Nam": "34 Nam",
    "Hab": "35 Hab",
    "Zep": "36 Zep",
    "Hag": "37 Hag",
    "Zec": "38 Zec",
    "Mal": "39 Mal"
};

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

                    // Create the link and wrap clonedP
                    let link = document.createElement("a");
                    let dataCit = clonedP.getAttribute("data-cit");

                    link.href = bookFiles[dataCit.substring(0, 3)] + ".html?q=" + searchQuery + "#x" + dataCit;

                    // Append clonedP to the link and insert the link.
                    link.appendChild(clonedP); // Append clonedP to the link
                    parentP.parentNode.insertBefore(link, parentP); // Insert link before parentP

                    matchingParagraphs.set(parentP, link); // Store the link, not the p.
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