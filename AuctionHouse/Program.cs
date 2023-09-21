using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace AuctionHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            // Establish the local endpoint
            // for the socket. Dns.GetHostName
            // returns the name of the host
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);

                bool running = true;
                do
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for
                    // incoming connection Using
                    // Accept() method the server
                    // will accept connection of client
                    Socket clientSocket = listener.Accept();

                    // Data buffer
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    byte[] index = Encoding.ASCII.GetBytes("\n1: For product \n2: For close connection");
                    clientSocket.Send(index);
                    do
                    {
                        int numByte = clientSocket.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, numByte);

                    } while (data.IndexOf("<EOF>") < -1);

                    byte[]? test = null;

                    switch (data)
                    {
                        case "1":
                            test = Encoding.ASCII.GetBytes("");
                            break;
                        default:
                            byte[] test2 = Encoding.ASCII.GetBytes("");
                            break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);

                    clientSocket.Send(test);

                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client
                    // using Send() method
                    clientSocket.Send(message);

                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket
                    // for a new Client Connection
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                } while (running);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}