Imports System.Xml

Partial Module Main
    Public Class LexEntry

        Public ReadOnly LexiconPartOfSpeech As String
        Public ReadOnly TopLevelStrongs As String
        Public ReadOnly regexGramSrc() As String
        Public ReadOnly regexGramDest() As String
        Public ReadOnly IsMoveableNegator As Boolean = False
        Public ReadOnly SuppressOf As Boolean = False
        Public ReadOnly SuppressThe As Boolean = False
        Public ReadOnly SuppressPossessive As Boolean = False

        Public Sub New(entry As XmlElement, strong As String, topLevelEntry As String)
            LexiconPartOfSpeech = entry.Item("pos").InnerText
            If topLevelEntry.Contains(","c) Then
                TopLevelStrongs = topLevelEntry.Substring(0, topLevelEntry.IndexOf(","c))
            Else
                TopLevelStrongs = topLevelEntry
            End If

            If entry.Item("xref").HasAttribute("suppress") Then
                Dim supressions = entry.Item("xref").Attributes.GetNamedItem("suppress").Value
                If supressions.Contains("of") Then SuppressOf = True
                If supressions.Contains("the") Then SuppressThe = True
                If supressions.Contains("possessive") Then SuppressPossessive = True
            End If
            If entry.Item("xref").HasAttribute("moveable") AndAlso
                entry.Item("xref").Attributes.GetNamedItem("moveable").Value = "negator" Then
                IsMoveableNegator = True
            End If

            If entry.Item("pos").HasAttribute("regex") Then
                Dim inc As Integer = 0
                Dim regexGramEntries() As String = entry.Item("pos").Attributes.GetNamedItem("regex").Value.Split(","c)
                regexGramSrc = New String(regexGramEntries.Length - 1) {}
                regexGramDest = New String(regexGramEntries.Length - 1) {}
                For Each regexGramEntry In regexGramEntries
                    regexGramSrc(inc) = regexGramEntry.Split("="c)(0)
                    regexGramDest(inc) = regexGramEntry.Split("="c)(1)
                    inc += 1
                Next

            Else
                regexGramSrc = Nothing
            End If

            Dim defTag As Boolean = False
            Dim verbTag As Boolean = False

            If entry.Item("def") IsNot Nothing Then
                particleholder = entry.Item("def").InnerText
                defTag = True
                ReplaceSpaces(particleholder)
                If LexiconPartOfSpeech = "V"c Then Throw New ArgumentException("Did not expect <def>: " & strong)
            End If

            If entry.Item("qalAct") IsNot Nothing OrElse
                entry.Item("qalPass") IsNot Nothing OrElse
                entry.Item("pielAct") IsNot Nothing OrElse
                entry.Item("pielPass") IsNot Nothing OrElse
                entry.Item("pielReflex") IsNot Nothing OrElse
                entry.Item("hiphilAct") IsNot Nothing OrElse
                entry.Item("hiphilPass") IsNot Nothing OrElse
                entry.Item("hiphilReflex") IsNot Nothing Then
                verbholder = New VerbRoot(entry)
                verbTag = True
                If defTag Then errorlist.Add("Unexpected <def> and <qal/piel/hiphil...> definitions in same entry: " & strong)
                If LexiconPartOfSpeech <> "V"c Then Throw New ArgumentException("Did not expect a verb definition: " & strong)
            End If

            If entry.Item("clone") IsNot Nothing Then
                If defTag Then Throw New ArgumentException("Unexpected <def> and <clone> tags in same entry: " & strong)
                If verbTag Then Throw New ArgumentException("Unexpected <qal/piel/hiphil...> and <clone> tags in same entry: " & strong)

                clone = entry.Item("clone").InnerText.TrimStart("A"c)

            ElseIf LexiconPartOfSpeech(0) = "N"c OrElse LexiconPartOfSpeech = "A"c OrElse LexiconPartOfSpeech = "P"c Then
                nounholder = New NounRoot(entry)
            End If

        End Sub

        Public ReadOnly clone As String = Nothing

        ReadOnly particleholder As String
        Public ReadOnly Property Particle As String
            Get
                If IsNothing(particleholder) Then
                    If clone IsNot Nothing Then
                        If LexiconPartOfSpeech(0) <> RtLexicon(clone).LexiconPartOfSpeech(0) Then
                            errorlist.Add("Cloned entries have mismatched part of speech: within " & TopLevelStrongs & " (" & LexiconPartOfSpeech & ") & " & clone & " (" & RtLexicon(clone).LexiconPartOfSpeech & ")")
                        End If
                        Return RtLexicon(clone).particleholder
                    Else
                        Throw New ArgumentException("Exception Occurred")
                    End If
                End If
                Return particleholder
            End Get
        End Property

        Dim nounholder As NounRoot
        Public ReadOnly Property Noun As NounRoot
            Get
                If nounholder Is Nothing Then
                    If clone IsNot Nothing Then
                        Return RtLexicon(clone).Noun
                    ElseIf particleholder IsNot Nothing Then
                        nounholder = New NounRoot(particleholder)
                    ElseIf verbholder IsNot Nothing Then
                        nounholder = New NounRoot(verbholder.EmergencyNounFunction)
                    Else
                        Throw New ArgumentException("Exception Occurred")
                    End If
                End If
                Return nounholder
            End Get
        End Property

        Dim verbholder As VerbRoot = Nothing
        Public ReadOnly Property Verb As VerbRoot
            Get
                If verbholder Is Nothing Then
                    If clone IsNot Nothing Then
                        Return RtLexicon(clone).Verb
                    ElseIf particleholder IsNot Nothing Then
                        verbholder = New VerbRoot(particleholder)
                    ElseIf nounholder IsNot Nothing Then
                        verbholder = New VerbRoot(nounholder.nounSing)
                    Else
                        Throw New ArgumentException("Exception Occurred")
                    End If
                End If
                Return verbholder
            End Get
        End Property

    End Class
End Module