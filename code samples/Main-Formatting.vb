Partial Module Main

	Public Const TagSing = Nothing ' or ¹
	Public Const TagDual = "²"c
	Public Const TagPlur = "⁺"c

	Public Const ColorMasculine = "<span class=""m"">"
	Public Const ColorFeminine = "<span class=""f"">"
	Public Const ColorCommon = "<span class=""c"">"
	Public Const ColorEnd = "</span>"

	Public Const WordLink = "·"c
	Public Const Space = " "c
	Public Const UnverifiedLexiconFlag = "¬"c ' "⌐"c

	' Public Const beginQuestion = "¿"c

	Private maqqef As String ' Defined later, after the lexicon is generated.
	Private aramaicThe As String ' Defined later, after the lexicon is generated.
	'Public ParticipleSingDefinite As String
	'Public ParticiplePlurDefinite As String

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

	Public Const CitationBegin = vbCrLf & "<span class=""citation"">"
	Public Const CitationBeginChapter = vbCrLf & "<span class=""citation chapter"">"
	Public Const CitationEnd = "</span> "
	Public Const AramaicFlag = " ▲"

	Public Const OutputHeaderString1 = "<!DOCTYPE html>" & vbCrLf &
		"<!-- This content may not be copied, shared, distributed, modified, or used for any purpose," & vbCrLf &
		"     commercial or non-commercial, without prior written permission from the author. -->" & vbCrLf &
		"<html lang=""en"">" & vbCrLf &
		"<head>" & vbCrLf &
		"<title>HIERO - Hebrew Idiom in English Roots</title>" & vbCrLf &
		"<link rel=""stylesheet"" href=""styles.css"">" & vbCrLf & ' "<script type=""text/javascript"" src=""slurs.js""></script>" & vbCrLf &
		"</head>" & vbCrLf &
		"<body>" & vbCrLf &
		"<span id=""header"">" & vbCrLf &
		"<a href=""../"" class=""title"">HIERO</a> " ' OUTPUTHEADER() will append current date and sequence number
	Public Const OutputHeaderString2 = " / <a href=""../hiero.html"">Contents</a>"

	Public Const OutputBeginTranslation = " / <a href=""#table-of-sections"">Sections</a>" & vbCrLf &
		"</span>" & vbCrLf &
		"<span id=""translation"">"

	Public Const OutputFooterTOC = vbCrLf &
		"</span>" & vbCrLf &
		"<span id=""table-of-sections"">" & vbCrLf &
		"<span class=""columnsTOC"">"
	Public Const OutputFooterTOCBook1 = vbCrLf &
		"</span>"
	Public Const OutputFooterTOCBook2 = vbCrLf &
		"<span class=""columnsTOC"">"
	Public Const OutputFooterFinal = vbCrLf &
		"</span>" & vbCrLf &
		"</span>" & vbCrLf &
		"</body>" & vbCrLf &
		"</html>"

	Public Const CustomTab = "&emsp;&emsp;"
	Public Const CustomTabx2 = CustomTab & CustomTab
	Public Const CustomTabx4 = CustomTabx2 & CustomTabx2

	'Public Const SofPasuq = " <span class=""sofpasuq"">:</span>"
	'Public Const PeOpenMajorBreak = vbCrLf & "<span class=""majorbreak""></span>"
	'Public Const SamekhClosedMinorBreak = vbCrLf & "<span class=""minorbreak""></span>"
	Public Const SofPasuq = " :<br/>"
	Public Const PeOpenMajorBreak = vbCrLf & "<br/>" & CustomTabx4 & "&sect;<br/><br/>"
	Public Const SamekhClosedMinorBreak = vbCrLf & "<br/>"

	Public Const ConstructChainBegin = "<span class=""cchain"">"
	Public Const ConstructChainEnd = "</span>"

	Public Const OpenBrackets = "<span class=""edit"">"
	Public Const CloseBrackets = "</span>"
	Public Const OpenXlit = "<i>"
	Public Const CloseXlit = "</i>"

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
	Private lastAramaic As Boolean

	Function StripAlternativeNumbering(citation As String) As String
		If citation.Contains("("c) Then
			Return citation.Substring(0, citation.IndexOf("("c))
		Else
			Return citation
		End If
	End Function

	Sub AnnounceHeader(bookNameThreeChars As String, Optional heading As String = Nothing, Optional subheading As String = Nothing)

		If bookNameThreeChars IsNot Nothing Then
			heading = "Book of " & Books(bookNameThreeChars).Item1
			subheading = Books(bookNameThreeChars).Item3
		End If

		Reveal(String.Concat("<br/>", vbCrLf), True)

		If bookNameThreeChars IsNot Nothing OrElse heading IsNot Nothing Then
			Reveal(String.Concat(CustomTabx4, "<span class=""book"" id=""book-", bookNameThreeChars, """>",
					 heading, ' ENGLISH TITLE
					 "</span><br/>", vbCrLf), True)
		End If

		If bookNameThreeChars IsNot Nothing OrElse subheading IsNot Nothing Then
			Reveal(CustomTabx4 & "<span class=""edit"">", True)
		End If

		If bookNameThreeChars IsNot Nothing Then
			TranslateArrayOfDabar(Books(bookNameThreeChars).Item2, Nothing, False) ' HEBREW TITLE
		End If

		If bookNameThreeChars IsNot Nothing Then
			Reveal(subheading & "</span><br/><br/>", True)
		ElseIf subheading IsNot Nothing Then
			Reveal(subheading & "</span><br/>", True)
		End If

	End Sub

	Sub AnnounceCitation(citation As String, announceStart As Boolean, aramaic As Boolean, Optional suppressBookStart As Boolean = False)
		citation = StripAlternativeNumbering(citation)

		Dim aramflag As String = IIf(aramaic, AramaicFlag, Nothing)
		lastAramaic = aramaic

		Dim citParts() As String = citation.Split("."c)

		If SaveToFileInsteadOfConsole Then ' SAVE to FILE

			If ((citParts(1) = "1" AndAlso citParts(2) = "1") OrElse announceStart) AndAlso Not suppressBookStart Then
				Console.Title = citParts(0)

				AnnounceHeader(citParts(0))
				'Anchors.Add((
				'			"book-" & citParts(0),      ' Anchor name
				'			"anchor-book",              ' CSS class in TOC
				'			Books(citParts(0)).Item1    ' Display name in TOC
				'			))

				lastCitChap = Nothing

			End If

			If citParts(0) = "Psa" AndAlso (citParts(1) <> lastCitChap OrElse announceStart) AndAlso Not suppressBookStart Then
				Reveal(String.Concat(vbCrLf & "<br/>" & vbCrLf & "<span class=""psalm"" id=""psalm-" & citParts(1) & """>Psalm ", citParts(1), "</span><br/>"), True)
				Anchors.Add((
							"psalm-" & citParts(1),   ' Anchor name
							"anchor-psalm",           ' CSS class in TOC
							"Psalm " & citParts(1)    ' Display name in TOC
							))

			End If

			If citParts(2) = "0" Then
				' Print nothing for Psalm dedication

			ElseIf citParts(2) <> "1" OrElse citParts(0) = "Psa" Then
				Reveal(CitationBegin & citParts(2) & aramflag & CitationEnd, True)

			Else
				Reveal(CitationBeginChapter & citParts(1) & aramflag & CitationEnd, True)

			End If

		Else ' PRINT to CONSOLE

			If announceStart Then
				Reveal(citation & aramflag & ": ", True)

			Else
				Reveal(" (" & citParts(1) & ":" & citParts(2) & aramflag & ") ", True)

			End If

		End If

		lastCitChap = citParts(1)

	End Sub

	Sub Reveal(revelation As String, Optional bypassSpaceCheck As Boolean = False)

		If Not SaveToFileInsteadOfConsole Then
			revelation = Text.RegularExpressions.Regex.Replace(revelation, "<[^<>]+>", "").Replace("&emsp;", "")
		End If

		If (Not bypassSpaceCheck) AndAlso Text.RegularExpressions.Regex.Replace(revelation, "<[^<>]+>", "").Contains(" "c) Then
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

	Function Bracket(input As String) As String
		Return input.Replace("["c, OpenBrackets).
					 Replace("]"c, CloseBrackets).
					 Replace("{"c, OpenXlit).
					 Replace("}"c, CloseXlit)
	End Function

	Private runtimeHeader As String
	Sub GenerateRuntimeHeader()
		runtimeHeader = OutputHeaderString1 &
			Now.ToString("yy.MM-") &
			(((((Now.Day - 1) * 24 + Now.Hour) * 60 + Now.Minute) * 60 + Now.Second) \ 27).ToString("D5") &
			OutputHeaderString2
	End Sub
End Module