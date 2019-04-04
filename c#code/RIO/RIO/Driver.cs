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
       
        private IPEndPoint RIOEndpoint; 
        private TCPClient RIO_TcpClient;
        bool Debug;


        public Driver()
        {
            Debug = true;
            if(Debug) ErrorLog.Notice("RIO.Driver.Started v19->");
        }

        private void SocketConnect()
        {
            SocketErrorCodes s;
            try
            {
                if (Debug) ErrorLog.Notice("RIO.Driver.OpeningSocket");
                
                RIO_TcpClient = new TCPClient(RIOEndpoint, 4096);

                s = RIO_TcpClient.ConnectToServer();
                if (Debug) ErrorLog.Notice("RIO.Driver.Connect: " + s.ToString());
            }
            catch (SocketException e)
            {
                ErrorLog.Error("RIO.Driver SocketException: {0}", e);
            }
        }

        public void Configure(string RussIPAddress, int DebugOn)
        {

            RIOEndpoint = new IPEndPoint(IPAddress.Parse(RussIPAddress), 9621);

            if (DebugOn == 1)
                Debug = true;
            else
                Debug = false;

            if (Debug) ErrorLog.Notice("RIO.Driver.UsingIPAddress:" + RIOEndpoint.Address.ToString());
            if (Debug) ErrorLog.Notice("RIO.Driver.DebugSet:" + Debug.ToString());
            SocketConnect();
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
            SendMsg("EVENT C[1].Z[" + Zone.ToString() + "]!ZoneOff");
        }
        public void SetVolume(int Zone, int Volume)
        {

            if (Debug) ErrorLog.Notice("RIO.Driver.VolumeSet: " + Volume.ToString());
            SendMsg("EVENT C[1].Z["+Zone.ToString()+ "]!KeyPress Volume " + Volume.ToString());
        }



        public void SendMsg(string TCPMessage)
        {
            SocketErrorCodes s;
            if (RIO_TcpClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)  // the sSocketConnect might have closed on us or timed out
            {
                if (Debug) ErrorLog.Notice("RIO.Driver.Closing Stale Socket");
                RIO_TcpClient.Dispose();
                if (Debug) ErrorLog.Notice("RIO.Driver.ReconnectingSocket..");
                SocketConnect(); 
            }
            try
            {
                string msg = TCPMessage + "\r";

                if (Debug) ErrorLog.Notice("RIO.Driver.CMD: " + msg);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);

                s = RIO_TcpClient.SendData(data, data.Length);

                if (Debug) ErrorLog.Notice("RIO.Driver.Send: " + s.ToString());

                RIO_TcpClient.ReceiveData();  ///clear down buffer.
            }
            catch (SocketException e)
            {
                ErrorLog.Error("RIO.Driver SocketException: {0}", e);
            }
        }
    }
}
