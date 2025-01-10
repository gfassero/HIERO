Imports System.Text.RegularExpressions
Module Main

	' *** CODE: ***
	' Review INTERROGATIVE pronouns in Parse(). It was returning before getting tagged, and I have no idea whether it's being translated consistently.
	' Preserve particle order in words such as Ecc.4.10#08, in which prepositions are AFTER the main lemma.

	' *** LEXICON: ***
	' - Pierce => strike/stricken/strike [out]
	' -        => beat/beaten
	' - Go back and add consistent annotation or translation for -wn suffix (sometimes just -n), and possibly -yt.
	' - Remove all clones.
	' - Download a list of ~1000 most common words: make sure that the lexicon uses few rare words, and see if any rarer words can be replaced with more common words instead.
	' - Address for/for-/fore-/before/forward, which is not marked as FAIL, nor listed in lexicon.md.

	' *** PARSING DICTIONARY: ***
	' - Merge parsing variants into main parsing.
	' - Make Nt = Np

	' Systematically review:
	' - relative particles and words
	' - demonstrative particles
	' - affirmative particles

	' *** TEXT: ***
	' Most proper nouns aren't tagged with gender or number. Can I add them programatically?

	' *** HEBREW COMPILER ***
	' Further ideas for reducing parsing notes to a minimum:
	' - Bring in the regexGrams from the lexicon. IDK how yet.

	' *** GRAMMAR & IDIOM: ***
	' Clean up prepositions: fork preposition translations depending on preceding verb.
	' Check error log (.err).
	' Article with perfect verb Td/V.p => "the one who ran" x20. Unknown nuance. (happens   1x in construct chain: Dan.8.1#12=L: probably a tagging error) (happens once with preposition Rd/V.p)

	' *** DOCUMENTATION ***
	' https://github.com/code4policy/simple-website
	' Change COHERENT to COHESIVE.

	' *** UNDERSTANDING ***
	' What sense should I make of--and what room is allowed for--suffixes as in 1503 and respellings as in 5421/5422, 2413?

	Public Const PrintConstructChains = True
	Public Const collapseParallelStrongs = False
	Sub Main()
		Console.OutputEncoding = System.Text.Encoding.UTF8

		'CompareHebrewSources()
		'CompileHebrewSources(collapseParallelStrongs) ' COMPILE STEPBible and OVERLAYS -> parsingDictionary.bin and annotatedHebrewText.bin

		DefinePNounSplit()
		BuildEnglishPronouns()
		BuildLexicon()

		LoadAnnotatedHebrew() ' LOAD HEBREW from parsingDictionary.bin and annotatedHebrewText.bin

		'VerifyAnnotations(collapseParallelStrongs) ' COMPARE PRE- and POST-ANNOTATION DEBUG OUTPUT from CompileHebrewSources() and LoadHebrewFromAnnotatedText() to SHOW THAT annotatedHebrewText.bin IS ACCURATE

		'TranslateAndSave("Psa") ' No argument (whole corpus), or "Psa", or "Psa.1", or "Psa.1.1"

		RunConcordance()
	End Sub

End Module

' *** DISCOVERIES THAT HAVE IMPRESSED ME ***
' - Psalm 2:6 "I have set my king on my holy hill" is actually "libated out".
' - There's an Aramaic root that yields both "word" and "lamb".
' - Hannah's name means "gracious".
' - Jesse's name is similar to the tetragrammaton.
' - Songs 5:10 My beloved is "ruddy, the chiefest" is actually "red, bannered"
' - Songs 5:11 "locks" of hair are also "thorns"
' - Ezekiel 9:4-6: God's people are marked with and protected by the cross; Psalm 78:41: they forgot his mercy and crossed the holy one.
' - Isaiah 25:11 The saving God has apertures in his hands.
' - The word for "good tidings" (30x) is the same as the word for "flesh" (270x).







' *** REPORTED ISSUES with the STEPBible TEXT ***
' These should all be fixed in future STEPBible uploads, but I should verify them.


' There are 154 words that are tagged As Strongs #854 (preposition) with grammar "To" (direct object indicator). My Hebrew isn't strong enough to determine whether they should be #853 With "To" Or #854 With "R", Or there might be some of both.
'
' \{H0854\}.*To			154 matches
'
' Broken down by type:
' H9006/\ {H0854 \} \ t.*To		112 matches: preceded by #9006 "from"; Nothing following.
' H9006/\ {H0854 \} / H903.*To	 37 matches: preceded by #9006 "from"; followed by an object pronoun #903x.
' H9006/\ {H0854 \} \\ H901.*To	  4 matches: preceded by #9006 "from"; followed by #9014 maqqef Or #9015 paseq.
' H9002/\ {H0854 \}.*To		  1 match: preceded by #9002 vav; followed by #9014 maqqef.

' Sent 28 apparent errors from overlay file.