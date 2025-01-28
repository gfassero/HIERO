Module Main

	Public Const PrintConstructChains = True
	Public Const collapseParallelStrongs = False

	Sub Main()
		Console.OutputEncoding = System.Text.Encoding.UTF8

		'CompareHebrewSources()
		CompileHebrewSources(collapseParallelStrongs) ' COMPILE STEPBible and OVERLAYS -> parsingDictionary.bin and annotatedHebrewText.bin

		DefinePNounSplit()
		BuildEnglishPronouns()
		BuildLexicon()

		LoadAnnotatedHebrew() ' LOAD HEBREW from parsingDictionary.bin and annotatedHebrewText.bin

		'VerifyAnnotations(collapseParallelStrongs) ' COMPARE PRE- and POST-ANNOTATION DEBUG OUTPUT from CompileHebrewSources() and LoadHebrewFromAnnotatedText() to SHOW THAT annotatedHebrewText.bin IS ACCURATE

		'TranslateAndSave("Psa") ' No argument (whole corpus), or "Psa", or "Psa.1", or "Psa.1.1"

		RunConcordance()
	End Sub

End Module