Module module1

    Sub learnSudoku(ByRef move As String)

        Dim myLearningGrid As New learningSudoku(4, 2)
        myLearningGrid.createFile()
        myLearningGrid.readFile()
        myLearningGrid.keepIntialGivenValues()
        myLearningGrid.convertGivenGridToTempGivenGrid()

        Do
            Console.Clear()
            myLearningGrid.CreateGrid()
            myLearningGrid.displayPointer(myLearningGrid.GetXlocation, myLearningGrid.getYLocation)
            move = myLearningGrid.interpretUserInput(myLearningGrid.displayGrid())

            If move = "Enter" Then
                myLearningGrid.convertAnswerGridToTempAnswerGrid()
                myLearningGrid.generateTempAnswerGrid()
                myLearningGrid.displayAnswerGrid()
                Console.ReadKey()
                Exit Do


            ElseIf move = "Escape" Then
                Exit Do

            ElseIf move = "BackSpace" Then
                myLearningGrid.addToGrid(0, myLearningGrid.getXTrue, myLearningGrid.getYTrue)

            ElseIf move = "LeftArrow" Then
                myLearningGrid.movePointer(move)

            ElseIf move = "RightArrow" Then
                myLearningGrid.movePointer(move)

            ElseIf move = "UpArrow" Then
                myLearningGrid.movePointer(move)

            ElseIf move = "DownArrow" Then
                myLearningGrid.movePointer(move)

            ElseIf move = "H" Then
                myLearningGrid.checkCellISCorrect(myLearningGrid.GetXlocation, myLearningGrid.getYLocation, myLearningGrid.getXTrue, myLearningGrid.getYTrue)

            Else
                myLearningGrid.addToGrid(move, myLearningGrid.getXTrue, myLearningGrid.getYTrue)
            End If
        Loop


    End Sub

    Sub solveCreated(ByRef move As String)
        Dim mySolveCreated As New SolveCreated(4, 2)
        Do
            Console.Clear()
            Console.WriteLine("*** CREATING GRID* **")
            Console.WriteLine()
            mySolveCreated.CreateGrid()
            mySolveCreated.displayPointer(mySolveCreated.GetXlocation, mySolveCreated.getYLocation)
            move = mySolveCreated.interpretUserInput(mySolveCreated.displayGrid())

            If move = "Enter" Then
                Console.Clear()


                Dim mySolver As New Solver(mySolveCreated.getGrid)

                mySolver.SetUpDictionaries()
                mySolver.setUpSolving()

                Dim pos As Integer = -1
                Dim posInBruteForce As Integer = 0

                Do

                    If mySolver.checkIfFinsished = True Then
                        Exit Do
                    End If

                    mySolver.start()
                    'mySolver.drawGrid()
                    posInBruteForce = 0
                    pos = -1
                    For Y = 0 To 8
                        For x = 0 To 8
                            pos += 1
                            If mySolver.CheckForExisitingNumbers(x, Y) = False Then
                                '   mySolver.DisplayInfomation(pos, x, Y)
                                posInBruteForce += 1
                                mySolver.findTemporaryVariables(1, x, Y, pos, posInBruteForce)
                                mySolver.FindLengthOfList(pos, x, Y)
                            End If

                        Next
                    Next

                    If mySolver.getNoMoreOnes = True Then
                        Try
                            mySolver.SetUPBruteForce()
                            mySolver.BruteForceSolver(0, 0, 0, 0)
                        Catch ex As StackOverflowException
                            Console.WriteLine("Grid impossible")
                            Console.ReadKey()
                            Exit Do
                            Exit Sub
                        End Try

                    End If
                    mySolver.CopyOverGrid()
                    mySolver.resetDictionaries()
                Loop Until mySolver.checkIfFinsished = True Or mySolver.sendNotValid = True Or mySolver.sendValidGrid = True

                If mySolver.sendNotValid = True Then
                    Exit Do
                Else
                    Console.Clear()
                    mySolveCreated.getSolvedGrid(mySolver.sendFinishedGrid)
                    mySolveCreated.CreateGrid()
                    mySolveCreated.displayAnswer()
                    Exit Do
                End If


            ElseIf move = "Escape" Then
                Exit Do

            ElseIf move = "BackSpace" Then
                mySolveCreated.addToGrid(0, mySolveCreated.getXTrue, mySolveCreated.getYTrue)

            ElseIf move = "LeftArrow" Then
                mySolveCreated.movePointer(move)

            ElseIf move = "RightArrow" Then
                mySolveCreated.movePointer(move)

            ElseIf move = "UpArrow" Then
                mySolveCreated.movePointer(move)

            ElseIf move = "DownArrow" Then
                mySolveCreated.movePointer(move)

            Else
                mySolveCreated.addToGrid(move, mySolveCreated.getXTrue, mySolveCreated.getYTrue)
            End If
        Loop
    End Sub

    Sub playCreated(ByRef move As String)
        Dim myPlayCreated As New PlayCreated(4, 2)
        Dim finished As Boolean = False

        Do
            If finished = True Then
                Exit Do
            Else
                Console.Clear()
                Console.WriteLine("*** CREATING GRID ***")
                Console.WriteLine()
                myPlayCreated.CreateGrid()
                myPlayCreated.displayPointer(myPlayCreated.GetXlocation, myPlayCreated.getYLocation)
                move = myPlayCreated.interpretUserInput(myPlayCreated.displayGrid())

                If move = "Enter" Then 'Plays Sudoku
                    myPlayCreated.ConvertCurrentGridintoGivenGrid()

                    Console.Clear()

                    Dim mySolver As New Solver(myPlayCreated.getGrid)

                    mySolver.SetUpDictionaries()
                    mySolver.setUpSolving()

                    Dim pos As Integer = -1
                    Dim posInBruteForce As Integer = 0

                    Do
                        mySolver.start()

                        posInBruteForce = 0
                        pos = -1
                        For Y = 0 To 8
                            For x = 0 To 8
                                pos += 1
                                If mySolver.CheckForExisitingNumbers(x, Y) = False Then
                                    posInBruteForce += 1
                                    mySolver.findTemporaryVariables(1, x, Y, pos, posInBruteForce)
                                    mySolver.FindLengthOfList(pos, x, Y)
                                End If

                            Next
                        Next

                        If mySolver.getNoMoreOnes = True Then
                            mySolver.SetUPBruteForce()
                            mySolver.BruteForceSolver(0, 0, 0, 0)
                        End If


                        mySolver.CopyOverGrid()
                        mySolver.resetDictionaries()
                    Loop Until mySolver.checkIfFinsished = True Or mySolver.sendNotValid = True Or mySolver.sendValidGrid = True

                    If mySolver.sendNotValid = True Then
                        Exit Do
                    Else 'PLAY SUDOKU
                        myPlayCreated.getSolutionGrid(mySolver.sendFinishedGrid)
                        myPlayCreated.convertSolutionGridToTempSolutionGrid()
                        myPlayCreated.ResetGrid()
                        myPlayCreated.CopyGrid()

                        myPlayCreated.keepIntialGivenValues()
                        myPlayCreated.convertGivenGridToTempGivenGrid()

                        Do
                            If move = "Escape" Then
                                Exit Do
                                Exit Do
                            Else
                                Console.Clear()
                                myPlayCreated.generateTempAnswerGrid()
                                myPlayCreated.displayPlayingPointer(myPlayCreated.getXTrue, myPlayCreated.getYTrue)
                                move = myPlayCreated.interpretUserInput(myPlayCreated.displayPlayingGrid())

                                If move = "Enter" Then
                                    myPlayCreated.convertAnswerGridToTempAnswerGrid()
                                    myPlayCreated.generateTempAnswerGrid()
                                    myPlayCreated.displayAnswerGrid()
                                    Console.ReadKey()
                                    mainMenu()

                                ElseIf move = "Escape" Then
                                    Exit Do
                                    Exit Do

                                ElseIf move = "BackSpace" Then
                                    If myPlayCreated.checkValid(myPlayCreated.GetXlocation, myPlayCreated.getYLocation) = False Then
                                        myPlayCreated.addToGrid(0, myPlayCreated.getXTrue, myPlayCreated.getYTrue)
                                    End If

                                ElseIf move = "LeftArrow" Then
                                    myPlayCreated.movePointer(move)

                                ElseIf move = "RightArrow" Then
                                    myPlayCreated.movePointer(move)

                                ElseIf move = "UpArrow" Then
                                    myPlayCreated.movePointer(move)

                                ElseIf move = "DownArrow" Then
                                    myPlayCreated.movePointer(move)

                                ElseIf move = "H" Then
                                    myPlayCreated.checkCellISCorrect(myPlayCreated.GetXlocation, myPlayCreated.getYLocation, myPlayCreated.getXTrue, myPlayCreated.getYTrue)

                                Else
                                    If myPlayCreated.checkValid(myPlayCreated.getXTrue, myPlayCreated.getYTrue) = True Then
                                        myPlayCreated.addToGrid(move, myPlayCreated.getXTrue, myPlayCreated.getYTrue)
                                    End If
                                End If
                            End If
                        Loop
                    End If



                ElseIf move = "Escape" Then
                    Exit Do

                ElseIf move = "BackSpace" Then
                    myPlayCreated.addToGrid(0, myPlayCreated.getXTrue, myPlayCreated.getYTrue)

                ElseIf move = "LeftArrow" Then
                    myPlayCreated.movePointer(move)

                ElseIf move = "RightArrow" Then
                    myPlayCreated.movePointer(move)

                ElseIf move = "UpArrow" Then
                    myPlayCreated.movePointer(move)

                ElseIf move = "DownArrow" Then
                    myPlayCreated.movePointer(move)

                Else
                    myPlayCreated.addToGrid(move, myPlayCreated.getXTrue, myPlayCreated.getYTrue)
                End If
            End If


        Loop

    End Sub

    Sub randomSudoku(ByRef move As String, ByRef y As Integer)
        y = 0
        Dim myDifficultySetting = New difficultyMenu1
        Dim RNDnum As Integer

        Dim tempGrid(8, 8) As Integer
        Dim posInBruteForce As Integer = 0
        Dim pos As Integer = -1

        Do
            Console.Clear()
            move = myDifficultySetting.interpretUserInput(myDifficultySetting.DisplayMenu(myDifficultySetting.getYlocation))
            If move = "Enter" Then
                y = myDifficultySetting.getYlocation
                Select Case y
                    Case 0 'EASY
                        myDifficultySetting.resetMenu()

                        Console.Clear()
                        Console.WriteLine("*** CREATING EASY GRID ***")
                        Console.WriteLine()

                        Randomize()
                        RNDnum = Int(4 * Rnd() + 1) + 36

                        Dim myRNDgrid As New gridCreation(tempGrid, RNDnum)
                        myRNDgrid.setUpTempDictionaires()

                        myRNDgrid.stopWatch()

                        Do
                            If myRNDgrid.getNeedToBeReset = True Then
                                myRNDgrid.resetEverything()
                                myRNDgrid.stopWatch()
                                Console.Write(".")
                            End If

                            myRNDgrid.generateRNDcellAndValue()
                            If myRNDgrid.checkIfNumInPlace(myRNDgrid.getY, myRNDgrid.getX) = False And myRNDgrid.checkIfNumberIsValidInCell = True Then
                                myRNDgrid.placeValueInCell()
                            End If

                            pos = -1
                            For y = 0 To 8
                                For x = 0 To 8
                                    pos += 1
                                    If myRNDgrid.checkIfNumInPlace(y, x) = False Then
                                        myRNDgrid.findTemporaryVariablesInTempSolving(1, x, y, pos)
                                        myRNDgrid.FindLengthOfTempList(pos, x, y)
                                    End If
                                Next
                            Next
                            myRNDgrid.resetTempDictionaries()
                        Loop Until myRNDgrid.checkIfFinishedCreating = True

                        myRNDgrid.copyToSolutionGrid()

                        For n = 0 To myRNDgrid.getAmountToRemove - 1
                            myRNDgrid.remove()
                        Next

                        Dim myRNDplayGrid = New PlayRandomGrid(4, 2)

                        myRNDplayGrid.getSolutionGrid(myRNDgrid.sendSolutionGrid)
                        myRNDplayGrid.getPlayingGrid(myRNDgrid.sendTempGrid)
                        myRNDplayGrid.convertRNDgivenGrid()
                        myRNDplayGrid.playingGridTotempGrid()
                        myRNDplayGrid.convertSolutionGridToTempGrid()

                        Do
                            If move = "Escape" Then
                                Exit Do
                                Exit Do
                            Else
                                Console.Clear()
                                myRNDplayGrid.createTempPlayingGrid()
                                myRNDplayGrid.displayPlayingPointer()
                                move = myRNDplayGrid.interpretUserInput(myRNDplayGrid.displayPlayingGrid("EASY"))

                                If move = "Enter" Then
                                    myRNDplayGrid.displayAnswerGrid()
                                    Console.ReadKey()
                                    mainMenu()
                                    Exit Do

                                ElseIf move = "Escape" Then
                                    Exit Do
                                    Exit Do

                                ElseIf move = "BackSpace" Then
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.GetXlocation, myRNDplayGrid.getYLocation) = False Then
                                        myRNDplayGrid.addToGrid(0, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If

                                ElseIf move = "LeftArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "RightArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "UpArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "DownArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                Else
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue) = True Then
                                        myRNDplayGrid.addToPlayingGrid(move, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If
                                End If
                            End If
                        Loop
                        Exit Do

                    Case 1 'MEDIUM

                        Console.Clear()
                        Console.WriteLine("*** CREATING MEDIUM GRID ***")
                        Console.WriteLine()

                        myDifficultySetting.resetMenu()


                        Randomize()
                        RNDnum = Int(3 * Rnd() + 1) + 45

                        Dim myRNDgrid As New gridCreation(tempGrid, RNDnum)
                        myRNDgrid.setUpTempDictionaires()

                        myRNDgrid.stopWatch()

                        Do
                            If myRNDgrid.getNeedToBeReset = True Then
                                myRNDgrid.resetEverything()
                                myRNDgrid.stopWatch()
                                Console.Write(".")
                            End If

                            myRNDgrid.generateRNDcellAndValue()
                            If myRNDgrid.checkIfNumInPlace(myRNDgrid.getY, myRNDgrid.getX) = False And myRNDgrid.checkIfNumberIsValidInCell = True Then
                                myRNDgrid.placeValueInCell()
                            End If


                            pos = -1
                            For y = 0 To 8
                                For x = 0 To 8
                                    pos += 1
                                    If myRNDgrid.checkIfNumInPlace(y, x) = False Then
                                        myRNDgrid.findTemporaryVariablesInTempSolving(1, x, y, pos)
                                        myRNDgrid.FindLengthOfTempList(pos, x, y)
                                    End If
                                Next
                            Next
                            myRNDgrid.resetTempDictionaries()
                        Loop Until myRNDgrid.checkIfFinishedCreating = True


                        myRNDgrid.copyToSolutionGrid()

                        For n = 0 To myRNDgrid.getAmountToRemove - 1
                            myRNDgrid.remove()
                            myRNDgrid.findTemporaryVariablesInTempSolving(1, myRNDgrid.getX, myRNDgrid.getY, myRNDgrid.getRandomCell)

                        Next

                        Dim myRNDplayGrid = New PlayRandomGrid(4, 2)

                        myRNDplayGrid.getSolutionGrid(myRNDgrid.sendSolutionGrid)
                        myRNDplayGrid.getPlayingGrid(myRNDgrid.sendTempGrid)
                        myRNDplayGrid.convertRNDgivenGrid()
                        myRNDplayGrid.playingGridTotempGrid()
                        myRNDplayGrid.convertSolutionGridToTempGrid()


                        Do
                            If move = "Escape" Then
                                myRNDplayGrid.displayAnswerGrid()
                                Console.ReadKey()
                                mainMenu()
                                Exit Do
                            Else
                                Console.Clear()
                                myRNDplayGrid.createTempPlayingGrid()
                                myRNDplayGrid.displayPlayingPointer()
                                move = myRNDplayGrid.interpretUserInput(myRNDplayGrid.displayPlayingGrid("MEDIUM"))

                                If move = "Enter" Then
                                    myRNDplayGrid.displayAnswerGrid()
                                    Console.ReadKey()
                                    Exit Do
                                    mainMenu()

                                ElseIf move = "Escape" Then
                                    Exit Do
                                    Exit Do

                                ElseIf move = "BackSpace" Then
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.GetXlocation, myRNDplayGrid.getYLocation) = False Then
                                        myRNDplayGrid.addToGrid(0, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If

                                ElseIf move = "LeftArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "RightArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "UpArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "DownArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                Else
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue) = True Then
                                        myRNDplayGrid.addToPlayingGrid(move, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If
                                End If
                            End If
                        Loop
                        Exit Do

                    Case 2 'HARD
                        myDifficultySetting.resetMenu()
                        Console.Clear()

                        Console.Clear()
                        Console.WriteLine("*** Hard Grid ***")
                        Console.WriteLine()

                        Randomize()
                        RNDnum = Int(3 * Rnd() + 1) + 51


                        Randomize()
                        RNDnum = Int(3 * Rnd() + 1) + 45

                        Dim myRNDgrid As New gridCreation(tempGrid, RNDnum)
                        myRNDgrid.setUpTempDictionaires()

                        myRNDgrid.stopWatch()

                        Do
                            If myRNDgrid.getNeedToBeReset = True Then
                                myRNDgrid.resetEverything()
                                myRNDgrid.stopWatch()
                                Console.Write(".")
                            End If

                            myRNDgrid.generateRNDcellAndValue()
                            If myRNDgrid.checkIfNumInPlace(myRNDgrid.getY, myRNDgrid.getX) = False And myRNDgrid.checkIfNumberIsValidInCell = True Then
                                myRNDgrid.placeValueInCell()
                            End If


                            pos = -1
                            For y = 0 To 8
                                For x = 0 To 8
                                    pos += 1
                                    If myRNDgrid.checkIfNumInPlace(y, x) = False Then
                                        myRNDgrid.findTemporaryVariablesInTempSolving(1, x, y, pos)
                                        myRNDgrid.FindLengthOfTempList(pos, x, y)
                                    End If
                                Next
                            Next
                            myRNDgrid.resetTempDictionaries()
                        Loop Until myRNDgrid.checkIfFinishedCreating = True

                        myRNDgrid.copyToSolutionGrid()
                        myRNDgrid.tempRemoving()

                        Dim myRNDplayGrid = New PlayRandomGrid(4, 2)

                        myRNDplayGrid.getSolutionGrid(myRNDgrid.sendSolutionGrid)
                        myRNDplayGrid.getPlayingGrid(myRNDgrid.sendTempGrid)
                        myRNDplayGrid.convertRNDgivenGrid()
                        myRNDplayGrid.playingGridTotempGrid()
                        myRNDplayGrid.convertSolutionGridToTempGrid()

                        Do
                            If move = "Escape" Then
                                Exit Do
                                Exit Do
                            Else
                                Console.Clear()
                                myRNDplayGrid.createTempPlayingGrid()
                                myRNDplayGrid.displayPlayingPointer()
                                move = myRNDplayGrid.interpretUserInput(myRNDplayGrid.displayPlayingGrid("HARD"))

                                If move = "Enter" Then
                                    myRNDplayGrid.displayAnswerGrid()
                                    Console.ReadKey()
                                    mainMenu()

                                ElseIf move = "Escape" Then
                                    Exit Do
                                    Exit Do

                                ElseIf move = "BackSpace" Then
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.GetXlocation, myRNDplayGrid.getYLocation) = False Then
                                        myRNDplayGrid.addToGrid(0, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If

                                ElseIf move = "LeftArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "RightArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "UpArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                ElseIf move = "DownArrow" Then
                                    myRNDplayGrid.movePointer(move)

                                Else
                                    If myRNDplayGrid.checkIfGiven(myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue) = True Then
                                        myRNDplayGrid.addToPlayingGrid(move, myRNDplayGrid.getXTrue, myRNDplayGrid.getYTrue)
                                    End If
                                End If
                            End If
                        Loop
                        Exit Do

                    Case 3 'BACK
                        Exit Do

                End Select
            Else
                myDifficultySetting.movePointer(move)
                myDifficultySetting.displayPointer(myDifficultySetting.getXlocation, myDifficultySetting.getYlocation)
                Console.Clear()
            End If
        Loop

    End Sub

    Sub mainMenu()

        Dim myMenu = New Menu
        Dim Y As Integer
        Dim move As String

        Do
            Console.Clear()
            move = myMenu.interpretUserInput(myMenu.DisplayMenu(myMenu.getYlocation))
            If move = "Enter" Then
                Y = myMenu.getYlocation
                Select Case Y
                    Case 0
                        randomSudoku(move, Y)
                        Exit Do
                    Case 1
                        playCreated(move)
                    Case 2
                        solveCreated(move)
                    Case 3
                        learnSudoku(move)
                    Case 4
                        Exit Do
                        Exit Do
                End Select
            Else
                myMenu.movePointer(move)
                myMenu.displayPointer(myMenu.getXlocation, myMenu.getYlocation)
                Console.Clear()
            End If
        Loop
    End Sub

    Sub Main()
        mainMenu()
    End Sub

End Module