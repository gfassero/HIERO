Partial Module Main

	Public Const TagSing = Nothing ' or ¹
	Public Const TagDual = "²"c
	Public Const TagPlur = "⁺"c

	Public Const ColorMasculine = "<span class=""m"">"
	Public Const ColorFeminine = "<span class=""f"">"
	Public Const ColorCommon = "<span class=""c"">"
	Public Const ColorEnd = "</span>"

	Public Const WordLink = "·"c
	Public Const Space = "&nbsp;&shy;" ' " "c
	Public Const UnverifiedLexiconFlag = "¬"c ' "⌐"c

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

	Public Const CitationBegin = vbCrLf & "<p data-number="""
	Public Const CitationBeginChapter = vbCrLf & "<p class=""chapter"" data-number="""
	Public Const CitationEnd = """>"
	Public Const AramaicFlag = "&#9653; "
	Public Const MidverseAramaicSwitch = " data-number=""" & AramaicFlag & """"

	Public Const OutputHeader = "<!DOCTYPE html>" & vbCrLf &
		"<!-- This content may not be copied, shared, distributed, modified, or used for any purpose," & vbCrLf &
		"     commercial or non-commercial, without prior written permission from the author. -->" & vbCrLf &
		"<html lang=""en"">" & vbCrLf &
		"<head>" & vbCrLf &
		"<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" & vbCrLf &
		"<title>HIERO - Hebrew Idiom in English Roots</title>" & vbCrLf &
		"<link rel=""stylesheet"" href=""styles.css"" />" & vbCrLf & ' "<script type=""text/javascript"" src=""slurs.js""></script>" & vbCrLf &
		"</head>" & vbCrLf &
		"<body>" & vbCrLf &
		"<div id=""header"" class=""headfoot"">" & vbCrLf &
		"<a href=""http://gfassero.github.io/HIERO/"" class=""split"">About</a>" &
		"<a href=""key.html"">Key</a>" &
		"<a href=""index.html"">Bible</a>"

	Public Const OutputBeginTranslationMinimal = "</div>" & vbCrLf &
		"<div id=""printable"" class=""panel"">" & vbCrLf &
		"<div id=""translation"">"
	Public Const OutputBeginTranslation = "<a href=""#table-of-sections"">Sections</a>" & vbCrLf & OutputBeginTranslationMinimal



	Public Const OutputFooter = vbCrLf &
		"</div>" & vbCrLf &
		"<div id=""footer"" class=""headfoot"">" &
		vbCrLf & "<a href=""http://gfassero.github.io/HIERO/"" class=""title"">HIERO"
	Public Const OutputFooterClose = "</a>"

	Public Const OutputTOC = vbCrLf &
		"</div>" & vbCrLf &
		"<div id=""table-of-sections"" class=""headfoot"">"
	Public Const OutputTOCBook1 = vbCrLf &
		"</span>"
	Public Const OutputTOCBook2 = vbCrLf &
		"<span class=""columnsTOC"">"

	Public Const OutputTOCGlossary = "</div>" & vbCrLf &
									 "<div id=""table-of-sections"" class=""headfoot"">" & vbCrLf &
									 "<a href=""glossary.html"">By Category</a>" & vbCrLf &
									 "<a href=""glossary-frequency.html"">By Frequency</a>" & vbCrLf &
									 "<a href=""glossary-alphabetical.html"">Alphabetical</a>"

	Public Const OutputEnd = vbCrLf &
							 "</div>" & vbCrLf &
							 "</div>" & vbCrLf &
							 "</body>" & vbCrLf &
							 "</html>"



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

	Sub AnnounceHeader(bookNameThreeChars As String)

		Reveal(String.Concat("<p class=""book"" id=""book-", bookNameThreeChars, """>",
					 "Book of " & Books(bookNameThreeChars).Item1, ' ENGLISH TITLE
					 "</p>", vbCrLf), True)

		Reveal("<p class=""subtitle"">", True)
		TranslateArrayOfDabar(Books(bookNameThreeChars).Item2, Nothing, False) ' HEBREW TITLE
		Reveal(Books(bookNameThreeChars).Item3 & "</p>", True)

	End Sub

	Sub AnnounceCitation(citation As String, announceStart As Boolean, aramaic As Boolean, Optional suppressBookStart As Boolean = False)
		citation = StripAlternativeNumbering(citation)

		lastAramaic = aramaic

		Dim citParts() As String = citation.Split("."c)

		If SaveToFileInsteadOfConsole Then ' SAVE to FILE

			If ((citParts(1) = "1" AndAlso citParts(2) = "1") OrElse announceStart) AndAlso Not suppressBookStart Then

				AnnounceHeader(citParts(0))

				'Anchors.Add((
				'			"book-" & citParts(0),      ' Anchor name
				'			Books(citParts(0)).Item1    ' Display name in TOC
				'			))

				lastCitChap = Nothing

			End If

			If citParts(0) = "Psa" AndAlso (citParts(1) <> lastCitChap OrElse announceStart) AndAlso Not suppressBookStart Then
				Reveal(String.Concat(vbCrLf & "<p class=""psalm"" id=""psalm-" & citParts(1) & """>Psalm ", citParts(1), "</p>"), True)
				Anchors.Add((
							"psalm-" & citParts(1),   ' Anchor name
							citParts(1)               ' Display name in TOC. Was:   "Psalm " & citParts(1)
							))

			End If

			If citParts(2) = "0" Then
				Reveal("<p>", True)

			ElseIf citParts(2) <> "1" OrElse citParts(0) = "Psa" Then
				Reveal(CitationBegin & IIf(aramaic, AramaicFlag, Nothing) & citParts(2) & CitationEnd, True)

			Else
				Reveal(CitationBeginChapter & IIf(aramaic, AramaicFlag.TrimEnd, Nothing) & citParts(1) & CitationEnd, True)

			End If

		Else ' PRINT to CONSOLE

			If announceStart Then
				Reveal(IIf(aramaic, AramaicFlag, Nothing) & citation & ": ", True)

			Else
				Reveal(" (" & citParts(1) & ":" & IIf(aramaic, AramaicFlag, Nothing) & citParts(2) & ") ", True)

			End If

		End If

		lastCitChap = citParts(1)

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

	Function Bracket(input As String) As String
		Return input.Replace("["c, OpenBrackets).
					 Replace("]"c, CloseBrackets).
					 Replace("{"c, OpenXlit).
					 Replace("}"c, CloseXlit)
	End Function
End Module