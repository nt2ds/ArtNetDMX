using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtNetDMX
{
    public partial class Form1 : Form
    {
        public string IP = "";
        public int uni;
        public int defaultUni = 0;
        public byte[] fileUni;
        public StreamWriter ipF;
        public StreamWriter unF;
        public Form1()
        {
            InitializeComponent();
            
            ArtNetC.mainWin = this;
            DiscoverNetworks();
            if (!File.Exists("ip"))
            {
                ipF = new StreamWriter("ip", true);
            }
            else
            {
                var enumLines = File.ReadLines("ip", UTF8Encoding.UTF8);
                foreach (var line in enumLines)
                {
                    IP = line;
                }

                
                File.Delete("ip");
                ipF = new StreamWriter("ip", true);
                recvIPcombobox.Text = IP;

            }
            if (!File.Exists("uni"))
            {
               unF = new StreamWriter("uni", true);
               recvUniCombobox.SelectedIndex = defaultUni;
            }
            else
            {
                var enumLines = File.ReadLines("uni", UTF8Encoding.UTF8);
                foreach (var line in enumLines)
                {
                    uni = Int32.Parse(line);
                }
                File.Delete("uni");
                unF = new StreamWriter("uni", true);
                recvUniCombobox.SelectedIndex = uni;
            }
            
        }

        
        
        private async void StartProgram_Click(object sender, EventArgs e)
        {
            if(recvIPcombobox.Text != null && recvIPcombobox.Text != "")
            {
                ArtNetC.recvUni = Int32.Parse(recvUniCombobox.Text);
                ArtNetC.recvIpAddress = recvIPcombobox.Text;
                ipF.Write(ArtNetC.recvIpAddress);
                unF.Write(ArtNetC.recvUni);
                ipF.Close();
                unF.Close();
                StartProgram.Enabled = false;
                recvIPcombobox.Enabled = false;
                recvUniCombobox.Enabled = false;
                ArtNetC.startArtNet();
                await Task.Delay(100);
                EnttecDMX.start();
                
            }
            else
            {
                ShowError();
            }
            
        }

        public void CallDmxUpdate(int dmxChannel, byte channelValue)
        {
            EnttecDMX.setDmxValue(dmxChannel, channelValue);
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
                        recvIPcombobox.Items.Add(addr.Address.ToString());
                    }
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(Environment.ExitCode);
        }

        private void ShowError()
        {
            MessageBox.Show("Enter valid properties", Name = "Properties error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
