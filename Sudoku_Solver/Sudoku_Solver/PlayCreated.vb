Public Class PlayCreated
    Inherits customGrid

    Private solvedGrid(8, 8) As Integer
    Private tempPlayingGrid(24, 48) As String
    Private tempSolutionGrid(24, 48) As String

    Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
        MyBase.New(startX, startY)
    End Sub


    Public Sub ConvertCurrentGridintoGivenGrid()
        For x = 0 To 8
            For y = 0 To 8
                givenGrid(x, y) = Grid(x, y)
            Next
        Next
    End Sub

    Public Sub copyGivenGrid()
        For x = 0 To 8
            For y = 0 To 8
                givenGrid(y, x) = Grid(y, x)
            Next
        Next
    End Sub

    Public Sub ResetGrid()
        For x = 0 To 8
            For y = 0 To 8
                Grid(x, y) = 0
            Next
        Next
    End Sub

    Public Sub CopyGrid()
        For x = 0 To 8
            For y = 0 To 8
                Grid(x, y) = givenGrid(x, y)
            Next
        Next
    End Sub


    Public Function checkValid(ByVal x As Integer, ByVal y As Integer) As Boolean
        If givenGrid(y, x) <> 0 Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Sub getSolutionGrid(ByVal solution As Integer(,))
        solvedGrid = solution
    End Sub

    Public Sub keepIntialGivenValues()
        For x = 0 To 8
            For y = 0 To 8
                If Grid(x, y) <> 0 Then
                    givenGrid(x, y) = Grid(x, y)
                End If
            Next
        Next

    End Sub

    Public Sub convertGivenGridToTempGivenGrid()
        For X = 0 To 8
            For Y = 0 To 8

                If givenGrid(Y, X) <> 0 Then

                    If X < 3 Then
                        tempX = (X + 1) * 4
                    ElseIf X >= 3 And X < 6 Then
                        tempX = (X + 2) * 4
                    ElseIf X >= 6 Then
                        tempX = (X + 3) * 4
                    End If

                    If Y < 3 Then
                        tempY = (Y + 1) * 2
                    ElseIf Y >= 3 And (Y < 6) Then
                        tempY = (Y + 2) * 2
                    ElseIf Y >= 6 Then
                        tempY = (Y + 3) * 2
                    End If

                    tempGivenGrid(tempY, tempX) = givenGrid(Y, X)
                End If
            Next
        Next
    End Sub

    Public Sub convertSolutionGridToTempSolutionGrid()
        For X = 0 To 8
            For Y = 0 To 8

                If X < 3 Then
                    tempX = (X + 1) * 4
                ElseIf X >= 3 And X < 6 Then
                    tempX = (X + 2) * 4
                ElseIf X >= 6 Then
                    tempX = (X + 3) * 4
                End If

                If Y < 3 Then
                    tempY = (Y + 1) * 2
                ElseIf Y >= 3 And (Y < 6) Then
                    tempY = (Y + 2) * 2
                ElseIf Y >= 6 Then
                    tempY = (Y + 3) * 2
                End If

                tempSolutionGrid(tempY, tempX) = solvedGrid(Y, X)
            Next
        Next
    End Sub

    Public Function displayPlayingGrid() As ConsoleKeyInfo
        Console.WriteLine("*** PLAYING GRID ***")
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.White
        For y = 0 To 24
            For x = 0 To 48
                Console.ForegroundColor = ConsoleColor.White
                If tempGivenGrid(y, x) <> 0 Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(tempPlayingGrid(y, x))
                ElseIf y = 2 Or y = 4 Or y = 6 Or y = 10 Or y = 12 Or y = 14 Or y = 18 Or y = 20 Or y = 22 Then
                    If x = 4 Or x = 8 Or x = 12 Or x = 20 Or x = 24 Or x = 28 Or x = 36 Or x = 40 Or x = 44 Then
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        Console.Write(tempPlayingGrid(y, x))
                    Else
                        Console.Write(tempPlayingGrid(y, x))
                    End If
                Else
                    Console.Write(tempPlayingGrid(y, x))
                End If
            Next
            Console.WriteLine()
        Next
        Return Console.ReadKey()
    End Function


    Public Sub displayPlayingPointer(ByVal X As Integer, ByVal Y As Integer)
        tempPlayingGrid(Ylocation, Xlocation + 2) = ("<")
        tempPlayingGrid(Ylocation, Xlocation - 2) = (">")
    End Sub

    Public Sub convertAnswerGridToTempAnswerGrid()
        For X = 0 To 8
            For Y = 0 To 8


                If X < 3 Then
                    tempX = (X + 1) * 4
                ElseIf X >= 3 And X < 6 Then
                    tempX = (X + 2) * 4
                ElseIf X >= 6 Then
                    tempX = (X + 3) * 4
                End If

                If Y < 3 Then
                    tempY = (Y + 1) * 2
                ElseIf Y >= 3 And (Y < 6) Then
                    tempY = (Y + 2) * 2
                ElseIf Y >= 6 Then
                    tempY = (Y + 3) * 2
                End If

                tempPlayingGrid(tempY, tempX) = tempPlayingGrid(Y, X)
            Next
        Next
    End Sub

    Public Sub generateTempAnswerGrid()
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
                            tempPlayingGrid(y, X) = "."
                        Else
                            tempPlayingGrid(y, X) = Mid(Str((Grid(tempY, tempX))), 2)
                        End If

                    ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempPlayingGrid(y, X) = "|"
                    Else
                        tempPlayingGrid(y, X) = " "
                    End If
                ElseIf y = 0 Or y = 8 Or y = 16 Or y = 24 Then
                    If X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempPlayingGrid(y, X) = "+"
                    Else
                        tempPlayingGrid(y, X) = "-"
                    End If
                ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                    tempPlayingGrid(y, X) = "|"
                Else
                    tempPlayingGrid(y, X) = " "
                End If
            Next
        Next
    End Sub

    Public Sub displayAnswerGrid()
        Console.WriteLine()
        Console.WriteLine("*** ANSWER ***")
        Console.WriteLine()

        For y = 0 To 24
            For x = 0 To 48
                Console.ForegroundColor = ConsoleColor.White
                If y = 2 Or y = 4 Or y = 6 Or y = 10 Or y = 12 Or y = 14 Or y = 18 Or y = 20 Or y = 22 Then
                    If x = 4 Or x = 8 Or x = 12 Or x = 20 Or x = 24 Or x = 28 Or x = 36 Or x = 40 Or x = 44 Then
                        If tempSolutionGrid(y, x) = tempGivenGrid(y, x) Then
                            Console.ForegroundColor = ConsoleColor.White
                            Console.Write(tempSolutionGrid(y, x))
                        ElseIf tempSolutionGrid(y, x) = tempPlayingGrid(y, x) Then
                            Console.ForegroundColor = ConsoleColor.Green
                            Console.Write(tempSolutionGrid(y, x))
                        ElseIf tempSolutionGrid(y, x) <> tempPlayingGrid(y, x) Then
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.Write(tempSolutionGrid(y, x))
                        End If
                    Else
                        Console.Write(tempPlayingGrid(y, x))
                    End If
                Else
                    Console.Write(tempPlayingGrid(y, x))
                End If


            Next
            Console.WriteLine()
        Next

        Console.ForegroundColor = ConsoleColor.White

        Console.WriteLine()
        Console.WriteLine("Press Any Key to return to main menu...")
    End Sub

    Public Sub checkCellISCorrect(ByVal x As Integer, ByVal y As Integer, ByVal tX As Integer, ByVal tY As Integer)
        If tempPlayingGrid(y, x) = tempSolutionGrid(y, x) Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine()
            Console.WriteLine("Cell ("(tX) & "," & (tY) & ") is CORRECT")
            Console.ForegroundColor = ConsoleColor.White
        Else
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine()
            Console.WriteLine("Cell ("(tX) & "," & (tY) & ") is INCORRECT")
            Console.ForegroundColor = ConsoleColor.White
        End If
    End Sub

End Class
