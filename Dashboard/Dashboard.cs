namespace Dashboard
{
    public partial class Dashboard : Form
    {
        Status status = new Status();
        GroundStationPacketHandler handler = new GroundStationPacketHandler();
        Command command = new Command();
        Destination destination = new Destination();
        Database db = new Database();

        public Dashboard()
        {
            InitializeComponent();
            status.StatusState = "ONLINE"; // online when launched
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            // Validate numerical inputs
            if (!ValidateNumericInput(latInput.Text, longInput.Text, altInput.Text, speedInput.Text))
            {
                MessageBox.Show("Inputs are invalid. Please enter numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearCommandInputs();
                return;
            }

            // Initialize command object
            command.CommandType = commandInput.Text.ToUpper();
            command.Longitude = Convert.ToDouble(longInput.Text);
            command.Latitude = Convert.ToDouble(latInput.Text);
            command.Altitude = Convert.ToDouble(altInput.Text);
            command.Speed = Convert.ToDouble(speedInput.Text);

            // Validate destination selection
            ValidateDestinationSelection(destination);

            // Concatenate all telemetry data for packet
            string function = latInput.Text + "," + longInput.Text + "," + altInput.Text + "," + speedInput.Text;

            // initiailizing packet
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
                consoleTextBox.AppendText("Error: Packet not sent\n");
            }

            // now uplink/downlink will need to sent packet

            // Store telemetry data in DB
            db.CommandsSentStored(command);

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

        private bool ValidateNumericInput(string lat, string lon, string alt, string speed)
        {
            return double.TryParse(lat, out _) &&
                   double.TryParse(lon, out _) &&
                   double.TryParse(alt, out _) &&
                   double.TryParse(speed, out _);
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