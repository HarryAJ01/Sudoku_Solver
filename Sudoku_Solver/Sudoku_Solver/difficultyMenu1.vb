Public Class difficultyMenu1
    Inherits Menu

    Public Sub New()
        Menu = {"> Easy", "  Medium", "  Hard", "  Back"}
        Y = 0
    End Sub

    Public Overrides Function DisplayMenu(Y As Integer) As ConsoleKeyInfo

        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine("*** Difficulty Settings *** ")
        Console.WriteLine()

        For i = 0 To 3
            If i = Y Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(Menu(i))
            Else
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(Menu(i))
            End If
        Next


        Return Console.ReadKey
    End Function

    Public Overrides Sub movePointer(MovementType As String)
        prevY = Y
        If MovementType = "UpArrow" Then
            Y -= 1
            If Y = -1 Then
                Y = 3
            End If

        ElseIf MovementType = "DownArrow" Then
            Y = Y + 1
            If Y = 4 Then
                Y = 0
            End If

        End If
    End Sub

    Public Sub resetMenu()
        Menu = {"> Play Random Sudoku", "  Play Created Sudoku", "  Solve Created Sudoku ", "  Learn Sudoku", "  Exit"}
        Y = 0
    End Sub

End Class
