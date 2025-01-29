Imports System.Text.RegularExpressions
Partial Module Main

    Public Class Strongs
        Public StrongsLemma As String
        Public GramMorph As String
        Public ReadOnly Is90xxPronoun As Boolean = False
        Public ReadOnly IsPunctuation As Boolean = False
        Public Definite As Boolean = False
        Public HasDefiniteArticle As Boolean = False
        Public SuppressDefArt As Boolean = False
        Public CustomArticle As String = Nothing
        Public Skip_ADDED_IN_PREPROCESSING = False
        Public ParticlePrefix As String = Nothing
        Public SuppressFollowingSpace As Boolean = False

        Public ReadOnly IsMainLemma As Boolean = False
        Public IsConstruct As Boolean
        Private ReadOnly ParentDabar As Dabar
        Public ReadOnly IsProper As Boolean = False

        Public Sub New(dStrongsValue As String, grammarValue As String, reference As String, parent As Dabar)
            ParentDabar = parent

            Dim extension As String = ""
            If dStrongsValue.EndsWith("+"c) Then
                extension = "+"
                dStrongsValue = dStrongsValue.Substring(0, dStrongsValue.Length - 1)
            End If
            StrongsLemma = String.Concat(dStrongsValue.TrimStart("{"c).TrimEnd("}"c).Substring(1), extension)

            Dim val As LexEntry = Nothing
            If RtLexicon.TryGetValue(StrongsLemma, val) Then
                If val.regexGramSrc IsNot Nothing Then
                    For inc As Integer = 0 To val.regexGramSrc.Length - 1
                        If grammarValue.StartsWith("Nt") Then
                            grammarValue = String.Concat("Np", grammarValue.AsSpan(2))
                        End If
                        If Regex.Match(grammarValue, "^"c & val.regexGramSrc(inc)).Success Then
                            grammarValue = Regex.Replace(grammarValue,
                                                         "^"c & val.regexGramSrc(inc),
                                                         val.regexGramDest(inc))
                            Exit For
                        End If
                    Next
                End If
            End If


            GramMorph = grammarValue

            If dStrongsValue(0) = "{"c Then
                IsMainLemma = True
                ' parent.MainStrong = StrongsLemma

                Select Case GramMorph(0)
                    Case "N"c, "A"c
                        If GramMorph.Length > 4 AndAlso GramMorph(4) = "c"c Then
                            IsConstruct = True
                            EnglishConstructSuffix = WordLink & "of"
                        End If
                        Select Case GramMorph(1)
                            Case "p", "t", "g"
                                IsProper = True
                                Definite = True
                        End Select
                    Case "V"c
                        If GramMorph.Length = 4 Then ' INFINITIVE
                            If GramMorph(3) = "c"c Then
                                IsConstruct = True
                                EnglishConstructSuffix = WordLink & "of"
                            End If
                        ElseIf GramMorph(5) = "c"c Then ' PARTICIPLE
                            IsConstruct = True
                            EnglishConstructSuffix = WordLink & "of"
                        End If
#Region "Resolve a Hebrew quirk in which NEGATIVE COMMANDS appear AS JUSSIVES instead of imperatives"
                        If GramMorph(2) = "j"c Then
                            If GramMorph(3) = "2"c Then ' Second-person, usually negated
                                GramMorph = String.Concat(GramMorph.AsSpan(0, 2), "v", GramMorph.AsSpan(3)) ' Replace jussive flag with imperative

                            ElseIf GramMorph(3) = "1"c Then ' There are also 10 first-person jussives, for a kind of subjunctive effect: that "I might see/learn/fear" *** It would be good to insert "might" or something like that, and to remove the imperative flag. This isn't really an imperative, like the 2nd person jussive is.
                                GramMorph = String.Concat(GramMorph.AsSpan(0, 2), "c", GramMorph.AsSpan(3)) ' Replace jussive flag with cohortative

                            End If
                        End If
#End Region
                End Select
            End If

            Select Case StrongsLemma
                Case "9012", "9013"
                    IsPunctuation = True
                Case "9020" To "9049"
                    Is90xxPronoun = True
                Case "0853", "9008"
                    SuppressFollowingSpace = True
            End Select

            Dim lexentry As LexEntry = Nothing
            If RtLexicon.TryGetValue(StrongsLemma, lexentry) Then
                If String.IsNullOrEmpty(GramMorph) Then
                    Select Case lexentry.LexiconPartOfSpeech
                        Case "C"
                        Case Else
                            Throw New ArgumentException("Lemma is not tagged with grammar morph. Lexicon expects POS " & lexentry.LexiconPartOfSpeech)
                    End Select
                ElseIf lexentry.LexiconPartOfSpeech.Length > 2 Then
                    Throw New ArgumentException("Unexpected <pos> in lexicon.")

                Else
                    Dim err As Boolean = False

                    If lexentry.LexiconPartOfSpeech.Length = 1 Then
                        If Not lexentry.LexiconPartOfSpeech.Equals(GramMorph(0).ToString, StringComparison.CurrentCultureIgnoreCase) Then
                            If GramMorph(0) <> "N"c AndAlso GramMorph(0) <> "A"c Then
                                err = True
                            ElseIf lexentry.LexiconPartOfSpeech <> "N"c AndAlso lexentry.LexiconPartOfSpeech <> "A"c Then
                                err = True
                            End If
                        End If
                    ElseIf lexentry.LexiconPartOfSpeech.Length = 2 Then
                        If lexentry.LexiconPartOfSpeech.Length > GramMorph.Length Then
                            err = True
                        ElseIf lexentry.LexiconPartOfSpeech <> GramMorph.Substring(0, 2) Then
                            If lexentry.LexiconPartOfSpeech(0) <> "N"c OrElse GramMorph(0) <> "N"c Then
                                err = True
                            End If
                        End If
                    End If

                    If err Then
                        errorlist.Add("Unexpected use of root " & StrongsLemma & " as " & GramMorph & "; lexicon lists as " & lexentry.LexiconPartOfSpeech & "; " & Regex.Matches("COMPILED STEP FILE", StrongsLemma).Count & "%% instances; " & reference)
                    End If

                End If
            ElseIf (Not Is90xxPronoun) Then
                If StrongsLemma.EndsWith("+"c) Then
                    errorlist.Add("Undefined ""plus"" lemma: " & StrongsLemma & "; " & reference)
                Else
                    Throw New ArgumentException("Lemma " & StrongsLemma & " missing from lexicon.")
                End If
            End If

            SuppressDefArt = RtLexicon(StrongsLemma).SuppressThe
        End Sub

        Private _ecsholder As String = Nothing
        Public Property EnglishConstructSuffix As String
            Get
                If RtLexicon(StrongsLemma).SuppressOf Then
                    Return Nothing
                Else
                    Return _ecsholder
                End If
            End Get
            Set(ByVal value As String)
                _ecsholder = value
            End Set
        End Property

        Public ReadOnly Property IsInfinitiveConstruct As Boolean
            Get
                If GramMorph IsNot Nothing AndAlso
                    GramMorph.Length = 4 AndAlso  ' an infinitive
                    GramMorph(0) = "V"c AndAlso   ' verb...
                    GramMorph(2) = "c"c Then      ' that is a construct...

                    Return True

                Else
                    Return False

                End If
            End Get
        End Property

        Public ReadOnly Property IsParticipleWithoutCustomActor As Boolean
            Get
                If GramMorph IsNot Nothing AndAlso
                    GramMorph(0) = "V"c Then                                                     ' If it's a verb...

                    If (GramMorph(2) = "r"c OrElse GramMorph(2) = "s"c) AndAlso                  ' and it's a participle...
                    Not GetStem(RtLexicon(StrongsLemma).Verb, GramMorph(1)).HasCustomActor Then  ' and it does NOT have a custom actor...

                        Return True

                    Else
                        Return False

                    End If

                Else
                    Return False

                End If
            End Get
        End Property

        Public Sub MakeDefiniteParticiple()
            'Select Case GramMorph(5) ' Update: I don't think this is relevant: it's just the difference bt "those who worship..." and "those who worship [you]"
            '    Case "a" ' ABSOLUTE, no action needed
            '    Case "c" ' CONSTRUCT *** need to process somehow, especially if there's a suffix ***
            '    Case "d" ' ARAMAIC DEFINITE *** haven't looked into this yet ***
            '    Case Else : Throw New ArgumentException("Exception Occured")
            'End Select

            If GramMorph.Substring(0, 3) = "Vqs" OrElse
                   (GetStem(RtLexicon(StrongsLemma).Verb, GramMorph(1)).infinitive.StartsWith("be" & WordLink) AndAlso
                   Not GetStem(RtLexicon(StrongsLemma).Verb, GramMorph(1)).HasCustomActor) Then
                Select Case GramMorph(4)
                    Case "s" : CustomArticle = ParticipleSingProgressive
                    Case "p" : CustomArticle = ParticiplePlurProgressive
                    Case Else : Throw New ArgumentException("Exception Occured")
                End Select

            Else
                Select Case GramMorph(4)
                    Case "s" : CustomArticle = ParticipleSing
                    Case "p" : CustomArticle = ParticiplePlur
                    Case Else : Throw New ArgumentException("Exception Occured")
                End Select

                GramMorph = String.Concat(GramMorph.AsSpan(0, 2), "i3", GramMorph.AsSpan(3, 2))
            End If

        End Sub

        Sub Translate(searchList() As String)
            ' TRANSLATE 90xx dStrong lemmas (some of the pronouns) ... handled here because they aren't in the lexicon.
            If Is90xxPronoun Then
                Reveal(ParseMorph())
                Exit Sub
            End If

            ' Else... TRANSLATE MOST OF EVERYTHING HERE

            Dim translation As String = ParseMorph()

            ' CAPITALIZE THE WORD THAT THE CONCORDANCE IS SEARCHING FOR
            If searchList IsNot Nothing _
                AndAlso searchList.Contains(StrongsLemma) Then translation = translation.ToUpper

            ' Add "THE" to construct chains, or add possessives, or add "THOSE THAT...", etc.
            If CustomArticle IsNot Nothing Then
                Reveal(CustomArticle & WordLink)
            ElseIf Definite AndAlso Not SuppressDefArt AndAlso (Not IsProper OrElse HasDefiniteArticle) Then
                Reveal(RtLexicon("9009").Particle & WordLink)
            End If


            ' HANDLE MULTI-WORD VERBS THAT PREFER INTERNALIZED OBJECT
            If IsMainLemma AndAlso ParentDabar.ObjectPronounSuffix IsNot Nothing Then
                If translation.Contains("°"c) Then
                    If translation.Length > 1 Then ' translation is a verb that prefers direct object as infix
                        translation = translation.Replace("°", ParentDabar.ObjectPronounSuffix & WordLink)
                    Else
                        translation &= ParentDabar.ObjectPronounSuffix
                        SuppressFollowingSpace = False
                    End If
                Else
                    translation &= WordLink & ParentDabar.ObjectPronounSuffix
                    SuppressFollowingSpace = False
                End If
            ElseIf translation.Contains("°"c) AndAlso translation.Length > 1 Then
                translation = translation.Replace("°", Nothing)
            End If

            Reveal(translation)


            Dim topLevelEntry As String = RtLexicon(StrongsLemma).TopLevelStrongs

            If Not IsProper Then ' TRACK WORD FREQUENCIES for use in glossary
                If Not UsedWords.TryAdd(topLevelEntry, 1) Then
                    UsedWords.Item(topLevelEntry) += 1
                End If
            End If

            ' TRACK UNTRANSLATED WORDS while developing the lexicon
            If translation.Contains(UnverifiedLexiconFlag) Then
                If Not UntranslatedRoots.TryAdd(topLevelEntry, 1) Then _
                    UntranslatedRoots.Item(topLevelEntry) += 1
            End If
        End Sub
    End Class
End Module