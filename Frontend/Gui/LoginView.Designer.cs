using System.ComponentModel;

namespace ConcursMotociclism.Gui;

partial class LoginView
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
        usernameTextBox = new System.Windows.Forms.TextBox();
        passwordTextBox = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        label4 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.Location = new System.Drawing.Point(70, 95);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(353, 34);
        label1.TabIndex = 0;
        label1.Text = "Concurs Motociclism Mnagament\r\n";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // usernameTextBox
        // 
        usernameTextBox.Location = new System.Drawing.Point(239, 180);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new System.Drawing.Size(184, 27);
        usernameTextBox.TabIndex = 1;
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new System.Drawing.Point(239, 213);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.Size = new System.Drawing.Size(184, 27);
        passwordTextBox.TabIndex = 2;
        passwordTextBox.KeyPress += passwordTextBox_KeyPress;
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label2.Location = new System.Drawing.Point(97, 181);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(142, 25);
        label2.TabIndex = 3;
        label2.Text = "Username";
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label3.Location = new System.Drawing.Point(97, 213);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(142, 25);
        label3.TabIndex = 4;
        label3.Text = "Password";
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(190, 263);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(108, 34);
        button1.TabIndex = 5;
        button1.Text = "Login";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(70, 321);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(353, 39);
        label4.TabIndex = 6;
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LoginView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(484, 423);
        Controls.Add(label4);
        Controls.Add(button1);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(passwordTextBox);
        Controls.Add(usernameTextBox);
        Controls.Add(label1);
        Text = "LoginView";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.TextBox usernameTextBox;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label1;

    #endregion
}