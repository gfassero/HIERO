Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml

Partial Module Main
    Public Class NounRoot
        Public ReadOnly nounSing As String
        Public ReadOnly nounDual As String
        Public ReadOnly nounPlur As String
        Public ReadOnly adjSing As String
        Public ReadOnly adjDual As String
        Public ReadOnly adjPlur As String
        Public Sub New(entry As XmlElement)
            Dim def As String() = entry.Item("def").InnerText.Replace(" ", WordLink).Split("/")
            nounSing = def(0)
            adjSing = nounSing & TagSing
            If def.Length = 1 Then ' Regular
                nounPlur = Pluralize(nounSing)
                adjDual = nounSing & TagDual
                adjPlur = nounSing & TagPlur
            Else ' Irregular
                nounPlur = def(1)
                adjDual = nounPlur & TagDual
                adjPlur = nounPlur & TagPlur
            End If
            nounDual = nounPlur & TagDual
            nounPlur += TagPlur
            nounSing += TagSing
        End Sub
        Public Sub New(root As String)
            nounSing = root & TagSing
            nounDual = root & TagDual
            nounPlur = root & TagPlur
            adjSing = root & TagSing
            adjDual = root & TagDual
            adjPlur = root & TagPlur
        End Sub
    End Class

    Dim pNounSplitter As Regex
    Dim pNounSplitChars As String = WordLink & "-[]{}√¿"

    Sub DefinePNounSplit()

        Dim pNounSplitPattern As String = ""
        For Each c As Char In pNounSplitChars
            pNounSplitPattern &= "|" & Regex.Escape(c)
        Next
        pNounSplitPattern = String.Concat("(", pNounSplitPattern.AsSpan(1), ")")

        pNounSplitter = New Regex(pNounSplitPattern)

    End Sub
    Function ProperNoun(noun As String) As String
        Dim result As New StringBuilder()
        Dim words() As String = pNounSplitter.Split(noun) ' Split by spaces, hyphens, and brackets
        Dim capitalizeNext As Boolean = True

        For Each word As String In words
            If String.IsNullOrEmpty(word) Then
                Continue For
            End If

            If word = "[" OrElse word = "]" OrElse word = "{" OrElse word = "}" Then
                result.Append(word) ' Preserve delimiters as-is

            ElseIf pNounSplitChars.Contains(word) Then
                result.Append(word) ' Preserve delimiters as-is
                capitalizeNext = True

            ElseIf result.Length = 0 Then
                ' Capitalize the first word, regardless of what it is
                result.Append(Char.ToUpper(word(0)) & word.Substring(1))
                capitalizeNext = False

            ElseIf word = "of" OrElse word = "by" OrElse word = "as" OrElse word = "to" OrElse word = "in" OrElse word = "with" OrElse word = "the" OrElse word = "a" OrElse word = "an" Then
                If result.Length + word.Length < noun.Length - 1 Then

                    ' Don't capitalize a preposition (unless it's the last word)
                    result.Append(word)
                    capitalizeNext = False

                Else
                    ' Capitalize a preposition, if it is the last word
                    result.Append(Char.ToUpper(word(0)) & word.Substring(1))
                    capitalizeNext = False
                End If

            ElseIf capitalizeNext Then
                result.Append(Char.ToUpper(word(0)) & word.Substring(1)) ' Capitalize first letter
                capitalizeNext = False

            Else
                result.Append(word)

            End If
        Next

        Return result.ToString()
    End Function

    Public Function Pluralize(singular As String) As String

        Select Case singular(singular.Length - 1)
            Case "s"c, "x"c, "z"c
                Return singular & "es"

            Case "h"c
                Select Case singular(singular.Length - 2)
                    Case "c"c, "s"c
                        Return singular & "es"
                End Select

            Case "y"c
                Select Case singular(singular.Length - 2)
                    Case "a"c, "e"c, "i"c, "o"c, "u"
                        Return singular & "s"
                    Case Else
                        Return String.Concat(singular.AsSpan(0, singular.Length - 1), "ies")
                End Select

        End Select

        Return singular & "s"

    End Function

End Module