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
        //��һ��������ʹ��IPv4����IPv6
        //�ڶ����������������ͣ�TCPʹ�����ķ�ʽ
        //������������Э������TCP����UDP
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //�ͻ���ͨ��socket.Connect��Զ��IP��ַ��Զ�̶˿ڣ����ӷ���ˡ�
        //Connect��һ����������������Ῠסֱ������˻�Ӧ�����ա��ܾ���ʱ����
        socket.Connect("127.0.0.1", 8888);
    }

    public void Send()
    {
        //send
        //�ͻ���ͨ��socket.Send�������ݣ���Ҳ��һ������������
        //�÷�������һ��byte[]���͵Ĳ���ָ��Ҫ���͵����ݡ�Send�ķ���ֵָ���������ݵĳ��ȡ�
        //������System.Text.Encoding.Default.GetBytes(�ַ���)���ַ���ת����byte[]����
        //��Ȼ���͸�����ˡ�
        //�ѱ�������ת��Ϊ�ֽ������͸�������
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
        //�ͻ���ͨ��socket.Receive���շ�������ݡ�
        //ReceiveҲ������������û���յ����������ʱ�����򽫿���Receive��������ִ�С�
        //Receive����һ��byte[]���͵Ĳ��������洢���յ������ݡ�Receive�ķ���ֵָ�����յ����ݵĳ��ȡ�֮��ʹ��System.Text.Encoding. Default.GetString(readBuff,0, count)��byte[]����ת�����ַ�����ʾ����Ļ��
        //�ѷ������ı���ת�����ַ���������ʾ
        byte[] receivebytes = new byte[1024];
        int count = socket.Receive(receivebytes);
        string text1 = System.Text.Encoding.Default.GetString(receivebytes, 0, count);
        text.text = text1;
        //�ر�����
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