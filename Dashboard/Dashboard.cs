using System.Text.RegularExpressions;

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
            if (!TryInitializeCommand(out Command command))
            {
                ClearCommandInputs();
                return;
            }

            if (!ValidateCommand(command))
            {
                ClearCommandInputs();
                return;
            }

            if (!SendPacket(command))
            {
                consoleTextBox.AppendText("Error: Packet not sent\n");
            }

            StoreCommandInDatabase(command);
            ClearCommandInputs();
        }

        private bool TryInitializeCommand(out Command command)
        {
            command = new Command();

            try
            {
                command.CommandType = commandInput.Text.ToUpper();
                command.Longitude = Convert.ToDouble(longInput.Text);
                command.Latitude = Convert.ToDouble(latInput.Text);
                command.Altitude = Convert.ToDouble(altInput.Text);
                command.Speed = Convert.ToDouble(speedInput.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Inputs are invalid. Please enter numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool ValidateCommand(Command command)
        {
            if (!CommandTypeValidation(command))
            {
                ShowValidationError("Command must be alphanumeric.");
                return false;
            }

            if (!IsCommandInputValid(command))
            {
                ShowValidationError("Latitude, Longitude, Speed, or Altitude are out of range.");
                return false;
            }



            return true;
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show($"Inputs are invalid. {message}", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool SendPacket(Command command)
        {
            // Concatenate telemetry data for the packet
            string function = $"{command.Latitude},{command.Longitude},{command.Altitude},{command.Speed}";

            // Initialize packet
            GroundStationPacket currentPacket = handler.CreatePacket(command.CommandType, function, "0xFFFF");

            // Convert to JSON
            string jsonPacket = handler.SerializePacket(currentPacket);

            // Send packet
            if (handler.SendPacket(currentPacket))
            {
                consoleTextBox.AppendText("Packet sent:\n" + jsonPacket + "\n");
                return true;
            }

            return false;
        }

        private void StoreCommandInDatabase(Command command)
        {
            db.CommandsSentStored(command);
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

        private bool IsCommandInputValid(Command c)
        {
            if (c.Latitude <= 90 && c.Latitude >= -90 && c.Longitude <= 180 && c.Longitude >= -180 && c.Speed > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CommandTypeValidation(Command c)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            return regex.IsMatch(c.CommandType);
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