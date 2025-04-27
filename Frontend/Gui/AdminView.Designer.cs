using System.ComponentModel;

namespace ConcursMotociclism.Gui;

partial class AdminView
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        racesTabControl = new System.Windows.Forms.TabControl();
        logoutButton = new System.Windows.Forms.Button();
        registerButton = new System.Windows.Forms.Button();
        searchTextBox = new System.Windows.Forms.TextBox();
        teamsDataGridView = new System.Windows.Forms.DataGridView();
        timer1 = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)teamsDataGridView).BeginInit();
        SuspendLayout();
        // 
        // racesTabControl
        // 
        racesTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
        racesTabControl.Location = new System.Drawing.Point(12, 49);
        racesTabControl.Name = "racesTabControl";
        racesTabControl.SelectedIndex = 0;
        racesTabControl.Size = new System.Drawing.Size(779, 357);
        racesTabControl.TabIndex = 0;
        // 
        // logoutButton
        // 
        logoutButton.Location = new System.Drawing.Point(671, 412);
        logoutButton.Name = "logoutButton";
        logoutButton.Size = new System.Drawing.Size(120, 32);
        logoutButton.TabIndex = 1;
        logoutButton.Text = "Logout";
        logoutButton.UseVisualStyleBackColor = true;
        logoutButton.Click += logoutButton_Click;
        // 
        // registerButton
        // 
        registerButton.Location = new System.Drawing.Point(12, 412);
        registerButton.Name = "registerButton";
        registerButton.Size = new System.Drawing.Size(147, 32);
        registerButton.TabIndex = 2;
        registerButton.Text = "Inscrie Participant";
        registerButton.UseVisualStyleBackColor = true;
        registerButton.Click += registerButton_Click;
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new System.Drawing.Point(264, 16);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.Size = new System.Drawing.Size(263, 27);
        searchTextBox.TabIndex = 3;
        searchTextBox.TextChanged += searchTextBox_TextChanged;
        // 
        // teamsDataGridView
        // 
        teamsDataGridView.AllowUserToAddRows = false;
        teamsDataGridView.AllowUserToDeleteRows = false;
        teamsDataGridView.AllowUserToResizeColumns = false;
        teamsDataGridView.AllowUserToResizeRows = false;
        teamsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        teamsDataGridView.Location = new System.Drawing.Point(15, 49);
        teamsDataGridView.Name = "teamsDataGridView";
        teamsDataGridView.RowHeadersWidth = 51;
        teamsDataGridView.Size = new System.Drawing.Size(775, 356);
        teamsDataGridView.TabIndex = 4;
        teamsDataGridView.Text = "dataGridView1";
        teamsDataGridView.Visible = false;
        // 
        // timer1
        // 
        timer1.Interval = 250;
        timer1.Tick += timer1_Tick;
        // 
        // AdminView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(teamsDataGridView);
        Controls.Add(searchTextBox);
        Controls.Add(registerButton);
        Controls.Add(logoutButton);
        Controls.Add(racesTabControl);
        Text = "AdminView";
        FormClosing += AdminView_FormClosing;
        ((System.ComponentModel.ISupportInitialize)teamsDataGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Timer timer1;

    private System.Windows.Forms.DataGridView teamsDataGridView;

    private System.Windows.Forms.TextBox searchTextBox;

    private System.Windows.Forms.Button logoutButton;
    private System.Windows.Forms.Button registerButton;

    private System.Windows.Forms.TabControl racesTabControl;

    #endregion
}