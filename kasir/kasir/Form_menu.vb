Imports System.Data.Odbc
Imports System.IO
Imports System.Data
Public Class Form_menu
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
                'MsgBox("koneksi berhasil", vbInformation, "koneksi berhasil")' 
            End If
        Catch ex As Exception
            MsgBox(String.Format("koneksi gagal. {0}", ex.Message), vbExclamation, "koneksi gagal")
        End Try
    End Sub
    Sub tampilmenu()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select mn.kode_menu,mn.nama_menu,mn.harga, mn.stok, kt.kategori, mn.image from tb_menu mn JOIN tb_kategori kt ON mn.kode_kategori=kt.kode_kategori order by mn.kode_menu asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("menampilkan data gagal")
        End Try
    End Sub
    Sub bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        TextBox1.Focus()
    End Sub
    Sub kode()
        Call koneksi()
        cmd = New OdbcCommand("select kode_menu from tb_menu order by kode_menu desc", con)
        dr = cmd.ExecuteReader
        dr.Read()

        If Not dr.HasRows Then
            TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + "0001"
        Else
            If Microsoft.VisualBasic.Mid(dr.Item("kode_menu"), 4, 6) = Format(Today, "ddMMyy") Then
                TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + Format(Microsoft.VisualBasic.Right(dr.Item("kode_menu"), 4) + 1, "0000")
            Else
                TextBox5.Text = "MNU" + Format(Today, "ddMMyy") + "0001"
            End If
        End If
    End Sub
    Sub tampilkategori()
        da = New OdbcDataAdapter("select * from tb_kategori where status='aktif' order by kode_kategori desc", con)
        ds = New DataSet
        da.Fill(ds, "tb_kategori")
        ComboBox1.DataSource = ds.Tables("tb_kategori")
        ComboBox1.ValueMember = "kode_kategori"
        ComboBox1.DisplayMember = "kategori"
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub Form_menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
        Me.WindowState = FormWindowState.Maximized
        tampilmenu()
        tampilkategori()
        kode()
        bersih()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        bersih()
        kode()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text Is Nothing Then
            MsgBox("silahkan lengkapi data", MsgBoxStyle.Critical, "PERINGATAN")
        Else
            Dim idkategori As String = ComboBox1.SelectedValue

            cmd = New OdbcCommand("select count(*) as ada from tb_menu where kode_menu = '" & TextBox5.Text & "'", con)
            dr = cmd.ExecuteReader
            dr.Read()
            Dim cek As Integer = dr.Item("ada")

            cmd = New OdbcCommand("update tb_menu set nama_menu = '" & TextBox1.Text & "', harga = '" & TextBox2.Text & "', stok = '" & TextBox3.Text & "', kode_kategori = '" & idkategori & "' where kode_menu = '" & TextBox5.Text & "'", con)
            cmd.ExecuteNonQuery()
            MsgBox("Mengubah data berhasil", vbInformation, "INFORMASI")
            tampilmenu()
            bersih()
            kode()
        End If
    End Sub
End Class