using ArtNet.Packets;
using System.IO;
using System.Net.NetworkInformation;

namespace ArtNetToDMX
{

    public partial class ArtNet_to_DMX : Form
    {
        public delegate void DataStream(ArtDmx dmx);
        public DataStream datastream = OneUniverse;
        public static int universe;
        public bool started;
        public string localIP;
        public string uni;
        public bool oneUni;
        public bool autoStart;
        public ArtNet_to_DMX()
        {
            InitializeComponent();
            if (File.Exists("info") && File.ReadAllLines("info").Length == 4)
            {
                localIP = File.ReadAllLines("info")[0].Substring(4);
                uni = File.ReadAllLines("info")[1].Substring(5);
                oneUni = Convert.ToBoolean(File.ReadAllLines("info")[2].Substring(8));
                autoStart = Convert.ToBoolean(File.ReadAllLines("info")[3].Substring(11));

                Address_cb.Text = localIP;
                Universe_cb.Text = uni;
                oneUniverse_checkbox.Checked = oneUni;
                autoStart_checkbox.Checked = autoStart;
            }
            else
            {
                File.WriteAllText("info", null);
            }

            ArtNetClass.mainWin = this;
            if(autoStart == true)
            {
                StartProgram();
            }
        }
        public void DiscoverNetworks()
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties ipProps = netInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                    if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        Address_cb.Items.Add(addr.Address.ToString());
                    }
                }
            }
        }

        private void ArtNet_to_DMX_Load(object sender, EventArgs e)
        {
            DiscoverNetworks();
        }

        public static bool CanStart(string address, string uni)
        {
            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(uni))
            {
                return true;
            }
            else { return false; }

        }

        public static void OneUniverse(ArtDmx dmx)
        {
            //Array.Copy(dmx.Data, 0, FTDI.buffer, 1, 512);
            for (int i = 0; i < 511; i++)
            {
                FTDI.buffer[i + 1] = dmx.Data[i];
            }
            Console.WriteLine("one universe");
        }

        public static void MultipleUniverse(ArtDmx dmx)
        {
            if (dmx.Universe == universe)
            {
                Array.Copy(dmx.Data, 0, FTDI.buffer, 1, 512);
                Console.WriteLine("multiple universe");
            }

        }

        public void StartProgram()
        {
            if (oneUniverse_checkbox.Checked == true && !string.IsNullOrEmpty(Address_cb.Text))
            {
                ArtNetClass.StartArtNet(Address_cb.Text);
                FTDI.start();
                Stop_button.Enabled = true;
                Start_button.Enabled = false;
                Address_cb.Enabled = false;
                Universe_cb.Enabled = false;
                oneUniverse_checkbox.Enabled = false;
                started = true;
            }
            else if (oneUniverse_checkbox.Checked == false && CanStart(Address_cb.Text, Universe_cb.Text) == true)
            {
                universe = Convert.ToInt32(Universe_cb.Text);
                ArtNetClass.StartArtNet(Address_cb.Text);
                FTDI.start();
                Stop_button.Enabled = true;
                Start_button.Enabled = false;
                Address_cb.Enabled = false;
                Universe_cb.Enabled = false;
                oneUniverse_checkbox.Enabled = false;
                started = true;
            }
            else
            {
                MessageBox.Show("Set Parameters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                started = false;
            }
        }


        public void Start_button_Click(object sender, EventArgs e)
        {
            StartProgram();
        }

        private void oneUniverse_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (oneUniverse_checkbox.Checked == true)
            {
                datastream = OneUniverse;
            }
            else
            {
                datastream = MultipleUniverse;

            }
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            Start_button.Enabled = true;
            Stop_button.Enabled = false;
            ArtNetClass.StopArtNet();
            FTDI.Close();
            started = false;
            Address_cb.Enabled = true;
            Universe_cb.Enabled = true;
            oneUniverse_checkbox.Enabled = true;
        }

        private void CloseApp(object sender, FormClosingEventArgs e)
        {
            if (started == true)
            {
                ArtNetClass.StopArtNet();
                FTDI.Close();
            }

            Application.ExitThread();
            Environment.Exit(Environment.ExitCode);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Address_cb.Text) && !string.IsNullOrEmpty(Universe_cb.Text))
            {
                File.WriteAllText("info", $"IP: {Address_cb.Text}\n");
                File.AppendAllText("info", $"Uni: {Universe_cb.Text}\n");
                File.AppendAllText("info", $"OneUni: {oneUniverse_checkbox.Checked.ToString().ToLower()}\n");
                File.AppendAllText("info", $"Autostart: {autoStart_checkbox.Checked.ToString().ToLower()}\n");
            }
            else
            {
                MessageBox.Show("Set Parameters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            }
        }

        private void Address_cb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void oneUniverse_checkbox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Checked: Will listen to any universe on the network\nUnchecked: Will listen to a specific universe on the network", oneUniverse_checkbox);
        }

        private void autoStart_checkbox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Autostart the program the next time opening the app.", autoStart_checkbox);
        }
    }
}