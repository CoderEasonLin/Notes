using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class MsgManager
    {
        private Socket SKT = null;
        private Form1 FRM = null;
        private Thread LST_TRD = null;
        private Int32 PORT;

        public void Listen(Form1 frm)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint ePt = null;
                foreach( IPAddress ip in entry.AddressList )
                {
                    if( ip.AddressFamily == AddressFamily.InterNetworkV6)
                        continue;
                    ePt = new IPEndPoint(ip, PORT);
                    break;
                }
                SKT = new Socket( ePt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                SKT.Bind(ePt);
                SKT.Listen(2);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }

            FRM = frm;
            LST_TRD = new Thread(Recieve);
            LST_TRD.Start();
        }

        public bool TestAlive(string target)
        {
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(target, PORT);
                if (client.Connected)
                    return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public void SetPort(Int32 port)
        {
            PORT = port;
        }

        public void Recieve()
        {
            while (true)
            {
                Socket clientSkt = SKT.Accept();
                byte[] bytes = new byte[9128];
                clientSkt.Receive(bytes);
                clientSkt.Send(Encoding.Unicode.GetBytes("NOTES:ACCEPT"));
                string msg = Encoding.Unicode.GetString(bytes);

                StringSecurity s = new StringSecurity();
                msg = s.Decrypt(msg.Trim('\0'));

                AssignMessage(msg);
            }
        }

        public void AssignMessage(string msg)
        {
            FRM.Append(msg);
        }

        public void StopListen()
        {
            if (LST_TRD != null && LST_TRD.IsAlive)
            {
                LST_TRD.Abort();
            }
            if (SKT != null && SKT.IsBound)
            {
                SKT.Close();
            }
        }

        public void Send(string target, string text)
        {
            try
            {
                TcpClient client = new TcpClient(target, PORT);

                if (!client.Connected)
                    throw new SocketException();

                StringSecurity s = new StringSecurity();
                text = s.Encrypt(text);

                Byte[] data = System.Text.Encoding.Unicode.GetBytes(text);

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("ArgumentNullException: " + e.Message);
            }
            catch (SocketException e)
            {
                MessageBox.Show("目標未啟動！");
            }
        }
    }
}
