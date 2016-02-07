<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SkypeWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KodiToSkypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcceptCallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EndCallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CallContactToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "SkypeXmlController"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowToolStripMenuItem, Me.SkypeWindowToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.KodiToSkypeToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 124)
        '
        'ShowToolStripMenuItem
        '
        Me.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem"
        Me.ShowToolStripMenuItem.Size = New System.Drawing.Size(176, 24)
        Me.ShowToolStripMenuItem.Text = "Show"
        '
        'SkypeWindowToolStripMenuItem
        '
        Me.SkypeWindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CatchToolStripMenuItem, Me.ReleaseToolStripMenuItem})
        Me.SkypeWindowToolStripMenuItem.Name = "SkypeWindowToolStripMenuItem"
        Me.SkypeWindowToolStripMenuItem.Size = New System.Drawing.Size(176, 24)
        Me.SkypeWindowToolStripMenuItem.Text = "Skype Window"
        '
        'CatchToolStripMenuItem
        '
        Me.CatchToolStripMenuItem.Name = "CatchToolStripMenuItem"
        Me.CatchToolStripMenuItem.Size = New System.Drawing.Size(129, 24)
        Me.CatchToolStripMenuItem.Text = "Catch"
        '
        'ReleaseToolStripMenuItem
        '
        Me.ReleaseToolStripMenuItem.Name = "ReleaseToolStripMenuItem"
        Me.ReleaseToolStripMenuItem.Size = New System.Drawing.Size(129, 24)
        Me.ReleaseToolStripMenuItem.Text = "Release"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.ReloadToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(176, 24)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(125, 24)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'ReloadToolStripMenuItem
        '
        Me.ReloadToolStripMenuItem.Name = "ReloadToolStripMenuItem"
        Me.ReloadToolStripMenuItem.Size = New System.Drawing.Size(125, 24)
        Me.ReloadToolStripMenuItem.Text = "Reload"
        '
        'KodiToSkypeToolStripMenuItem
        '
        Me.KodiToSkypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcceptCallToolStripMenuItem, Me.EndCallToolStripMenuItem, Me.CallContactToolStripMenuItem})
        Me.KodiToSkypeToolStripMenuItem.Name = "KodiToSkypeToolStripMenuItem"
        Me.KodiToSkypeToolStripMenuItem.Size = New System.Drawing.Size(176, 24)
        Me.KodiToSkypeToolStripMenuItem.Text = "KodiToSkype"
        '
        'AcceptCallToolStripMenuItem
        '
        Me.AcceptCallToolStripMenuItem.Name = "AcceptCallToolStripMenuItem"
        Me.AcceptCallToolStripMenuItem.Size = New System.Drawing.Size(156, 24)
        Me.AcceptCallToolStripMenuItem.Text = "Accept call"
        '
        'EndCallToolStripMenuItem
        '
        Me.EndCallToolStripMenuItem.Name = "EndCallToolStripMenuItem"
        Me.EndCallToolStripMenuItem.Size = New System.Drawing.Size(156, 24)
        Me.EndCallToolStripMenuItem.Text = "End call"
        '
        'CallContactToolStripMenuItem
        '
        Me.CallContactToolStripMenuItem.Name = "CallContactToolStripMenuItem"
        Me.CallContactToolStripMenuItem.Size = New System.Drawing.Size(156, 24)
        Me.CallContactToolStripMenuItem.Text = "Call contact"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(176, 24)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 420)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.Text = "SkypeXmlController"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SkypeWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CatchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReleaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KodiToSkypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcceptCallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EndCallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CallContactToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
