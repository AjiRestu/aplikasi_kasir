Public Class Form_utama
    Sub otomatisclose()
        For Each f As Form In Me.MdiChildren
            f.Close()
        Next
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel1.Click

    End Sub

    Private Sub DaftarMenuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaftarMenuToolStripMenuItem.Click
        otomatisclose()
        Form_menu.MdiParent = Me
        Form_menu.Show()
    End Sub

    Private Sub DaftarKategoriToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaftarKategoriToolStripMenuItem.Click
        otomatisclose()
        Form_kategori.MdiParent() = Me
        Form_kategori.Show()
    End Sub

    Shared Function LabelNama() As Object
        Throw New NotImplementedException
    End Function

    Shared Function LabelEmail() As Object
        Throw New NotImplementedException
    End Function

    Shared Function LabelHakases() As Object
        Throw New NotImplementedException
    End Function

    Shared Function LabelKodepengguna() As Object
        Throw New NotImplementedException
    End Function

    Shared Function MenuPelayan() As Object
        Throw New NotImplementedException
    End Function

    Shared Function MenuMaster() As Object
        Throw New NotImplementedException
    End Function

    Shared Function DataMenuToolStripMenuItem() As Object
        Throw New NotImplementedException
    End Function

End Class