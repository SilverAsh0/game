using System.Net;
using System.Net.Sockets;

public class MainClass
{
    public static void Main(string[] args)
    {
        //create
        Socket sever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //bind
        IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8888);
        sever.Bind(iPEndPoint);
        //listen,参数backlog指定队列中最多可容纳等待接受的连接数，0表示不限制
        sever.Listen(0);
        Console.WriteLine("服务器启动成功！");
        while (true)
        {
            //只能有一个客户端接入，最基础的写法。
            Console.WriteLine("服务器正在等待客户端接入");
            //accept
            //当没有客户端连接时，服务器程序卡在listenfd.Accept()不会往下执行，直到接收了客户端的连接
            //Accept用于处理客户端连接，sever用于处理客户端监听
            Socket accept = sever.Accept();
            Console.WriteLine("客户端已接入！");
            //receive
            while (accept != null)
            {
                byte[] receive_bytes = new byte[1024];
                int count = accept.Receive(receive_bytes);
                string message = System.Text.ASCIIEncoding.UTF8.GetString(receive_bytes, 0, count);
                Console.WriteLine("服务器已接受的消息：" + message);
                //System.Threading.Thread.Sleep(10000);
                //send
                byte[] send_bytes = System.Text.Encoding.Default.GetBytes("服务器已接受到消息");
                accept.Send(send_bytes);
            }
        }
    }
}