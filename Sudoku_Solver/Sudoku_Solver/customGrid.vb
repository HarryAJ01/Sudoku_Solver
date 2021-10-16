Public MustInherit Class customGrid
    Inherits Grid

    Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
        MyBase.New(startX, startY)
    End Sub

    Public Function getGrid() As Integer(,)
        Return Grid
    End Function

    Public Sub getSolvedGrid(ByVal givenGrid As Integer(,))
        Grid = givenGrid
    End Sub

End Class
