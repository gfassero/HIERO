Imports System.Reflection
Imports System.Xml

Partial Module Main

    Dim UntranslatedRoots As New Dictionary(Of String, Integer)
    Dim Anchors As New List(Of (String, String, String))

    Sub TranslateAndSave(Optional biblicalcitation As String = Nothing, Optional outputDest As String = Nothing)
        CleanVariables()

        Dim defaultDir As Boolean = False
        If outputDest = Nothing Then
            defaultDir = True
            Console.WriteLine(vbCrLf & "Updating translation...")
            outputDest = ProjectPath & "\HIERO.html"
        End If
        UntranslatedRoots.Clear()
        Anchors.Clear()

        Dim referenceMatches() As Dabar
        If biblicalcitation = Nothing OrElse biblicalcitation = " " Then ' Select the entire OT for translation
            referenceMatches = New Dabar(HebrewText.Length - 1) {}
            HebrewText.CopyTo(referenceMatches, 0)

        Else ' Select only a book, chapter, verse, etc. for translation
            referenceMatches = GetReferenceMatches(biblicalcitation)

        End If

        Try
            ' Open File Stream and HTML
            OutputFile = FileIO.FileSystem.OpenTextFileWriter(outputDest, False)

            OutputFile.Write(OutputHeader & OutputBeginTranslation)

            ' Translate everything
            SaveToFileInsteadOfConsole = True
            TranslateArrayOfDabar(referenceMatches) ' Translate
            SaveToFileInsteadOfConsole = False

            OutputFile.Write(OutputTOC)
            ' Write TOC
            For Each anchor As (String, String, String) In Anchors
                If anchor.Item2 = "anchor-book" Then
                    OutputFile.Write(OutputTOCBook1)
                End If
                Dim refSplits = anchor.Item3.Split(".")
                Dim sectionRef As String
                If refSplits.Length = 1 Then
                    sectionRef = refSplits(0)
                Else
                    sectionRef = Books(refSplits(0)).Item4 & " " & refSplits(1) & ":" & refSplits(2)
                End If
                OutputFile.Write(vbCrLf & "<a href=""#" & anchor.Item1 & """ class=""" & anchor.Item2 & """>" & sectionRef & "</a>")
                If anchor.Item2 = "anchor-book" Then
                    OutputFile.Write(OutputTOCBook2)
                End If
            Next

            ' Close out HTML and File Stream
            OutputFile.Write(RuntimeFooter & OutputEnd)
            OutputFile.Close()

            Console.Title = "Translate"

            ' GENERATE PROGRESS REPORT
            If defaultDir AndAlso referenceMatches IsNot Nothing Then
                Dim txt As String = FileIO.FileSystem.ReadAllText(outputDest)
                Dim remDots As Integer = txt.Split(UnverifiedLexiconFlag).Length - 1
                Console.WriteLine("   " & remDots & " words remaining.")
                Console.WriteLine("   " &
                              Math.Round((referenceMatches.Length - remDots) * 100 / referenceMatches.Length, 2) _
                              & "% complete!" & vbCrLf)
            End If

            If UntranslatedRoots.Count > 0 Then
                Dim sortedL As List(Of KeyValuePair(Of String, Integer)) = UntranslatedRoots.ToList
                sortedL.Sort(
                 Function(firstPair As KeyValuePair(Of String, Integer), nextPair As KeyValuePair(Of String, Integer))
                     Return CInt(nextPair.Value).CompareTo(CInt(firstPair.Value))
                 End Function)

                If defaultDir Then
                    Console.WriteLine("   " & sortedL.Count & " roots remaining. Top roots:")
                    For i As Integer = 0 To Math.Min(10, sortedL.Count - 1)
                        Console.WriteLine("      " & sortedL(i).Key & ": " & sortedL(i).Value)
                    Next

                Else
                    Dim txt As String = FileIO.FileSystem.ReadAllText(outputDest)
                    Dim remDots As Integer = txt.Split(UnverifiedLexiconFlag).Length - 1
                    Console.WriteLine(biblicalcitation & ": " & sortedL.Count & " roots remaining. " &
                                      Math.Round((referenceMatches.Length - remDots) * 100 / referenceMatches.Length, 2) _
                                      & "% complete.")



                End If

            End If

        Catch ex As System.IO.IOException

            Console.WriteLine("ERROR: " & ex.Message)
            Console.WriteLine()

        End Try
    End Sub
End Module