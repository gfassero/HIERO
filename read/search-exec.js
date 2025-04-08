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
    resultsContainer.innerHTML = "<p class=\"book\">Searching for matches in the text...</p>";

    fetch("full.xml")
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.text();
        })
        .then(xmlText => {
            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(xmlText, "application/xml");
            const matchingParagraphs = new Map();

            citations.forEach(citation => {
                const parentP = xmlDoc.querySelector(`p[data-cit='${citation}']`);
                if (parentP && !matchingParagraphs.has(parentP)) {
                    const clonedP = parentP.cloneNode(true);
                    const clonedSpans = clonedP.querySelectorAll(`span[data-root='${query}']`);
                    clonedSpans.forEach(span => span.classList.add("match"));

                    let link = document.createElement("a");
                    link.href = bookFiles[citation.substring(0, 3)] + ".html?q=" + query + "#x" + citation;
                    link.appendChild(clonedP);
                    matchingParagraphs.set(parentP, link);
                }
            });

            resultsContainer.innerHTML = "";
            let matchCount = matchingParagraphs.size;
            let summary = document.createElement("p");
            summary.className = "book";
            summary.textContent = `Root ${query}: ${matchCount} matching lines`;
            document.title = `HIERO | Root ${query}`;
            resultsContainer.appendChild(summary);

            matchingParagraphs.forEach(link => {
                resultsContainer.appendChild(link);
            });
            console.log("Results displayed using citations from index.");

        })
        .catch(error => console.error("Error loading and parsing XML:", error));
}

function displayNoResults(query) {
    let resultsContainer = document.getElementById("translation");
    resultsContainer.innerHTML = `<p class="book">Root ${query}: 0 matches</p>`;
    document.title = `HIERO | Root ${query}`;
}