using ConcursMotociclism.Utils;

namespace ConcursMotociclism.Gui;

public partial class AdminView : Form, Observer
{
    private Service.Service service;
    private List<Form> childForms = new List<Form>();
    public AdminView()
    {
        InitializeComponent();
    }
    
    public void SetService(Service.Service service)
    {
        this.service = service;
        this.service.addObserver(this);
        SetUpTableControl();
        teamsDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nume" });
        teamsDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Clase" });
    }

    public void SetUpTableControl()
    {
        var index = racesTabControl.TabIndex;
        racesTabControl.TabPages.Clear();
        
        var tabPage = new TabPage("Toate");
        var tabTable = createTable();
        
        tabTable.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nume" });
        tabTable.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Clasa" });
        tabTable.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nr Participanti" });

        foreach (var race in service.GetAllRaces())
        {
            tabTable.Rows.Add(race.RaceName, race.RaceClass, service.GetRacersCountForRace(race.Id));
        }
        
        tabPage.Controls.Add(tabTable);
        racesTabControl.TabPages.Add(tabPage);
        
        var classes = service.GetUsedRaceClasses();
        foreach (var raceClass in classes)
        {
            tabPage = new TabPage(raceClass.ToString() + "mc");
            tabTable = createTable();

            tabTable.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nume" });
            tabTable.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nr Participanti" });

            foreach (var race in service.GetRacesByClass(raceClass))
            {
                tabTable.Rows.Add(race.RaceName, service.GetRacersCountForRace(race.Id));
            }
            
            tabPage.Controls.Add(tabTable);
            racesTabControl.TabPages.Add(tabPage);
        }

        racesTabControl.TabIndex = index;
    }

    private static DataGridView createTable()
    {
        var dataGridView = new DataGridView();
        dataGridView.ReadOnly = true;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView.AllowUserToAddRows = false;
        dataGridView.AllowUserToDeleteRows = false;
        dataGridView.AllowUserToOrderColumns = false;
        dataGridView.AllowUserToResizeColumns = false;
        dataGridView.AllowUserToResizeRows = false;
        dataGridView.RowHeadersVisible = false;
        dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView.Dock = DockStyle.Fill;
        return dataGridView;
    }

    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {   
        timer1.Stop();
        timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Stop();
        if (searchTextBox.Text.Length == 0)
        {
            racesTabControl.Visible = true;
            teamsDataGridView.Visible = false;
        }
        else
        {
            racesTabControl.Visible = false;
            teamsDataGridView.Visible = true;
            teamsDataGridView.Rows.Clear();
            foreach (var team in service.GetTeamsByPartialName(searchTextBox.Text))
            {
                var racers = service.GetRacersByTeam(team.Id);
                foreach (var racer in racers)
                {
                    teamsDataGridView.Rows.Add(racer.Name, string.Join(", ", service.GetRacerClasses(racer.Id)));
                }
            }
        }
    }

    private void registerButton_Click(object sender, EventArgs e)
    {
        var registerForm = new RegisterView();
        registerForm.setService(service);
        registerForm.Show();
        childForms.Add(registerForm);
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        foreach (var form in childForms)
        {
            form.Close();
        }
        service.removeObserver(this);
        this.Close();
    }

    public void update()
    {
        SetUpTableControl();
    }
}