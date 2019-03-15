using System;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;

namespace RIO
{
    /* todo
     * test socket status a reopen if closed
     * close socet at end of the day
     */
   
    public class Driver
    {
        private string RussSoundIPAddress;
        private IPEndPoint RIOEndpoint; 
        private TCPClient RIO_TcpClient;


        public Driver()
        {
            RussSoundIPAddress = "10.0.0.20";//145
            ErrorLog.Notice("RIO.Driver.Started v14->" + RussSoundIPAddress);

            SocketErrorCodes s;
            try
            {
                ErrorLog.Notice("RIO.Driver.OpeningSocket");
                RIOEndpoint = new IPEndPoint(IPAddress.Parse(RussSoundIPAddress), 9621);
                RIO_TcpClient = new TCPClient(RIOEndpoint,4096);

                s = RIO_TcpClient.ConnectToServer();
                ErrorLog.Notice("RIO.Driver.Connect: " + s.ToString());

            }
            catch (SocketException e)
            {
                ErrorLog.Error("RIO.Driver SocketException: {0}", e);
            }
        }

        public void SelectSonos(int Zone)
        {
            SendMsg("EVENT C[1].Z[" + Zone.ToString() + "]!SelectSource 5");
        }
        public void SelectAppleTV(int Zone)
        {
            SendMsg("EVENT C[1].Z[" + Zone.ToString() + "]!SelectSource 7");
        }
        public void ZoneOff(int Zone)
        {
            SendMsg("EVENT C[1].Z[" + Zone.ToString() + "].status off");
        }
        public void SetVolume(int Zone, int Volume)
        {

            ErrorLog.Notice("RIO.Driver.VolumeSet: " + Volume.ToString());
            SendMsg("EVENT C[1].Z["+Zone.ToString()+ "]!KeyPress Volume " + Volume.ToString());
        }



        public void SendMsg(string TCPMessage)
        {
            SocketErrorCodes s;
            try
            {
                string msg = TCPMessage + "\r";

                ErrorLog.Notice("RIO.Driver.CMD: " + msg);
     
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);

                s=RIO_TcpClient.SendData(data,data.Length);

                ErrorLog.Notice("RIO.Driver.Send: " + s.ToString());

                RIO_TcpClient.ReceiveData();  ///clear down buffer.
                
            }
            catch (SocketException e)
            {
                ErrorLog.Error("RIO.Driver SocketException: {0}", e);
            }
        }
    }
}
