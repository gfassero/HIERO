Imports System.Xml

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