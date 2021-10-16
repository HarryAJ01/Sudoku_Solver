Public Class Solver

    Private temporaryVariables As New Dictionary(Of Integer, List(Of Integer))
    Private BruteForceDict As New Dictionary(Of Integer, List(Of Integer))

    Private ListOfMaximumLengthInBruteForce() As Integer
    Private ListOfPosInBruteForce() As Integer

    Protected lengthOfList As Integer
    Protected amountOfCells As Integer

    Protected valid As Boolean
    Protected valid2 As Boolean
    Protected noMoreOnes As Boolean
    Protected hasFinished As Boolean
    Protected FinsishedValidGrid As Boolean
    Protected invalidGrid As Boolean

    Private PosInDict As Integer
    Protected PosInList As Integer
    Private PosInTempVarList As Integer
    Private posInBruteForce As Integer
    Protected PosInPosList As Integer

    Private Grid(8, 8) As Integer
    Protected solvingGrid(8, 8) As Integer

    Protected n As Integer


    Public Sub New(ByRef givenGrid As Integer(,))

        Grid = givenGrid

        invalidGrid = False
        amountOfCells = -1
        lengthOfList = 0
        PosInDict = 0
        PosInList = 0
        PosInTempVarList = 0
        posInBruteForce = 0
        PosInPosList = 0
        hasFinished = False
        FinsishedValidGrid = False
        valid2 = True
        valid = True

    End Sub
    'set Grid = to grid sent in

    Public Sub drawGrid()
        For i = 0 To 8
            For l = 0 To 8
                Console.Write("|" & Grid(i, l))
            Next
            Console.WriteLine()
        Next
        Console.WriteLine()
    End Sub

    Public Sub draw()
        For i = 0 To 8
            For l = 0 To 8
                Console.Write("|" & solvingGrid(i, l))
            Next
            Console.WriteLine()
        Next
        Console.WriteLine()
    End Sub    'Draws the sudoku Grid


    '*******************************
    'MANIPULATING GRIDS AND SOLVERS
    '******************************
    Public Overridable Sub setUpSolving()
        For i = 0 To 8
            For l = 0 To 8
                solvingGrid(i, l) = Grid(i, l)
            Next
        Next
    End Sub      'Copy the Grid array to solving grid array

    Public Sub CopyOverGrid()
        For y = 0 To 8
            For x = 0 To 8
                Grid(x, y) = solvingGrid(x, y)
            Next
        Next
    End Sub      'Copy the solving grid array to the grid array

    Public Sub start()
        noMoreOnes = True
    End Sub


    '*********************
    'DICTIONARY OPERATIONS
    '*********************
    Public Sub SetUpDictionaries()
        PosInDict = 0
        For i = 0 To 80
            temporaryVariables.Add(i, New List(Of Integer))
            BruteForceDict.Add(i, New List(Of Integer))
        Next
    End Sub    'Creates new dictionary with keys for each cell

    Public Sub resetDictionaries()
        PosInList = 0

        Try
            For i = 0 To 80
                temporaryVariables(i).Clear()
            Next
        Catch ex As KeyNotFoundException
            hasFinished = True
            invalidGrid = True
            sendNotValid()
        End Try

        For i = 0 To amountOfCells
            ListOfMaximumLengthInBruteForce(i) = 0
        Next

        amountOfCells = -1
    End Sub    'Clears all of the values in the lists for each key


    '************************
    'INFORMED APPROACH SOLVER
    '************************
    Public Overridable Function CheckForExisitingNumbers(ByVal X As Integer, ByVal y As Integer) As Boolean
        If Grid(y, X) <> 0 Then
            Return True
        Else
            amountOfCells += 1
            Return False
        End If
    End Function     'Checks if there is a number in a specific cell

    Public Overridable Function checkIfFinsished() As Boolean
        valid = True
        For y = 0 To 8
            For x = 0 To 8
                If Grid(y, x) = 0 Then
                    valid = False
                End If
            Next
        Next
        Return valid
    End Function

    Public Overridable Function CheckIfValid() As Boolean
        PosInPosList = 0
        valid = True

        For y = 0 To 8
            For x = 0 To 8
                n = Grid(y, x)

                valid = True
                For i = 0 To 8
                    If Grid(y, i) = n Or Grid(i, x) = n Then
                        valid = False
                    End If
                Next

                If x < 3 And y < 3 Then 'SubGrid 1
                    For l = 0 To 2
                        For j = 0 To 2
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y < 3 Then 'SubGrid 2
                    For j = 3 To 5
                        For l = 0 To 2
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y < 3 Then 'SubGrid 3
                    For j = 6 To 8
                        For l = 0 To 2
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x < 3 And y > 2 And y < 6 Then 'Subgrid 4
                    For j = 0 To 2
                        For l = 3 To 5
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y > 2 And y < 6 Then 'SubGrid 5
                    For j = 3 To 5
                        For l = 3 To 5
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y > 2 And y < 6 Then 'Subgird 6
                    For j = 6 To 8
                        For l = 3 To 5
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x < 3 And y >= 6 Then 'SubGrid 7
                    For j = 0 To 2
                        For l = 6 To 8
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y >= 6 Then 'SubGrid 8
                    For j = 3 To 5
                        For l = 6 To 8
                            If Grid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y >= 6 Then 'SubGrid 9
                    For l = 6 To 8
                        For j = 6 To 8
                            If Grid(j, l) = n Then
                                valid = False
                            End If
                        Next
                    Next
                End If

                If Grid(y, x) = 0 Then
                    valid = False
                End If

            Next
        Next

        Return valid

    End Function     'Checks if the grid is full

    Public Sub findTemporaryVariables(ByVal n As Integer, ByVal x As Integer, ByVal y As Integer, ByVal PosInDict As Integer, ByVal posInBruteForce As Integer)
        If n = 10 Then
        Else
            valid = True
            For i = 0 To 8
                If Grid(y, i) = n Or Grid(i, x) = n Then
                    valid = False
                End If
            Next

            If x < 3 And y < 3 Then 'SubGrid 1
                For l = 0 To 2
                    For j = 0 To 2
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x > 2 And x < 6 And y < 3 Then 'SubGrid 2
                For j = 3 To 5
                    For l = 0 To 2
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x >= 6 And y < 3 Then 'SubGrid 3
                For j = 6 To 8
                    For l = 0 To 2
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x < 3 And y > 2 And y < 6 Then 'Subgrid 4
                For j = 0 To 2
                    For l = 3 To 5
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x > 2 And x < 6 And y > 2 And y < 6 Then 'SubGrid 5
                For j = 3 To 5
                    For l = 3 To 5
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x >= 6 And y > 2 And y < 6 Then 'Subgird 6
                For j = 6 To 8
                    For l = 3 To 5
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x < 3 And y >= 6 Then 'SubGrid 7
                For j = 0 To 2
                    For l = 6 To 8
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x > 2 And x < 6 And y >= 6 Then 'SubGrid 8
                For j = 3 To 5
                    For l = 6 To 8
                        If Grid(l, j) = n Then
                            valid = False
                        End If
                    Next
                Next

            ElseIf x >= 6 And y >= 6 Then 'SubGrid 9
                For l = 6 To 8
                    For j = 6 To 8
                        If Grid(j, l) = n Then
                            valid = False
                        End If
                    Next
                Next
            End If

            If valid = False Then
                findTemporaryVariables(n + 1, x, y, PosInDict, posInBruteForce)
            Else
                temporaryVariables(PosInDict).AddRange(New Integer() {n})
                Try
                    BruteForceDict(posInBruteForce).AddRange(New Integer() {n})
                Catch ex As KeyNotFoundException
                    Console.Clear()
                    Console.WriteLine("No number added")
                    Console.WriteLine("Press Any Key to return to main menu...")
                    Console.ReadKey()
                    hasFinished = True
                    invalidGrid = True
                    sendNotValid()
                    Exit Sub
                End Try

                findTemporaryVariables(n + 1, x, y, PosInDict, posInBruteForce)
            End If
        End If
    End Sub       'Finds all the possible values for each cell

    Public Sub FindLengthOfList(ByVal pos As Integer, ByVal x As Integer, ByVal y As Integer)
        lengthOfList = 0
        Try
            For Each element In temporaryVariables(pos)
                lengthOfList += 1
            Next
        Catch ex As KeyNotFoundException
            hasFinished = True
            invalidGrid = True
            sendNotValid()
            Exit Sub
        End Try

        ReDim Preserve ListOfMaximumLengthInBruteForce(amountOfCells)
        ListOfMaximumLengthInBruteForce(amountOfCells) = lengthOfList

        If lengthOfList = 1 Or lengthOfList = 0 Then
            noMoreOnes = False
            solvingGrid(y, x) = temporaryVariables(pos).Item(0)
        End If

    End Sub      'Checks how many values are present for each cell and adds it to solving grid if only one value

    Public Function getNoMoreOnes() As Boolean
        Return noMoreOnes
    End Function


    '********************
    'BRUTE FORCE APPROACH
    '********************
    Public Sub SetUPBruteForce()
        posInBruteForce = 0

        ReDim ListOfPosInBruteForce(amountOfCells)

        For i = 0 To amountOfCells
            ListOfPosInBruteForce(i) = 0
        Next

        For i = 0 To amountOfCells
            ListOfMaximumLengthInBruteForce(i) = ListOfMaximumLengthInBruteForce(i) - 1
        Next
    End Sub

    Public Overridable Sub BruteForceSolver(ByVal posInBruteForce As Integer, ByVal currentPosInList As Integer, ByVal x As Integer, ByVal y As Integer)

        Try
            If hasFinished <> True Then

                '***************************************
                'CHECKING NUMBER IS VALID IN SUDOKU GRID
                '***************************************

                Try
                    n = temporaryVariables((9 * y) + x).Item(currentPosInList)
                Catch ex As ArgumentOutOfRangeException
                    Console.Clear()
                    Console.WriteLine("No Solution to the grid")
                    Console.WriteLine("Press Any Key to return to main menu...")
                    Console.ReadKey()
                    hasFinished = True
                    invalidGrid = True
                    sendNotValid()
                    Exit Sub
                End Try

                valid = True
                For i = 0 To 8
                    If solvingGrid(y, i) = n Or solvingGrid(i, x) = n Then
                        valid = False
                    End If
                Next

                If x < 3 And y < 3 Then 'SubGrid 1
                    For l = 0 To 2
                        For j = 0 To 2
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y < 3 Then 'SubGrid 2
                    For j = 3 To 5
                        For l = 0 To 2
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y < 3 Then 'SubGrid 3
                    For j = 6 To 8
                        For l = 0 To 2
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x < 3 And y > 2 And y < 6 Then 'Subgrid 4
                    For j = 0 To 2
                        For l = 3 To 5
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y > 2 And y < 6 Then 'SubGrid 5
                    For j = 3 To 5
                        For l = 3 To 5
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y > 2 And y < 6 Then 'Subgird 6
                    For j = 6 To 8
                        For l = 3 To 5
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x < 3 And y >= 6 Then 'SubGrid 7
                    For j = 0 To 2
                        For l = 6 To 8
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x > 2 And x < 6 And y >= 6 Then 'SubGrid 8
                    For j = 3 To 5
                        For l = 6 To 8
                            If solvingGrid(l, j) = n Then
                                valid = False
                            End If
                        Next
                    Next

                ElseIf x >= 6 And y >= 6 Then 'SubGrid 9
                    For l = 6 To 8
                        For j = 6 To 8
                            If solvingGrid(j, l) = n Then
                                valid = False
                            End If
                        Next
                    Next
                End If

                If valid = True Then

                    '***************************
                    'SUCCESSFUL NUMBER RECURSION
                    '***************************
                    solvingGrid(y, x) = n
                    ListOfPosInBruteForce(posInBruteForce) = currentPosInList

                    Do
                        x += 1
                        If x = 9 Then
                            x = 0
                            y += 1
                        End If

                        If y = 9 And x = 0 Then 'FINISHED
                            sendFinishedGrid()
                            hasFinished = True
                            FinsishedValidGrid = True
                            sendValidGrid()
                            Exit Sub
                        Else
                            If Grid(y, x) = 0 Then
                                BruteForceSolver(posInBruteForce + 1, 0, x, y)
                            End If
                        End If
                    Loop Until hasFinished = True


                Else
                    '*****************************
                    'UNSUCCESSFUL NUMBER RECURSION
                    '*****************************


                    currentPosInList += 1

                    If ListOfMaximumLengthInBruteForce(posInBruteForce) < currentPosInList Then

                        '************
                        'BACKTRACKING
                        '************

                        'Resets the cell to clear
                        ListOfPosInBruteForce(posInBruteForce) = 0
                        solvingGrid(y, x) = 0


                        'Goes back by one in the BruteForcePositionValue as given values won't effect it
                        posInBruteForce -= 1
                        currentPosInList = ListOfPosInBruteForce(posInBruteForce) + 1



                        valid2 = True 'True means it is going to go through loop again

                        Do
                            valid2 = True
                            x -= 1

                            If x = -1 Then 'CHECKING IF IT IS OUT OF BOUNDS
                                x = 8
                                y -= 1
                            End If


                            If Grid(y, x) <> 0 Then 'Checks if it is a given Number
                                valid2 = False
                            Else
                                If ListOfMaximumLengthInBruteForce(posInBruteForce) < currentPosInList Then 'Only backtracking once instead of multiple times
                                    solvingGrid(y, x) = 0
                                    valid2 = False
                                    posInBruteForce -= 1
                                    currentPosInList = ListOfPosInBruteForce(posInBruteForce) + 1
                                End If
                            End If


                        Loop Until valid2 = True
                        BruteForceSolver(posInBruteForce, currentPosInList, x, y)

                    Else 'IF backtracking not necessary
                        BruteForceSolver(posInBruteForce, currentPosInList, x, y)
                    End If
                End If


            End If
        Catch ex As StackOverflowException
            Console.Clear()
            Console.WriteLine("Impossible grid")
            Console.WriteLine("Press Any Key to return to main menu...")
            Console.ReadKey()
            hasFinished = True
            invalidGrid = True
            sendNotValid()
            Exit Sub
        End Try


    End Sub

    Public Function sendFinishedGrid() As Integer(,)
        Return solvingGrid
    End Function

    Public Function sendValidGrid() As Boolean
        Return FinsishedValidGrid
    End Function

    Public Function sendNotValid() As Boolean
        Return invalidGrid
    End Function

    '**********
    'INFOMATION
    '**********

    Public Sub DisplayInfomation(ByVal pos As Integer, ByVal x As Integer, ByVal y As Integer)
        Console.Write("(" & x & "," & y & ") : ")
        PosInList = 0

        Try
            For Each element In temporaryVariables(pos)
                Console.Write(temporaryVariables(pos).Item(PosInList) & ", ")
                PosInList += 1
            Next
            Console.WriteLine()
        Catch ex As KeyNotFoundException
            hasFinished = True
            invalidGrid = True
            sendNotValid()
        End Try

    End Sub     'TEMPORARY Displays necessary infomation to see how program is performing

    Public Sub displayMaxLength()
        For i = 0 To amountOfCells
            Console.WriteLine(i & " has a max length of : " & ListOfMaximumLengthInBruteForce(i))
        Next
    End Sub

End Class