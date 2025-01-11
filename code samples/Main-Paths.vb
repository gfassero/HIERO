Partial Module Main

    Dim OutputFile As System.IO.StreamWriter
    Dim ProjectPath As String = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("\Psalms Translator\bin"))
    Dim RepositoryRoot As String = "C:\Users\Graham\HIERO"
    Dim books_downloadedPath As String = ProjectPath & "\STEPBible\STEPBible\"
    Dim SaveToFileInsteadOfConsole As Boolean = False

    Dim OT_Overlay As String = "\Hebrew OT Overlay.txt"

    '    Dim DEBUG_STEP_HebrewFiles() As String = {"development\sample.bin"}

    Dim ExistingCompilation As String = books_downloadedPath & "compiledSTEP-20240526.bin"
    Dim comp As String = FileIO.FileSystem.ReadAllText(ExistingCompilation)

    Dim Lexicons() As String = {"multilex.xml", "multilex - aramaic - regenerated.xml"}
    Dim Helpers As String = "multilex - helpers.xml"

    '    Dim StrongsMapping As New Dictionary(Of String, String) ' dStrongs ("disambiguated" by Tyndale House, in the STEP text), eStrongs ("extended" by OSHB, basis of my lexicon)
    Dim dStrongsSplitChars() As Char = {"\"c, "/"c}

End Module