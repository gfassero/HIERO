Imports System.IO

Partial Module Main

    Sub LoadAnnotatedHebrew()

        Console.Write("Loading Hebrew lexicon...")
        Dim parsingDictionary As New Dictionary(Of (String, Integer), (String, String))
        Dim currentRow As String()

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(ProjectPath & "\STEPBible\parsingDictionary.bin")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            While Not MyReader.EndOfData
                currentRow = MyReader.ReadFields()

                For i As Integer = 0 To (currentRow.Length - 4) / 3
                    parsingDictionary.Add((currentRow(0), i), (currentRow(i * 3 + 1), currentRow(i * 3 + 2)))
                Next

            End While
        End Using
        Console.WriteLine(" Done!")


        Console.Write("Reading Hebrew text...")
        Dim hebrewTextList As New List(Of Dabar)

        ' Make one mega array containing the entire OT

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(ProjectPath & "\STEPBible\annotatedHebrewText.bin"),
            compWriter As New StreamWriter(ProjectPath & "\STEPBible\development\expandedFromAnnotations.bin")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            While Not MyReader.EndOfData
                currentRow = MyReader.ReadFields()

                If currentRow.Length <= 3 Then ' THIS IS the VAST MAJORITY of WORDS

                    Dim parsing As (String, String) = Nothing
                    Dim parsingVariant As Integer
                    If currentRow.Length = 2 Then
                        parsingVariant = 0
                    Else
                        parsingVariant = CInt(currentRow(2))
                    End If

                    Dim wordVsPunctuation As String() = {"", ""}
                    If currentRow(1).IndexOfAny(HebrewTaggedPunctuation) <> -1 Then
                        wordVsPunctuation(0) = currentRow(1).Substring(0, currentRow(1).IndexOfAny(HebrewTaggedPunctuation))
                        wordVsPunctuation(1) = currentRow(1).Substring(wordVsPunctuation(0).Length)
                    Else
                        wordVsPunctuation(0) = currentRow(1)
                    End If

                    If parsingDictionary.TryGetValue(
                            (
                                Strip(wordVsPunctuation(0), Disj_Conj_Code),
                                parsingVariant),
                            parsing) Then

                        parsing.Item1 &= ParseTaggedPunctuation(wordVsPunctuation(1))

                        compWriter.WriteLine(
                                                            currentRow(0) & vbTab & ' Citation
                                                            currentRow(1) & vbTab & ' Hebrew
                                                            parsing.Item1 & vbTab & ' Strongs
                                                            parsing.Item2           ' Grammar
                                                            )

                        hebrewTextList.Add(New Dabar(
                                            currentRow(0),
                                            currentRow(1),
                                            parsing.Item1,
                                            parsing.Item2
                                            )
                                        )

                    Else
                        Throw New ArgumentException("Exception Occured")
                    End If

                ElseIf currentRow.Length > 3 AndAlso currentRow(3).Contains("//") Then ' THIS IS a WORD with VARIANTS THAT NEEDS to BE SPLIT (rare)

                    compWriter.WriteLine(
                                                            currentRow(0) & vbTab & ' Citation
                                                            currentRow(1) & vbTab & ' Hebrew
                                                            currentRow(2) & vbTab & ' Strongs
                                                            currentRow(3)           ' Grammar
                                                            )

                    hebrewTextList.Add(New Dabar(
                                            currentRow(0).Insert(currentRow(0).IndexOf("="c), "01"),
                                            Split(currentRow(1), 0),
                                            Split(currentRow(2), 0),
                                            currentRow(3).Substring(0, currentRow(3).IndexOf("//"))
                                            )
                                        )
                    hebrewTextList.Add(New Dabar(
                                            currentRow(0).Insert(currentRow(0).IndexOf("="c), "02"),
                                            Split(currentRow(1), 1),
                                            Split(currentRow(2), 1),
                                            currentRow(3)(0) & currentRow(3).Substring(currentRow(3).IndexOf("//") + 2)
                                            )
                                        )
                Else                                               ' THIS IS NORMAL QERE/KETIV

                    compWriter.WriteLine(
                                                            currentRow(0) & vbTab & ' Citation
                                                            currentRow(1) & vbTab & ' Hebrew
                                                            currentRow(2) & vbTab & ' Strongs
                                                            currentRow(3)           ' Grammar
                                                            )

                    hebrewTextList.Add(New Dabar(
                                            currentRow(0),
                                            currentRow(1),
                                            currentRow(2),
                                            currentRow(3)
                                            )
                                        )
                End If

            End While
        End Using

        ' Remove Dabars that have requested self-destruct
        For i As Integer = hebrewTextList.Count - 1 To 0 Step -1
            If hebrewTextList(i).Reference = Nothing Then hebrewTextList.RemoveAt(i)
        Next

        HebrewText = hebrewTextList.ToArray
        Console.WriteLine(" Done!")

        Console.Write("Pre-parsing Hebrew idiom...")
        PreparseHebrewIdiom(HebrewText)
        Console.WriteLine(" Done!")

        ' MarkAlliteration()

        LoadBookNames()

    End Sub
End Module