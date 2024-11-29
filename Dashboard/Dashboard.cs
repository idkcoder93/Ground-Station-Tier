using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Dashboard
{
    public partial class Dashboard : Form
    {
        Status status = new Status();
        GroundStationPacketHandler handler = new GroundStationPacketHandler();
        SpaceCraftPacket SpaceCraftPacket = new SpaceCraftPacket();
        Command command = new Command();
        Destination destination = new Destination();
        Database db = new Database();
        private HttpListener httpListener; // listens for incoming packets
        private NetworkConnection sendMessage = new NetworkConnection();

        public Dashboard()
        {
            InitializeComponent();
            StartHttpListener();
            status.StatusState = "ONLINE"; // online when launched
            Task.Run(() => BandWidthDisplay());
            Task.Run(() => LatencyDisplay());
        }
        private async void StartHttpListener()
        {
            // Initialize the HTTP listener
            httpListener = new HttpListener();

            httpListener.Prefixes.Add("http://localhost:9000/"); // listens to authenication token
            httpListener.Start();

            // Update the UI
            MessageBox.Show("Server is online", "listening to port 9000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Asynchronously handle requests
            await Task.Run(() => HandleRequests());
        }
        private async Task HandleRequests()
        {
            await sendMessage.SendAuthenticationRequestAsync(); // authenicating connection first

            while (httpListener.IsListening)
            {
                try
                {
                    // Wait for an incoming request
                    var context = await httpListener.GetContextAsync();

                    // Process the request
                    var request = context.Request;
                    if (request.HttpMethod == "POST" && request.ContentType == "application/json")
                    {
                        var queryparameters = System.Web.HttpUtility.ParseQueryString(request.Url!.Query);
                        string parameterValue = queryparameters["packet"];
                        UpdateStatus(parameterValue);
                        DisplayReceivedData(parameterValue);
                    }

                    // Send a response
                    var response = context.Response;
                    var responseMessage = Encoding.UTF8.GetBytes("Request processed successfully.");
                    response.ContentLength64 = responseMessage.Length;
                    await response.OutputStream.WriteAsync(responseMessage, 0, responseMessage.Length);
                    response.Close();
                }
                catch (Exception ex)
                {
                    UpdateStatus(ex.Message);
                }
            }
        }
        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(message)));
            }
            else
            {
                //listBoxLogs.Items.Add(message); // Example: Display logs in a ListBox
                consoleTextBox.AppendText(message);
            }
        }
        private async void SendButton_Click(object sender, EventArgs e)
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

            if (!await SendPacket())
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

        private async Task<bool> SendPacket()
        {
            try
            {
                // Concatenate telemetry data for the packet
                string function = $"{latInput.Text},{longInput.Text},{altInput.Text},{speedInput.Text}";
                string commandType = commandInput.Text;

                // Construct the string literal for the JSON packet
                string command = $"{{\"Date\":\"{DateTime.Now.ToString("o")}\",\"FunctionType\":\"{function}\",\"Command\":\"{commandType}\",\"PacketCRC\":\"0xFFFF\"}}";

                // Log the packet to the console or UI
                consoleTextBox.AppendText("Packet sent:\n" + command + "\n");

                // Send the JSON packet
                await sendMessage.SendHttpRequestAsync(command);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., invalid inputs)
                consoleTextBox.AppendText("Error while sending packet:\n" + ex.Message + "\n");
                return false;
            }
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

        private void LatencyDisplay()
        {
            while (true)
            {
                Random r = new Random();
                int bandWidth = r.Next(1, 50); // Generate random value in the range 1 - 10

                // Sleep for one second
                System.Threading.Thread.Sleep(1000);

                // Update the TextBox safely
                UpdateTextBoxSafely(latencyTextBox, bandWidth.ToString());
            }
        }

        private void BandWidthDisplay()
        {
            while (true)
            {
                Random r = new Random();
                int bandWidth = r.Next(1, 10); // Generate random value in the range 1 - 10

                // Sleep for one second
                System.Threading.Thread.Sleep(1000);

                // Update the TextBox safely
                UpdateTextBoxSafely(bandTextBox, bandWidth.ToString());
            }
        }

        private void DisplayReceivedData(string json)
        {
            // Deserialize the JSON into a SpaceCraftPacket object
            SpaceCraftPacket packet = SpaceCraftPacketHandler.DeserializeSpacePacket(json);

            // Safely update UI controls using Invoke
            UpdateTextBoxSafely(dateTextBox, packet.Datetime);
            UpdateTextBoxSafely(dataTypeTextBox, packet.DataType);
            UpdateTextBoxSafely(radTextBox, packet.Data);
            UpdateTextBoxSafely(tempTextBox, packet.Data);
        }

        private void UpdateTextBoxSafely(TextBox textBox, string value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() => textBox.Text = value));
            }
            else
            {
                textBox.Text = value;
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
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            httpListener?.Stop();
        }
    }
}