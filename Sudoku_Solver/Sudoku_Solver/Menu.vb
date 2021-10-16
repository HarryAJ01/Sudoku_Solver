Public Class Menu
    Implements IPointer, IUserInput

    Protected Menu(4) As String
    Protected X As Integer
    Protected Y As Integer
    Protected prevY As Integer

    Public Sub New()
        Menu = {"> Play Random Sudoku", "  Play Created Sudoku", "  Solve Created Sudoku ", "  Learn Sudoku", "  Exit"}
        X = 0
        Y = 0
        prevY = 0
    End Sub

    Public Overridable Function DisplayMenu(ByVal Y As Integer) As ConsoleKeyInfo
        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine("*** Console Sudoku Program *** ")
        Console.WriteLine()

        For i = 0 To 4
            If i = Y Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(Menu(i))
            Else
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(Menu(i))
            End If
        Next

        Return Console.ReadKey()
    End Function

    Public Function interpretUserInput(ByVal userInput As ConsoleKeyInfo) As String Implements IUserInput.interpretUserInput
        Select Case userInput.Key
            Case ConsoleKey.UpArrow
                Return "UpArrow"

            Case ConsoleKey.DownArrow
                Return "DownArrow"

            Case ConsoleKey.Enter
                Return "Enter"

        End Select
        Return "Invalid Input"
    End Function


    Public Overridable Sub movePointer(ByVal MovementType As String) Implements IPointer.movePointer
        prevY = Y
        If MovementType = "UpArrow" Then
            Y -= 1
            If Y = -1 Then
                Y = 4
            End If

        ElseIf MovementType = "DownArrow" Then
            Y = Y + 1
            If Y = 5 Then
                Y = 0
            End If

        End If
    End Sub


    Sub displayPointer(ByVal X As Integer, ByVal Y As Integer) Implements IPointer.displayPointer
        If prevY = 4 Then
            Menu(4) = "  " + Menu(4).Remove(0, 2)
            Menu(Y) = ">" + Menu(Y).Remove(0, 1)
        ElseIf prevY = 0 Then
            Menu(0) = "  " + Menu(0).Remove(0, 2)
            Menu(Y) = ">" + Menu(Y).Remove(0, 1)
        Else
            Menu(prevY) = "  " + Menu(prevY).Remove(0, 2)
            Menu(Y) = ">" + Menu(Y).Remove(0, 1)
        End If
    End Sub

    Public Function getXlocation() As Integer Implements IPointer.getXlocation
        Return X
    End Function


    Public Function getYlocation() As Integer Implements IPointer.getYlocation
        Return Y
    End Function
End Class
