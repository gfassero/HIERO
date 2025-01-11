# Root-for-Root Translation Parameter
The English lexicon is the most important part of HIERO—and the part that has taken the most time by far to develop. The primary goal of the English lexicon is to link each Hebrew root with a single English root and vice versa. This one-to-one linking of Hebrew and English roots does not attempt to provide a translation that will fit the meaning of the Hebrew word in all cases. Rather, the purpose is to allow the reader to recognize where the same Hebrew root occurs in different places.

This parameter works in both directions. First, each Hebrew root and all its derivatives are always represented by derivatives of a single English root—I call this “root coherence.” Second, unrelated Hebrew words are represented by unrelated English words—I call this “root uniqueness.”

In normal translations, different Hebrew words are translated by the same English word (no root uniqueness), and the same Hebrew word is translated in different places by different English words (no root coherence). For example, the following Hebrew words are translated as follows:

|Hebrew|Existing translations|HIERO|
| ------------- | ------------- | ------------- |
|עדר|flock, drove, herds|flock|
|צאן|flock, sheep, lamb, cattle|sheep|
|כבשׂ|ewe, sheep, lamb|lamb|
|רחל|ewe, sheep|ewe|

In this example, we can easily see the lack of root coherence. Each Hebrew root is translated in 2-4 different ways. We have to look a little more closely to see the lack of root uniqueness. The words “flock,” “ewe,” and “lamb” might each represent two different Hebrew words, and the word “sheep” might represent any of three different Hebrew words. As a result, it is impossible for the reader to know what Hebrew root is being used without looking at the Hebrew text itself. This does not obscure the meaning of the translation—we can understand the story without a problem—but it does obscure the poetic choices made by the author. By contrast, HIERO makes it easy for the reader to identify recurrences of the same Hebrew root. I have compiled a few [sample texts to illustrate root coherence](output%20samples/short-samples.html) as it appears in HIERO.

This parameter is admittedly artificial and unnecessary for normal translation work. It often obscures the meaning of a text, which is why, if you want to understand a text, you should read one of the many excellent translations already available in English.

### More about Root Uniqueness
Root uniqueness means that unrelated Hebrew words are represented by unrelated English words. Here “unrelated” means that the words do not share a common root word. For example, if I find three unrelated Hebrew words that mean “light”—*'or*, *nogah*, and *peqach*—I have to represent them with unrelated English words—for example, “light,” “brightness,” and “illumination.”

This is more challenging with many English compound words. For example, the English word “lightning” is obviously related to the word “light.” But in Hebrew, the word *baraq*, which means “lightning,” is its own root word, unrelated to any word for “light.” Root uniqueness does not allow me to use the English word “lightning,” so I decided to translate it “flash.”

Root uniqueness does not restrict word choices within the same root. So the Hebrew words *'ur*, *'ury*, and *ma'or*, all derived from *'or*, are translated “light.” Similarly, *'orah* and *ya'yr*, also derived from *'or*, are translated “light[plant]” and “enlightener.”

### More about Root Coherence
Root coherence means that each Hebrew root and all its derivatives are always represented by derivatives of a single English root. For example, all words derived from *'or*, the Hebrew word for “light,” have to be represented in English by words derived from “light.” Some of our options include “lighting,” “enlighten,” and “lit.”

This gets challenging when a the Hebrew root has a derivative that seems unrelated in English. For example, the Hebrew word *me'urah*, which is used once to mean a “snake hole,” is also derived from *'or* (think of it from the snake’s perspective—the hole is where light comes in). Root coherence requires me to use the word “light” or a word derived from it. In a case like this, the best translation option seems to be “light[hole],” which actually works pretty well in the context where it appears. If you think of a better option, let me know.

Usually, the connection between the Hebrew words is more intuitive. For example, the word *ro'sh*, meaning “head” of the human body, also means the leader or “head” of a group of people. The related word *re'shyt*, meaning “beginning” or “first,” translates as “headmost” without much difficulty. And the related word *me**ra'ash**awot*, meaning “pillow,” translates easily as “head[rest].”

### Related words
Both root coherence and root uniqueness focus on whether words are related. In order to determine whether words are related in Hebrew, I rely primarily on [Strong’s Hebrew Dictionary](http://openscriptures.github.io/HebrewLexicon/HomeFiles/Lexicon.html) and [Gesenius’s Hebrew and Chaldee Lexicon](http://www.blueletterbible.org/study/lexica/gesenius/index.cfm). In English, I rely on the [Online Etymology Dictionary](http://www.etymonline.com/) and [Wiktionary](http://www.wiktionary.org/). Usually, there is a good consensus about word histories, but sometimes it is a judgment call that I have to make. I am open to input from others.

### Special cases
The lexicon uses two kinds of special notation to make root-for-root translation more feasible.

One special notation is brackets [brackets]. Brackets indicate a word that is not part of the Hebrew root but is helpful in clarifying the meaning of the word. In the examples above “head[rest]” contains brackets because the Hebrew word *mera'ashawot* contains the word *ro'sh*, meaning “head,” but does not contain the word *shabat*, meaning “rest.” This is especially common in names of plants and animals—for example, “algum[tree],” “'eshel[tree],” “white[tree],” and “smooth[tree]” are all trees, but none contains the word *`etz*, meaning “tree.”

The other special notation is the square root sign (√). The square root sign indicates a word that occurs only once and is unrelated to any other word in the Hebrew scriptures. This is helpful because Hebrew sometimes has more unrelated synonyms for the same thing than English does. For example, there are seven Hebrew words that mean something like “goat,” but I only know of four in English. One of the Hebrew words, *'aqqow*, appears only once and is not related to any other word in the Hebrew scriptures. Using the square root sign, I can translate *'aqqow* as “√goat” instead of having to find or invent another word that might be unintelligible to the reader. The square root sign indicates that the rare word “√goat” (*'aqqow*) is not related to the common word “goat” (*karar*). This satisfies the need for root uniqueness. The lexicon contains 85 definitions marked with the square root sign; since each word occurs only once, the sign appears 85 times in HIERO.

# Other Lexicon Principles
Other guiding principles in developing the lexicon are:

### Follow existing translations
When possible, the lexicon represents a Hebrew root with a word similar to that used in [popular existing translations](sources.md#bible-translations). HIERO is not an attempt to innovate.

### Transliterate familiar words
If the Hebrew root is somewhat familiar, a transliteration may be used. This is useful if the root’s meaning is unknown or if available English translations are fraught. For example, the root שׁלם is transliterated as “shalom,” ירה as “torah,” and חרם as “herem.”

### Make up words
When no suitable English word is available, the lexicon may use a made-up word. The new word should be simple and quickly intelligible with some explanation.

   For example, the Hebrew root רעה yields words related to both friendship and shepherding. Lacking an equivalent word in English, the lexicon uses the root “tend” and makes up several derivative words.

   |Hebrew|Existing translations|HIERO|
   | ------------- | ------------- | ------------- |
   |רעה|shepherd (verb)|tend|
   |רעה|shepherd (noun)|tendent (a shepherd, who tends sheep)|
   |רעה|friend|tendent (a friend, who attends to another)|
   |מרעה|pasture|tendage (like pasturage or herbage)|

### Treat Hebrew and Aramaic vocabulary as a single lexicon
Hebrew and Aramaic cognates should be represented by the same English root.

# Reviewing the Lexicon
The English lexicon is the most important part of HIERO—and the part that has taken the most time by far to develop. Because of this, I am not ready to post the entire lexicon online. Instead, I have included links to the glossary below. The glossary is generated by HIERO and lists each Hebrew root word, its frequency (excluding proper noun instances), its corresponding English root from the lexicon, and some information about the root.
- [Glossary grouped by topic](alphas/glossary.html)
- [Glossary in alphabetical order](alphas/glossary-alphabetical.html)

If you want to learn more about the lexicon, you can [read about the lexicon’s XML code](technical.md#english-lexicon) or review [an excerpt from the lexicon code here](resource%20samples/multilex_EXCERPT.xml).

# Exceptions to the Root-for-Root Parameter
This section lists all exceptions to the root-for-root translation parameter. The etymologies of these words are so uncertain or interwoven that applying the root-for-root translation parameter seems impossible. I allow exceptions for them because they are function words that do not seem generally to indicate significant word choices.

## Temporary exceptions
The lexicon is still in progress, so not every root is coherent and unique yet. Roots that are not yet coherent are marked in pink ![persian pink](https://placehold.co/15x15/ff80c0/ff80c0.png), roots that are not yet unique are marked in orange ![pastel orange](https://placehold.co/15x15/ffc040/ffc040.png), and roots that are neither coherent nor unique are marked in red ![red-orange](https://placehold.co/15x15/ff4000/ff4000.png). All roots that are not marked are both coherent and unique. A [complete list of exceptions to be resolved](alphas/glossary-noncompliant.html) is available.

## Permanent exceptions

### Suffixes in English derivatives
The following suffixes have been used in the lexicon to derive English words from English roots in a way that represents the meaning of the corresponding Hebrew words: -*fy*, -*ly*, -*ish*, -*ous*, -*ful*, -*ize*, -*hood*, -*ward*, -*ness*, -*dom*, -*er*, -*ster*, -*tion*, -*ive*, -*ant*/-*ent*, -*able*/-*ible*, -*y*, -*ery*, -*en*, -*est*, -*let*/-*ette*. These suffixes do not correspond to particular Hebrew suffixes. They have been added on the basis of meaning, not form.

Similarly, Latin and Greek prepositional prefixes are not subject to the root-for-root translation parameter when they appear as part of an English word.

### Function words as auxiliaries
HIERO uses the following function words as auxiliaries to represent the various forms of Hebrew verbs and nouns. Those marked with an asterisk (*) also appear in the lexicon as standalone roots. As standalone roots, they are subject to the two translation parameters. As auxiliaries that represent verb and noun forms, they are joined to the root verb or noun by a middle dot (·) and are not subject to the parameters.
- Personal pronouns*
- Possessive adjectives*
- Reflexive affixes: -*self*, -*selves*, *self*-
- Auxiliary verbs: *be*\*, *do*\*, *cause*\*, *have*, *make*, *let*
- Some prepositions: *of*, *to*, *on*, *by*, *at*, *among*, *against*
- Other minor words: *that*\*, *over*, *off*

### Independant function words
The following words are not compliant with the root-for-root translation parameter. Each line represents a unique Hebrew root and lists how the root is translated in English. In HIERO, the underlying roots can be distinguished only by the diacritics added to the English translation. Details can be found in the [glossary](alphas/glossary.html), where these words are grouped under “Function Words” and are marked in gray ![light silver](https://placehold.co/15x15/d8d8d8/d8d8d8.png).
- Demonstratives
  - Root *zeh*, *hallazeh*: *this*, *ⱦhat*
  - Root *'el*, *'elleh*: *these*
  - Root *she*: *which*
  - Root *'eyk*: *¿ħow*
  - Root *'asher*: *that*, *wħich*, *how*
  - Root *kol*: *all*, *thing*
- Conjunctions
  - Root *k*: *as*, *but*, *for*, *thus*
  - Root *qobel*: *ḟor*
  - Root *'im*: *if*, *rather*
  - Root *beram*: *ƀut*
- Adverbs of place
  - Root *hennah*/*halom*: *here*
  - Root *poh*: *ẖere*, *tẖen*, *¿wẖere*
  - Root *sham*: *there*
  - Root *'ay*: *¿where*
- Adverbs of time
  - Root *'az*: *then*
  - Root *matay*: *¿wħen*
  - Root *b*: *in*, *when*
- Negatives
  - Root *lo'*/*'al*: *not*
  - Root *'ayin*: *ńot*
  - Root *lu*: *iᵮ*, *ᵰot*

### The English root “one”
The English root “one” is not compliant with the root-for-root translation parameter. It appears both as an auxiliary and as a standalone root. Still, the underlying root can always be distinguished, because each Hebrew root uses a different form of the word, or uses it in a restricted construction.
- Coherent root *'echad*: *one* (x974) and *unit* (x164)
- Coherent root *`ashtey*, always in the construction “øne and ten,” meaning “eleven”: *øne* (x19)
- Coherent root *raq*: *only* (x109)
- Incoherent root *bad*/*badad*: *alone* (x105)
- Function words used as auxiliaries and joined to the root by a middle dot (·): *one’s*, *one another*, *oneself*, *a*, *an*

### Some gendered terms for people
When Hebrew uses a single root to refer to a person, identifying the gender by a suffix, but English uses different roots to differentiate gender, the lexicon sometimes allows an exception from the requirement for root coherence (but not for root uniqueness). The following is a complete list of these exceptions.
- Root *'ach*/*'achowt*: *brother*, *sister*
- Root *'almah*/*'elem*: *girl*, *boy*

These words are excused because the words “brother” and “sister” and the words “girl” and “boy” are so closely related in English. These roots are still root unique, meaning that no other Hebrew word is translated “brother,” “sister,” “girl,” or “boy.” Details are in the lexicon.
