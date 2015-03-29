Imports System.Net, System.Speech, System.String, System.Net.Sockets, System.Net.Security, System.Security.Cryptography
Public Class [CORE]
#Region "CORE"
    ''' <summary>
    ''' Allows the DEFCON client to send the user a text message.
    ''' </summary>
    ''' <param name="CountryCode">EG: +44</param>
    ''' <param name="ISO">EG: GB</param>
    ''' <param name="NUM">EG: 01234567890</param>
    ''' <param name="FROM">EG: DEFCON</param>
    ''' <param name="TEXT">EG: Hello World</param>
    ''' <returns></returns>
    ''' <remarks>Dubito</remarks>
    Public Shared Function SMS_SEND(ByVal CountryCode As String, ByVal ISO As String, ByVal NUM As String, ByVal FROM As String, ByRef TEXT As String)
        SLINGSHOT("http://smsspoofer.tk/api.php?toCODE=" + CountryCode + "&toISO=" + ISO + "&to=" + NUM.ToString + "&from=" + FROM + "&text=" + TEXT + "&send=Send", "WEBCLIENT")
    End Function
#End Region
#Region "NETWORKING"
    Public Shared Function SLINGSHOT(ByVal URL As String, ByVal METHOD As String)
        ' TEMPORARY CODE | The code below is purely as a test until this function is correctly re-written.

        If (METHOD = "WEBCLIENT") = True Then
            Dim webClient As New System.Net.WebClient
            Dim result As String = webClient.DownloadString(URL)
        End If

        ' END OF TEMPORARY CODE
    End Function
    Public Shared Function PORTCHECK(ByVal minPort As Integer, ByVal maxPort As Integer) As String

        'CODE CURRENTLY BROKEN

        Dim intPortStart As Integer = minPort
        Dim intPortEnd As Integer = maxPort
        Dim lngLoop As Long
        Dim closed As New SortedList(Of String, String)
        Dim open As New SortedList(Of String, String)
        Dim x As Integer = 0
        Dim cx As Integer = 0
        While (lngLoop = intPortEnd) = False
            For lngLoop = intPortStart To intPortEnd
                Dim myTcpClient As New TcpClient()
                Try
                    myTcpClient.Connect("127.0.0.1", lngLoop)
                    open.Add(lngLoop.ToString, lngLoop.ToString)
                    x = x + 1
                    myTcpClient.Close()
                Catch ex As SocketException
                    closed.Add(lngLoop.ToString, lngLoop.ToString)
                    cx = cx + 1
                End Try
            Next
            Dim theReturn As String = ""
            For y = 0 To open.Count - 1
                theReturn = theReturn & open(y).ToString
            Next
            Return theReturn

            ' Return ("Open ports: " + open.ToArray.ToString + "| Closed ports: " + closed.ToArray.ToString)
        End While
    End Function

#End Region
#Region "LOCAL"

#End Region
#Region "DEFENSIVE"
    ''' <summary>
    ''' LOCKDOWN allows the DEFCON client to enable and disable the current user's internet connection.
    ''' This is useful when attempting to inhibit the transmission of malicious traffic whilst destroying
    ''' the malware.
    ''' </summary>
    ''' <param name="LOCK">TRUE = Disable Internet Connection | FALSE = Enable Internet Connection</param>
    ''' <returns></returns>
    ''' <remarks>Dubito</remarks>
    Public Shared Function LOCKDOWN(ByVal LOCK As Boolean)
        If LOCK = True Then
            Shell("ipconfig /release")
        ElseIf LOCK = False Then
            Shell("ipconfig /renew")
        End If
    End Function
#End Region
#Region "OFFENSIVE"

#End Region
#Region "SPEECH"
    ''' <summary>
    ''' Allows the DEFCON client to "Talk" to the user via speech synthesis.
    ''' </summary>
    ''' <param name="TEXT">The text you want the client to "say"</param>
    ''' <returns></returns>
    ''' <remarks>Dubito</remarks>
    Public Shared Function SPEAKTOCLIENT(ByRef TEXT As String)
        Dim x = CreateObject("sapi.spvoice")
        x.Speak(TEXT)
    End Function
#End Region
#Region "MISC"
    Public Shared Function QUOTE_GEN()
        Dim rand As Random = New Random()
        Return Choose(rand.Next(1, 11 + 1), "You better step away from the mirror long enough to see the damage that your leaving behind you. ~Anon", "Storms don't last forever. ~Anon", "There's a whole universe inside you; Don't give up because one star has lost it's shine. ~Anon", "Impossible is but a product of the times ~ DEFCON Team", "Don't let LOYALTY become SLAVERY ~Anon", "Who controls the past controls the future. Who controls the present controls the past. ~ George Orwell", "Cows eat grass to make milk. Therefore, cheese equals grass. ~ Koen Oosterhuis", "Freedom is the right to tell people what they do not want to hear. ~ George Orwell", "All animals are created equal, but some animals are created more equal then others. ~ George Orwell", "Man is atleast himself when he talks in his own person, but give that man a mask and he will tell you the truth. ~ Oscar Wilde", "We sleep safe in our beds because rough men stand ready in the night to visit violence on those who would do us harm. ~ Oscar Wilde")
    End Function
#End Region
#Region "ENCRYPTION"
    Public Shared Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
        End Try
    End Function
#End Region
#Region "COMMUNICATION"
    Public Shared Function VENTURE()

    End Function
#End Region
End Class
