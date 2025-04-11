using ConcursMotociclism.Service;
using ConcursMotociclism.Utils;

namespace ConcursMotociclism.Gui;

public partial class LoginView : Form
{
    private IObservableService service;
    public LoginView()
    {
        InitializeComponent();

    }
    
    public void SetService(IObservableService service)
    {
        this.service = service;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (usernameTextBox.Text.Length != 0)
        {
            if (service.CheckUserExists(usernameTextBox.Text))
            {
                if (service.CheckUserPassword(usernameTextBox.Text, PasswordHasher.HashPassword(passwordTextBox.Text, usernameTextBox.Text)))
                {
                    label4.Text = "";
                    var adminView = new AdminView();
                    adminView.SetService(service);
                    adminView.Show();
                }
                else
                {
                    label4.Text = "Incorrect password!";
                    label4.ForeColor = Color.Red;
                }
            }
            else
            {
                label4.Text = "User does not exist!";
                label4.ForeColor = Color.Red;
            }
        }
    }

    private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            button1_Click(sender, e);
        }
    }
}