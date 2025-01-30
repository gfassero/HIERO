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
			End If

			'If dabar.ConstructChainPos = ConstructChainPosition.Open AndAlso PrintConstructChains Then _
			'	Reveal(ConstructChainBegin, True) ' Open construct chain

			dabar.Translate(searchList) ' Translate the dabar

			'If dabar.ConstructChainPos = ConstructChainPosition.Close AndAlso PrintConstructChains Then _
			'	Reveal(ConstructChainEnd, True) ' Close construct chain

			If dabar.Cantillation Is Nothing Then ' Add a space to prepare for the next dabar
				If dabar.Entries.Length <> 0 AndAlso Not dabar.Entries(dabar.Entries.Length - 1).SuppressFollowingSpace Then
					Reveal(Space, True)
				End If

			ElseIf dabar.Cantillation.EndsWith(FLAG_PeOpenMajor) Then
				Reveal(IIf(dabar.Cantillation.StartsWith(SofPasuq), SofPasuq, EndLine), True)

				If h < dabars.Length - 1 Then

					If dabars(h + 1).Citation = dabars(h).Citation Then Reveal("</p>", True)

					Reveal(PeOpenMajorBreakBegin & dabars(h + 1).Citation & PeOpenMajorBreakEnd, True)

					If dabars(h + 1).Citation = dabars(h).Citation Then Reveal(vbCrLf & "<p>", True)

					Anchors.Add((
							"section-" & dabars(h + 1).Citation,                ' Anchor name
							"anchor-section",                                   ' CSS class in TOC
							StripAlternativeNumbering(dabars(h + 1).Citation)   ' Display name in TOC
							))
				End If

			ElseIf dabar.Cantillation.EndsWith(FLAG_SaClosMinor) Then
				Reveal(IIf(dabar.Cantillation.StartsWith(SofPasuq), SofPasuq, EndLine), True)

				If h < dabars.Length - 1 Then
					If dabars(h + 1).Citation = dabars(h).Citation Then Reveal("</p>", True)

					Reveal(SamekhClosedMinorBreak, True)

					If dabars(h + 1).Citation = dabars(h).Citation Then Reveal(vbCrLf & "<p>", True)
				End If

			ElseIf h < dabars.Length - 1 AndAlso dabars(h + 1).Aramaic AndAlso Not dabar.Aramaic Then  ' Mid-verse Aramaic/Hebrew changes Gen.31.46 and Dan.2.4
				Reveal(dabar.Cantillation.Insert(dabar.Cantillation.LastIndexOf(">"c), MidverseAramaicSwitch), True) ' Add cantillation-based punctuation
			Else
				Reveal(dabar.Cantillation, True) ' Add cantillation-based punctuation
			End If

		Next
	End Sub

End Module