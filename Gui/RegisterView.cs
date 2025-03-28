namespace ConcursMotociclism.Gui;

public partial class RegisterView : Form
{
    private Service.Service service;
    
    public RegisterView()
    {
        InitializeComponent();
    }
    
    public void setService(Service.Service service)
    {
        this.service = service;
        foreach (var team in this.service.GetAllTeams())
        {
            teamComboBox.Items.Add(team.Name);
        }

        foreach (var race in this.service.GetAllRaces())
        {
            raceComboBox.Items.Add(race.RaceName);
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        service.AddRaceRegistration(nameTextBox.Text, cnpTextBox.Text, teamComboBox.Text, raceComboBox.Text);
    }
}