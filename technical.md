# The Program
HIERO consists of an [English lexicon](#english-lexicon), an [annotated Hebrew text](#annotated-hebrew-text), a [Hebrew parsing dictionary](#hebrew-parsing-dictionary), and the [program code](#program-code).

# English Lexicon
The English lexicon is the most important part of HIERO—and the part that has taken the most time by far to develop. The primary goal of the English lexicon is to link each Hebrew root with a single English root and vice versa. HIERO’s English lexicon contains about 9,500 definitions of Hebrew and Aramaic words. The lexicon is written in XML code. The XML code, part-of-speech mappings, and Strongs number mappings that structure the lexicon are based on the [OSHB Hebrew Lexicon](http://github.com/openscriptures/HebrewLexicon/blob/master/LexicalIndex.xml) by the [Open Scriptures Hebrew Bible Project](http://hb.openscriptures.org/). All of the definitions are my own. The following is a partial sample from the lexicon:
```xml
<entry><w xlit="bārāʾ">בָּרָא</w><pos>V</pos><qalAct>shape</qalAct>
		<pielAct>reshape</pielAct>
		<hiphilAct>[keep] °[in] shape</hiphilAct><xref strong="1254A,1254B" />
	<entry><w xlit="bĕrî">בְּרִי</w><pos>A</pos><def>shapely</def><xref strong="1274" /></entry>
	<entry><w xlit="bārîʾ">בָּרִיא</w><pos>A</pos><def>shapely</def><xref strong="1277" /></entry>
	<entry><w xlit="bĕrîʾâ">בְּרִיאָה</w><pos>N</pos><def>shaping</def><xref strong="1278" /></entry>
	<entry><w xlit="bĕrāʾyâ">בְּרָאיָה</w><pos>Np</pos><def>shaped-of-Yʜ</def><xref strong="1256" /></entry>
</entry>
```
The lexicon tags follow the following rules:
1. Each Hebrew root has a single root `<entry>` element. Related words derived from the root will have <entry> elements nested within the root element.
2. A `<w>` element contains the entry’s Hebrew root.
3. A `<pos>` element describes the entry’s part of speech.
4. A `<def>` element contains the entry’s English translation.
5. A verb entry does not contain a `<def>` element. Instead, each verb entry contains up to eight different translations corresponding to the eight Hebrew functional verb stem types (nine in Aramaic). In this example, the stem translations are contained in the `<qalAct>`, `<pielAct>`, and `<hiphilAct>` elements.

   Any English translation of a stem that appears in the Hebrew text but not in the English lexicon is derived regularly from the translation of another stem. For example, HIERO can automatically derive the translation of the Piel passive stem (“be reshaped”) from the translation provided for the Piel active stem (“reshape”).
6. Verb translations may contain the degree sign (“[keep] °[in] shape”). This marks the position at which the object of the verb should appear in English idiom. If, in the Hebrew text, the object of the verb is indicated by a suffix appended to the verb itself, then HIERO will place the object at the position indicated by the degree sign (“[keep] him [in] shape”). Otherwise, if the verb and its object are separate Hebrew words, the object will not be repositioned (“[keep in] shape the king”), as doing so would require disrupting the Hebrew word order.
7. The lexicon may contain words in brackets. Brackets indicate a part of the translation that is essential to the meaning but is not derived from the Hebrew root. For example, the Hiphil stem of בָּרָא means to “keep in shape,” but it does not actually include a word meaning “keep” nor a preposition meaning “in.“” The translation “[keep] °[in] shape” includes words in brackets to indicate the meaning more clearly without suggesting that these words appear in Hebrew. In the final rendering, brackets are replaced with gray text.
8. The lexicon may contain words in braces. Braces indicate a translation that is transliterated from the Hebrew root. For example, the Hebrew word for “cinnamon” is pronounced “kinamon.” The translation “{cinnamon}” includes braces to indicate that this word is transliterated from Hebrew. In the final rendering, braces are replaced with italic text.
9. An `<xref>` element contains a list of Hebrew lexical numbers that HIERO will represent using the English entry. Entry numbers are based on the Strongs numbering system but include letter suffixes to differentiate senses that were not differentiated in the original Strongs system.

Further reading:
- [More about the lexicon](lexicon.md)
- [A longer excerpt from the lexicon](resource%20samples/multilex_EXCERPT.xml)
- [Complete glossary grouped by topic](read/glossary.html)
- [Complete glossary in alphabetical order](read/glossary-alphabetical.html)

# Annotated Hebrew Text
HIERO translates from the four-part [Translators Amalgamated Hebrew OT](http://github.com/STEPBible/STEPBible-Data/tree/master/Translators%20Amalgamated%20OT%2BNT) (TAHOT) by [STEP Bible](http://www.stepbible.org/). TAHOT is an edition of the Leningrad codex, following the qere of the Masoretic Text.

I have extracted the relevant data from TAHOT and reformatted it for use with HIERO. The following is a sample from the reformatted text:

|Word number|Hebrew|Parsing variant|
|-----|-----|-----|
|Gen.1.1#01=L|בְּרֵאשִׁ֖ית|1|
|Gen.1.1#02=L|בָּרָ֣א|1|
|Gen.1.1#03=L|אֱלֹהִ֑ים||
|Gen.1.1#04=L|אֵ֥ת||
|Gen.1.1#05=L|הַשָּׁמַ֖יִם||
|Gen.1.1#06=L|וְאֵ֥ת||
|Gen.1.1#07=L|הָאָֽרֶץ׃||

The Hebrew text is formatted with one Hebrew word per line. Each word is numbered (two digits, preceded by #), and its source text is noted (“L” indicates the Leningrad codex). Words that could be parsed in multiple ways are annotated with a number to indicate the appropriate parsing variant. In this example, word #01 could be followed by the word “of,” but since the context makes that impossible, the annotation “1” indicates to translate it without the word “of.” Word #02 could be parsed as an Aramaic word, but since this is a Hebrew text, the annotation “1” indicates to translate it as Hebrew.

Further reading:
- [Full annotated Hebrew text](resource%20samples/annotatedHebrewText.bin)
- [Original TAHOT data](https://github.com/STEPBible/STEPBible-Data/tree/master/Translators%20Amalgamated%20OT%2BNT) from STEP Bible
- [A sample of my corrections to the TAHOT data](resource%20samples/Hebrew%20OT%20Overlay_EXCERPT.txt)

# Hebrew Parsing Dictionary
HIERO includes a Hebrew parsing dictionary of 53,000 unique word forms found in the text, along with the corresponding morphological and lexical tags. This parsing dictionary includes parsing information taken from TAHOT as well as additional corrections of my own. The following is a sample from the parsing dictionary:

|Hebrew|Lexicon|Morphology|Lex. variant 1|Morph. var. 1|Lex. var. 2|Morph. var. 2|
|-----|-----|-----|-----|-----|-----|-----|
|גָדוֹל|1419A|HAamsa|1431|Vqaa|||
|גָדְלָה|1431|HVqp3fs|||||
|גָּדַלְתָּ|1431|HVqp2ms|||||
|הִגְדִּיל|1431|HVhp3ms|||||
|גִּדַּל|1431|HVpp3ms|||||
|יַגְדִּיל|1431|HVhi3ms|||||
|גָּדְלוּ|1431|HVqp3cp|||||

Lexical tags refer to entry numbers found in the `<xref>` tags in the English lexicon. Morphology codes describe the word’s part of speech, form, person, gender, number, and state. Alternative parsing variants are given in additional columns.

Further reading:
- [A full description of morphology codes](http://docs.google.com/document/d/1wQ67vPIrNxvICy5QmSeromQUJmePml1nQxv7n1gJ8qw)  from STEP Bible
- [A longer excerpt from the parsing dictionary](resource%20samples/parsingDictionary_EXCERPT.bin)

# Program Code
HIERO is written in Visual Basic and contains 2,100 lines of executable code. HIERO can start up and represent the entire Hebrew Old Testament in fifteen seconds on the laptop on which it was developed. HIERO does not use artificial intelligence, although AI was used to make the initial drafts of some sections of the code. Much of the code is available in HIERO’s GitHub repository under “code samples.”

To begin translation, HIERO iterates through the annotated Hebrew text, one word at a time. For each word, HIERO looks up the corresponding parsing from the Hebrew parsing dictionary, using the annotations from the Hebrew text, if any exist. Once the parsing has been obtained, HIERO uses the word’s lexicon tag to look up its translation in the English lexicon. HIERO uses the word’s morphology tag to inflect the English translation and apply appropriate formatting via CSS. HIERO extracts cantillation marks from the Hebrew word and uses them to apply English punctuation or line breaks via XHTML. Finally, HIERO outputs the result to an HTML file. HIERO then moves to the next word and repeats.

The user views the output by opening the [HTML output files](read/). Read about [HIERO’s formatting here](reading.md#formatting).

# Further Development
- Improve the English lexicon.
- Add consistent annotation for the Hebrew ון- suffix.
- Compare the English lexicon to a list of the most common English words, and ensure that the lexicon tends toward common English words.
- Review relative, demonstrative, and affirmative particles and words.
- Verify that interrogative pronouns are being tagged correctly.
- Eliminate tagging errors caught by the error log.
- Tag proper nouns with gender and number.
- Make translations of compound proper nouns more consistent.
- Look into Hebrew texts of deuterocanonical books.
- Provide textual footnotes, including ketiv.
- Determine whether additional punctuation or cantillation marks should be rendered.
- Clean up translation of prepositions to improve English idiom.
