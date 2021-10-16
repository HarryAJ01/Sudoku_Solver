Public Interface IPointer
    Sub movePointer(MovementType As String)

    Sub displayPointer(ByVal X As Integer, ByVal Y As Integer)
    Function getXlocation() As Integer
    Function getYlocation() As Integer

End Interface
