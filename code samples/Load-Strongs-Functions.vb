Partial Module main
	Partial Class Strongs

		Function English(stem As VerbStem, morph As String, Optional verbFlavor As VerbType = VerbType.Other, Optional omitPronoun As String = Nothing) As String
			Select Case verbFlavor
				Case VerbType.Imperfect
					If String.IsNullOrEmpty(omitPronoun) Then
						English = SubjectPronouns(morph.Substring(3, 3)) & WordLink
					Else
						English = ""
					End If

					If morph(5) = "s"c AndAlso morph(3) = "3"c Then '     THIRD-PERSON SINGULAR
						English &= stem.thirdPersonSingular
					ElseIf morph(5) = "s"c AndAlso morph(3) = "1"c Then ' FIRST-PERSON SINGULAR
						English &= stem.firstPersonSingular
					Else
						English &= stem.presentPlural
					End If

				Case VerbType.Perfect
					English = SubjectPronouns(morph.Substring(3, 3)) & WordLink
					If morph(5) = "s"c AndAlso (morph(3) = "3"c OrElse morph(3) = "1"c) Then '     FIRST- OR THIRD-PERSON SINGULAR
						English &= stem.past13PersonSingular
					Else
						English &= stem.past
					End If

				Case VerbType.Actor
					Select Case morph(5) ' VERB NUMBER
						Case "s"c : English = stem._actorSingular ' SINGLE
						Case "p"c : English = stem._actorPlural ' PLURAL
						Case "d"c : English = stem._actorPlural ' DUAL
						Case Else : English = stem._actorSingular ' ERROR
							Throw New ArgumentException("Unexpected verb number: " + morph)
					End Select

				Case VerbType.Actee
					Select Case morph(5) ' VERB NUMBER
						Case "s"c : English = stem._acteeSingular ' SINGLE
						Case "p"c : English = stem._acteePlural ' PLURAL
						Case "d"c : English = stem._acteePlural ' DUAL
						Case Else : English = stem._acteeSingular ' ERROR
							Throw New ArgumentException("Unexpected verb number: " + morph)
					End Select

				Case VerbType.InfinitiveGerund
					English = stem.gerund

				Case VerbType.InfinitiveTrue
					English = stem.infinitive

				Case Else
					English = stem.infinitive

			End Select

			Return ProcForReflex(English, morph)

		End Function

		Function ProcForReflex(english As String, morph As String, Optional trimBe As Boolean = False) As String
			If morph.Length > 5 AndAlso english.Contains("one") Then
				english = english.Replace("oneself", ReflexivePronouns(morph.Substring(3, 3))) _
					.Replace("one's", PossessiveAdjectives(morph.Substring(3, 3)))
			End If
			If trimBe Then
				english = english.Substring(3)
			End If
			Return english
		End Function

		Function GetStem(verb As VerbRoot, stem As Char) As VerbStem

			Select Case stem ' VERB STEM
				' ACTING: to kill, to be killed
				' ACTING: to love, to be loved
				Case "q"c : Return verb.QalActive ' QAL, simple active
				Case "N"c : Return verb.QalPassive ' NIPHAL, simple passive
				Case "Q"c : Return verb.QalPassive ' QAL PASSIVE  "   (could be same as pual)
				Case "i"c : Return verb.QalReflexive ' ITPEEL, simple reflexive  (aramaic only)
				' CAUSING A STATE: to cause to be killed, to be caused to be killed, to cause oneself to be killed
				' CAUSING A STATE: to cause to be loved, to be caused to be loved, to cause oneself to be loved
				Case "p"c : Return verb.PielActive ' PIEL, make to be
			'Case "o"c : Return verb.PielActive ' POLEL      "
			'Case "m"c : Return verb.PielActive ' POEL       "
			'Case "l"c : Return verb.PielActive ' PILPEL     "
			'Case "k"c : Return verb.PielActive ' PALEL      "
			'Case "j"c : Return verb.PielActive ' PEALAL     "
			'Case "i"c : Return verb.PielActive ' PILEL      "
				Case "P"c : Return verb.PielPassive ' PUAL, be made to be
			'Case "O"c : Return verb.PielPassive ' POLAL      "
			'Case "L"c : Return verb.PielPassive ' POLPAL     "
			'Case "K"c : Return verb.PielPassive ' PULAL      "
				Case "u"c : Return verb.PielPassive ' HOTHPAAL    "
				Case "D"c : Return verb.PielPassive ' NITHPAEL    "
				Case "t"c : Return verb.PielReflexive ' HITHPAEL, make oneself to be
				Case "M"c : Return verb.PielReflexive ' HITHPAAL    "  (aramaic only)
			'Case "r"c : Return verb.PielReflexive ' HITHPOLEL  "
			'Case "f"c : Return verb.PielReflexive ' HITHPALPEL "
			'Case "z"c : Return verb.PielReflexive ' HITHPOEL   "
				' CAUSING AN ACTION: to make someone kill, to be made to kill, to make oneself kill
				' CAUSING AN ACTION: to make someone love, to be made to love, to make oneself love
				Case "h"c : Return verb.HiphilActive ' HIPHIL, make to love
				Case "c"c : Return verb.HiphilActive ' TIPHIL     "
				Case "e"c : Return verb.HiphilActive ' SHAPHEL    "  (aramaic only)
				Case "a"c : Return verb.HiphilActive ' APHEL      "  (aramaic only)
				Case "H"c : Return verb.HiphilPassive ' HOPHAL, be made to love
				Case "v"c : Return verb.HiphilReflexive ' HISHTAPHEL, make oneself to love    (almost exclusively #7812, bow oneself)
					' ERROR
				Case Else : Return verb.QalActive
					Throw New ArgumentException("Exception Occured")
			End Select

		End Function

	End Class
End Module