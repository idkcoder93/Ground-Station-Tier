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

        private void signInLabel_Click(object sender, EventArgs e)
        {
            // Retrieve user credentials
            string username = userTextBox.Text;
            string password = passTextBox.Text;

            // User instance
            User tempUser = new User(username, password);

            // Call database instance
            Database userDB = new Database();

            // Function to search for user and logic
            if (userDB.SearchUserInDB(tempUser) == true )
            {
                // open next page
            }
            else
            {
                string errorMessage = "Incorrect credential or no connection";
                errorMessageLabel.Text = errorMessage;
            }
        }
    }
}
