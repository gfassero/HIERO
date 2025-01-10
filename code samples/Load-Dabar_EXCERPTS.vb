Imports Psalms_Translator.Main

Partial Module Main

    Dim curlybraces() As Char = {"{"c, "}"c}

    Public Class Dabar
        Public ReadOnly Reference As String
        Public ReadOnly Citation As String
        Public ReadOnly Aramaic As Boolean = False
        Public ReadOnly Cantillation As String
        ' Public ReadOnly AlliterationConsonants As String
        ' Public Alliterations As New List(Of (String, Double)) ' UID, opacity
        ' Public MainStrong As String = Nothing

        Public Entries As Strongs()

        Public NegatorStrongs As String = Nothing
        Public ConstructChainPos As ConstructChainPosition
        Public ReadOnly ObjectPronounSuffix As String = Nothing

        Public Sub New(referenceValue As String, hebrewText As String, dStrongsValues As String, grammarValues As String)

#Region "Basic Initial Parse"
            Reference = referenceValue
            Citation = referenceValue.Substring(0, referenceValue.IndexOf("#"c))

            If grammarValues(0) = "A"c Then Aramaic = True

            Dim dStrongSplits() As String = dStrongsValues.Split(dStrongsSplitChars)
            Dim grammarSplits() As String = grammarValues.Substring(1).Split(dStrongsSplitChars)
            Dim initEntries(9) As Strongs

            Dim i As Integer = 0
            Dim count As Integer = 0

            While i <> dStrongSplits.Length
                If String.IsNullOrWhiteSpace(dStrongSplits(i)) Then
                    i += 1
                    Continue While

                ElseIf i < grammarSplits.Length Then
                    initEntries(count) = New Strongs(dStrongSplits(i), grammarSplits(i), Reference, Me)

                ElseIf dStrongSplits(i) <> " " Then
                    initEntries(count) = New Strongs(dStrongSplits(i), Nothing, Reference, Me)

                End If

                If initEntries(count).IsMainLemma Then
                    mainlemmaindex = count
                End If

                i += 1
                count += 1

            End While

            ReDim Preserve initEntries(count - 1)
            Me.Entries = initEntries
#End Region

#Region "Handle Specific Strongs"
            ' Code to handle special cases...
#End Region

#Region "Cantillation"
            Select Case Entries(Entries.Length - 1).StrongsLemma
                Case "9016"
                    Cantillation = Nothing
                    Entries(Entries.Length - 2).IsConstruct = False
                    Entries(Entries.Length - 2).EnglishConstructSuffix = Nothing
                Case "9017", "9018"
                    Cantillation = Nothing
                Case Else
                    Cantillation = ExtractCantillationHTML(hebrewText)
            End Select
#End Region

            ' AlliterationConsonants = ExtractAlliteration(hebrewText, Reference, dStrongsValues)

            ' Variables needed for the upcoming loop
            Dim constructWithPronounSuffix As Boolean = False
            Dim textObjPronSuff As Integer = -1
            Dim preposition As String = Nothing
            Dim prepositionIndex As Integer

#Region "Identify Constructs, Prepositions, & Object Pronoun Suffixes"
            ' Code to handle special cases...
#End Region

#Region "THERE IS NOT #369 'ayin with PRONOMIAL SUFFIX"
            ' Code to handle special cases...
#End Region
#Region "THERE IS #3426 yesh with PRONOMIAL SUFFIX"
            ' Code to handle special cases...
#End Region


#Region "Process Infinitive Construct w Preposition"
            ' Code to handle special cases...
#End Region

#Region "LOOP: Suffixes & Articles"
            ' Code to handle special cases...
#End Region

        End Sub

        Private mainlemmaindex As Integer = Nothing
        Public ReadOnly Property MainLemma As Strongs
            Get
                Return Entries(mainlemmaindex)
            End Get
        End Property
        Public ReadOnly Property MainLemmaIsFinalLemma As Boolean
            Get
                If mainlemmaindex = (Entries.Length - 1) Then
                    Return True
                ElseIf Entries(Entries.Length - 1).IsPunctuation Then
                    If mainlemmaindex = (Entries.Length - 2) Then
                        Return True
                    ElseIf Entries(Entries.Length - 2).IsPunctuation Then
                        If mainlemmaindex = (Entries.Length - 3) Then
                            Return True
                        End If
                    End If
                End If
                Return False
            End Get
        End Property

        Public ReadOnly Property EndsWithNegativeParticleAndOptionalMaqqef As Boolean
            Get
                Select Case MainLemma.StrongsLemma
                    Case "0408", "3808" ' Main lemma is a negative particle 0408/3808

                        If mainlemmaindex = Entries.Length - 1 Then ' Main lemma is final lemma
                            Return True

                        ElseIf Entries(Entries.Length - 1).StrongsLemma = "9014" Then ' Final lemma is a maqqef
                            If mainlemmaindex = Entries.Length - 2 Then ' There is nothing between the negative particle and the maqqef
                                Return True

                            Else
                                Throw New ArgumentException("Exception Occured")
                                Return True
                            End If

                        Else
                            Return False
                        End If

                    Case Else
                        Return False

                End Select
            End Get
        End Property

        Public ReadOnly Property IsFiniteVerb As Boolean
            Get
                If MainLemma.GramMorph(0) = "V"c Then
                    Select Case MainLemma.GramMorph(2)
                        Case "r"c, "s"c, "n"c, "a"c
                            'Throw New ArgumentException("Exception Occured")
                            Return False ' Verb is participle or infinitive

                        Case "c"c
                            Select Case MainLemma.GramMorph.Length
                                Case 6 ' COHORTATIVE          : tagged V[stem]c1cs (example)
                                    Return True ' Verb is cohortative, and thus finite

                                Case 4 ' INFINITIVE CONSTRUCT : tagged V[stem]cc   (example)
                                    Return False ' Verb is infinitive

                                Case Else
                                    Throw New ArgumentException("Exception Occured")
                                    Return False

                            End Select

                        Case Else
                            Return True ' Verb is finite

                    End Select

                Else
                    Return False ' Word is not a verb

                End If
            End Get
        End Property

        Public Sub PurgeSkippedLemmas()

            Dim entries(9) As Strongs

            Dim i As Integer = 0
            Dim count As Integer = 0

            While i <> Me.Entries.Length
                If Me.Entries(i).Skip_ADDED_IN_PREPROCESSING Then
                    i += 1
                    Continue While
                Else
                    entries(count) = Me.Entries(i)
                    If entries(count).IsMainLemma Then mainlemmaindex = count
                End If

                i += 1
                count += 1

            End While

            ReDim Preserve entries(count - 1)
            Me.Entries = entries

        End Sub


        Sub Translate(searchList() As String, Optional nextCitation As String = Nothing)

            ' For each morph/lemma pair, parse it as its own word
            For i As Integer = 0 To Entries.Length - 1

                If Entries(i).ParticlePrefix IsNot Nothing Then Reveal(RtLexicon(Entries(i).ParticlePrefix).Particle)

                If i <> 0 AndAlso Not Entries(i).IsPunctuation AndAlso Not Entries(i - 1).SuppressFollowingSpace Then
                    Reveal(WordLink, True) ' Add link between each Strongs in a given Dabar
                End If

                Entries(i).Translate(searchList, nextCitation)

            Next

        End Sub

        Public ReadOnly Property Negator As String
            Get
                If NegatorStrongs <> Nothing Then
                    Return RtLexicon(NegatorStrongs).Particle & WordLink
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Sub PrintFollowingSpaceIfAppropriate()
            If Entries.Length <> 0 AndAlso Not Entries(Entries.Length - 1).SuppressFollowingSpace Then _
                Reveal(Space, True)
        End Sub

    End Class
End Module