namespace Dashboard
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            // Initialize all objects
            Command command = new Command(); // stores command
            Status status = new Status(); // stores status (online vs offline)
            Destination destination = new Destination();

            // capture all the inputs for commands
            command.CommandType = ""; // no input for this
            command.Latitude = Convert.ToDouble(latInput.Text);
            command.Longitude = Convert.ToDouble(longInput.Text);
            command.Altitude = Convert.ToDouble(altInput.Text);
            command.Speed = Convert.ToDouble(speedInput.Text);

            if (!this.IsDisposed)
            {
                status.StatusState = "ONLINE";
            }

            // need to made this a function VALIDATION
            if (satRadioButton.Checked)
            {
                destination.DestinationInfo = "SATELLITE";
            }
            else if (centreRadioButton.Checked)
            {
                destination.DestinationInfo = "CENTRE";
            }
            else
            {
                MessageBox.Show("An error has occurred.", "Both boxes can not be selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // packetize data (PAYLOAD OPS CODE HERE)
            // send packet over network (Uplink/Downlink CODE HERE)

            MessageBox.Show("Command has been sent", "", MessageBoxButtons.OK);
            ClearCommandInputs();
        }

        private void consoleTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearCommandInputs()
        {
            latInput.Clear();
            longInput.Clear();
            altInput.Clear();
            speedInput.Clear();
            satRadioButton.Checked = false;
            centreRadioButton.Checked = false;
        }

        private void satRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}