Imports System.Text.RegularExpressions
Imports System.Xml
Partial Module Main

	Dim RtLexicon As New Dictionary(Of String, LexEntry)

	Sub BuildLexicon()

		Console.Write("Building English lexicon...")
		CompileHelpingVerbs()
		RtLexicon.Clear()

		Dim lexdoc As New XmlDocument
		While True
			Try
				lexdoc.Load(ProjectPath & "\" & Lexicon)
				Exit While
			Catch e As XmlException
				Console.WriteLine()
				Console.WriteLine()
				Console.Write("XML error in main lexicon: " & e.Message & " Press enter to try again...")
				Console.ReadLine()
			End Try
		End While

		Dim entries As XmlNodeList = lexdoc.GetElementsByTagName("entry")
		Dim strong As String
		Dim parallelStrongs() As String

		Dim topLevelEntry As XmlNode
		Dim topLevelStrong As String

		For Each entry As XmlNode In entries
			If entry.Name <> "#comment" AndAlso entry.Item("xref") IsNot Nothing Then

				strong = entry.Item("xref").GetAttribute("strong")

				If Not String.IsNullOrEmpty(strong) Then
					parallelStrongs = strong.Split(",")

					topLevelEntry = entry
					While topLevelEntry.ParentNode.Name = "entry"
						topLevelEntry = topLevelEntry.ParentNode
					End While
					If topLevelEntry.Item("xref") IsNot Nothing Then
						topLevelStrong = topLevelEntry.Item("xref").GetAttribute("strong")
					Else
						topLevelStrong = topLevelEntry.Item("entry").Item("xref").GetAttribute("strong")
					End If

					For Each strongsItem As String In parallelStrongs

						If Not RtLexicon.TryAdd(strongsItem.TrimStart("A"), New LexEntry(entry, strongsItem.TrimStart("A"), topLevelStrong.TrimStart("A"))) Then
							errorlist.Add("Lexicon contains duplicate entries #" & strongsItem.TrimStart("A"))
							Console.WriteLine(vbCrLf & "*** ERROR: Lexicon contains duplicate entries #" & strongsItem.TrimStart("A") & " ***")
						End If

					Next

				End If

			End If
		Next

		aramaicThe = RtLexicon("9010").Particle & WordLink
		'ParticipleSingDefinite = RtLexicon("9009").Particle & WordLink & "[one]" & WordLink
		'ParticiplePlurDefinite = RtLexicon("9009").Particle & WordLink & "[one]s"

		Console.WriteLine(" Done!")

	End Sub

	Sub ReplaceSpaces(ByRef root As String)
		root = root.Replace(" ", WordLink).Split("/")(0)
	End Sub

End Module