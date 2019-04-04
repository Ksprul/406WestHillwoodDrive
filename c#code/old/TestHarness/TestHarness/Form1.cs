using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TestHarness
{
    public partial class Form1 : Form
    {
        public IPEndPoint remoteEP;
        public byte[] msg;
        public long timecounter;
        public Socket soc;
        public byte[] bytes; 

        public Form1()
        {
            InitializeComponent();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = IPAddress.Parse("10.0.0.20"); 
            remoteEP = new IPEndPoint(ipAddress,9621);
            msg = Encoding.ASCII.GetBytes("EVENT C[1].Z[1]!KeyPress VolumeUp\r");
            bytes = new byte[1024];

            if (soc == null || !soc.Connected)
                OpenSock();
  

        }

        private void OpenSock()
        {
            // Connect to a remote device.  
            try
            {
                // Create a TCP/IP  socket.  
                soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                PrintTimeTaken("socket created");
                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    soc.Connect(remoteEP);
                    PrintTimeTaken(String.Format("Socket connected to {0}", soc.RemoteEndPoint.ToString()));
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            soc.Shutdown(SocketShutdown.Both);
            soc.Close();
            PrintTimeTaken("after close");
        }

        public void PrintTimeTaken(string msg)
        {
            if (timecounter.Equals(null))
                  timecounter=DateTime.Now.Ticks;

            Console.WriteLine("{0}:{1}",
                (DateTime.Now.Ticks - timecounter)/10000, msg);

            timecounter = DateTime.Now.Ticks;
        }


        private void button1_Click(object sender, EventArgs ea)
        {
            if (soc == null || !soc.Connected)
                OpenSock();
  
                try {
                    
                    PrintTimeTaken("button pressed");
                    // Send the data through the socket.  
                    int bytesSent = soc.Send(msg);
                    PrintTimeTaken("after send and before read");
                    // Receive the response from the remote device.  
                    int bytesRec = soc.Receive(bytes);
                    PrintTimeTaken("after revice");
                    
                    Console.WriteLine("Echoed test = {0}",  
                        Encoding.ASCII.GetString(bytes,0,bytesRec));
                    

                } catch (ArgumentNullException ane) {  
                    Console.WriteLine("ArgumentNullException : {0}",ane.ToString());  
                } catch (SocketException se) {  
                    Console.WriteLine("SocketException : {0}",se.ToString());  
                } catch (Exception e) {  
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());  
                }  
      


        }

        private void button2_Click(object sender, EventArgs e)
        {
            soc.Shutdown(SocketShutdown.Both);
            soc.Close();
        }
    }
}
