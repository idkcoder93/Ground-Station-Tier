namespace Dashboard
{
    public partial class Dashboard : Form
    {
        Destination Destination;
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

            // Validate destination selection
            if (ValidateDestinationSelection(destination))
            {
                // Packetize data (PAYLOAD OPS CODE HERE)
                // Send packet over network (Uplink/Downlink CODE HERE)

                MessageBox.Show("Command has been sent", "", MessageBoxButtons.OK);
                ClearCommandInputs();
            }

            ClearCommandInputs();
        }

        private bool ValidateDestinationSelection(Destination destination)
        {
            if (satRadioButton.Checked && centreRadioButton.Checked)
            {
                MessageBox.Show("An error has occurred.", "Both boxes cannot be selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (satRadioButton.Checked)
            {
                destination.DestinationInfo = "SATELLITE";
            }
            else if (centreRadioButton.Checked)
            {
                destination.DestinationInfo = "CENTRE";
            }
            else
            {
                MessageBox.Show("Please select a destination.", "Destination Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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