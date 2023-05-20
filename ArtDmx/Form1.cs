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
        public ArtNet_to_DMX()
        {
            InitializeComponent();
            if (File.Exists("info") && File.ReadAllLines("info").Length == 3)
            {
                localIP = File.ReadAllLines("info")[0].Substring(4);
                uni = File.ReadAllLines("info")[1].Substring(5);
                oneUni = Convert.ToBoolean(File.ReadAllLines("info")[2].Substring(8));
                Address_cb.Text = localIP;
                Universe_cb.Text = uni;
                oneUniverse_checkbox.Checked = oneUni;
            }
            else
            {
                File.WriteAllText("info",null);
            }
            
            ArtNetClass.mainWin = this;
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
            Array.Copy(dmx.Data, 0, FTDI.buffer, 1, 512);
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



        public void Start_button_Click(object sender, EventArgs e)
        {
            if (oneUniverse_checkbox.Checked == true && !string.IsNullOrEmpty(Address_cb.Text))
            {
                ArtNetClass.StartArtNet(Address_cb.Text);
                FTDI.start();
                Stop_button.Enabled = true;
                Start_button.Enabled = false;
                Address_cb.Enabled = false;
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

        private void oneUniverse_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (oneUniverse_checkbox.Checked == true)
            {
                datastream = OneUniverse;
                Universe_cb.Enabled = false;
            }
            else
            {
                datastream = MultipleUniverse;
                Universe_cb.Enabled = true;

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
            oneUniverse_checkbox.Enabled = true;
            if (oneUniverse_checkbox.Checked == false)
            {
                Universe_cb.Enabled = true;
            }
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
            File.WriteAllText("info", $"IP: {Address_cb.Text}\n");
            File.AppendAllText("info", $"Uni: {Universe_cb.Text}\n");
            File.AppendAllText("info", $"OneUni: {oneUniverse_checkbox.Checked.ToString().ToLower()}\n");
        }

        private void Address_cb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void oneUniverse_checkbox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Checked: Will listen to any universe from any app (skips an if statement => no check on data packet)\nUnchecked: Will listen to a specific universe (uses if statement => check on data packet)", oneUniverse_checkbox);

        }
    }
}