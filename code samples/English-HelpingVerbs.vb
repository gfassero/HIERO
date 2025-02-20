Imports System.Xml

Partial Module Main

    Dim vBe, vCause, vDo As VerbStem
    Dim helpingVerbs() As VerbStem

    Sub CompileHelpingVerbs()
        helpingVerbs = Array.Empty(Of VerbStem)()
        Dim lexdoc As New XmlDocument
        lexdoc.Load(HelpersXML)

        Dim hvtemp As New List(Of VerbStem)

        For Each entry As XmlNode In lexdoc.GetElementsByTagName("verb")

            Dim hv As New VerbStem(entry)
            hvtemp.Add(hv)

            Select Case hv.infinitive
                Case "be" : vBe = hv
                Case "do" : vDo = hv
                Case "cause" : vCause = hv
            End Select

        Next

        helpingVerbs = hvtemp.ToArray
    End Sub

End Module