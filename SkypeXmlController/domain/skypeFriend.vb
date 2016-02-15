Public Class skypeFriend
    Implements IComparer
    Implements IComparable

    Public label As String
    Public handle As String
    Public avatar As String


    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Return String.Compare(x.label, y.label)
    End Function

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(Me.label, obj.label)
    End Function
End Class
