using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtNetDMX.Properties;
using System.Security.Cryptography.X509Certificates;

namespace ArtNetDMX
{
    public partial class Form1 : Form
    {
        public string IP = "";
        public int uni;
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
                Console.WriteLine(IP);
                
                File.Delete("ip");
                ipF = new StreamWriter("ip", true);
                recvIPcombobox.Text = IP;
                Console.WriteLine(IP + " Second Time");

            }
            if (!File.Exists("uni"))
            {
               unF = new StreamWriter("uni", true);
            }
            else
            {
                var enumLines = File.ReadLines("uni", UTF8Encoding.UTF8);
                foreach (var line in enumLines)
                {
                    uni = Int32.Parse(line);
                }
                Console.WriteLine(uni);
                
                File.Delete("uni");
                unF = new StreamWriter("uni", true);
                recvUniCombobox.Text = uni.ToString();
                Console.WriteLine(uni + " Second Time");
            }
            
        }

        
        
        private async void StartProgram_Click(object sender, EventArgs e)
        {
            if(recvIPcombobox.Text != null && recvIPcombobox.SelectedIndex >= 0 && recvIPcombobox.SelectedIndex <= 15)
            {
                ArtNetC.startArtNet();
                ipF.Write(recvIPcombobox.Text);
                unF.Write(recvUniCombobox.Text);
                ipF.Close();
                unF.Close();
                await Task.Delay(2000);
                EnttecDMX.start();
                StartProgram.Enabled = false;
                recvIPcombobox.Enabled = false;
                recvUniCombobox.Enabled = false;
            }
            else
            {
                label3.Text = "Set Properties";
                await Task.Delay(2000);
                label3.Text = "";

            }
            
        }

        public void CallDmxUpdate(int dmxChannel, byte channelValue)
        {
            EnttecDMX.setDmxValue(dmxChannel, channelValue);
            //Console.WriteLine($"Dmx Channel: {dmxChannel} Channel Value: {channelValue}");
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

        private void recvUniCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArtNetC.recvUni = recvUniCombobox.SelectedIndex;
            
            
            
        }

        private void recvIPcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArtNetC.recvIpAddress = recvIPcombobox.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();

            Environment.Exit(Environment.ExitCode);
        }
    }
}
