using System.ComponentModel;

namespace ConcursMotociclism.Gui;

partial class RegisterView
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
        label1 = new System.Windows.Forms.Label();
        nameTextBox = new System.Windows.Forms.TextBox();
        cnpTextBox = new System.Windows.Forms.TextBox();
        teamComboBox = new System.Windows.Forms.ComboBox();
        raceComboBox = new System.Windows.Forms.ComboBox();
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.Location = new System.Drawing.Point(-9, 61);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(366, 48);
        label1.TabIndex = 0;
        label1.Text = "Inscriere participant";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new System.Drawing.Point(60, 112);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.PlaceholderText = "Nume...";
        nameTextBox.Size = new System.Drawing.Size(239, 27);
        nameTextBox.TabIndex = 1;
        // 
        // cnpTextBox
        // 
        cnpTextBox.Location = new System.Drawing.Point(60, 145);
        cnpTextBox.Name = "cnpTextBox";
        cnpTextBox.PlaceholderText = "CNP...";
        cnpTextBox.Size = new System.Drawing.Size(239, 27);
        cnpTextBox.TabIndex = 2;
        // 
        // teamComboBox
        // 
        teamComboBox.FormattingEnabled = true;
        teamComboBox.Location = new System.Drawing.Point(60, 182);
        teamComboBox.Name = "teamComboBox";
        teamComboBox.Size = new System.Drawing.Size(238, 28);
        teamComboBox.TabIndex = 3;
        // 
        // raceComboBox
        // 
        raceComboBox.FormattingEnabled = true;
        raceComboBox.Location = new System.Drawing.Point(61, 216);
        raceComboBox.Name = "raceComboBox";
        raceComboBox.Size = new System.Drawing.Size(238, 28);
        raceComboBox.TabIndex = 4;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(132, 250);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 34);
        button1.TabIndex = 5;
        button1.Text = "Inscrie!";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // RegisterView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(348, 320);
        Controls.Add(button1);
        Controls.Add(raceComboBox);
        Controls.Add(teamComboBox);
        Controls.Add(cnpTextBox);
        Controls.Add(nameTextBox);
        Controls.Add(label1);
        Text = "RegisterView";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.ComboBox teamComboBox;
    private System.Windows.Forms.ComboBox raceComboBox;

    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.TextBox cnpTextBox;

    private System.Windows.Forms.Label label1;

    #endregion
}