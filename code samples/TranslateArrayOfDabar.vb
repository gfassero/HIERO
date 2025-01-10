Partial Module Main
	Sub TranslateArrayOfDabar(dabars() As Dabar, Optional searchList() As String = Nothing, Optional printCitations As Boolean = True, Optional suppressBookStart As Boolean = False)
		If dabars Is Nothing OrElse dabars.Length = 0 Then Exit Sub

		Dim lastCitation As String = ""
		Dim dabar As Dabar

		For h As Integer = 0 To dabars.Length - 1
			dabar = dabars(h)

			If dabar.Citation <> lastCitation AndAlso printCitations Then ' Reveal verse/chapter/book change
				AnnounceCitation(dabar.Citation, (lastCitation = "" AndAlso Not suppressBookStart), dabar.Aramaic, suppressBookStart)
				lastCitation = dabar.Citation

			ElseIf dabar.Aramaic <> lastAramaic Then
				Dim aramflag As String = IIf(dabar.Aramaic, AramaicFlag.Trim, Nothing)
				lastAramaic = dabar.Aramaic
				If SaveToFileInsteadOfConsole Then ' SAVE to FILE
					Reveal(CitationBegin & aramflag & CitationEnd, True)
				Else ' PRINT to CONSOLE
					Reveal(" {" & aramflag & "} ", True)
				End If
			End If

			'If dabar.ConstructChainPos = ConstructChainPosition.Open AndAlso PrintConstructChains Then _
			'	Reveal(ConstructChainBegin, True) ' Open construct chain

			If h + 1 < dabars.Length Then
				dabar.Translate(searchList, dabars(h + 1).Citation) ' Translate the dabar
			Else
				dabar.Translate(searchList) ' Translate the dabar
			End If

			'If dabar.ConstructChainPos = ConstructChainPosition.Close AndAlso PrintConstructChains Then _
			'	Reveal(ConstructChainEnd, True) ' Close construct chain

			If dabar.Cantillation IsNot Nothing Then Reveal(dabar.Cantillation, True) ' Add cantillation-based punctuation

			dabar.PrintFollowingSpaceIfAppropriate() ' Add a space to prepare for the next dabar

		Next
	End Sub

End Module