Partial Module Main

    Public Const CantillationOpen = "<span class="""
    Public Const CantillationClose = """></span>"

    Public CantillationVariations As New Dictionary(Of String, Integer)

    Dim HebrewConsonants() As Char = {
    ChrW(&H5D0), ' letter alef
    ChrW(&H5D1), ' letter bet
    ChrW(&H5D2), ' letter gimel
    ChrW(&H5D3), ' letter dalet
    ChrW(&H5D4), ' letter he
    ChrW(&H5D5), ' letter vav
    ChrW(&H5D6), ' letter zayin
    ChrW(&H5D7), ' letter het
    ChrW(&H5D8), ' letter tet
    ChrW(&H5D9), ' letter yod
    ChrW(&H5DA), ' letter final kaf
    ChrW(&H5DB), ' letter kaf
    ChrW(&H5DC), ' letter lamed
    ChrW(&H5DD), ' letter final mem
    ChrW(&H5DE), ' letter mem
    ChrW(&H5DF), ' letter final nun
    ChrW(&H5E0), ' letter nun
    ChrW(&H5E1), ' letter samekh
    ChrW(&H5E2), ' letter ayin
    ChrW(&H5E3), ' letter final pe
    ChrW(&H5E4), ' letter pe
    ChrW(&H5E5), ' letter final tsadi
    ChrW(&H5E6), ' letter tsadi
    ChrW(&H5E7), ' letter qof
    ChrW(&H5E8), ' letter resh
    ChrW(&H5E9), ' letter shin
    ChrW(&H5EA),  ' letter tav
                  _ '             SHIN/SIN DOTS
    ChrW(&H5C1), ' Mark Shin Dot
    ChrW(&H5C2)  ' Mark Sin Dot
}
    Dim HebrewVowels() As Char = {
    ChrW(&H5B0), ' Point Sheva
    ChrW(&H5B1), ' Point Hataf Segol
    ChrW(&H5B2), ' Point Hataf Patah
    ChrW(&H5B3), ' Point Hataf Qamats
    ChrW(&H5B4), ' Point Hiriq
    ChrW(&H5B5), ' Point Tsere
    ChrW(&H5B6), ' Point Segol
    ChrW(&H5B7), ' Point Patah
    ChrW(&H5B8), ' Point Qamats
    ChrW(&H5B9), ' Point Holam
    ChrW(&H5BB), ' Point Qubuts
    ChrW(&H5BC), ' Point Dagesh or Mapiq
    ChrW(&H5BF) ' Point Rafe
}
    Dim HebrewDisj() As Char = {
   ChrW(&H591), ' Disjunctive atnach, etnahta
   ChrW(&H594), ' Disjunctive zaqef qatan
   ChrW(&H592), ' Disjunctive segol, segol, sgol, segolta
   ChrW(&H59D), ' Disjunctive geresh muqdam
   ChrW(&H593), ' Disjunctive shalshelet
   ChrW(&H595), ' Disjunctive zaqef gadol
   ChrW(&H596), ' Disjunctive tipcha, tipeha, meayla, tarcha
   ChrW(&H598), ' Disjunctive zarqa
   ChrW(&H599), ' Disjunctive pashta
   ChrW(&H59A), ' Disjunctive yetiv
   ChrW(&H59B), ' Disjunctive tevir
   ChrW(&H597), ' Disjunctive revia
   ChrW(&H5AD), ' Disjunctive dehi
   ChrW(&H59C), ' Disjunctive geresh
   ChrW(&H59E), ' Disjunctive gershayim
   ChrW(&H59F), ' Disjunctive qarney, qarney pfara, pazer gadol
   ChrW(&H5A0), ' Disjunctive telisha gedola
   ChrW(&H5A1), ' Disjunctive pazer
   ChrW(&H5AB), ' Disjunctive ole
   ChrW(&H5AE), ' Disjunctive zinor
   ChrW(&H5A5)  ' Accent merkha
}
    Dim HebrewConj() As Char = {
                                _ '             CONJUNCTIVE MARKS (chant notation?)
    ChrW(&H5A3), ' Accent munah
    ChrW(&H5AA), ' Accent yerah ben yomo/yareach ben yomo/galgal
    ChrW(&H5A8), ' Accent qadma
    ChrW(&H5A4), ' Accent mahapakh
                 _ '             3bks  - CONJUNCTIVE MARKS (Job, Psalms, Proverbs) *** error check to make sure it's not showing up in wrong books!
    ChrW(&H5AC), ' Accent iluy
                 _ '             21bks - CONJUNCTIVE MARKS (all others) *** error check to make sure it's not showing up in wrong books!
    ChrW(&H5A9), ' Accent telisha qetana
    ChrW(&H5A6), ' Accent merkha kefula
    ChrW(&H5A7), ' Accent darga
    ChrW(&H5BD), ' Point Meteg
                 _ '             RANDOM MARKS (unknown meaning)
    ChrW(&H5C4), ' Punctuation Upper Dot
    ChrW(&H5C5) ' Punctuation Lower Dot
}
    Dim HebrewTaggedPunctuation() As Char = {
                                             _ ' PUNCTUATION (already encoded as lemmas)
    ChrW(&H5BE), ' Punctuation Maqaf (*** conjunctive)
    ChrW(&H5C0), ' Punctuation Paseq (*** actually conjunctive, according to Mechon Mamre)
    ChrW(&H5C3), ' Punctuation Sof Pasuq (disjunctive) ' EMPEROR
    ChrW(&H5C6)  ' Punctuation Reversed Nun
}
    Dim CodeFormat() As Char = {
    "/"c,
    "\"c,
    " "c
}

    Dim Maqaf As Char = ChrW(&H5BE)
    Dim Paseq As Char = ChrW(&H5C0)

    Dim Cons_Vow_Conj_TagPunct_Code() As Char = HebrewConsonants & HebrewVowels & HebrewConj & HebrewTaggedPunctuation & CodeFormat
    Dim Disj_Conj_Code() As Char = HebrewDisj & HebrewConj & CodeFormat

    Function Strip(hebrew As String, toRemove As Char()) As String
        Dim result As String = ""

        For Each c As Char In hebrew

            If Not toRemove.Contains(c) Then
                result &= c
            End If

        Next

        Return result
    End Function

    Dim lastCantOle As Boolean = False
    Function ExtractCantillationHTML(HebrewText As String) As String
        Dim cant As String = Nothing

        If Strip(HebrewText, Cons_Vow_Conj_TagPunct_Code) = ChrW(&H5AB) & ChrW(&H5A5) Then ' Ole we-Yored (Ole + Merkha)
            lastCantOle = False
            Return Indent1
        ElseIf Strip(HebrewText, Cons_Vow_Conj_TagPunct_Code) = ChrW(&H5AB) Then ' Ole
            lastCantOle = True
        ElseIf lastCantOle AndAlso Strip(HebrewText, Cons_Vow_Conj_TagPunct_Code) = ChrW(&H5A5) Then ' Yored (Merkha)
            lastCantOle = False
            Return Indent1
        Else
            lastCantOle = False
        End If

        For Each c As Char In HebrewText
            If Not Cons_Vow_Conj_TagPunct_Code.Contains(c) Then
                Select Case c
                    Case ChrW(&H591) : Return Indent1 ' Accent etnahta/atnach     - EMPEROR     43423    empr    king

                    Case ChrW(&H594) : cant &= Indent2 ' Accent zaqef qatan        -  KING       51278    king    
                    Case ChrW(&H592) : Return Indent1 ' Accent segol/sgol/segolta -  KING strong  1919   king   
                    Case ChrW(&H59D) ': cant &= "gereshmuqdam" ' Accent geresh muqdam      -  KING        5960             
                    Case ChrW(&H593) ': cant &= "shalshelet"' Accent shalshelet         -  KING          92  king     not bad at all, just too few to be much use right now
                    Case ChrW(&H595) ': cant &= "zaqefgadol"' Accent zaqef gadol        -  KING        3324  king     bad king
                    Case ChrW(&H596) ': cant &= ""' Accent tipeha/meayla/tipcha/tarcha KING 74469  king    

                    Case ChrW(&H598) ': cant &= "zarqa" ' DUKE
                    Case ChrW(&H599) ': cant &= "pashta" ' DUKE
                    Case ChrW(&H59A) ': cant &= "yetiv" ' DUKE
                    Case ChrW(&H59B) ': cant &= "tevir" ' DUKE
                    Case ChrW(&H597) ': cant &= "" ' DUKE revia
                    Case ChrW(&H5AD) ': cant &= "" ' DUKE dehi/dechi

                    Case ChrW(&H59C) ': cant &= "geresh" ' COUNT
                    Case ChrW(&H59E) ': cant &= "gershayim" ' COUNT
                    Case ChrW(&H59F) ': cant &= "qarney" ' COUNT : qarney pfara/pazer gadol
                    Case ChrW(&H5A0) ': cant &= "telishagedola" ' COUNT
                    Case ChrW(&H5A1) ': cant &= "," ' COUNT pazer

                    Case ChrW(&H5AE) ': cant &= "" ' zinor
                    Case ChrW(&H5AB) ': cant &= "" ' ole

                    Case ChrW(&H5A5) ': cant &= "" ' Accent merkha

                    Case Else : Throw New ArgumentException("Exception Occurred")
                End Select
            End If
        Next

        Return cant
    End Function

    Function ExtractCantillationCSS(HebrewText As String) As String
        Dim cant As String = ""

        For Each c As Char In HebrewText
            If Not Cons_Vow_Conj_TagPunct_Code.Contains(c) Then
                Select Case c
                    Case ChrW(&H591) : cant &= "atnach" ' Accent etnahta/atnach     - EMPEROR     43423    empr    king

                    Case ChrW(&H594) : cant &= "zaqefqatan" ' Accent zaqef qatan        -  KING       51278    king    
                    Case ChrW(&H592) : cant &= "segol" ' Accent segol/sgol/segolta -  KING strong  1919   king   
                    Case ChrW(&H59D) : cant &= "gereshmuqdam" ' Accent geresh muqdam      -  KING        5960             
                    Case ChrW(&H593) ': cant &= "shalshelet"' Accent shalshelet         -  KING          92  king     not bad at all, just too few to be much use right now
                    Case ChrW(&H595) ': cant &= "zaqefgadol"' Accent zaqef gadol        -  KING        3324  king     bad king
                    Case ChrW(&H596) ': cant &= "tipcha"' Accent tipeha/meayla/tipcha/tarcha KING 74469  king    

                    Case ChrW(&H598) ': cant &= "zarqa" ' DUKE
                    Case ChrW(&H599) ': cant &= "pashta" ' DUKE
                    Case ChrW(&H59A) ': cant &= "yetiv" ' DUKE
                    Case ChrW(&H59B) ': cant &= "tevir" ' DUKE
                    Case ChrW(&H597) : cant &= "revia" ' DUKE
                    Case ChrW(&H5AD) ': cant &= "dehi" ' DUKE

                    Case ChrW(&H59C) ': cant &= "geresh" ' COUNT
                    Case ChrW(&H59E) ': cant &= "gershayim" ' COUNT
                    Case ChrW(&H59F) ': cant &= "qarney" ' COUNT : qarney pfara/pazer gadol
                    Case ChrW(&H5A0) ': cant &= "telishagedola" ' COUNT
                    Case ChrW(&H5A1) : cant &= "pazer" ' COUNT

                    Case ChrW(&H5AB) ': cant &= "ole"
                    Case ChrW(&H5AE) ': cant &= "zinor"

                    Case Else : Throw New ArgumentException("Exception Occurred")
                End Select
                cant &= " "c
            End If
        Next

        If Not CantillationVariations.TryAdd(cant, 1) Then
            CantillationVariations(cant) += 1
        End If

        If cant.Replace(" "c, "").Length = 0 Then
            Return Nothing
        Else
            Return CantillationOpen & cant.TrimEnd(" "c) & CantillationClose
        End If
    End Function

    Function ParseTaggedPunctuation(punctuation As String) As String
        Dim parsing As String = ""

        For Each c As Char In punctuation
            Select Case c
                Case ChrW(&H5BE) : parsing &= "\H9014" ' Maqaf
                Case ChrW(&H5C0) : parsing &= "\H9015" ' Paseq
                Case ChrW(&H5C3) : parsing &= "\H9016" ' Sof Pasuq
                Case ChrW(&H5E4) : parsing &= "\ \H9017" ' Pe break
                Case ChrW(&H5E1) : parsing &= "\ \H9018" ' Samekh break
                Case ChrW(&H5C6) : parsing &= "\ \H9019" ' Reversed Nun

                Case Else : Throw New ArgumentException("Exception Occurred")
            End Select
        Next

        Return parsing
    End Function

End Module