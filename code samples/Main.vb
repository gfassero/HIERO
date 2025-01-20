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

	' *** PARSING DICTIONARY: ***
	' - Merge parsing variants into main parsing.
	' - Make Nt = Np

	' Systematically review:
	' - relative particles and words
	' - demonstrative particles
	' - affirmative particles

	' *** TEXT: ***
	' Most proper nouns aren't tagged with gender or number. Can I add them programatically?
	' H9016.*c$ shouldn't happen.

	' *** HEBREW COMPILER ***
	' Further ideas for reducing parsing notes to a minimum:
	' - Bring in the regexGrams from the lexicon. IDK how yet.

	' *** GRAMMAR & IDIOM: ***
	' Clean up prepositions: fork preposition translations depending on preceding verb.
	' Check error log (.err).
	' Article with perfect verb Td/V.p => "the one who ran" x20. Unknown nuance. (happens   1x in construct chain: Dan.8.1#12=L: probably a tagging error) (happens once with preposition Rd/V.p)

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