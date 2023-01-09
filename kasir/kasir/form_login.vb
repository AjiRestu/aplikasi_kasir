Imports System.Data.Odbc
Public Class form_login
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand

    Public Sub koneksi()
        Try
            con = New OdbcConnection("dsn=db_kasir")

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
        Catch ex As Exception
            MsgBox(String.Format("koneksi gagal. {0}", ex.Message), vbExclamation, "koneksi gagal")
        End Try
    End Sub

    Private Sub form_login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cmd = New OdbcCommand("select count(*) as ada, usr.* from tb_pengguna usr where Email = '" & TextBox1.Text & "' AND Password = '" & TextBox2.Text & "'", con)
        dr = cmd.ExecuteReader
        dr.Read()
        Dim cek As Integer = dr.Item("ada")
        Dim hakakses As String = dr.Item("hak_akses").ToString()
        Dim stts As String = dr.Item("status").ToString()
        If cek > 0 And stts = "Aktif" Then
            MsgBox("login berhasil.", MsgBoxStyle.Information, "INFORMASI")
            Form_utama.LabelNama.Text = dr.Item("nama_lengkap")
            Form_utama.LabelEmail.Text = dr.Item("Email")
            Form_utama.LabelHakases.Text = dr.Item("hak_akses")
            Form_utama.LabelKodepengguna.Text = dr.Item("kode_pengguna")

            If (hakakses = "Admin") Then
                Form_utama.Show()
            ElseIf (hakakses = "kasir") Then
                Form_utama.MenuMaster.visible = False
                Form_utama.MenuPelayan.visible = False

                Form_utama.DataMenuToolStripMenuItem.visible = False
                Form_utama.LapPenggunaToolStripMenuItem.visible = False
                Form_utama.LapKatiToolStripMenuItem.visible = False
                Form_utama.Show()
            ElseIf (hakakses = "Pelayan") Then
                Form_utama.MenuMaster.visible = False
                Form_utama.MenuKasir.visible = False
                Form_utama.MenuLaporan.visible = False
                Form_utama.Show()
            End If
            Me.Hide()
        ElseIf stts = "Tidak Aktif" Then
            MsgBox("Login Gagal, pengguna telah dinonaktifkan!", MsgBoxStyle.Critical, "Peringatan")
        Else
            MsgBox("Login Gagal!", MsgBoxStyle.Critical, "Peringatan")
        End If

        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class
