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
    let searchQuery = new URLSearchParams(window.location.search).get("q");
    console.log("Search query:", searchQuery);

    if (!searchQuery) {
        console.warn("No search query provided.");
        return;
    }

    fetch("index.json")
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.json();
        })
        .then(index => {
            console.log("Loaded index:", Object.keys(index).length, "roots indexed.");
            const matchingCitations = index[searchQuery];

            if (!matchingCitations || matchingCitations.length === 0) {
                console.warn("No matching citations found in index.");
                displayNoResults(searchQuery);
                return;
            }

            console.log("Matching citations found:", matchingCitations.length);
            fetchAndDisplayResults(searchQuery, matchingCitations);
        })
        .catch(error => console.error("Error loading index:", error));
});

function fetchAndDisplayResults(query, citations) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book">Searching for matches in the text...</p>`; // Initial "Searching..." message
    document.title = `HIERO | Root ${query} (Searching...)`;
    let matchCount = 0;
    let citationIndex = 0;
    const batchSize = 10;
    let currentBatch = [];
    let summaryElement = resultsContainer.querySelector(".book"); // Get the initial summary element

    function updateSummary() {
        if (summaryElement) {
            summaryElement.textContent = `Root ${query}: ${matchCount} matching lines`;
            document.title = `HIERO | Root ${query} (${matchCount})`;
        } else {
            const newSummary = document.createElement("p");
            newSummary.className = "book";
            newSummary.textContent = `Root ${query}: ${matchCount} matching lines`;
            resultsContainer.insertBefore(newSummary, resultsContainer.firstChild);
            document.title = `HIERO | Root ${query} (${matchCount})`;
            summaryElement = newSummary; // Update the reference
        }
    }

    function processCitationBatch() {
        const batchEndTime = Math.min(citationIndex + batchSize, citations.length);

        for (; citationIndex < batchEndTime; citationIndex++) {
            const citation = citations[citationIndex];
            const parentP = xmlDoc.querySelector(`p[data-cit='${citation}']`);

            if (parentP) {
                const clonedP = parentP.cloneNode(true);
                const clonedSpans = clonedP.querySelectorAll(`span[data-root='${query}']`);

                if (clonedSpans.length > 0) {
                    clonedSpans.forEach(span => span.classList.add("match"));

                    const dataCitValue = clonedP.getAttribute("data-cit");
                    if (dataCitValue) {
                        const trimmedCitationForLink = dataCitValue.split(/[_-]/)[0];
                        let link = document.createElement("a");
                        link.href = bookFiles[trimmedCitationForLink.substring(0, 3)] + ".html?q=" + query + "#x" + trimmedCitationForLink;
                        link.appendChild(clonedP);
                        currentBatch.push(link);
                        matchCount++;
                    } else {
                        console.warn("Paragraph found with citation from index but no data-cit attribute:", clonedP);
                        currentBatch.push(clonedP);
                    }
                }
            } else {
                console.warn(`Paragraph with citation '${citation}' not found in XML.`);
            }
        }

        // Append the current batch to the results container
        currentBatch.forEach(item => resultsContainer.appendChild(item));
        currentBatch = []; // Clear the batch

        // Update the summary
        updateSummary();

        if (citationIndex < citations.length) {
            requestAnimationFrame(processCitationBatch);
        } else {
            console.log("Incremental display finished.");
            if (matchCount === 0) {
                resultsContainer.innerHTML = `<p class="book">Root ${query}: 0 matches</p>`;
                document.title = `HIERO | Root ${query} (0)`;
            }
        }
    }

    fetch("full.xml")
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.text();
        })
        .then(xmlText => {
            const parser = new DOMParser();
            xmlDoc = parser.parseFromString(xmlText, "application/xml");
            console.log("full.xml parsed into DOM. Starting incremental display with batch size:", batchSize);
            requestAnimationFrame(processCitationBatch);
        })
        .catch(error => console.error("Error loading and parsing XML:", error));

    let xmlDoc;
}

function displayNoResults(query) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book">Root ${query}: 0 matches</p>`;
    document.title = `HIERO | Root ${query}`;
}