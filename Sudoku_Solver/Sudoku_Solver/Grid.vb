Public MustInherit Class Grid
    Implements IPointer, IUserInput

    Protected Grid(8, 8) As Integer 'Working Grid 
    Protected givenGrid(8, 8) As Integer 'The intial Values Given
    Protected tempGrid(24, 48) As String
    Protected tempGivenGrid(24, 48) As Integer

    Protected TrueX As Integer 'Value for normal Grid
    Protected TrueY As Integer 'Value for normal Grid

    Protected Xlocation As Integer 'Adapted Value for tempGrid
    Protected Ylocation As Integer 'Adapted Value for tempGrid

    Protected tempX As Integer 'For Grid Creation
    Protected tempY As Integer 'For Grid Creation

    Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
        Xlocation = startX
        Ylocation = startY

        TrueX = 0
        TrueY = 0
    End Sub

    Public Overridable Function displayGrid() As ConsoleKeyInfo
        Console.ForegroundColor = ConsoleColor.White
        For I = 0 To 24
            For L = 0 To 48
                Console.Write(tempGrid(I, L))
            Next
            Console.WriteLine()
        Next
        Return Console.ReadKey
    End Function


    Public Sub displayPointer(ByVal X As Integer, ByVal Y As Integer) Implements IPointer.displayPointer
        tempGrid(Y, X + 2) = ("<")
        tempGrid(Y, X - 2) = (">")
    End Sub


    Public Sub CreateGrid()
        For y = 0 To 24
            For X = 0 To 48
                If y = 2 Or y = 4 Or y = 6 Or y = 10 Or y = 12 Or y = 14 Or y = 18 Or y = 20 Or y = 22 Then
                    If X = 4 Or X = 8 Or X = 12 Or X = 20 Or X = 24 Or X = 28 Or X = 36 Or X = 40 Or X = 44 Then

                        tempX = X \ 4
                        If tempX <= 3 Then
                            tempX -= 1
                        ElseIf tempX > 3 And tempX <= 7 Then
                            tempX -= 2
                        Else
                            tempX -= 3
                        End If

                        tempY = y \ 2
                        If tempY <= 3 Then
                            tempY -= 1
                        ElseIf tempY > 3 And tempY <= 7 Then
                            tempY -= 2
                        Else
                            tempY -= 3
                        End If

                        If Grid(tempY, tempX) = 0 Then
                            tempGrid(y, X) = "."
                        Else
                            tempGrid(y, X) = Mid(Str((Grid(tempY, tempX))), 2)
                        End If

                    ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempGrid(y, X) = "|"
                    Else
                        tempGrid(y, X) = " "
                    End If
                ElseIf y = 0 Or y = 8 Or y = 16 Or y = 24 Then
                    If X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempGrid(y, X) = "+"
                    Else
                        tempGrid(y, X) = "-"
                    End If
                ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                    tempGrid(y, X) = "|"
                Else
                    tempGrid(y, X) = " "
                End If
            Next
        Next
    End Sub


    Public Function interpretUserInput(ByVal userInput As ConsoleKeyInfo) As String Implements IUserInput.interpretUserInput
        Select Case userInput.Key
            Case ConsoleKey.UpArrow
                Return "UpArrow"

            Case ConsoleKey.DownArrow
                Return "DownArrow"

            Case ConsoleKey.RightArrow
                Return "RightArrow"

            Case ConsoleKey.LeftArrow
                Return "LeftArrow"

            Case ConsoleKey.Enter
                Return "Enter"

            Case ConsoleKey.Backspace
                Return "Backspace"

            Case ConsoleKey.Escape
                Return "Escape"

            Case ConsoleKey.D1
                Return "D1"

            Case ConsoleKey.D2
                Return "D2"

            Case ConsoleKey.D3
                Return "D3"

            Case ConsoleKey.D4
                Return "D4"

            Case ConsoleKey.D5
                Return "D5"

            Case ConsoleKey.D6
                Return "D6"

            Case ConsoleKey.D7
                Return "D7"

            Case ConsoleKey.D8
                Return "D8"

            Case ConsoleKey.D9
                Return "D9"

            Case ConsoleKey.H
                Return "H"

        End Select
        Return "Invalid Input"
    End Function


    Public Sub movePointer(ByVal MovementType As String) Implements IPointer.movePointer

        If MovementType = "UpArrow" Then
            TrueY -= 1
            If TrueY = -1 Then
                TrueY = 8
            End If

            If TrueY < 3 Then
                Ylocation = (TrueY + 1) * 2
            ElseIf TrueY >= 3 And (TrueY < 6) Then
                Ylocation = (TrueY + 2) * 2
            ElseIf TrueY >= 6 Then
                Ylocation = (TrueY + 3) * 2
            End If

        ElseIf MovementType = "DownArrow" Then
            TrueY += 1
            If TrueY = 9 Then
                TrueY = 0
            End If

            If TrueY < 3 Then
                Ylocation = (TrueY + 1) * 2
            ElseIf TrueY >= 3 And (TrueY < 6) Then
                Ylocation = (TrueY + 2) * 2
            ElseIf TrueY >= 6 Then
                Ylocation = (TrueY + 3) * 2
            End If

        ElseIf MovementType = "RightArrow" Then
            TrueX += 1
            If TrueX = 9 Then
                TrueX = 0
            End If

            If TrueX < 3 Then
                Xlocation = (TrueX + 1) * 4
            ElseIf TrueX >= 3 And TrueX < 6 Then
                Xlocation = (TrueX + 2) * 4
            ElseIf TrueX >= 6 Then
                Xlocation = (TrueX + 3) * 4
            End If

        ElseIf MovementType = "LeftArrow" Then
            TrueX -= 1
            If TrueX = -1 Then
                TrueX = 8
            End If

            If TrueX < 3 Then
                Xlocation = (TrueX + 1) * 4
            ElseIf TrueX >= 3 And TrueX < 6 Then
                Xlocation = (TrueX + 2) * 4
            ElseIf TrueX >= 6 Then
                Xlocation = (TrueX + 3) * 4
            End If
        End If
    End Sub


    Public Sub addToGrid(ByVal numToAdd As String, ByVal X As Integer, ByVal Y As Integer)
        Select Case numToAdd
            Case "D1"
                Grid(Y, X) = 1
            Case "D2"
                Grid(Y, X) = 2
            Case "D3"
                Grid(Y, X) = 3
            Case "D4"
                Grid(Y, X) = 4
            Case "D5"
                Grid(Y, X) = 5
            Case "D6"
                Grid(Y, X) = 6
            Case "D7"
                Grid(Y, X) = 7
            Case "D8"
                Grid(Y, X) = 8
            Case "D9"
                Grid(Y, X) = 9
            Case "Backspace"
                Grid(Y, X) = 0
        End Select
    End Sub


    Public Function getXTrue() As Integer Implements IPointer.getXlocation
        Return TrueX
    End Function

    Public Function getYTrue() As Integer Implements IPointer.getYlocation
        Return TrueY
    End Function

    Public Function GetXlocation() As Integer
        Return Xlocation
    End Function

    Public Function getYLocation() As Integer
        Return Ylocation
    End Function

End Class
