Imports System.Text.RegularExpressions
Imports System.Xml

Partial Module Main

    Dim UsedWords As New Dictionary(Of String, Integer)

    Sub PrintGlossary()
        Console.Write("Building glossary...")
        hebVowelRemoval = New Regex("[" & New String(HebrewVowels) & New String(HebrewConj) & New String(HebrewDisj) & "]")

        Dim lexdoc As New XmlDocument
        lexdoc.Load(ProjectPath & "\" & Lexicons(0))

        Dim docSections As New GlossarySection(lexdoc.GetElementsByTagName("index").Item(0), True)

        Dim outputBegin As String = runtimeHeader & vbCrLf &
                            "</span><br/>" & vbCrLf &
                             "<span id=""translation""><br/>" & vbCrLf &
                            CustomTabx4 & "<span class=""book"" id=""glossary"">Glossary (from MULTILEX ONLY!)</span><br/>"
        Dim outputEnd As String = "</span>" & vbCrLf &
                             "</body>" & vbCrLf &
                             "</html>"

        Try
            ' FIRST: PRINT TOPICAL GLOSSARY --------------------------------------------------------------------------------

            ' Open File Stream and HTML
            OutputFile = FileIO.FileSystem.OpenTextFileWriter(RepositoryRoot & "\alphas\glossary.html", False)
            OutputFile.Write(outputBegin)

            docSections.Print()

            ' Close out HTML and File Stream
            OutputFile.Write(outputEnd)
            OutputFile.Close()

            ' SECOND: PRINT ALPHABETICAL GLOSSARY --------------------------------------------------------------------------

            ' Open File Stream and HTML
            OutputFile = FileIO.FileSystem.OpenTextFileWriter(RepositoryRoot & "\alphas\glossary-alphabetical.html", False)
            OutputFile.Write(outputBegin)

            Dim allEntries As List(Of GlossaryEntry) = docSections.AllEntries()
            allEntries.Sort(Function(ge1 As GlossaryEntry, ge2 As GlossaryEntry) ge1.EnglishSort.CompareTo(ge2.EnglishSort))
            For Each entry As GlossaryEntry In allEntries
                If entry.English(0) <> UnverifiedLexiconFlag Then
                    OutputFile.WriteLine(entry.EntryXML)
                End If
            Next

            ' Close out HTML and File Stream
            OutputFile.Write(outputEnd)
            OutputFile.Close()

            ' THIRD: PRINT FREQUENCY GLOSSARY ------------------------------------------------------------------------------

            ' Open File Stream and HTML
            OutputFile = FileIO.FileSystem.OpenTextFileWriter(RepositoryRoot & "\alphas\glossary-frequency.html", False)
            OutputFile.Write(outputBegin)

            allEntries.Sort(Function(ge1 As GlossaryEntry, ge2 As GlossaryEntry) ge2.Frequency.CompareTo(ge1.Frequency))
            For Each entry As GlossaryEntry In allEntries
                If entry.English(0) <> UnverifiedLexiconFlag Then
                    OutputFile.WriteLine(entry.EntryXML)
                End If
            Next

            ' Close out HTML and File Stream
            OutputFile.Write(outputEnd)
            OutputFile.Close()

            ' FOURTH: PRINT NONCOMPLIANT GLOSSARY --------------------------------------------------------------------------

            ' Open File Stream and HTML
            OutputFile = FileIO.FileSystem.OpenTextFileWriter(RepositoryRoot & "\alphas\glossary-noncompliant.html", False)
            OutputFile.Write(outputBegin)

            For Each entry As GlossaryEntry In allEntries
                If entry.UnexcusedFail AndAlso entry.English(0) <> UnverifiedLexiconFlag Then
                    OutputFile.WriteLine(entry.EntryXML)
                End If
            Next

            ' Close out HTML and File Stream
            OutputFile.Write(outputEnd)
            OutputFile.Close()

            ' --------------------------------------------------------------------------------------------------------------

            Console.WriteLine(" Done!")

        Catch ex As System.IO.IOException

            Console.WriteLine("ERROR: " & ex.Message)
            Console.WriteLine()
            Console.WriteLine(" FAILED!")

        End Try

    End Sub

    Dim hebVowelRemoval As Regex
    Private ReadOnly bracketRemoval As New Regex("\[[^\[\]]*\]")
    Private ReadOnly helperRemoval As New Regex("^(be|make) ")
    Function ProcessEnglish(def As String, processed As Boolean) As String
        If processed Then
            Return Bracket(def)
        Else
            If def.Contains("/"c) Then def = def.Remove(def.IndexOf("/"c)) ' Split nouns with custom plural

            def = bracketRemoval.Replace(                  ' Remove bracketed, clarifying words
                def.Replace("°"c, ""),                     ' Remove direct object position marker
                "").
                Trim(" "c).                                ' Trim spaces left by removing bracketed words
                Replace("one's ", "").                     ' Remove reflexive references, where first English reference is reflexive
                Replace(" oneself", "").                   ' Remove reflexive references, where first English reference is reflexive
                Replace("  ", " "c)                       ' Remove any double spaces that may have cropped up

            Return Bracket(
                            helperRemoval.Replace(def, "")
                          )
        End If
    End Function



    Private Class GlossarySection

        Private ReadOnly Sections As New List(Of GlossarySection)
        Private ReadOnly Entries As New List(Of GlossaryEntry)
        Private ReadOnly Name As String
        Private ReadOnly IsTop

        Public Sub New(section As XmlNode, Optional IsTopLevelNode As Boolean = False)
            IsTop = IsTopLevelNode
            If Not IsTop Then Name = section.Attributes.GetNamedItem("name").Value

            For Each childNode As XmlNode In section.ChildNodes
                Select Case childNode.Name
                    Case "entry"
                        Dim ge As New GlossaryEntry(childNode)
                        If ge.EntryXML IsNot Nothing Then
                            Entries.Add(ge)
                        End If
                    Case "section"
                        Sections.Add(New GlossarySection(childNode))
                End Select
            Next

        End Sub

        Public Sub Print()

            Entries.Sort(Function(ge1 As GlossaryEntry, ge2 As GlossaryEntry) ge2.Frequency.CompareTo(ge1.Frequency))

            If Not IsTop Then
                OutputFile.WriteLine("<details><summary>" & Name & " - " & GetDescendantCount() & "</summary>")
            End If
            For Each section As GlossarySection In Sections
                section.Print()
            Next

            If Sections.Count > 0 AndAlso Entries.Count > 0 Then _
                OutputFile.WriteLine("<span class=""glossary-caption"">Miscellaneous - " & Entries.Count & "</span>")

            For Each entry As GlossaryEntry In Entries
                OutputFile.WriteLine(entry.EntryXML)
            Next

            If Not IsTop Then OutputFile.WriteLine("</details>")

        End Sub

        Function AllEntries() As List(Of GlossaryEntry)

            Dim result As New List(Of GlossaryEntry)
            result.AddRange(Entries)

            For Each section As GlossarySection In Sections
                result.AddRange(section.AllEntries)
            Next

            Return result

        End Function

        Function GetDescendantCount() As Integer
            GetDescendantCount = Entries.Count
            For Each subsect As GlossarySection In Sections
                GetDescendantCount += subsect.GetDescendantCount
            Next
        End Function

    End Class



    Private Class GlossaryEntry

        Public ReadOnly Frequency As Integer
        Public ReadOnly EntryXML As String = Nothing
        Public ReadOnly English As String
        Public ReadOnly EnglishSort As String
        Public ReadOnly UnexcusedFail As Boolean = False

        Public Sub New(entry As XmlElement)

            Dim listedStrongs As New List(Of String)

            For Each node As XmlNode In entry.SelectNodes(".//xref") ' Look for nested xrefs
                listedStrongs.AddRange(node.Attributes.GetNamedItem("strong").Value.Split(","c))
            Next

            If listedStrongs(0).StartsWith("90") Then
                EntryXML = Nothing
                Exit Sub
            End If

            Dim firstW As XmlNode = entry.SelectSingleNode(".//w")
            If firstW Is Nothing Then
                EntryXML = "<span class=""glossary-entry"">ERROR: No W element found: " & listedStrongs(0) & ".</span>"
                Exit Sub
            End If

            Dim firstDef As XmlElement = entry.SelectSingleNode(".//def | .//clone | .//qalAct | .//qalPass | .//qalReflex | .//pielAct | .//pielPass | .//pielReflex | .//hiphilAct | .//hiphilPass | .//hiphilReflex") ' Look for nested defs
            If firstDef Is Nothing Then
                EntryXML = "<span class=""glossary-entry"">ERROR: No definition found: " & listedStrongs(0) & ".</span>"
                Exit Sub
            End If

            Dim gloss As String
            Dim firstGloss As XmlNode = entry.SelectSingleNode("gloss")
            If firstGloss Is Nothing Then
                gloss = ""
            Else
                gloss = firstGloss.InnerText
            End If

            ' Write glossary entry
            If Not UsedWords.TryGetValue(RtLexicon(listedStrongs(0).TrimStart("A")).TopLevelStrongs, Frequency) Then Frequency = 0

            English = ProcessEnglish(firstDef.InnerText, firstDef.HasAttribute("processed") AndAlso CBool(firstDef.GetAttribute("processed")))
            EnglishSort = English.Replace("¡"c, "").Replace("!"c, "").
                Replace("√"c, "").Replace("¿"c, "").Replace("?"c, "").
                Replace("<i>", "").Replace("</i>", "").Replace("‹"c, "")
            If English.Contains("√"c) Then EnglishSort &= "√"c

            Dim fail As String = Nothing
            If entry.HasAttribute("fail") Then
                fail = " " & entry.GetAttribute("fail")
                UnexcusedFail = (fail <> " excused")
            End If

            EntryXML = "<span class=""glossary-entry" & fail & """>" &
                    "<span class=""gloss-heb"">" & hebVowelRemoval.Replace(firstW.InnerText, "") & "</span> " &
                    "<span class=""gloss-count"">" & IIf(Frequency = 0, "&mdash;", "x" & Frequency) & "</span>" &
                    "<span class=""gloss-eng"">" & English & "</span>" &
                    "<span class=""gloss-comment"">" &
                    "<span class=""gloss-xlit"">" & SimplifyXlit(firstW.Attributes.GetNamedItem("xlit").Value) & "</span>" &
                    gloss & " Strongs " & SimplifyStrongs(listedStrongs.ToArray) & ".</span>" &
                    "</span>"
        End Sub

    End Class

    Function SimplifyXlit(xlit As String) As String
        Return xlit.Replace("ṣ", "tz").
            Replace("š", "sh")
    End Function

    Function SimplifyStrongs(listedStrongs() As String) As String
        Dim Strongs As New List(Of Integer)
        Dim temp As Integer

        ' Trim extended Strongs letters, and remove duplicates: A4976A,A4976B  =>  4976
        For Each strong As String In listedStrongs
            temp = CInt(strong.TrimStart("A"c).Remove(4))
            If Not Strongs.Contains(temp) Then Strongs.Add(temp)
        Next

        ' Consolidate contiguous Strongs into ranges: 4976, 4977, 4978, 4979, 4980, 4981, 4982, 4983 => 4976-4983
        Strongs.Sort()

        Dim result As New List(Of String)
        Dim start As Integer = Strongs(0)
        Dim [end] As Integer = start

        ' Iterate starting from the second item
        For i As Integer = 1 To Strongs.Count - 1
            Dim strong As Integer = Strongs(i)

            If strong = [end] + 1 Then
                [end] = strong
            Else
                If start = [end] Then
                    result.Add(start.ToString("0000"))
                Else
                    result.Add(start.ToString("0000") & "-" & [end].ToString("0000"))
                End If
                start = strong
                [end] = start
            End If
        Next

        ' Add the last range
        If start = [end] Then
            result.Add(start.ToString("0000"))
        Else
            result.Add(start.ToString("0000") & "-" & [end].ToString("0000"))
        End If

        Return String.Join(", ", result)
    End Function

End Module