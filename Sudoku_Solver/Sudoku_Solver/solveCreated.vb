Public Class SolveCreated
    Inherits customGrid

    Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
        MyBase.New(startX, startY)
    End Sub

    Public Sub displayAnswer()

        Console.WriteLine("*** ANSWER ***")
        Console.WriteLine()

        For I = 0 To 24
            For L = 0 To 48
                Console.Write(tempGrid(I, L))
            Next
            Console.WriteLine()
        Next

        Console.WriteLine()
        Console.WriteLine("Press Any Key to return to main menu...")
        Console.ReadKey()
    End Sub
End Class
