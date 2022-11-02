using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class InternetRunTest : MonoBehaviour
{
    public Socket socket;
    public InputField inputField;
    public Text text;

    public Button connection;
    public Button send;

    // Start is called before the first frame update
    private void Start()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        text = GameObject.Find("TextShow").GetComponent<Text>();
        connection = GameObject.Find("connection").GetComponent<Button>();
        send = GameObject.Find("send").GetComponent<Button>();
        connection.onClick.AddListener(Connection);
        send.onClick.AddListener(Send);
    }

    private void Connection()
    {
        //第一个参数，使用IPv4还是IPv6
        //第二个参数，报文类型，TCP使用流的方式
        //第三个参数，协议类型TCP还是UDP
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //客户端通过socket.Connect（远程IP地址，远程端口）连接服务端。
        //Connect是一个阻塞方法，程序会卡住直到服务端回应（接收、拒绝或超时）。
        socket.Connect("127.0.0.1", 8888);
    }

    public void Send()
    {
        //send
        //客户端通过socket.Send发送数据，这也是一个阻塞方法。
        //该方法接受一个byte[]类型的参数指明要发送的内容。Send的返回值指明发送数据的长度。
        //程序用System.Text.Encoding.Default.GetBytes(字符串)把字符串转换成byte[]数组
        //，然后发送给服务端。
        //把本地数据转化为字节流发送给服务器
        if (socket == null)
        {
            Connection();
        }

        if (inputField.text == "")
        {
            return;
        }
        string message = inputField.text;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(message);
        socket.Send(bytes);
        //receive
        //客户端通过socket.Receive接收服务端数据。
        //Receive也是阻塞方法，没有收到服务端数据时，程序将卡在Receive不会往下执行。
        //Receive带有一个byte[]类型的参数，它存储接收到的数据。Receive的返回值指明接收到数据的长度。之后使用System.Text.Encoding. Default.GetString(readBuff,0, count)将byte[]数组转换成字符串显示在屏幕上
        //把服务器的报文转化成字符串用于显示
        byte[] receivebytes = new byte[1024];
        int count = socket.Receive(receivebytes);
        string text1 = System.Text.Encoding.Default.GetString(receivebytes, 0, count);
        text.text = text1;
        //关闭链接
        //socket.Close();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnApplicationQuit()
    {
        socket.Close();
    }
}