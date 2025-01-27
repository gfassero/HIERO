Partial Module Main
	Partial Class Strongs

		' Notes relevant to making each translation unique:
		' *** I'm not sure whether "determined" noun/adjective state is ever used.

		Function ParseMorph() As String
			Dim root As LexEntry = Nothing
			If RtLexicon.TryGetValue(StrongsLemma, root) Then ' use RootLexicon to translate lemma
				' Successful lookup: Proceed as usual.

			ElseIf StrongsLemma.EndsWith("+"c) Then ' Lemma is part of a compound word like Ben-jamin, Bath-sheba, or Muth-labben, but the definitions of both lemmas are assigned to one lemma.
				Console.WriteLine(vbCrLf & "Unexpected ""plus"" lemma: " & StrongsLemma)
				errorlist.Add("Unexpected ""plus"" lemma: " & StrongsLemma)
				Return Nothing

			Else ' lemma not found in RootLexicon, i.e., SOMETHING SLIPPED THROUGH AND STILL NEEDS TO BE PROGRAMMED
				Console.WriteLine(vbCrLf & "Unexpected lemma: " & StrongsLemma)
				errorlist.Add("Unexpected lemma: " & StrongsLemma)
				Return Nothing

			End If


			'If morph.Replace("Pdx", "").Contains("x"c) Then
			If GramMorph IsNot Nothing AndAlso GramMorph.Contains("x"c) Then
				Throw New ArgumentException("Unparsed word in Hebrew text.")
				Return root.Particle & "~"
			End If

			' Return punctuation immediately
			If GramMorph Is Nothing OrElse String.IsNullOrEmpty(GramMorph) Then
				Return root.Particle
			End If

			' Climb through morph one character at a time.

			Select Case GramMorph(0) ' PART OF SPEECH
				Case "A"c ' ADJECTIVE
#Region "Adjective"

					' IGNORE morph(1), which could be a/c/o, for adjective, cardinal number, ordinal number

					Select Case GramMorph(3) ' ADJECTIVE NUMBER
						Case "s"c : ParseMorph = root.Noun.adjSing
						Case "p"c : ParseMorph = root.Noun.adjPlur
						Case "d"c : ParseMorph = root.Noun.adjDual
						Case Else : ParseMorph = root.Noun.adjSing
							Throw New ArgumentException("Unexpected adjective number: " + GramMorph)  ' ERROR
					End Select

					TagGender(ParseMorph, GramMorph(2))
					ParseMorph &= EnglishConstructSuffix
#End Region
				Case "C"c, "c"c 'CONJUNCTION
#Region "Conjunction"
					Return root.Particle
#End Region
				Case "D"c ' ADVERB
#Region "Adverb"
					Return root.Particle
#End Region
				Case "N"c ' NOUN
#Region "Noun"
					Select Case GramMorph(1)
						Case "c", "t"c, "g"c, "p"c ' NOUN TYPE COMMON, TITLE, GENTILIC, or PROPER

							If GramMorph.Length > 3 Then
								Select Case GramMorph(3) ' NOUN NUMBER
									Case "s"c : ParseMorph = root.Noun.nounSing
									Case "p"c : ParseMorph = root.Noun.nounPlur
									Case "d"c : ParseMorph = root.Noun.nounDual
									Case Else : ParseMorph = root.Noun.nounSing
										Throw New ArgumentException("Unexpected noun number: " + GramMorph)  ' ERROR
								End Select
							ElseIf GramMorph(1) = "p"c Then
								ParseMorph = root.Noun.nounSing
							Else
								Throw New ArgumentException("Unexpected short, non-proper noun morph: " + GramMorph)  ' ERROR
							End If

							If IsProper Then
								ParseMorph = ProperNoun(ParseMorph)
							End If

							TagGender(ParseMorph, GramMorph(2), True)
							ParseMorph &= EnglishConstructSuffix

						Case Else : ParseMorph = root.Noun.nounSing
							Throw New ArgumentException("Unexpected noun morph: " + GramMorph)

					End Select
#End Region
				Case "P"c, "S"c ' PRONOUNS & SUFFIXES
#Region "Pronouns & Suffixes"

					If GramMorph(0) = "S"c Then
						Select Case GramMorph(1)
							Case "p"c
								Return ObjectPronouns(GramMorph.Substring(2)) ' PRONOMIAL SUFFIX, only this suffix type has additional morph chars
							Case "n", "h"
								Return root.Particle
							Case Else
								Throw New ArgumentException("Unexpected suffix type")
						End Select
					End If

					' ****** for all of these, I want to make sure there's only one morph mappable to each lemma, and vice-versa,
					'        or find a way to differentiate between them if there's any relevant difference *****

					' IsDStrongPronoun only captures 90xx lemmas, not the others below. Address this issue.

					Select Case GramMorph(1)
						Case "i"c ' INTERROGATIVE
							If GramMorph <> "Pi" OrElse (
							StrongsLemma <> "3964" AndAlso
							StrongsLemma <> "4100" AndAlso
							StrongsLemma <> "4101" AndAlso
							StrongsLemma <> "4310" AndAlso
							StrongsLemma <> "4478B" AndAlso
							StrongsLemma <> "4479") Then
								Throw New ArgumentException("Exception Occurred")
							End If

							Return root.Noun.adjSing ' IS THIS RIGHT???

						Case "p"c ' PERSONAL    The second lemma options are Aramaic equivalents. ' *** Should these all be Sp instead of Pp? ***
							If Not ((StrongsLemma = "1931" OrElse StrongsLemma = "1932") AndAlso (GramMorph = "Pp3ms" OrElse GramMorph = "Pp3fs")) AndAlso ' he/she
							Not ((StrongsLemma = "0589" OrElse StrongsLemma = "0576B") AndAlso GramMorph = "Pp1bs") AndAlso ' I
							Not (StrongsLemma = "0595" AndAlso GramMorph = "Pp1bs") AndAlso ' I myself
							Not ((StrongsLemma = "0587" OrElse StrongsLemma = "0586") AndAlso GramMorph = "Pp1bp") AndAlso ' we
							Not (StrongsLemma = "5168" AndAlso GramMorph = "Pp1bp") AndAlso ' we x5 *** Should this be differentiated from previous?
							Not ((StrongsLemma = "0859A" OrElse StrongsLemma = "0607") AndAlso GramMorph = "Pp2ms") AndAlso ' you (ms)
							Not (StrongsLemma = "0859C" AndAlso GramMorph = "Pp2fs") AndAlso ' you (fs)
							Not ((StrongsLemma = "0859D" OrElse StrongsLemma = "0608") AndAlso GramMorph = "Pp2mp") AndAlso ' you (mp)
							Not (StrongsLemma = "0859E" AndAlso GramMorph = "Pp2fp") AndAlso ' you (fp)
							Not ((StrongsLemma = "2007" OrElse StrongsLemma = "0581B") AndAlso GramMorph = "Pp3fp") AndAlso ' they (f)
							Not ((StrongsLemma = "1992" OrElse StrongsLemma = "1994" OrElse StrongsLemma = "0581A") AndAlso GramMorph = "Pp3mp") AndAlso ' they (m) *** unknown why there are two Aramaic equivalents.
																																						 _ ' BEGIN 90xx
							Not (StrongsLemma = "9020" AndAlso GramMorph = "Pp1bs") AndAlso
							Not (StrongsLemma = "9021" AndAlso GramMorph = "Pp2ms") AndAlso
							Not (StrongsLemma = "9022" AndAlso GramMorph = "Pp2fs") AndAlso
							Not (StrongsLemma = "9023" AndAlso GramMorph = "Pp3ms") AndAlso
							Not (StrongsLemma = "9024" AndAlso GramMorph = "Pp3fs") AndAlso
							Not (StrongsLemma = "9025" AndAlso GramMorph = "Pp1bp") AndAlso
							Not (StrongsLemma = "9026" AndAlso GramMorph = "Pp2mp") AndAlso
							Not (StrongsLemma = "9027" AndAlso GramMorph = "Pp2fp") AndAlso
							Not (StrongsLemma = "9028" AndAlso GramMorph = "Pp3mp") AndAlso
							Not (StrongsLemma = "9029" AndAlso GramMorph = "Pp3fp") AndAlso
							Not (StrongsLemma = "9030" AndAlso GramMorph = "Pp1bs") AndAlso
							Not (StrongsLemma = "9031" AndAlso GramMorph = "Pp2ms") AndAlso
							Not (StrongsLemma = "9032" AndAlso GramMorph = "Pp2fs") AndAlso
							Not (StrongsLemma = "9033" AndAlso GramMorph = "Pp3ms") AndAlso
							Not (StrongsLemma = "9034" AndAlso GramMorph = "Pp3fs") AndAlso
							Not (StrongsLemma = "9035" AndAlso GramMorph = "Pp1bp") AndAlso
							Not (StrongsLemma = "9036" AndAlso GramMorph = "Pp2mp") AndAlso
							Not (StrongsLemma = "9037" AndAlso GramMorph = "Pp2fp") AndAlso
							Not (StrongsLemma = "9038" AndAlso GramMorph = "Pp3mp") AndAlso
							Not (StrongsLemma = "9039" AndAlso GramMorph = "Pp3fp") AndAlso
							Not (StrongsLemma = "9040" AndAlso GramMorph = "Pp1bs") AndAlso
							Not (StrongsLemma = "9041" AndAlso GramMorph = "Pp2ms") AndAlso
							Not (StrongsLemma = "9042" AndAlso GramMorph = "Pp2fs") AndAlso
							Not (StrongsLemma = "9043" AndAlso GramMorph = "Pp3ms") AndAlso
							Not (StrongsLemma = "9044" AndAlso GramMorph = "Pp3fs") AndAlso
							Not (StrongsLemma = "9045" AndAlso GramMorph = "Pp1bp") AndAlso
							Not (StrongsLemma = "9046" AndAlso GramMorph = "Pp2mp") AndAlso
							Not (StrongsLemma = "9047" AndAlso GramMorph = "Pp2fp") AndAlso
							Not (StrongsLemma = "9048" AndAlso GramMorph = "Pp3mp") AndAlso
							Not (StrongsLemma = "9049" AndAlso GramMorph = "Pp3fp") Then

								' Lemma 859C "you (fs)" occurs 4x with morph Pp2ms "you (ms)": Psa.6.3(6.4)#04, Num.11.15#03, Deu.5.27#10, and Ezk.28.14#01
								' These are tagged incorrectly, so fix the tag and render them as feminine.
								If StrongsLemma = "0859C" Then : GramMorph = "Pp2fs"
								Else : Throw New ArgumentException("Lemma " & StrongsLemma & " occurred with morph " & GramMorph & ".")
								End If

							End If
						Case Else
							Throw New ArgumentException("Exception Occurred")
					End Select

					Select Case StrongsLemma
						Case "9020" To "9029" ' Possessive pronouns... is it used here, or only in lookahead? Or does lookahead come through here?
							Return PossessiveAdjectives(GramMorph.Substring(2))
						Case "9030" To "9039" ' Object pronouns
							Return ObjectPronouns(GramMorph.Substring(2))
						Case "9040" To "9049" ' Subject pronouns
							ParseMorph = SubjectPronouns(GramMorph.Substring(2))
						Case Else             ' Subject pronouns
							ParseMorph = SubjectPronouns(GramMorph.Substring(2))
					End Select

					Select Case GramMorph(4) ' PRONOUN NUMBER
						Case "s"c : ParseMorph &= TagSing
						Case "p"c : ParseMorph &= TagPlur
							'Case "d"c : ParseMorph &= TagDual
						Case Else : Throw New ArgumentException("Unexpected pronoun number: " + GramMorph)  ' ERROR
					End Select

					' PRONOUN PERSON = morph(2) ***** haven't processed this at all!! need to... unless I'm sure that there will never be ambiguity, which seems likely. 1/2/3 are the options


					TagGender(ParseMorph, GramMorph(3))
#End Region
				Case "R"c ' PREPOSITION
#Region "Preposition"

					ParseMorph = root.Particle

					If GramMorph <> "R" Then
						Throw New ArgumentException("Unexpected preposition type: " + GramMorph)
					End If
#End Region
				Case "T"c ' PARTICLE
#Region "Particle"
					ParseMorph = root.Particle

					'If morph.Length = 1 AndAlso lemma = "1768" AndAlso OSHB_flagAramaic Then
					'	' Aramaic often shows lemma 1768 "who" with morph "T". Unknown meaning, need to research later.
					'	Exit Function
					'End If

					Select Case GramMorph(1) ' PARTICLE TYPE     *** Particles might already be translated consistently by means of the strongs numbers. Have to check later...
						Case "a"c : ParseMorph += "" ' ARAMAIC FINAL ARTICLE
						Case "d"c : ParseMorph += "" ' DEFINITE ARTICLE
					' Case "e"c : ParseMorph += "" ' EXHORTATION
						Case "i"c : ParseMorph += "" ' INTERROGATIVE
						Case "j"c : ParseMorph += "" ' INTERJECTION
						Case "m"c : ParseMorph += "" ' DEMONSTRATIVE
						Case "n"c : ParseMorph += "" ' NEGATIVE
						Case "o"c : ParseMorph += "" ' DIRECT OBJECT MARKER
						Case "r"c : ParseMorph += "" ' RELATIVE
						Case "c"c : ParseMorph += "" ' CONDITIONAL/LOGICAL
						Case Else : Throw New ArgumentException("Unexpected particple type: " + GramMorph)  ' ERROR
					End Select
#End Region
				Case "V"c ' VERB
#Region "Verb"
					Dim stem As VerbStem = GetStem(root.Verb, GramMorph(1))
					If IsNothing(stem) Then
						Console.WriteLine("    ERROR: Lemma #" & StrongsLemma & " is missing a verb stem.")
						Return ""
					End If

#Region "Fix NOT idiom"
					' FIX IDIOM INVOLVING "NOT": e.g., "NOT murder" -> "do NOT murder" ... "NOT let me fall" -> "let me NOT fall"
					Dim negator As String = ParentDabar.Negator
					If Not String.IsNullOrEmpty(negator) Then
						If "pqiwv".Contains(GramMorph(2)) Then ' If it's a finite verb or imperative...

							If GramMorph(2) <> "v"c _ ' If it's a finite stative verb: "not he was happy" -> "he was not happy"...
						AndAlso stem.infinitive.StartsWith("be" & WordLink) Then
								negator = String.Concat(WordLink, negator, ProcForReflex(stem.infinitive, GramMorph, True))
								stem = vBe

							ElseIf GramMorph(2) <> "v"c _ ' If it's the finite "be" verb: "not he was" -> "he was not"...
						AndAlso StrongsLemma = "1961" Then
								negator = String.Concat(WordLink, negator.TrimEnd(WordLink))

							Else
								negator = String.Concat(WordLink, negator, ProcForReflex(stem.infinitive, GramMorph))
								stem = vDo

							End If

						End If
					End If
#End Region

					Select Case GramMorph(2) ' VERB TYPE
						Case "p"c, "q"c : ParseMorph = English(stem, GramMorph, VerbType.Perfect) & negator ' PERFECT (QATAL) and SEQUENTIAL PERFECT (WEQATAL)
						Case "i"c, "w"c, "u"c : ParseMorph = English(stem, GramMorph, VerbType.Imperfect, CustomArticle) & negator ' IMPERFECT (YIQTOL), SEQUENTIAL IMPERFECT (WAYYIQTOL), and "CONJUNCTIVE" (WEYYIQTOL)
						' ----------------------------------------------------------------------
						Case "j"c ' JUSSIVE
							If GramMorph(5) = "s" Then
								Select Case GramMorph(4) ' VERB GENDER
									Case "m"c : ParseMorph = JussiveMasc & negator & English(stem, GramMorph) ' MASCULINE
									Case "f"c : ParseMorph = JussiveFemn & negator & English(stem, GramMorph) ' FEMININE
									Case "c"c : ParseMorph = JussiveNeut & negator & English(stem, GramMorph) ' COMMON (verb)
									Case Else : ParseMorph = JussiveNeut & negator & English(stem, GramMorph)
										Throw New ArgumentException("Unexpected verb gender: " + GramMorph) ' ERROR
								End Select
							Else : ParseMorph = JussivePlur & negator & English(stem, GramMorph) : End If
						Case "v"c : ParseMorph = CommandFlag & English(stem, GramMorph) & negator ' IMPERATIVE
						' -------------------------------------- - - - - - - - - - - - - - - - - - - - - - - - - - - -
						Case "c"c ' COHORTATIVE (STEP: c / OSHB: h)  -OR-  INFINITIVE CONSTRUCT
							Select Case GramMorph.Length
								Case 6 ' COHORTATIVE          : tagged V[stem]c1cs (example)
									If GramMorph(5) = "s" Then : ParseMorph = CohortativeSing & negator & English(stem, GramMorph)
									Else : ParseMorph = CohortativePlur & negator & English(stem, GramMorph) : End If

								Case 4 ' INFINITIVE CONSTRUCT : tagged V[stem]cc   (example)
									Return English(stem, GramMorph, VerbType.InfinitiveGerund) & EnglishConstructSuffix

								Case Else : ParseMorph = root.Particle
									Throw New ArgumentException("Unexpected c-form verb: " + GramMorph) ' ERROR
							End Select
						' -------------------------------------- - - - - - - - - - - - - - - - - - - - - - - - - - - -
						Case "a"c : Return English(stem, GramMorph, VerbType.InfinitiveGerund) ' INFINITIVE ABSOLUTE, all tagged V[stem]aa
						' ----------------------------------------------------------------------

						'               PASSIVE (s)   ACTIVE (r)
						' QAL (q)           978x        5578x  qal act    (THIS IS THE ONLY STEM WITH BOTH PASSIVE & ACTIVE PARTICIPLES)
						' PUAL (PDu)        192x           6x  piel pass  (#8246, Exo.25.33#03=L Exo.25.33#10=L Exo.25.34#04=L Exo.37.19#03=L Exo.37.19#10=L Exo.37.19#04=L might be VPsmpa) *** Probably needs an overlay or regexGram
						' HOPHAL (H)        110x           2x  hiph pass  (#0540, Dan.2.45#29=L Dan.6.4(6.5)#20=L might be VHsmsa or Vhrmsa) *** Might need an overlay or regexGram
						' OTHERS (QvNMcpth)  ——         2601x

						Case "r"c, "s"c ' PARTICIPLE ACTIVE, PARTICIPLE PASSIVE
							If GramMorph(1) = "q"c AndAlso GramMorph(2) = "s"c Then
								ParseMorph = English(stem, GramMorph.Insert(3, "-"), VerbType.Actee)
							Else
								ParseMorph = English(stem, GramMorph.Insert(3, "-"), VerbType.Actor)
							End If


						' ----------------------------------------------------------------------
						Case "n"c : Return English(stem, GramMorph, VerbType.InfinitiveTrue) ' TRUE INFINITIVE: never occurs in text. All occurances are generated programatically.
							' ----------------------------------------------------------------------
						Case Else : ParseMorph = English(stem, GramMorph)
							Throw New ArgumentException("Unexpected verb type: " + GramMorph)  ' ERROR
					End Select


					' VERB NUMBER
					If GramMorph(2) = "r"c OrElse GramMorph(2) = "s"c Then ' participle
						Select Case GramMorph(4) ' VERB NUMBER
							Case "s"c : ParseMorph &= TagSing ' SINGLE
							Case "p"c : ParseMorph &= TagPlur ' PLURAL
							Case "d"c : ParseMorph &= TagDual ' DUAL
							Case Else : Throw New ArgumentException("Unexpected verb number: " + GramMorph)  ' ERROR
						End Select

					Else ' Anything but participle or infinitive
						Select Case GramMorph(5) ' VERB NUMBER
							Case "s"c : ParseMorph &= TagSing ' SINGLE
							Case "p"c : ParseMorph &= TagPlur ' PLURAL
							Case "d"c : ParseMorph &= TagDual ' DUAL
							Case Else : Throw New ArgumentException("Unexpected verb number: " + GramMorph)  ' ERROR
						End Select

					End If

					' VERB STATE
					If GramMorph(2) = "r"c OrElse GramMorph(2) = "s"c Then ' participle

						TagGender(ParseMorph, GramMorph(3))
						ParseMorph &= EnglishConstructSuffix

					Else ' Anything but participle or infinitive
						If GramMorph(2) = "r"c OrElse GramMorph(2) = "s"c Then Throw New ArgumentException("Unexpected stateless participle: " + GramMorph)  ' ERROR

						TagGender(ParseMorph, GramMorph(4))

					End If
#End Region
				Case Else ' ERROR
					Throw New ArgumentException("Unexpected part of speech: " + GramMorph)
					Return ""
			End Select
		End Function
	End Class
End Module