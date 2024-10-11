namespace CDH_GroundStation_Group6
{
    public partial class SignInPage : Form
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void SignInPage_Load(object sender, EventArgs e)
        {

        }

        private void passTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void signInLabel_Click(object sender, EventArgs e)
        {
            errorMessageLabel.Text = string.Empty;

            // Retrieve user credentials
            string username = userTextBox.Text;
            string password = passTextBox.Text;

            // User instance
            User tempUser = new User(username, password);

            // Call database instance
            Database userDB = new Database();

            // Function to search for user and logic
            bool isUserFound = await userDB.SearchUserInDB(tempUser);

            if (isUserFound)
            {
                this.Hide();
                Dashboard.Dashboard dashboardForm = new Dashboard.Dashboard();
                dashboardForm.Show();
            }
            else
            {
                string errorMessage = "Incorrect credential or no connection";
                errorMessageLabel.Text = errorMessage;
            }
        }

    }
}
