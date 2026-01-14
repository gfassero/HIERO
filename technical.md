# The Program
In this article:

* Auto-generated table of contents:
{:toc}

## English Lexicon
The English lexicon is the most important part of HIERO—and the part that has taken the most time to develop. The goal of the English lexicon is to link each Hebrew root with a single English root and vice versa.

### Lexicon Code
HIERO’s English lexicon contains about 9,500 definitions of Hebrew and Aramaic words. The lexicon is written in XML code. The XML code, part-of-speech mappings, and Strongs number mappings that structure the lexicon are based on the [OSHB Hebrew Lexicon](http://github.com/openscriptures/HebrewLexicon/blob/master/HebrewStrong.xml) by the [Open Scriptures Hebrew Bible Project](http://hb.openscriptures.org/). All of the definitions are my own. The following is a partial sample from the lexicon:
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

### Format
The lexicon tags follow the following rules:
1. Each Hebrew root has a single root `<entry>` element. Related words derived from the root will have <entry> elements nested within the root element.
2. A `<w>` element contains the entry’s Hebrew root.
3. A `<pos>` element describes the entry’s part of speech.
4. A `<def>` element contains the entry’s English translation.
5. A verb entry does not contain a `<def>` element. Instead, each verb entry contains up to eight different translations corresponding to the eight Hebrew functional verb stem types (nine in Aramaic). In this example, the stem translations are contained in the `<qalAct>`, `<pielAct>`, and `<hiphilAct>` elements.

   Any English translation of a stem that appears in the Hebrew text but not in the English lexicon is derived regularly from the translation of another stem. For example, HIERO can automatically derive the translation of the Piel passive stem (“be reshaped”) from the translation provided for the Piel active stem (“reshape”).
6. Verb translations may contain the degree sign (“[keep] °[in] shape”). This marks the position at which the object of the verb should appear in English idiom. If, in the Hebrew text, the object of the verb is indicated by a suffix appended to the verb itself, then HIERO will place the object at the position indicated by the degree sign (“[keep] him [in] shape”). Otherwise, if the verb and its object are separate Hebrew words, the object will not be repositioned (“[keep in] shape the king”), as doing so would require disrupting the Hebrew word order.
7. The lexicon may contain words in brackets. Brackets indicate a part of the translation that is essential to the meaning but is not derived from the Hebrew root. For example, the Hiphil stem of בָּרָא means to “keep in shape,” but it does not actually include a word meaning “keep” nor a preposition meaning “in.“” The translation “[keep] °[in] shape” includes words in brackets to indicate the meaning more clearly without suggesting that these words appear in Hebrew. In the final rendering, brackets are replaced with gray text.
8. The lexicon may contain words in braces. Braces indicate a transliteration from the Hebrew root, or a close English cognate of the Hebrew root. For example, the Hebrew word for “cinnamon” is “kinamon.” The translation “{cinnamon}” includes braces to indicate that this word is transliterated from Hebrew. In the final rendering, braces are replaced with italic text.
9. An `<xref>` element contains a list of Hebrew lexical numbers that HIERO will represent using the English entry. Entry numbers are based on the Strongs numbering system but include letter suffixes to differentiate senses that were not differentiated in the original Strongs system.

### Further Reading
- [More about the lexicon](lexicon.md)
- [A longer excerpt from the lexicon](resource%20samples/multilex_EXCERPT.xml)
- [Complete glossary](https://gfassero.github.io/HIERO/read/glossary.html)

## Annotated Hebrew Text
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

### Format
The Hebrew text is formatted with one Hebrew word per line. Each word is numbered (two digits, preceded by #), and its source text is noted (“L” indicates the Leningrad codex). Words that could be parsed in multiple ways are annotated with a number to indicate the appropriate parsing variant. In this example, word #01 could be followed by the word “of,” but since the context makes that impossible, the annotation “1” indicates to translate it without the word “of.” Word #02 could be parsed as an Aramaic word, but since this is a Hebrew text, the annotation “1” indicates to translate it as Hebrew.

### Further Reading
- [Full annotated Hebrew text](resource%20samples/annotatedHebrewText.bin)
- [Original TAHOT data](https://github.com/STEPBible/STEPBible-Data/tree/master/Translators%20Amalgamated%20OT%2BNT) from STEP Bible
- [A sample of my corrections to the TAHOT data](resource%20samples/Hebrew%20OT%20Overlay_EXCERPT.txt)

## Hebrew Parsing Dictionary
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

### Format
Lexical tags refer to entry numbers found in the `<xref>` tags in the English lexicon. Morphology codes describe the word’s part of speech, form, person, gender, number, and state. Alternative parsing variants are given in additional columns.

### Further Reading
- [A full description of morphology codes](http://docs.google.com/document/d/1wQ67vPIrNxvICy5QmSeromQUJmePml1nQxv7n1gJ8qw) from STEP Bible
- [A longer excerpt from the parsing dictionary](resource%20samples/parsingDictionary_EXCERPT.bin)

## Program Code
HIERO is written in Visual Basic and contains 2,100 lines of executable code. Samples of the code are available in [HIERO’s GitHub repository](https://github.com/gfassero/HIERO) under “code samples.” HIERO does not use artificial intelligence. Translating the Hebrew Old Testament takes 24 seconds on an ordinary laptop.

HIERO iterates through the annotated Hebrew text and looks up each word’s corresponding parsing from the Hebrew parsing dictionary. After finding the parsing, HIERO looks up the translation in the English lexicon and inflects and formats the English translation. Finally, HIERO saves the translation to the user’s computer.

## Translation Walkthrough

In this walkthrough, we will follow HIERO as it translates the first word of the Hebrew scriptures.

First, HIERO reads the first word of the annotated Hebrew text:

|Word number|Hebrew|Parsing variant|
|-----|-----|-----|
|Gen.1.1#01=L|בְּרֵאשִׁ֖ית|1|

HIERO selects the Hebrew word `בְּרֵאשִׁית` from this line, and then searches the first column of Hebrew parsing dictionary for `בְּרֵאשִׁית`. When it finds `בְּרֵאשִׁית`, it selects the rest of the row:

|Hebrew|Lexicon|Morphology|Lex. variant 1|Morph. var. 1|Lex. var. 2|Morph. var. 2|
|-----|-----|-----|-----|-----|-----|-----|
|בְּרֵאשִׁית|H9003/{H7225G}|HR/Ncfsc|H9003/{H7225G}|HR/Ncfsa|||

This entry from the parsing dictionary offers two parsing options, so HIERO returns to the current line of the annotated Hebrew text:

|Word number|Hebrew|Parsing variant|
|-----|-----|-----|
|Gen.1.1#01=L|בְּרֵאשִׁ֖ית|1|

and selects the parsing variant number `1`. It then selects parsing variant number `1` from the Hebrew parsing dictionary:

|Hebrew|Lex. variant 1|Morph. var. 1|
|-----|-----|-----|
|בְּרֵאשִׁית|H9003/{H7225G}|HR/Ncfsa|

This is the appropriate parsing for the word `בְּרֵאשִׁית` in this instance.

The first part of the parsing, `H9003/{H7225G}`, is the lexicon tag. The second part, `HR/Ncfsa`, is the morphological tag. Each of these tags is divided by a forward slash, `/`, indicating that this word, `בְּרֵאשִׁית`, has two parts: the prefix `בְּ`, and the main word `רֵאשִׁית`.

HIERO begins with the prefix `בְּ`, which is marked with the lexicon tag `H9003` and the morphological tag `HR`. The `H` at the beginning of each tag is not used.

HIERO selects the lexicon tag `H9003`, discards the `H`, and searches the English lexicon for the entry matching `9003`:

```xml
	<xref strong="9003" />
```

HIERO selects the full lexicon entry:

```xml
<entry>
	<w xlit="bĕ">בְּ</w>
	<pos>R</pos>
	<def>in</def>
	<xref strong="9003" />
</entry>
```

HIERO then selects the definition `in` from this entry:

```xml
	<def>in</def>
```

HIERO then returns to the morphological tag `HR` and discards the `H`. The remaining `R` indicates a preposition with no additional inflection. This means that `in` does not need to be inflected.

HIERO saves `in` as the first part of the translation of `בְּרֵאשִׁית`. Since there is another part to be translated, HIERO appends a middle dot `·`, indicating that what follows is a continuation of the same Hebrew word `בְּרֵאשִׁית`:

```
in·
```

HIERO then moves on to the main word `רֵאשִׁית`, which is marked with the lexicon tag `{H7225G}` and the morphological tag `Ncfsa`.

HIERO selects the lexicon tag `{H7225G}`. HIERO removes the braces `{...}`, which mark the main word, and discards the `H`. It then searches the English lexicon for the entry matching `7225G`:

```xml
	<xref strong="7225G,7225H" />
```

HIERO selects the full lexicon entry:

```xml
<entry>
	<w xlit="rēʾšît">רֵאשִׁית</w>
	<pos>N</pos>
	<def>headmost</def>
	<xref strong="7225G,7225H" />
</entry>
```

HIERO then selects the definition from the entry:

```xml
	<def>headmost</def>
```

HIERO then returns to the morphological tag `Ncfsa`. The first character, `N`, indicates a noun. HIERO parses the remainder of the morphological tag using parsing rules as defined for nouns.

The second character, `c`, indicates a common noun. Common nouns require no capitalization, so HIERO keeps the translation in lower case.

HIERO skips forward to the fourth character, which indicates number. Since `s` indicates singular number, HIERO keeps the translation singular.

The fifth character, indicates the state of the verb, which is either construct or absolute. `a` indicates the absolute state, which does not require any inflection in English.

So far, our translation of the main word `רֵאשִׁית` is still simple:

```
headmost
```

HIERO then returns to the third character, which indicates gender. `f` indicates feminine gender, so HIERO wraps the translation in HTML tags indicating `class="f"`:

```
<span class="f">headmost</span>
```

The two parts of the word `בְּרֵאשִׁית` have now been translated:

```
in·
```

and

```
<span class="f">headmost</span>
```

The Hebrew word `בְּרֵאשִׁית` does not contain any significant cantillation marks, so no punctuation or line breaks are added.

Finally, HIERO saves the result to an HTML file on the user’s computer:

```
in·<span class="f">headmost</span>
```

The HTML output file references a CSS stylesheet in the same folder as the HTML output file. The CSS stylesheet includes rules for rendering the output. The HTML class `class="f"` in the output references the `.f` rule in the stylesheet, which indicates that the output should be rendered with a red arc. When the HTML output file is opened by the user, the output is rendered as:

in·<span class="f">headmost</span>

HIERO then moves on to the second word of the annotated Hebrew text:

|Word number|Hebrew|Parsing variant|
|-----|-----|-----|
|Gen.1.1#02=L|בָּרָ֣א|1|

## Further Development
- Improve the English lexicon.
- Add consistent annotation for the Hebrew ון- suffix.
- Compare the English lexicon to a list of the most common English words, and ensure that the lexicon tends toward common English words.
- Review relative, demonstrative, and affirmative particles and words.
- Verify that interrogative pronouns are being tagged correctly.
- Eliminate tagging errors caught by the error log.
- Tag proper nouns with gender and number.
- Finish adding proper nouns to lexicon.
- Make translations of compound proper nouns more consistent.
- Clean up translation of prepositions to improve English idiom.
