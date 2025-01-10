Imports System.Xml
Partial Module Main
	Public Class VerbRoot
		Public Sub New(entry As XmlElement)
			For Each elem As XmlNode In entry.ChildNodes
				Select Case elem.Name

					Case "qalAct" : QalActive = New VerbStem(elem)
					Case "qalPass" : qalPassiveHolder = New VerbStem(elem)
					Case "qalReflex" : qalReflexiveHolder = New VerbStem(elem)

					Case "pielAct" : pielActiveHolder = New VerbStem(elem)
					Case "pielPass" : pielPassiveHolder = New VerbStem(elem)
					Case "pielReflex" : pielReflexiveHolder = New VerbStem(elem)

					Case "hiphilAct" : hiphilActiveHolder = New VerbStem(elem)
					Case "hiphilPass" : hiphilPassiveHolder = New VerbStem(elem)
					Case "hiphilReflex" : hiphilReflexiveHolder = New VerbStem(elem)

				End Select
			Next
		End Sub
		Public Sub New(rootWord As String)
			QalActive = New VerbStem(rootWord)
		End Sub

		Public ReadOnly Property EmergencyNounFunction As String
			Get
				If QalActive IsNot Nothing Then
					Return QalActive.infinitive
				ElseIf pielActiveHolder IsNot Nothing Then
					Return PielActive.infinitive
				ElseIf hiphilActiveHolder IsNot Nothing Then
					Return HiphilActive.infinitive
				ElseIf QalPassiveholder IsNot Nothing Then
					Return QalPassive.infinitive
				ElseIf pielpassiveHolder IsNot Nothing Then
					Return PielPassive.infinitive
				ElseIf hiphilpassiveHolder IsNot Nothing Then
					Return HiphilPassive.infinitive
				ElseIf pielreflexiveHolder IsNot Nothing Then
					Return PielReflexive.infinitive
				ElseIf hiphilreflexiveHolder IsNot Nothing Then
					Return HiphilReflexive.infinitive
				Else
					Throw New ArgumentException("Exception Occurred")
				End If
			End Get
		End Property

		' QAL
		' ACTING: to kill, to be killed
		' ACTING: to love, to be loved

		Public ReadOnly QalActive As VerbStem = Nothing
		Private qalPassiveHolder As VerbStem = Nothing
		Private qalReflexiveHolder As VerbStem = Nothing

		Public ReadOnly Property QalPassive As VerbStem
			Get
				If IsNothing(qalPassiveHolder) Then

					qalPassiveHolder = New VerbStem(vBe, WordLink, QalActive._passiveParticiple)

				End If
				Return qalPassiveHolder
			End Get
		End Property
		Public ReadOnly Property QalReflexive As VerbStem
			Get
				If IsNothing(qalReflexiveHolder) Then

					qalReflexiveHolder = New VerbStem(QalActive)

				End If
				Return qalReflexiveHolder
			End Get
		End Property

		' PIEL
		' CAUSING A STATE: to cause to be killed, to be caused to be killed, to cause oneself to be killed
		' CAUSING A STATE: to cause to be loved, to be caused to be loved, to cause oneself to be loved
		' WHICH SIMPLIFIES TO: to kill, to be killed, to kill oneself, BUT EMPHATICALLY
		' WHICH SIMPLIFIES TO: to love, to be loved, to love oneself, BUT EMPHATICALLY

		Private pielActiveHolder As VerbStem = Nothing
		Private pielPassiveHolder As VerbStem = Nothing
		Private pielReflexiveHolder As VerbStem = Nothing

		Public ReadOnly Property PielActive As VerbStem
			Get
				If IsNothing(pielActiveHolder) Then

					pielActiveHolder = QalActive

				End If
				Return pielActiveHolder
			End Get
		End Property
		Public ReadOnly Property PielPassive As VerbStem
			Get
				If IsNothing(pielPassiveHolder) Then

					If Not IsNothing(pielActiveHolder) Then
						pielPassiveHolder = New VerbStem(vBe, WordLink, pielActiveHolder._passiveParticiple)
					ElseIf Not IsNothing(qalPassiveHolder) Then
						pielPassiveHolder = QalPassive
					Else
						pielPassiveHolder = New VerbStem(vBe, WordLink, QalActive._passiveParticiple)
					End If

				End If
				Return pielPassiveHolder
			End Get
		End Property
		Public ReadOnly Property PielReflexive As VerbStem
			Get
				If IsNothing(pielReflexiveHolder) Then

					If Not IsNothing(pielActiveHolder) Then
						pielReflexiveHolder = New VerbStem(pielActiveHolder)
					Else
						pielReflexiveHolder = New VerbStem(QalActive)
					End If

				End If
				Return pielReflexiveHolder
			End Get
		End Property

		' HIPHIL
		' CAUSING AN ACTION: to make someone kill, to be made to kill, to make oneself kill
		' CAUSING AN ACTION: to make someone love, to be made to love, to make oneself love

		Private hiphilActiveHolder As VerbStem = Nothing
		Private hiphilPassiveHolder As VerbStem = Nothing
		Private hiphilReflexiveHolder As VerbStem = Nothing
		Public ReadOnly Property HiphilActive As VerbStem
			Get
				If IsNothing(hiphilActiveHolder) Then

					hiphilActiveHolder = New VerbStem(vCause, WordLink & "°to" & WordLink, QalActive.infinitive)

				End If
				Return hiphilActiveHolder
			End Get
		End Property
		Public ReadOnly Property HiphilPassive As VerbStem
			Get
				If IsNothing(hiphilPassiveHolder) Then

					hiphilPassiveHolder = New VerbStem(vBe, WordLink, HiphilActive._passiveParticiple)

				End If
				Return hiphilPassiveHolder
			End Get
		End Property
		Public ReadOnly Property HiphilReflexive As VerbStem
			Get
				If IsNothing(hiphilReflexiveHolder) Then

					hiphilReflexiveHolder = New VerbStem(vCause, WordLink & "oneself" & WordLink & "to" & WordLink, QalActive.infinitive)

				End If
				Return hiphilReflexiveHolder
			End Get
		End Property

	End Class

End Module