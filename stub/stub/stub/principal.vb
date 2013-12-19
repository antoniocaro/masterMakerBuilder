Imports System.IO

Public Class principal
    Const FileSplit As String = "|split|"
    'string necesarios extraidos que contienen toda la informacion
    Dim stub, exeString, argString, frontString, xpadderString, xpadderArgsString, tipoJuego, opt() As String
    'variables que contendra el proceso
    Dim game, xpadder As New Process
    Dim tiempo As Integer
    'Obtiene las variables modificadas con el creador

    Private Sub seteaVariables()
        On Error Resume Next
        FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        stub = Space(LOF(1))
        FileGet(1, stub)
        FileClose(1)
        opt = Split(stub, FileSplit)
        exeString = opt(1)
        argString = opt(2)
        frontString = opt(3)
        xpadderString = opt(4)
        xpadderArgsString = opt(5)
        tipoJuego = opt(6)
        tiempo = CInt(obtenerTiempo())

    End Sub
    'cargar el juego
    Private Sub cargarjuego()

        'cargamos el juego checando primeroq ue tipo de juego es
        'Tiempo o Moneda
        If tipoJuego = "Tiempo" Then
            If tiempo <= 5 Then
                tiempoInsuficiente.Show()
            Else
                'cargamos el juego con timer
                game.StartInfo.FileName = exeString
                game.StartInfo.Arguments = argString
                game.StartInfo.WorkingDirectory = Path.GetDirectoryName(exeString)
                game.StartInfo.CreateNoWindow = True
                'game.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                game.StartInfo.UseShellExecute = True
                game.Start()
            End If

        ElseIf tipoJuego = "Moneda" Then
            'cargamos solo el emulador
            game.StartInfo.FileName = exeString
            game.StartInfo.Arguments = argString

            game.StartInfo.WorkingDirectory = Path.GetDirectoryName(exeString)
            game.StartInfo.CreateNoWindow = True
            'game.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            game.StartInfo.UseShellExecute = True
            game.Start()
        End If
    End Sub
    'cerramos el juego
    Private Sub cerrarJuego()

    End Sub

    'cerramos frontend
    Private Sub cerrarFront()

    End Sub

    'abrimos frontend
    Private Sub abrirFront()

    End Sub
    'guarda Tiempo en el archivo ini
    Private Sub guardarTiempo(ByVal tiempo As Integer)
        Dim myIniFile As New IniFile
        With myIniFile
            .Filename = "D:\config.ini"  'or any other file
            If .OpenIniFile() Then
                .SetValue("tiempoTotal", tiempo)
                If Not .SaveIni Then
                    MessageBox.Show("Problemas escribiendo el ini")
                End If
            Else
                MessageBox.Show("No se encuentra el archivo ini")
            End If
        End With
    End Sub
    Private Function obtenerTiempo() As String
        Dim tiempo As String
        tiempo = ""
        Dim myIniFile As New IniFile
        With myIniFile
            .Filename = "d:\config.ini"  'or any other file
            If .OpenIniFile() Then
                tiempo = .GetValue("tiempoTotal")
                If Not .SaveIni Then
                    MessageBox.Show("problemas en el ini")
                End If
            Else
                MessageBox.Show("No Ini-File found")
            End If
        End With
        Return tiempo
    End Function

    Private Sub principal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        seteaVariables()
        cargarjuego()
    End Sub
End Class
