Imports System.IO
Public Class learningSudoku
    Inherits Grid

    Private fileName As String = "trainingFile.Bin"
    Private gridFileFormat As String
    Private trainingFileIntegerFortmat(80) As Integer
    Private trainingFileAnswerIntegerFormat(80) As Integer
    Private positionInArray As Integer
    Private trainingAnswerGrid(8, 8) As Integer
    Private tempTrainingAnswerGrid(24, 48) As String

    Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
        MyBase.New(startX, startY)
        positionInArray = 0
    End Sub


    Public Sub createFile()
        Using writer As BinaryWriter = New BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate))
            writer.Write("12 6 8   5842397 1 6 14    37  6158 691 8 2744587 2 13 3  2415 2 985 436   3 6 92123678945584239761967145328372461589691583274458792613836924157219857436745316892")
        End Using
    End Sub


    Public Sub readFile()
        Using reader As BinaryReader = New BinaryReader(File.OpenRead(fileName))
            gridFileFormat = reader.ReadString
        End Using

        For i = 0 To 80
            If gridFileFormat(i) <> " " Then
                trainingFileIntegerFortmat(i) = Integer.Parse(gridFileFormat(i))
            Else
                trainingFileIntegerFortmat(i) = 0
            End If
        Next

        For l = 0 To 80
            trainingFileAnswerIntegerFormat(l) = Integer.Parse(gridFileFormat(l + 81))
        Next

        positionInArray = 0

        For x = 0 To 8
            For y = 0 To 8
                Grid(x, y) = trainingFileIntegerFortmat(positionInArray)
                positionInArray += 1
            Next
        Next

        positionInArray = 0

        For x = 0 To 8
            For y = 0 To 8
                trainingAnswerGrid(x, y) = trainingFileAnswerIntegerFormat(positionInArray)
                positionInArray += 1
            Next
        Next

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

                tempTrainingAnswerGrid(tempY, tempX) = trainingAnswerGrid(Y, X)
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

                        tempTrainingAnswerGrid(y, X) = trainingAnswerGrid(tempY, tempX)

                    ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempTrainingAnswerGrid(y, X) = "|"
                    Else
                        tempTrainingAnswerGrid(y, X) = " "
                    End If
                ElseIf y = 0 Or y = 8 Or y = 16 Or y = 24 Then
                    If X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                        tempTrainingAnswerGrid(y, X) = "+"
                    Else
                        tempTrainingAnswerGrid(y, X) = "-"
                    End If
                ElseIf X = 0 Or X = 16 Or X = 32 Or X = 48 Then
                    tempTrainingAnswerGrid(y, X) = "|"
                Else
                    tempTrainingAnswerGrid(y, X) = " "
                End If
            Next
        Next
    End Sub

    Public Sub checkCellISCorrect(ByVal x As Integer, ByVal y As Integer, ByVal tX As Integer, ByVal tY As Integer)
        Console.WriteLine()
        If Grid(tY, tX) = trainingAnswerGrid(tY, tX) Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.Write("Cell (")
            Console.WriteLine((tX) & "," & (tY) & ") is CORRECT")
            Console.ForegroundColor = ConsoleColor.White
        Else
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write("Cell (")
            Console.WriteLine((tX) & "," & (tY) & ") is INCORRECT")
            Console.ForegroundColor = ConsoleColor.White
        End If
        Console.ReadKey()
    End Sub

    Public Sub displayAnswerGrid()
        Console.WriteLine()
        Console.WriteLine("***ANSWER***")
        Console.WriteLine()

        For y = 0 To 24
            For x = 0 To 48
                If y = 2 Or y = 4 Or y = 6 Or y = 10 Or y = 12 Or y = 14 Or y = 18 Or y = 20 Or y = 22 Then
                    If x = 4 Or x = 8 Or x = 12 Or x = 20 Or x = 24 Or x = 28 Or x = 36 Or x = 40 Or x = 44 Then

                        Console.ForegroundColor = ConsoleColor.White

                        tempX = x \ 4
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

                        If givenGrid(tempY, tempX) <> 0 Then
                            Console.ForegroundColor = ConsoleColor.White
                            Console.Write(tempTrainingAnswerGrid(y, x))

                        ElseIf Grid(tempY, tempX) <> trainingAnswerGrid(tempY, tempX) Then
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.Write(tempTrainingAnswerGrid(y, x))

                        ElseIf Grid(tempY, tempX) = trainingAnswerGrid(tempY, tempX) Then
                            Console.ForegroundColor = ConsoleColor.Green
                            Console.Write(tempTrainingAnswerGrid(y, x))
                        End If
                    Else
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write(tempTrainingAnswerGrid(y, x))
                    End If
                Else
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(tempTrainingAnswerGrid(y, x))
                End If


            Next
            Console.WriteLine()
        Next

        Console.ForegroundColor = ConsoleColor.White

        Console.WriteLine()
        Console.WriteLine("Press Any Key to return to main menu...")
    End Sub


    Public Overrides Function displayGrid() As ConsoleKeyInfo
        Console.ForegroundColor = ConsoleColor.White
        For y = 0 To 24
            For x = 0 To 48
                Console.ForegroundColor = ConsoleColor.White
                If tempGivenGrid(y, x) <> 0 Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(tempGivenGrid(y, x))

                Else
                    Select Case tempGrid(y, x)
                        Case 1
                            Console.ForegroundColor = ConsoleColor.DarkBlue
                            Console.Write(tempGrid(y, x))
                        Case 2
                            Console.ForegroundColor = ConsoleColor.DarkCyan
                            Console.Write(tempGrid(y, x))
                        Case 3
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.Write(tempGrid(y, x))
                        Case 4
                            Console.ForegroundColor = ConsoleColor.DarkGreen
                            Console.Write(tempGrid(y, x))
                        Case 5
                            Console.ForegroundColor = ConsoleColor.DarkMagenta
                            Console.Write(tempGrid(y, x))
                        Case 6
                            Console.ForegroundColor = ConsoleColor.DarkRed
                            Console.Write(tempGrid(y, x))
                        Case 7
                            Console.ForegroundColor = ConsoleColor.DarkYellow
                            Console.Write(tempGrid(y, x))
                        Case 8
                            Console.ForegroundColor = ConsoleColor.Yellow
                            Console.Write(tempGrid(y, x))
                        Case 9
                            Console.ForegroundColor = ConsoleColor.Cyan
                            Console.Write(tempGrid(y, x))
                        Case Else
                            Console.Write(tempGrid(y, x))
                    End Select

                End If

            Next
            Select Case y
                Case 12
                    Console.Write("      ***CONTROLS***")
                Case 14
                    Console.Write("     -Use the Arrow Keys to move the pointer")
                Case 16
                    Console.Write("     -Use the Number Keys to add the respective number to the grid")
                Case 18
                    Console.Write("     -Use the Backspace Key to remove a non-given number from the grid")
                Case 20
                    Console.Write("     -Use the Enter Key when finshed to compare with solution")
                Case 22
                    Console.Write("     -Use the Escape Key to return to the menu")
                Case 0
                    Console.Write("     ***Rules***")
                Case 2
                    Console.Write("     -Use the numbers 1-9")
                Case 4
                    Console.Write("     -Each subgrid cannot contain more that one copy of a number")
                Case 6
                    Console.Write("     -Each row cannot contain more that one copy of a number ")
                Case 8
                    Console.Write("     -Each column cannot contian more than one copy of a number")
            End Select

            Console.WriteLine()
        Next
        Return Console.ReadKey
    End Function
End Class
