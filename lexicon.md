# Root-for-Root Translation Parameter
The English lexicon is the most important part of HIERO—and the part that has taken the most time by far to develop. The primary goal of the English lexicon is to link each Hebrew root with a single English root and vice versa. This one-to-one linking of Hebrew and English roots does not attempt to provide a translation that will fit the meaning of the Hebrew word in all cases. Rather, the purpose is to allow the reader to recognize where the same Hebrew root occurs in different places.

This parameter works in both directions. First, each Hebrew root and all of its derivatives are always represented by derivatives of a single English root—I call this “root coherence.” Second, unrelated Hebrew words are represented by unrelated English words—I call this “root uniqueness.”

In normal translations, different Hebrew words are translated by the same English word (no root uniqueness), and the same Hebrew word is translated in different places by different English words (no root coherence). For example, several Hebrew words for “sheep” are translated as follows:

|Hebrew|Existing translations|HIERO|
| ------------- | ------------- | ------------- |
|עדר|flock, drove, herds|flock|
|צאן|flock, sheep, lamb, cattle|sheep|
|שׂה|ewe, sheep, lamb, cattle|ovine|
|כבשׂ|ewe, sheep, lamb|lamb|
|רחל|ewe, sheep|ewe|

In this example, we can easily see the lack of root coherence. Each Hebrew root is translated in 2-4 different ways. We have to look a little more closely to see the lack of root uniqueness. The words “flock” and “cattle” each represent two different words; “ewe” and “lamb” each represent three; and “sheep” represents four different Hebrew words. As a result, it is impossible for the reader of a normal translation to know which Hebrew root is being used without looking at the Hebrew text itself. This does not obscure the meaning of the translation—we can easily understand the story—but it does obscure the poetic choices made by the author. By contrast, HIERO makes it easy for the reader to identify recurrences of the same Hebrew root. I have compiled a few [sample texts to illustrate root coherence](output%20samples/short-samples.html) as it appears in HIERO.

This parameter is admittedly artificial and unnecessary for normal translation work. It often obscures the meaning of a text, which is why, if you want to understand a text, you should read one of the many excellent translations already available in English.

One unusual challenge for HIERO is the fact that root uniqueness makes it impossible to offer the best translation of a root. For example, some of the Hebrew words for “sheep” have similar meanings:

|Hebrew| Existing translations     | HIERO | Best translation|Frequency|
| ---: | :------------------------ | :---- | :-------------  | ------: |
|  עדר | flock, drove, herds       | flock | flock           |     x55 |
|  צאן | flock, sheep, lamb, cattle| sheep | sheep           |    x276 |
|   שׂה | ewe, sheep, lamb, cattle  | ovine | sheep           |     x47 |
|  כבשׂ | ewe, sheep, lamb          | lamb  | lamb            |    x129 |
|  רחל | ewe, sheep                | ewe   | lamb            |      x4 |

As this example shows, there are two Hebrew roots that are best translated “sheep.” However, root uniqueness allows us to translate only one Hebrew root as “sheep.” As a result, the more common root is translated “sheep,” while the less common root is translated “ovine.” Of course, “ovine” is a poor, unfamiliar word, but I have not found a better unique option in English. In the end, HIERO’s lexicon cannot offer the best translation for many words, but only the best unique translation.

### More about Root Uniqueness
Root uniqueness means that unrelated Hebrew words are represented by unrelated English words. Here “unrelated” means that the words do not share a common root word. For example, if there are three unrelated Hebrew words that mean “light”—*'or*, *nogah*, and *peqach*—HIERO has to represent them with unrelated English words—for example, “light,” “brightness,” and “illumination.”

This is more challenging with many English compound words. For example, the English word “lightning” is obviously derived from the word “light.” But in Hebrew, the word *baraq*, which means “lightning,” is its own root word, unrelated to any word for “light.” Root uniqueness does not allow HIERO to use the English word “lightning,” so it is translated as “flash.”

Root uniqueness does not restrict word choices within the same root. So the Hebrew words *'ur*, *'ury*, and *ma'or*, all derived from *'or*, are translated “light.” Similarly, *'orah* and *ya'yr*, also derived from *'or*, are translated “light[plant]” and “enlightener.”

#### The Problem of Root Uniqueness
Root uniqueness poses a unique problem that prevents HIERO from offering the best translation for every word.

שֶׂה

### More about Root Coherence
Root coherence means that each Hebrew root and all its derivatives are always represented by derivatives of a single English root. For example, all words derived from *'or*, the Hebrew word for “light,” have to be represented in English by words derived from “light.” Some of our options include “lighting,” “enlighten,” and “lit.”

This gets challenging when a the Hebrew root has a derivative that seems unrelated in English. For example, the Hebrew word *me'urah*, which is used once to mean a “snake hole,” is also derived from *'or*—if we think of it from the snake’s perspective, the hole is where light comes in. Root coherence requires HIERO to use the word “light” or a word derived from it. In a case like this, the best translation option seems to be “light[hole],” which actually works pretty well in the context where it appears. If you think of a better option, let me know.

Usually, the connection between the Hebrew words is more intuitive. For example, the word *ro'sh*, meaning “head” of the human body, also means the leader or “head” of a group of people. The related word *re'shyt*, meaning “beginning” or “first,” translates as “headmost” without much difficulty. And the related word *me**ra'ash**awot*, meaning “pillow,” translates easily as “head[rest].”

### What Qualifies as a “Related Word”
Both root coherence and root uniqueness focus on whether words are related—and how closely. In order to determine word relationships in Hebrew, I rely primarily on [Strong’s Hebrew Dictionary](http://openscriptures.github.io/HebrewLexicon/HomeFiles/Lexicon.html) and [Gesenius’s Hebrew and Chaldee Lexicon](http://www.blueletterbible.org/study/lexica/gesenius/index.cfm). In English, I rely on the [Online Etymology Dictionary](http://www.etymonline.com/) and [Wiktionary](http://www.wiktionary.org/). Usually, there is a good consensus about word histories, but sometimes it is a judgment call that I have to make. Doubtlessly, a Hebrew scholar could make these judgments better than I can.

In addition, far more words are related than most people realize, and the number of unique roots is fairly small, so this parameter cannot be applied in an entirely objective way. But I think my application of it is helpful and fairly intuitive. I am open to input from others.

### Special notation
The lexicon uses two kinds of special notation to make root-for-root translation more feasible.

One special notation is brackets [brackets]. Brackets indicate a word that is not part of the Hebrew root but is helpful in clarifying the meaning of the word. In the examples above “head[rest]” contains brackets because the Hebrew word *mera'ashawot* contains the word *ro'sh*, meaning “head,” but does not contain the word *shabat*, meaning “rest.” This is especially common in names of plants and animals—for example, “algum[tree],” “'eshel[tree],” “white[tree],” and “smooth[tree]” are all trees, but none contains the word *`etz*, meaning “tree.” In HIERO’s final output, brackets are replaced by gray text ![gray](https://placehold.co/15x15/808080/808080.png).

The other special notation is the square root sign (√). The square root sign indicates a word that occurs only once and is unrelated to any other word in the Hebrew scriptures. This is helpful because Hebrew sometimes has more unrelated synonyms for the same thing than English does. For example, there are seven Hebrew words that mean something like “goat,” but I only know of four in English. One of the Hebrew words, *'aqqow*, appears only once and is not related to any other word in the Hebrew scriptures. Using the square root sign, I can translate *'aqqow* as “√goat” instead of having to find or invent another word that might be unintelligible to the reader. The square root sign indicates that the rare word “√goat” (*'aqqow*) is not related to the common word “goat” (*karar*). This satisfies the need for root uniqueness. The lexicon contains 85 definitions marked with the square root sign; since each word occurs only once, the sign appears 85 times in HIERO.

# Other Lexicon Principles
Other guiding principles in developing the lexicon are:

### Follow existing translations
When possible, the lexicon represents a Hebrew root with a word similar to that used in [popular existing translations](sources.md#bible-translations). HIERO is not an attempt to innovate.

### Translate proper nouns
When the meaning of a proper noun is known, HIERO translates it instead of translating it as other translations do. Proper nouns hold to the root-fot-root translation parameter just as other words do. Because there is no existing tradition of translating proper nouns, HIERO’s translations of proper nouns are a “best guess” and may be imprecise.

### Transliterate familiar words
Some Hebrew roots are transliterated. These roots are italicized, and are still required to follow the root-fot-root translation parameter. Roots may be transliterated for the following reasons:
- The root is foreign (e.g., *Pharaoh*, *Purim*).
- The root’s meaning has been lost (e.g., many kinds of plants and animals).
- The root has a close English cognate with the same meaning (e.g., *matzah*, *camel*, *horn*, *peg*).
- The root is onomatopoeic, and English has a similar onomatopoeic word (e.g., m*ama*, *hum*, *hush*).
- The root appears to be a technical word with no equivalent in English (e.g., *herem*, *goel*, *yabam*).
- The root is somewhat familiar and covers a range of meanings that HIERO is otherwise unable to represent in English (e.g., *shalom*, *adam*, *torah*).

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
The English lexicon is the most important part of HIERO—and the part that has taken the most time by far to develop. Because of this, I am not ready to post the entire lexicon online. Instead, I offer a [complete glossary](read/glossary.html) generated by HIERO, listing each Hebrew root word, its frequency (excluding proper noun instances), its corresponding English root from the lexicon, and some information about the root.

If you want to learn more about the lexicon, you can [read about the lexicon’s XML code](technical.md#english-lexicon) or review [an excerpt from the lexicon code here](resource%20samples/multilex_EXCERPT.xml).

# Exceptions to the Root-for-Root Parameter
This section lists all exceptions to the root-for-root parameter. The etymologies of these words are so uncertain or interwoven that applying the root-for-root translation parameter seems impossible. I allow exceptions for them because they are function words that do not seem generally to indicate significant word choices. In the lexicon, these exceptions are marked in gray ![light silver](https://placehold.co/15x15/d8d8d8/d8d8d8.png).

<!--
## Temporary exceptions
The lexicon is still in progress, so not every root is coherent and unique yet. Roots that are not yet coherent are marked in pink ![persian pink](https://placehold.co/15x15/ff80c0/ff80c0.png), roots that are not yet unique are marked in orange ![pastel orange](https://placehold.co/15x15/ffc040/ffc040.png), and roots that are neither coherent nor unique are marked in red ![red-orange](https://placehold.co/15x15/ff4000/ff4000.png). All roots that are not marked are both coherent and unique. A [complete list of exceptions to be resolved](read/glossary-noncompliant.html) is available.
## Permanent exceptions
In the glossary, permanent exceptions to the root-for-root translation parameter are marked in gray ![light silver](https://placehold.co/15x15/d8d8d8/d8d8d8.png). The following is a complete list of these exceptions.
-->

### Suffixes in English derivatives
The following suffixes have been used in the lexicon to derive English words from English roots in a way that represents the meaning of the corresponding Hebrew words: -*fy*, -*ly*, -*ish*, -*ous*, -*ful*, -*ize*, -*hood*, -*ward*, -*ness*, -*less*, -*dom*, -*er*, -*ster*, -*tion*, -*ive*, -*ant*/-*ent*, -*able*/-*ible*, -*y*, -*ery*, -*en*, -*est*, -*let*/-*ette*. These suffixes do not correspond to particular Hebrew suffixes. They have been added on the basis of meaning, not form.

Similarly, Latin and Greek prepositional prefixes are not subject to the root-for-root parameter when they appear as part of an English word.

### Function words as auxiliaries
HIERO uses the following function words as auxiliaries to represent the various forms of Hebrew verbs and nouns. Those marked with an asterisk (*) also appear in the lexicon as standalone roots. As standalone roots, they are subject to the two translation parameters. As auxiliaries that represent verb and noun forms, they are joined to the root verb or noun by a middle dot (·) and are not subject to the parameters.
- Personal pronouns*
- Possessive adjectives*
- Reflexive affixes: -*self*, -*selves*, *self*-
- Auxiliary verbs: *be*\*, *do*\*, *cause*\*, *have*, *make*, *let*
- Some prepositions: *of*, *to*\*, *on*, *by*, *at*, *among*, *against*
- Other minor words: *that*\*, *over*, *off*

### Independant function words
The following words are not compliant with the root-for-root parameter. Each line represents a unique Hebrew root and lists how the root is translated in English. In HIERO, some of these underlying roots can be distinguished only by the diacritics added to the English translation. Details can be found in the [glossary](read/glossary.html), where these words are grouped under “Function Words” and marked in gray ![light silver](https://placehold.co/15x15/d8d8d8/d8d8d8.png).

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
  - Root *lu*: *iᵮ*
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
  - Root *'ad*/*'aden*/*'owd*/*ba'ad*: *unto*, *about*, *again*, *onward*, *still*
- Negatives
  - Root *lo'*/*'al*: *not*
  - Root *'ayin*: *ńot*

### Some numbers
#### “one”—not root unique
The English root “one” is not root unique. It appears both as an auxiliary and as representing standalone roots. The underlying roots are all root coherent and can always be distinguished, because each Hebrew root uses a different form of the word, or uses it in a restricted construction.
- Coherent root *'echad*: *one* (x974) and *unit* (x164)
- Coherent root *`ashtey*, always in the construction “øne and ten,” meaning “eleven”: *øne* (x19)
- Coherent root *raq*: *only* (x109)
- Coherent root *bad*/*badad*: *alone*/*lone* (x105)
- Coherent root *kipper*: *atone* (x169)
- Function words used as auxiliaries and joined to the root by a middle dot (·): *one’s*, *one another*, *oneself*, *a*, *an*

#### *'aleph*—not root coherent
The Hebrew root *'aleph* is not root coherent. It appears either as “thousand” or as “aleph”/“alpha.” These two senses are separated because they have no common meaning and no common word history. The sense of “thousand” is derived not from the word *'aleph*, but from the position of the letter Aleph in the alphabet.

### Some gendered terms for people
When Hebrew uses a single root to refer to a person, identifying the gender by a suffix, but English uses different roots to differentiate gender, the lexicon sometimes allows an exception from the requirement for root coherence. The words must still be root unique. The following is a complete list of these exceptions.
- Root *'ach*/*'achowt*: *brother*, *sister*
- Root *'almah*/*'elem*: *girl*, *boy*
- Root *melek*/*malkah*: *king*, *queen*

These words are excused because the masculine and feminine words in each pair are so closely related in English that we think of them as related. These roots are still root unique, meaning that no other Hebrew word is translated “brother,” “sister,” etc. Details are in the lexicon.
