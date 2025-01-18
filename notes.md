# On Meaning

Translations represent meaning, and I believe that our modern English translations already represent the Hebrew text accurately. HIERO attempts to do something rather different—that is, to represent more of the author’s poetic choices than is possible in a translation. HIERO should always be read in light of the meaning given by existing translations.

HIERO attempts to give studious English readers a better view of the native beauty of the Hebrew scriptures. It does not attempt to uncover hidden meanings in the text or to overthrow traditional interpretations. To do so would be fundamentalistic at best and gnostic at worst. If additional meaning is discovered in HIERO’s output, it will probably be limited to nuances that complement the traditional interpretations.

Instead, if you want to know what the scriptures mean, you should read a modern translation. In all questions of meaning, HIERO defers to the modern translations. And if you want special insights, you should read the commentaries of the Church fathers.

# Machine Translation Isn’t the Point
HIERO is a machine translation, but being a machine translation is not what makes it unique. There are other machine translations of the scriptures, some of which are interesting to read. What makes HIERO unique is its [root-for-root translation parameter](lexicon.md). Using machine translation makes it much easier to work within this parameter, but there is no reason why a manual translation could not do so as well. As far as I know, no other translator has attempted to do this yet. Read about HIERO’s [machine translation here](technical.md).

# The Choice of a Canon

Most of the ancient Jewish scriptures were originally written in Hebrew. A few chapters were written in Aramaic, an ancient Semitic language very similar to Hebrew. Seven books have come to us in Greek—the books of Tobit, Judith, 1 & 2 Maccabees, Wisdom, Sirach, and Baruch, known collectively as the “deuterocanonical” books. Some of these deuterocanonical books may have been written originally in Greek, but most were written in Hebrew, translated into Greek, and the original Hebrew texts lost. As the Jewish and Christian canons were compiled, some groups chose to include the deuterocanonical books, while others omitted them because they did not exist in Hebrew.

HIERO, being a computer program, has no preference on the canon. However, it was designed to parse Hebrew, not Greek. Hebrew and Aramaic are so similar that HIERO can handle both without much added complexity, but Greek is so different that it would require an entirely different lexicon and parsing functions. As a result, the deuterocanonical books are not included in HIERO.
