﻿Partial Module Main

	Dim ObjectPronouns As New Dictionary(Of String, String)
	Dim SubjectPronouns As New Dictionary(Of String, String)
	Dim PossessiveAdjectives As New Dictionary(Of String, String)
	Dim ReflexivePronouns As New Dictionary(Of String, String)
	Sub BuildEnglishPronouns()
		Console.Write("Building English pronoun lists...")
		BuildObjectPronouns()
		BuildSubjectPronouns()
		BuildPossessiveAdjectives()
		BuildReflexivePronouns()
		Console.WriteLine(" Done!")
	End Sub
	Sub BuildSubjectPronouns()
		SubjectPronouns.Add("1ms", "I") ' PERSON: 1, 2, 3  //  GENDER: masc, fem, common (verb), both (noun)  //  NUMBER: sing, plu, dual  
		SubjectPronouns.Add("2ms", "you")
		SubjectPronouns.Add("3ms", "he")
		SubjectPronouns.Add("1fs", "I")
		SubjectPronouns.Add("2fs", "you")
		SubjectPronouns.Add("3fs", "she")
		SubjectPronouns.Add("1cs", "I")
		SubjectPronouns.Add("2cs", "you")
		SubjectPronouns.Add("3cs", "it")
		SubjectPronouns.Add("1bs", "I")
		SubjectPronouns.Add("2bs", "you")
		SubjectPronouns.Add("3bs", "it")

		SubjectPronouns.Add("1mp", "we")
		SubjectPronouns.Add("2mp", "you")
		SubjectPronouns.Add("3mp", "they")
		SubjectPronouns.Add("1fp", "we")
		SubjectPronouns.Add("2fp", "you")
		SubjectPronouns.Add("3fp", "they")
		SubjectPronouns.Add("1cp", "we")
		SubjectPronouns.Add("2cp", "you")
		SubjectPronouns.Add("3cp", "they")
		SubjectPronouns.Add("1bp", "we")
		SubjectPronouns.Add("2bp", "you")
		SubjectPronouns.Add("3bp", "they")

		'SubjectPronouns.Add("1md", "we")
		'SubjectPronouns.Add("2md", "you")
		'SubjectPronouns.Add("3md", "they")
		'SubjectPronouns.Add("1fd", "we")
		'SubjectPronouns.Add("2fd", "you")
		'SubjectPronouns.Add("3fd", "they")
		'SubjectPronouns.Add("1cd", "we")
		'SubjectPronouns.Add("2cd", "you")
		'SubjectPronouns.Add("3cd", "they")
		'SubjectPronouns.Add("1bd", "we")
		'SubjectPronouns.Add("2bd", "you")
		'SubjectPronouns.Add("3bd", "they")
	End Sub
	Sub BuildObjectPronouns()
		ObjectPronouns.Add("1ms", ColorMasculine & "me" & ColorEnd) ' PERSON: 1, 2, 3 // GENDER: masc, fem, common (verb), both (noun) // NUMBER: sing, plu, dual 
		ObjectPronouns.Add("2ms", ColorMasculine & "you" & TagSing & ColorEnd)
		ObjectPronouns.Add("3ms", ColorMasculine & "him" & ColorEnd)
		ObjectPronouns.Add("1fs", ColorFeminine & "me" & ColorEnd)
		ObjectPronouns.Add("2fs", ColorFeminine & "you" & TagSing & ColorEnd)
		ObjectPronouns.Add("3fs", ColorFeminine & "her" & ColorEnd)
		'ObjectPronouns.Add("1cs", ColorCommon & "me" & ColorEnd)
		'ObjectPronouns.Add("2cs", ColorCommon & "you" & TagSing & ColorEnd)
		'ObjectPronouns.Add("3cs", ColorCommon & "it" & ColorEnd)
		ObjectPronouns.Add("1bs", ColorCommon & "me" & ColorEnd)
		ObjectPronouns.Add("2bs", ColorCommon & "you" & TagSing & ColorEnd)
		ObjectPronouns.Add("3bs", ColorCommon & "it" & ColorEnd)

		ObjectPronouns.Add("1mp", ColorMasculine & "us" & ColorEnd)
		ObjectPronouns.Add("2mp", ColorMasculine & "you" & TagPlur & ColorEnd)
		ObjectPronouns.Add("3mp", ColorMasculine & "them" & ColorEnd)
		ObjectPronouns.Add("1fp", ColorFeminine & "us" & ColorEnd)
		ObjectPronouns.Add("2fp", ColorFeminine & "you" & TagPlur & ColorEnd)
		ObjectPronouns.Add("3fp", ColorFeminine & "them" & ColorEnd)
		'ObjectPronouns.Add("1cp", ColorCommon & "us" & ColorEnd)
		'ObjectPronouns.Add("2cp", ColorCommon & "you" & TagPlur & ColorEnd)
		'ObjectPronouns.Add("3cp", ColorCommon & "them" & ColorEnd)
		ObjectPronouns.Add("1bp", ColorCommon & "us" & ColorEnd)
		ObjectPronouns.Add("2bp", ColorCommon & "you" & TagPlur & ColorEnd)
		ObjectPronouns.Add("3bp", ColorCommon & "them" & ColorEnd)

		'ObjectPronouns.Add("1md", ColorMasculine & "us" & TagDual & ColorEnd)
		'ObjectPronouns.Add("2md", ColorMasculine & "you" & TagDual & ColorEnd)
		'ObjectPronouns.Add("3md", ColorMasculine & "them" & TagDual & ColorEnd)
		'ObjectPronouns.Add("1fd", ColorFeminine & "us" & TagDual & ColorEnd)
		'ObjectPronouns.Add("2fd", ColorFeminine & "you" & TagDual & ColorEnd)
		'ObjectPronouns.Add("3fd", ColorFeminine & "them" & TagDual & ColorEnd)
		'ObjectPronouns.Add("1cd", ColorCommon & "us" & TagDual & ColorEnd)
		'ObjectPronouns.Add("2cd", ColorCommon & "you" & TagDual & ColorEnd)
		'ObjectPronouns.Add("3cd", ColorCommon & "them" & TagDual & ColorEnd)
		'ObjectPronouns.Add("1bd", ColorCommon & "us" & TagDual & ColorEnd)
		'ObjectPronouns.Add("2bd", ColorCommon & "you" & TagDual & ColorEnd)
		'ObjectPronouns.Add("3bd", ColorCommon & "them" & TagDual & ColorEnd)
	End Sub
	Sub BuildPossessiveAdjectives()
		PossessiveAdjectives.Add("1ms", ColorMasculine & "my" & ColorEnd) ' PERSON: 1, 2, 3 // GENDER: masc, fem, common (verb), both (noun) // NUMBER: sing, plu, dual 
		PossessiveAdjectives.Add("2ms", ColorMasculine & "your" & TagSing & ColorEnd)
		PossessiveAdjectives.Add("3ms", ColorMasculine & "his" & ColorEnd)
		PossessiveAdjectives.Add("1fs", ColorFeminine & "my" & ColorEnd)
		PossessiveAdjectives.Add("2fs", ColorFeminine & "your" & TagSing & ColorEnd)
		PossessiveAdjectives.Add("3fs", ColorFeminine & "her" & ColorEnd)
		PossessiveAdjectives.Add("1cs", ColorCommon & "my" & ColorEnd)
		PossessiveAdjectives.Add("2cs", ColorCommon & "your" & TagSing & ColorEnd)
		PossessiveAdjectives.Add("3cs", ColorCommon & "its" & ColorEnd)
		PossessiveAdjectives.Add("1bs", ColorCommon & "my" & ColorEnd)
		PossessiveAdjectives.Add("2bs", ColorCommon & "your" & TagSing & ColorEnd)
		PossessiveAdjectives.Add("3bs", ColorCommon & "its" & ColorEnd)

		PossessiveAdjectives.Add("1mp", ColorMasculine & "our" & ColorEnd)
		PossessiveAdjectives.Add("2mp", ColorMasculine & "your" & TagPlur & ColorEnd)
		PossessiveAdjectives.Add("3mp", ColorMasculine & "their" & ColorEnd)
		PossessiveAdjectives.Add("1fp", ColorFeminine & "our" & ColorEnd)
		PossessiveAdjectives.Add("2fp", ColorFeminine & "your" & TagPlur & ColorEnd)
		PossessiveAdjectives.Add("3fp", ColorFeminine & "their" & ColorEnd)
		PossessiveAdjectives.Add("1cp", ColorCommon & "our" & ColorEnd)
		PossessiveAdjectives.Add("2cp", ColorCommon & "your" & TagPlur & ColorEnd)
		PossessiveAdjectives.Add("3cp", ColorCommon & "their" & ColorEnd)
		PossessiveAdjectives.Add("1bp", ColorCommon & "our" & ColorEnd)
		PossessiveAdjectives.Add("2bp", ColorCommon & "your" & TagPlur & ColorEnd)
		PossessiveAdjectives.Add("3bp", ColorCommon & "their" & ColorEnd)

		PossessiveAdjectives.Add("1md", ColorMasculine & "our" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("2md", ColorMasculine & "your" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("3md", ColorMasculine & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("1fd", ColorFeminine & "our" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("2fd", ColorFeminine & "your" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("3fd", ColorFeminine & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("1cd", ColorCommon & "our" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("2cd", ColorCommon & "your" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("3cd", ColorCommon & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("1bd", ColorCommon & "our" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("2bd", ColorCommon & "your" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("3bd", ColorCommon & "their" & TagDual & ColorEnd)

		PossessiveAdjectives.Add("-ms", ColorMasculine & "one's" & ColorEnd) ' PERSON: 1, 2, 3 // GENDER: masc, fem, common (verb), both (noun) // NUMBER: sing, plu, dual
		PossessiveAdjectives.Add("-fs", ColorFeminine & "one's" & ColorEnd)
		PossessiveAdjectives.Add("-cs", ColorCommon & "one's" & ColorEnd)
		PossessiveAdjectives.Add("-bs", ColorCommon & "one's" & ColorEnd)

		PossessiveAdjectives.Add("-mp", ColorMasculine & "their" & ColorEnd)
		PossessiveAdjectives.Add("-fp", ColorFeminine & "their" & ColorEnd)
		PossessiveAdjectives.Add("-cp", ColorCommon & "their" & ColorEnd)
		PossessiveAdjectives.Add("-bp", ColorCommon & "their" & ColorEnd)

		PossessiveAdjectives.Add("-md", ColorMasculine & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("-fd", ColorFeminine & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("-cd", ColorCommon & "their" & TagDual & ColorEnd)
		PossessiveAdjectives.Add("-bd", ColorCommon & "their" & TagDual & ColorEnd)
	End Sub
	Sub BuildReflexivePronouns()
		ReflexivePronouns.Add("1ms", "myself") ' PERSON: 1, 2, 3  //  GENDER: masc, fem, common (verb), both (noun)  //  NUMBER: sing, plu, dual  
		ReflexivePronouns.Add("2ms", "yourself")
		ReflexivePronouns.Add("3ms", "himself")
		ReflexivePronouns.Add("1fs", "myself")
		ReflexivePronouns.Add("2fs", "yourself")
		ReflexivePronouns.Add("3fs", "herself")
		ReflexivePronouns.Add("1cs", "myself")
		ReflexivePronouns.Add("2cs", "yourself")
		ReflexivePronouns.Add("3cs", "itself")
		ReflexivePronouns.Add("1bs", "myself")
		ReflexivePronouns.Add("2bs", "yourself")
		ReflexivePronouns.Add("3bs", "itself")

		ReflexivePronouns.Add("1mp", "ourselves")
		ReflexivePronouns.Add("2mp", "yourselves")
		ReflexivePronouns.Add("3mp", "themselves")
		ReflexivePronouns.Add("1fp", "ourselves")
		ReflexivePronouns.Add("2fp", "yourselves")
		ReflexivePronouns.Add("3fp", "themselves")
		ReflexivePronouns.Add("1cp", "ourselves")
		ReflexivePronouns.Add("2cp", "yourselves")
		ReflexivePronouns.Add("3cp", "themselves")
		ReflexivePronouns.Add("1bp", "ourselves")
		ReflexivePronouns.Add("2bp", "yourselves")
		ReflexivePronouns.Add("3bp", "themselves")

		ReflexivePronouns.Add("1md", "ourselves")
		ReflexivePronouns.Add("2md", "yourselves")
		ReflexivePronouns.Add("3md", "themselves")
		ReflexivePronouns.Add("1fd", "ourselves")
		ReflexivePronouns.Add("2fd", "yourselves")
		ReflexivePronouns.Add("3fd", "themselves")
		ReflexivePronouns.Add("1cd", "ourselves")
		ReflexivePronouns.Add("2cd", "yourselves")
		ReflexivePronouns.Add("3cd", "themselves")
		ReflexivePronouns.Add("1bd", "ourselves")
		ReflexivePronouns.Add("2bd", "yourselves")
		ReflexivePronouns.Add("3bd", "themselves")

		ReflexivePronouns.Add("-ms", "oneself") ' PERSON: 1, 2, 3  //  GENDER: masc, fem, common (verb), both (noun)  //  NUMBER: sing, plu, dual
		ReflexivePronouns.Add("-fs", "oneself")
		ReflexivePronouns.Add("-cs", "oneself")
		ReflexivePronouns.Add("-bs", "oneself")

		ReflexivePronouns.Add("-mp", "themselves")
		ReflexivePronouns.Add("-fp", "themselves")
		ReflexivePronouns.Add("-cp", "themselves")
		ReflexivePronouns.Add("-bp", "themselves")

		ReflexivePronouns.Add("-md", "themselves")
		ReflexivePronouns.Add("-fd", "themselves")
		ReflexivePronouns.Add("-cd", "themselves")
		ReflexivePronouns.Add("-bd", "themselves")
	End Sub
End Module