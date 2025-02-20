Imports System.IO
Imports System.Text.RegularExpressions

Partial Module Main

    Dim hebVowelRemoval As Regex

    Function AutoProcessTextualWordToGloss(innerText As String) As String
        Return hebVowelRemoval.Replace(innerText, "")
    End Function

    Sub LoadAnnotatedHebrew()
        hebVowelRemoval = New Regex("[" & New String(HebrewVowels) & New String(HebrewConj) & New String(HebrewDisj) & "]")

        Console.Write("Loading Hebrew lexicon...")
        Dim MyParsingDict As New ParsingDictReader
        Console.WriteLine(" Done!")

        Console.Write("Reading Hebrew text...")
        Dim hebrewTextList As New List(Of Dabar)

        ' Make one mega array containing the entire OT

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(AnnotatedTextBIN),
            compWriter As New StreamWriter(ExpandedFromAnnotationsBIN)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            Dim currentRow() As String
            While Not MyReader.EndOfData
                currentRow = MyReader.ReadFields()

                Dim tags As ParsingDictReader.Tagging

                If currentRow.Length < 3 Then ' THIS IS the VAST MAJORITY of WORDS
                    tags = MyParsingDict.Parse(currentRow(1))

                Else
                    tags = MyParsingDict.Parse(currentRow(1), currentRow(2))
                End If

                compWriter.WriteLine(currentRow(0) & vbTab & ' Citation
                                     currentRow(1) & vbTab & ' Hebrew
                                     tags.Strongs & vbTab &  ' Strongs
                                     tags.Grammar            ' Grammar
                                    )
                If Not tags.Grammar.Contains("//") Then
                    hebrewTextList.Add(New Dabar(
                                             currentRow(0),
                                             currentRow(1),
                                             tags.Strongs,
                                             tags.Grammar
                                            )
                                  )
                Else
                    hebrewTextList.Add(New Dabar(
                                             currentRow(0),
                                             Strip(currentRow(1), Disj_Conj_Code),
                                             tags.Strongs.Replace("/H9014/", "//").Replace("/ /", "//").Split("//")(0),
                                             tags.Grammar.Split("//")(0)
                                            )
                                  )
                    hebrewTextList.Add(New Dabar(
                                             currentRow(0).Insert(currentRow(0).IndexOf("="c), "02"),
                                             currentRow(1),
                                             tags.Strongs.Replace("/H9014/", "//").Replace("/ /", "//").Split("//")(1),
                                             "H" & tags.Grammar.Split("//")(1)
                                            )
                                  )
                End If
            End While
        End Using

        ' Remove Dabars that have requested self-destruct
        For i As Integer = hebrewTextList.Count - 1 To 0 Step -1
            If hebrewTextList(i).Reference = Nothing Then hebrewTextList.RemoveAt(i)
        Next

        OriginalText = hebrewTextList.ToArray
        Console.WriteLine(" Done!")

        Console.Write("Pre-parsing Hebrew idiom...")
        PreparseHebrewIdiom(OriginalText)
        Console.WriteLine(" Done!")

        ' MarkAlliteration()

        LoadBookNames()

    End Sub


    Class ParsingDictReader
        Private spellings As New Dictionary(Of String, UniqueHebrewSpelling)

        Sub New()
            Dim currentRow As String()

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(ParsingDictionaryBIN)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(vbTab)
                While Not MyReader.EndOfData
                    currentRow = MyReader.ReadFields()

                    spellings.Add(currentRow(0), New UniqueHebrewSpelling(currentRow))

                End While
            End Using
        End Sub

        Public ReadOnly Property Parse(hebrew As String, Optional parsingVariant As Integer = 0) As Tagging
            Get
                Dim spell = spellings(StripPunctuationMarks(hebrew))

                Return spell.Taggings(parsingVariant)
            End Get
        End Property

        Private Class UniqueHebrewSpelling
            Public ReadOnly Taggings() As Tagging

            Sub New(row() As String)
                Dim tags As New List(Of Tagging)

                For i As Integer = 0 To (row.Length - 4) / 3
                    tags.Add(New Tagging(row(i * 3 + 1), row(i * 3 + 2)))
                Next

                Taggings = tags.ToArray
            End Sub
        End Class

        Public Class Tagging
            Public ReadOnly Strongs As String
            Public ReadOnly Grammar As String

            Sub New(strg As String, gram As String)
                Strongs = strg
                Grammar = gram
            End Sub
        End Class

    End Class
End Module