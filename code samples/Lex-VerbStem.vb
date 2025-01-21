Imports System.Text.RegularExpressions
Imports System.Xml
Partial Module Main
	Public Class VerbStem
		Public ReadOnly infinitive As String
		Public ReadOnly past As String
		Public ReadOnly _passiveParticiple As String
		Public ReadOnly _actorSingular As String
		Public ReadOnly _actorPlural As String
		Public ReadOnly thirdPersonSingular As String
		Public ReadOnly firstPersonSingular As String
		Public ReadOnly presentPlural As String
		Public ReadOnly past13PersonSingular As String
		Public ReadOnly gerund As String
		Public ReadOnly _acteeSingular As String
		Public ReadOnly _acteePlural As String
		Public ReadOnly HasCustomActor As Boolean = False
		Public Sub New(entry As XmlElement) ' Create stem from LEXICON.XML or HELPING-VERBS.XML 
			Dim fixes As String()
			If System.String.IsNullOrEmpty(entry.InnerText) Then
				Console.WriteLine()
				Console.WriteLine("ERROR: Empty verb stem near " & entry.ParentNode.Item("xref").GetAttribute("strong"))
				fixes = {"", "", "[NO VERB STEM]", ""}
			Else
				fixes = PrefixVerbCloserSuffix(entry.InnerText.Replace(" "c, WordLink))
			End If
			infinitive = fixes(0) & fixes(1) & fixes(2) & fixes(3)

			Dim handled As Boolean = False
			For Each helpingVerb As VerbStem In helpingVerbs
				If fixes(1) = helpingVerb.infinitive Then
					past = fixes(0) & helpingVerb.past & fixes(2) & fixes(3)
					_passiveParticiple = fixes(0) & helpingVerb._passiveParticiple & fixes(2) & fixes(3)
					thirdPersonSingular = fixes(0) & helpingVerb.thirdPersonSingular & fixes(2) & fixes(3)
					firstPersonSingular = fixes(0) & helpingVerb.firstPersonSingular & fixes(2) & fixes(3)
					presentPlural = fixes(0) & helpingVerb.presentPlural & fixes(2) & fixes(3)
					past13PersonSingular = fixes(0) & helpingVerb.past13PersonSingular & fixes(2) & fixes(3)
					If helpingVerb Is vBe AndAlso fixes(3) IsNot Nothing Then
						gerund = fixes(3).TrimStart(WordLink)
						_actorSingular = gerund
						_actorPlural = gerund
					Else
						gerund = fixes(0) & helpingVerb.gerund & fixes(2) & fixes(3)
						_actorSingular = fixes(0) & helpingVerb._actorSingular & fixes(2) & fixes(3)
						_actorPlural = fixes(0) & helpingVerb._actorPlural & fixes(2) & fixes(3)
					End If
					_acteeSingular = fixes(0) & helpingVerb._acteeSingular & fixes(2) & fixes(3)
					_acteePlural = fixes(0) & helpingVerb._acteePlural & fixes(2) & fixes(3)
					HasCustomActor = helpingVerb.HasCustomActor
					handled = True
					Exit For
				End If
			Next

			If Not handled Then

				firstPersonSingular = infinitive
				presentPlural = infinitive
				gerund = fixes(0) & fixes(1) & fixes(2) & "ing" & fixes(3)

				If fixes(1).EndsWith("e"c) Then
					thirdPersonSingular = fixes(0) & fixes(1) & fixes(2) & "s" & fixes(3)
					past = fixes(0) & fixes(1) & fixes(2) & "d" & fixes(3)
					gerund = fixes(0) & fixes(1).Remove(fixes(1).Length - 1) & fixes(2) & "ing" & fixes(3)

				ElseIf fixes(1).EndsWith("y"c) AndAlso Not "aeiou".Contains(fixes(1)(fixes(1).Length - 2)) Then
					thirdPersonSingular = fixes(0) & fixes(1).Remove(fixes(1).Length - 1) & fixes(2) & "ies" & fixes(3)
					past = fixes(0) & fixes(1).Remove(fixes(1).Length - 1) & fixes(2) & "ied" & fixes(3)

				ElseIf fixes(1).EndsWith("s"c) OrElse fixes(1).EndsWith("sh") OrElse fixes(1).EndsWith("ch") OrElse fixes(1).EndsWith("x"c) Then
					thirdPersonSingular = fixes(0) & fixes(1) & fixes(2) & "es" & fixes(3)
					past = fixes(0) & fixes(1) & fixes(2) & "ed" & fixes(3)

				Else
					thirdPersonSingular = fixes(0) & fixes(1) & fixes(2) & "s" & fixes(3)
					past = fixes(0) & fixes(1) & fixes(2) & "ed" & fixes(3)
				End If

				If entry.HasAttribute("form") Then
					Select Case entry.Attributes.GetNamedItem("form").Value
						Case "doubleFinal"
							past = fixes(0) & fixes(1) & fixes(1)(fixes(1).Length - 1) & fixes(2) & "ed" & fixes(3)
							gerund = fixes(0) & fixes(1) & fixes(1)(fixes(1).Length - 1) & fixes(2) & "ing" & fixes(3)

						Case "doNotDoubleFinal"
							' Regular (as in ORBIT -> ORBITED), but don't double the final letter as in (FIT -> FITTED)

						Case "be"
							firstPersonSingular = entry.Attributes.GetNamedItem("firstSing").Value
							presentPlural = entry.Attributes.GetNamedItem("presPlu").Value
							past13PersonSingular = entry.Attributes.GetNamedItem("pastFirstThirdSing").Value

						Case Else
							Throw New ArgumentException("Exception Occured")
					End Select
				ElseIf DoubleFinalLetterForEdIng.IsMatch(fixes(1)) Then
					past = fixes(0) & fixes(1) & fixes(1)(fixes(1).Length - 1) & fixes(2) & "ed" & fixes(3)
					gerund = fixes(0) & fixes(1) & fixes(1)(fixes(1).Length - 1) & fixes(2) & "ing" & fixes(3)

				End If

				If entry.HasAttribute("thrdSing") Then
					thirdPersonSingular = entry.Attributes.GetNamedItem("thrdSing").Value
				End If

				If entry.HasAttribute("gerund") Then
					gerund = entry.Attributes.GetNamedItem("gerund").Value
				End If
				_actorSingular = gerund
				_actorPlural = gerund

				If entry.HasAttribute("past") Then
					past = entry.Attributes.GetNamedItem("past").Value
				End If
				If past13PersonSingular = Nothing Then
					past13PersonSingular = past
				End If

				If entry.HasAttribute("pcpl") Then
					_passiveParticiple = entry.Attributes.GetNamedItem("pcpl").Value
				Else
					_passiveParticiple = past
				End If
			End If



			If entry.HasAttribute("actor") Then
				HasCustomActor = True
				Dim actor() As String = entry.Attributes.GetNamedItem("actor").Value.Replace(" ", WordLink).Split("/")
				_actorSingular = actor(0)
				If actor.Length = 2 Then
					_actorPlural = actor(1)
				Else
					_actorPlural = Pluralize(actor(0))
				End If
			End If

			If entry.HasAttribute("actee") Then ' ONLY INTENDED FOR QAL STEMS
				Dim actee() As String = entry.Attributes.GetNamedItem("actee").Value.Replace(" ", WordLink).Split("/")
				_acteeSingular = actee(0)
				If actee.Length = 2 Then
					_acteePlural = actee(1)
				Else
					_acteePlural = Pluralize(actee(0))
				End If
			Else
				_acteeSingular = _passiveParticiple
				_acteePlural = _passiveParticiple
			End If
		End Sub
		Public Sub New(activeStemToReflexify As VerbStem) ' Create reflexive stem from PRE-EXISTING active stem

			HasCustomActor = activeStemToReflexify.HasCustomActor

			infinitive = Reflexify(activeStemToReflexify.infinitive)
			past = Reflexify(activeStemToReflexify.past)
			_passiveParticiple = Reflexify(activeStemToReflexify._passiveParticiple)
			_actorSingular = Reflexify(activeStemToReflexify._actorSingular)
			thirdPersonSingular = Reflexify(activeStemToReflexify.thirdPersonSingular)
			firstPersonSingular = Reflexify(activeStemToReflexify.firstPersonSingular)
			presentPlural = Reflexify(activeStemToReflexify.presentPlural)
			past13PersonSingular = Reflexify(activeStemToReflexify.past13PersonSingular)
			_actorPlural = Reflexify(activeStemToReflexify._actorPlural)
			gerund = Reflexify(activeStemToReflexify.gerund)
			_acteeSingular = Reflexify(activeStemToReflexify._passiveParticiple)
			_acteePlural = Reflexify(activeStemToReflexify._passiveParticiple)

		End Sub
		Public Sub New(helpingVerb As VerbStem, infix As String, root As String) ' OCCURS TO PRODUCE STEMS REGULARLY DERIVED FROM QAL, etc.
			Dim inRootSuff = infix & root
			infinitive = helpingVerb.infinitive & inRootSuff
			past = helpingVerb.past & inRootSuff
			_passiveParticiple = helpingVerb._passiveParticiple & inRootSuff
			thirdPersonSingular = helpingVerb.thirdPersonSingular & inRootSuff
			firstPersonSingular = helpingVerb.firstPersonSingular & inRootSuff
			presentPlural = helpingVerb.presentPlural & inRootSuff
			past13PersonSingular = helpingVerb.past13PersonSingular & inRootSuff
			gerund = helpingVerb.gerund & inRootSuff
			If helpingVerb Is vBe Then
				If infix = WordLink Then
					_actorSingular = root
					_actorPlural = root
				Else
					Throw New ArgumentException("Exception Occured")
				End If
			Else
				_actorSingular = helpingVerb._actorSingular & WordLink & root
				_actorPlural = helpingVerb._actorPlural & WordLink & root
			End If
			_acteeSingular = helpingVerb._acteeSingular & inRootSuff
			_acteePlural = helpingVerb._acteePlural & inRootSuff
		End Sub
		Public Sub New(root As String)
			errorlist.Add("Had to fabricate an ad hoc verb from non-verb " & root)

			infinitive = root
			firstPersonSingular = infinitive
			presentPlural = infinitive

			thirdPersonSingular = root & "s"

			If root.EndsWith("e"c) Then root = root.Substring(0, root.Length - 1)

			past = root & "ed"

			past13PersonSingular = past
			_passiveParticiple = past

			gerund = root & "ing"
			_actorSingular = gerund
			_actorPlural = gerund
			_acteeSingular = past
			_acteePlural = past
		End Sub
	End Class

	Dim DoubleFinalLetterForEdIng As New Regex("[^aeiou][aeiou][^aeiouhnrwyx]$")

	Public Function PrefixVerbCloserSuffix(root As String) As String()

		' PREFIX = optional character [ or { or √
		' VERB = main verb
		' CLOSER = optional character }
		' SUFFIX = ] and anything after a space

		Dim fixes(3) As String

		If root(0) = "["c OrElse root(0) = "{"c OrElse root(0) = "√"c Then
			fixes(0) = root(0)
			root = root.Substring(1)
		End If

		If root.Contains(WordLink) Then
			fixes(3) = root.Substring(root.IndexOf(WordLink))
			root = root.Substring(0, root.IndexOf(WordLink))
		End If

		If root.EndsWith("]"c) Then
			fixes(3) = "]"c & fixes(3)
			root = root.Substring(0, root.Length - 1)
		ElseIf root.EndsWith("}"c) Then
			fixes(2) = "}"c
			root = root.Substring(0, root.Length - 1)
		End If

		fixes(1) = root

		Return fixes

	End Function

	Public Function Reflexify(toBeCloned As String) As String

		If toBeCloned.Contains("°"c) Then
			Return toBeCloned.Replace("°"c, "oneself" & WordLink)

		Else
			Return toBeCloned & WordLink & "oneself"

		End If


	End Function

End Module