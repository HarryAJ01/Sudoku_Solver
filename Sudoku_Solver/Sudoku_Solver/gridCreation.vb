Imports System.Timers

Public Class gridCreation
    Inherits Solver

    Private needToBeReset As Boolean = False

    Private timeTaken As Integer
    Private timer As Timer = New Timer(500)

    Private tempTemporaryVariables As New Dictionary(Of Integer, List(Of Integer))

    Private tempCreatingGrid(8, 8) As Integer
    Private solutionGrid(8, 8) As Integer

    Private difficulyToRemove As Integer
    Private randomCell As Integer
    Private randomValue As Integer

    Private x As Integer
    Private y As Integer

    Private BFpos As Integer
    Private BFcheckValid As Boolean
    Private validValue As Boolean
    Private amountofsolutions As Integer


    Public Sub New(ByRef givenGrid As Integer(,), ByVal amountToRemove As Integer)
        MyBase.New(givenGrid)
        difficulyToRemove = amountToRemove
        tempCreatingGrid = givenGrid
    End Sub

    'GRID CREATION ALGORITHM
    '
    ' CREATE DO LOOP
    '     Will place and check a random number in a random location on the grid *
    '     Will run solver algorithm, if no solutions loop again until solved *
    '     Solved Algorithm saved to solution array
    '     RND number removed
    '         Checks number of solutions
    '             If 1 solution can move on
    '             Else remove different number instead and recheck
    '     Keep Removing until amount that is needed to be removed is


    'Making and Placing Random Numbers
    Public Sub generateRNDcellAndValue()
        'Generate RND number for grid
        'Generate RND number for cell
        'Place that number temporarly in cell
        'Check if it is valid

        randomValue = Int(9 * (Rnd()) + 1)
        randomCell = Int(81 * (Rnd()) + 1) - 1

        y = randomCell \ 9
        x = randomCell Mod 9

        '     Console.WriteLine("Attempt: " & randomValue & " at the coordinates (" & x & "," & y & ")" & "   (CELL " & randomCell & ")")
    End Sub

    Public Function checkIfNumInPlace(ByVal inY As Integer, ByVal inX As Integer) As Boolean
        If tempCreatingGrid(inY, inX) <> 0 Then
            Return True
        Else
            amountOfCells += 1
            Return False
        End If
    End Function

    Public Function checkIfNumberIsValidInCell() As Boolean
        valid = True
        For i = 0 To 8
            If tempCreatingGrid(y, i) = randomValue Or tempCreatingGrid(i, x) = randomValue Then

                valid = False
            End If
        Next

        If x < 3 And y < 3 Then 'SubGrid 1
            For l = 0 To 2
                For j = 0 To 2
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x > 2 And x < 6 And y < 3 Then 'SubGrid 2
            For j = 3 To 5
                For l = 0 To 2
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x >= 6 And y < 3 Then 'SubGrid 3
            For j = 6 To 8
                For l = 0 To 2
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x < 3 And y > 2 And y < 6 Then 'Subgrid 4
            For j = 0 To 2
                For l = 3 To 5
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x > 2 And x < 6 And y > 2 And y < 6 Then 'SubGrid 5
            For j = 3 To 5
                For l = 3 To 5
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x >= 6 And y > 2 And y < 6 Then 'Subgird 6
            For j = 6 To 8
                For l = 3 To 5
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x < 3 And y >= 6 Then 'SubGrid 7
            For j = 0 To 2
                For l = 6 To 8
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x > 2 And x < 6 And y >= 6 Then 'SubGrid 8
            For j = 3 To 5
                For l = 6 To 8
                    If tempCreatingGrid(l, j) = randomValue Then
                        valid = False
                    End If
                Next
            Next

        ElseIf x >= 6 And y >= 6 Then 'SubGrid 9
            For l = 6 To 8
                For j = 6 To 8
                    If tempCreatingGrid(j, l) = randomValue Then
                        valid = False
                    End If
                Next
            Next
        End If
        Return valid
    End Function

    Public Sub placeValueInCell()
        tempCreatingGrid(y, x) = randomValue
        '      Console.WriteLine("Placed: " & randomValue & " at the coordinates (" & x & "," & y & ")" & "   (CELL " & randomCell & ")")
    End Sub


    'Dictionaries Operations
    Public Sub setUpTempDictionaires()
        For i = 0 To 80
            tempTemporaryVariables.Add(i, New List(Of Integer))
        Next
    End Sub

    Public Sub resetTempDictionaries()
        For i = 0 To 80
            tempTemporaryVariables(i).Clear()
        Next
    End Sub


    'Filling in the Creating Grid
    Public Sub findTemporaryVariablesInTempSolving(ByVal n As Integer, ByVal inX As Integer, ByVal inY As Integer, ByVal pos As Integer)
        If n = 10 Then
        Else
            valid = True
            For i = 0 To 8
                If tempCreatingGrid(inY, i) = n Or tempCreatingGrid(i, inX) = n Then
                    valid = False
                End If
            Next

            If inX < 3 And inY < 3 Then 'SubGrid 1
                For l = 0 To 2
                    For j = 0 To 2
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX > 2 And inX < 6 And inY < 3 Then 'SubGrid 2
                For j = 3 To 5
                    For l = 0 To 2
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX >= 6 And inY < 3 Then 'SubGrid 3
                For j = 6 To 8
                    For l = 0 To 2
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX < 3 And inY > 2 And inY < 6 Then 'Subgrid 4
                For j = 0 To 2
                    For l = 3 To 5
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX > 2 And inX < 6 And inY > 2 And inY < 6 Then 'SubGrid 5
                For j = 3 To 5
                    For l = 3 To 5
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX >= 6 And inY > 2 And inY < 6 Then 'Subgird 6
                For j = 6 To 8
                    For l = 3 To 5
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX < 3 And inY >= 6 Then 'SubGrid 7
                For j = 0 To 2
                    For l = 6 To 8
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX > 2 And inX < 6 And inY >= 6 Then 'SubGrid 8
                For j = 3 To 5
                    For l = 6 To 8
                        If tempCreatingGrid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf inX >= 6 And inY >= 6 Then 'SubGrid 9
                For l = 6 To 8
                    For j = 6 To 8
                        If tempCreatingGrid(j, l) = n Then
                            valid = False
                        End If
                    Next
                Next
            End If

            If valid = False Then
                findTemporaryVariablesInTempSolving(n + 1, inX, inY, pos)
            Else
                tempTemporaryVariables(pos).AddRange(New Integer() {n})
                findTemporaryVariablesInTempSolving(n + 1, inX, inY, pos)
            End If
        End If
    End Sub

    Public Sub FindLengthOfTempList(ByVal pos As Integer, ByVal Inx As Integer, ByVal Iny As Integer)
        lengthOfList = 0
        For Each element In tempTemporaryVariables(pos)
            lengthOfList += 1
        Next

        If lengthOfList = 1 Then
            tempCreatingGrid(Iny, Inx) = tempTemporaryVariables(pos).Item(0)
            '        Console.WriteLine(tempCreatingGrid(Iny, Inx) & " added at location : (", Inx & "," & Iny & ")")
        End If
    End Sub


    'Timer
    Public Sub stopWatch()
        AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf TimerElapsed)
        timer.Start()
    End Sub

    Sub TimerElapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        If timeTaken = 5 Then
            timeTaken = 1
            timer.Stop()
            needToBeReset = True
        End If
        timeTaken += 1
    End Sub

    Public Sub resetEverything()
        For i = 0 To 8
            For l = 0 To 8
                tempCreatingGrid(i, l) = 0
            Next
        Next

        For i = 0 To 80
            tempTemporaryVariables(i).Clear()
        Next

        x = 0
        y = 0
        amountOfCells = 0
        randomValue = 0
        randomCell = 0
        needToBeReset = False

    End Sub


    'Display Infomation
    Public Sub DisplayExtraInfomation(ByVal pos As Integer, ByVal x As Integer, ByVal y As Integer)
        Console.Write("(" & x & "," & y & ") : ")
        posInList = 0
        For Each element In tempTemporaryVariables(pos)
            Console.Write(tempTemporaryVariables(pos).Item(posInList) & ", ")
            posInList += 1
        Next
        Console.WriteLine()
    End Sub

    Public Sub drawTempCreating()
        Console.WriteLine()
        For i = 0 To 8
            For l = 0 To 8
                Console.Write(tempCreatingGrid(i, l) & "|")
            Next
            Console.WriteLine()
        Next
        timer.Stop()
    End Sub


    'Removing to play
    Public Sub copyToSolutionGrid()
        For i = 0 To 8
            For l = 0 To 8
                solutionGrid(i, l) = tempCreatingGrid(i, l)
            Next
        Next
    End Sub

    Public Sub tempRemoving()
        For i = 0 To difficulyToRemove - 1
            randomCell = Int(81 * (Rnd()) + 1) - 1
            y = randomCell \ 9
            x = randomCell Mod 9
            tempCreatingGrid(y, x) = 0
        Next
        '   drawTempCreating()
    End Sub

    Public Sub remove()
        randomCell = Int(81 * (Rnd()) + 1) - 1
        y = randomCell \ 9
        x = randomCell Mod 9
        tempCreatingGrid(y, x) = 0
    End Sub

    Public Function checkAmountOfSolutions() As Boolean 'ATTEMPT 1
        BFpos = -1
        hasFinished = False
        Dim listOfN(80) As Integer
        Dim FullyAttempted(80) As Boolean
        Do
            'Increments position in list
            BFpos += 1

            'Gets Y and X value
            y = BFpos \ 9
            x = BFpos Mod 9

            If tempCreatingGrid(y, x) <> 0 Then
                BFcheckValid = True
                Do
                    x += 1
                    If x = 9 Then
                        x = 0
                        y += 1
                        If y = 9 Then
                            hasFinished = True
                            Exit Do
                        End If
                    End If
                    If tempCreatingGrid(y, x) <> 0 Then
                        BFpos += 1
                        BFcheckValid = False
                    End If
                Loop Until BFcheckValid = True
            Else
                n = 1

                Do
                    validValue = False
                    valid = True
                    For i = 0 To 8
                        If tempCreatingGrid(y, i) = n Or tempCreatingGrid(i, x) = n Then
                            valid = False
                        End If
                    Next

                    If x < 3 And y < 3 Then 'SubGrid 1
                        For l = 0 To 2
                            For j = 0 To 2
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x > 2 And x < 6 And y < 3 Then 'SubGrid 2
                        For j = 3 To 5
                            For l = 0 To 2
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x >= 6 And y < 3 Then 'SubGrid 3
                        For j = 6 To 8
                            For l = 0 To 2
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x < 3 And y > 2 And y < 6 Then 'Subgrid 4
                        For j = 0 To 2
                            For l = 3 To 5
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x > 2 And x < 6 And y > 2 And y < 6 Then 'SubGrid 5
                        For j = 3 To 5
                            For l = 3 To 5
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x >= 6 And y > 2 And y < 6 Then 'Subgird 6
                        For j = 6 To 8
                            For l = 3 To 5
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x < 3 And y >= 6 Then 'SubGrid 7
                        For j = 0 To 2
                            For l = 6 To 8
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x > 2 And x < 6 And y >= 6 Then 'SubGrid 8
                        For j = 3 To 5
                            For l = 6 To 8
                                If tempCreatingGrid(l, j) = n Then
                                    valid = False
                                End If
                            Next
                        Next

                    ElseIf x >= 6 And y >= 6 Then 'SubGrid 9
                        For l = 6 To 8
                            For j = 6 To 8
                                If tempCreatingGrid(j, l) = n Then
                                    valid = False
                                End If
                            Next
                        Next
                    End If

                    If valid = True Then
                        listOfN(BFpos) = n
                        validValue = True
                    Else
                        n += 1
                    End If
                Loop Until n = 9 Or validValue = True
            End If
        Loop Until hasFinished = True

        If amountofsolutions > 1 Then
            Return False
        Else
            Return True
        End If

    End Function

    'Getters and senders
    Public Function checkIfFinishedCreating() As Boolean
        valid = True
        For i = 0 To 8
            For l = 0 To 8
                If tempCreatingGrid(i, l) = 0 Then
                    valid = False
                End If
            Next
        Next
        Return valid
    End Function

    Public Function getY() As Integer
        Return y
    End Function

    Public Function getX() As Integer
        Return y
    End Function

    Public Function getNeedToBeReset() As Boolean
        Return needToBeReset
    End Function

    Public Function getAmountToRemove() As Integer
        Return difficulyToRemove
    End Function

    Public Function sendSolutionGrid() As Integer(,)
        Return solutionGrid
    End Function

    Public Function sendTempGrid() As Integer(,)
        Return tempCreatingGrid
    End Function

    Public Function getRandomCell() As Integer
        Return randomCell
    End Function


End Class
