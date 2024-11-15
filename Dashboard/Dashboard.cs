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
            command.CommandType = commandInput.Text;
            command.Latitude = Convert.ToDouble(latInput.Text);
            command.Longitude = Convert.ToDouble(longInput.Text);
            command.Altitude = Convert.ToDouble(altInput.Text);
            command.Speed = Convert.ToDouble(speedInput.Text);

            if (!this.IsDisposed)
            {
                status.StatusState = "ONLINE";
            }

            // Validate destination selection
            ValidateDestinationSelection(destination);

            string function = latInput.Text + "," + longInput.Text + "," + altInput.Text + "," + speedInput.Text;



            // initiailizing packet and handler
            GroundStationPacketHandler handler = new GroundStationPacketHandler();
            GroundStationPacket currentPacket = handler.CreatePacket(command.CommandType, function, "");

            // Converting to JSON
            string jsonPacket = handler.SerializePacket(currentPacket);

            // Displaying JSON in consoleTextBox
            if (handler.SendPacket(currentPacket))
            {
                consoleTextBox.AppendText("Packet sent:\n" + jsonPacket + "\n");
            }
            else
            {
                consoleTextBox.AppendText("Error");
            }

            // now uplink/downlink will need to sent packet


            ClearCommandInputs();
        }

        private void ValidateDestinationSelection(Destination destination)
        {
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
                MessageBox.Show("Please select a destination.", "Destination Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void consoleTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearCommandInputs()
        {
            commandInput.Clear();
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