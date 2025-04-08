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

    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book loading-message">Searching the text...</p>`;
    document.title = `HIERO | Root ${searchQuery || 'Search'} (Loading...)`;

    if (!searchQuery) {
        console.warn("No search query provided.");
        resultsContainer.innerHTML = `<p class="book">No search query provided.</p>`;
        document.title = `HIERO | Search`;
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
                resultsContainer.innerHTML = `<p class="book">Root ${searchQuery}: 0 matches</p>`;
                document.title = `HIERO | Root ${searchQuery} (0)`;
                return;
            }

            console.log("Matching citations found:", matchingCitations);
            fetchAndDisplayResults(searchQuery, matchingCitations, resultsContainer);
        })
        .catch(error => {
            console.error("Error loading index:", error);
            resultsContainer.innerHTML = `<p class="book error">Error loading search index.</p>`;
            document.title = `HIERO | Search (Error)`;
        });
});

function fetchAndDisplayResults(query, citations, resultsContainer) {
    let matchCount = 0;
    let resultsHTML = '';

    fetch("full.json")
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.json();
        })
        .then(fullData => {
            resultsContainer.innerHTML = '';
            const initialSummary = document.createElement('p');
            initialSummary.className = 'book';
            initialSummary.textContent = `Root ${query}: 0 matching lines`;
            resultsContainer.appendChild(initialSummary);
            document.title = `HIERO | Root ${query} (0)`;
            let summaryElement = initialSummary;

            citations.forEach(citation => {
                if (fullData[citation]) {
                    let lineParts = [];
                    let hasMatchInLine = false;
                    fullData[citation].forEach(wordObj => {
                        if (wordObj.r === query) {
                            lineParts.push(`<span class="match">${wordObj.t}</span>`);
                            hasMatchInLine = true;
                        } else {
                            lineParts.push(wordObj.t);
                        }
                    });

                    if (hasMatchInLine) {
                        const pElement = document.createElement('p');
                        pElement.setAttribute('data-cit', citation.split(/_/)[0].replace('.', ' ').replace('.', ':'));
                        pElement.innerHTML = lineParts.join(' ');

                        const trimmedCitationForLink = citation.split(/_/)[0];
                        let link = document.createElement("a");
                        link.href = bookFiles[trimmedCitationForLink.substring(0, 3)] + ".html?q=" + query + "#x" + trimmedCitationForLink;
                        link.appendChild(pElement);
                        resultsHTML += link.outerHTML;
                        matchCount++;
                        summaryElement.textContent = `Root ${query}: ${matchCount} matching lines`;
                        document.title = `HIERO | Root ${query} (${matchCount})`;
                    }
                } else {
                    console.warn(`Citation '${citation}' not found in full data.`);
                }
            });

            resultsContainer.innerHTML = `<p class="book">Root ${query}: ${matchCount} matching lines</p>${resultsHTML}`;
            document.title = `HIERO | Root ${query} (${matchCount})`;

            console.log("Results displayed.");
        })
        .catch(error => console.error("Error loading full JSON:", error));
}

function displayNoResults(query) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book">Root ${query}: 0 matches</p>`;
    document.title = `HIERO | Root ${query}`;
}