Partial Module Main

	Public Const Space = "&nbsp;&shy;" ' " "c

	Private aramaicThe As String ' Defined later, after the lexicon is generated.

	Public Const ParticipleSing = "that"
	Public Const ParticiplePlur = "that" ' "those" & WordLink & "that"
	Public Const ParticipleSingProgressive = "that" & WordLink & "is"
	Public Const ParticiplePlurProgressive = "that" & WordLink & "are" ' "those" & WordLink & "that" & WordLink & "are"

	Public Const CommandFlag = "ꜝ"c
	Public Const CohortativeSing = CommandFlag & "let" & WordLink & "me" & WordLink
	Public Const CohortativePlur = CommandFlag & "let" & WordLink & "us" & WordLink
	Public Const JussiveMasc = CommandFlag & "let" & WordLink & "him" & WordLink
	Public Const JussiveFemn = CommandFlag & "let" & WordLink & "her" & WordLink
	Public Const JussiveNeut = CommandFlag & "let" & WordLink & "it" & WordLink
	Public Const JussivePlur = CommandFlag & "let" & WordLink & "them" & WordLink

	Public Const MidverseAramaicSwitch = " data-number=""" & AramaicFlag & """"

	Public Const IncludeChaptersInTOC = False






	' Public Const CustomTab = "&emsp;&emsp;"
	' Public Const CustomTabx2 = CustomTab & CustomTab
	' Public Const CustomTabx4 = CustomTabx2 & CustomTabx2

	Public Const Indent1 = "</p><p class=""in1"">" ' "<br/>" & CustomTab
	Public Const Indent2 = "</p><p class=""in2"">" ' "<br/>" & CustomTabx2

	Public Const SofPasuq = "&nbsp;:</p>"
	Public Const EndLine = "</p>"
	Public Const FLAG_PeOpenMajor = "FLAG_PeOpenMajor"
	Public Const FLAG_SaClosMinor = "FLAG_SaClosMinor"
	Public Const PeOpenMajorBreakBegin = vbCrLf & "<p class=""section"" id=""section-"
	Public Const PeOpenMajorBreakEnd = """>&sect;</p>"
	Public Const SamekhClosedMinorBreak = vbCrLf & "<p class=""paragraph""></p>"

	' Public Const ConstructChainBegin = "<span class=""cchain"">"
	' Public Const ConstructChainEnd = "</span>"


	Sub TagGender(ByRef contents As String, wordGender As Char, Optional allowPropers As Boolean = False)
		Select Case wordGender
			Case "m"c
				contents = ColorMasculine & contents & ColorEnd
			Case "f"c
				contents = ColorFeminine & contents & ColorEnd
			Case "c"c, "b"c
				contents = ColorCommon & contents & ColorEnd
			Case "t"c, "l"c ' TITLE or LOCATION (PROPER NOUN) maybe should still have gender color though
				If allowPropers Then
					contents = ColorCommon & contents & ColorEnd
				Else
					Throw New ArgumentException("Unexpected gender: " & wordGender)
				End If
			Case Else
				Throw New ArgumentException("Unexpected gender: " & wordGender)
		End Select
	End Sub

	Private lastCitChap As String

	Function StripAlternativeNumbering(citation As String) As String
		If citation.Contains("("c) Then
			Return citation.Substring(0, citation.IndexOf("("c))
		Else
			Return citation
		End If
	End Function

	Sub AnnounceHeader(bookNameThreeChars As String)

		Reveal(String.Concat("<p class=""book"" id=""book-", bookNameThreeChars, """>",
					 "Book of " & Books(bookNameThreeChars).Item1, ' ENGLISH TITLE
					 "</p>", vbCrLf), True)

		Reveal("<p class=""subtitle"">", True)
		TranslateArrayOfDabar(Books(bookNameThreeChars).Item2, Nothing, False) ' HEBREW TITLE
		Reveal(Books(bookNameThreeChars).Item3 & "</p>", True)

	End Sub

	Sub Reveal(revelation As String, Optional bypassSpaceCheck As Boolean = False)
		If revelation Is Nothing Then Exit Sub

		If Not SaveToFileInsteadOfConsole Then ' IF PRINTING TO CONSOLE...
			revelation = Text.RegularExpressions.Regex.Replace(revelation.Replace(":</p>", ":").Replace("</p>", ", "), "<[^<>]+>", "").Replace(Space, " "c).Replace("&nbsp;", " "c).Replace("&#9653; ", "")
		End If

		If (Not bypassSpaceCheck) AndAlso Text.RegularExpressions.Regex.Replace(revelation.TrimEnd, "<[^<>]+>", "").Contains(" "c) Then
			Throw New ArgumentException("Unexpected space character in translation of single word")
		End If

		revelation = revelation.
			Replace("]"c & WordLink & "["c, WordLink). ' [WORD] [WORD] => [WORD WORD]
			Replace("]"c & WordLink, WordLink & "]"c). ' [WORD] WORD   => [WORD ]WORD
			Replace(WordLink & "["c, "["c & WordLink)  '  WORD [WORD]  => WORD[ WORD]

		If SaveToFileInsteadOfConsole Then
			OutputFile.Write(Bracket(revelation))
		Else
			Console.Write(revelation)
		End If

	End Sub

End Module