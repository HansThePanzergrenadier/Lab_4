using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Threading;
using System.Text;
using System.Linq;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour
{
    public GameObject player;
    public GameObject entity;
    public GameObject scoreboardContent;
    public GameObject scoreboard;
    public GameObject lobbyScr;
    List<GameObject> nativeEntities = new List<GameObject>();
    EndPoint clientEndPoint, serverEndPoint;
    const string ip = "127.0.0.1";
    int port;
    Socket udpSocket;
    bool connected = false;
    bool scoresShowing = false;
    bool failedCon = false;
    DataCommand dataComm;
    List<string> scores;
    Thread recieveTh;

    void Start()
    {
        //port = 8081;
        port = new System.Random().Next(8081, 10200);
        clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udpSocket.Bind(clientEndPoint);
        serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8080);
        recieveTh = new Thread(RecieveUDP);
        recieveTh.Start();
        player.GetComponent<GooController>().Nickname = PlayerPrefs.GetString("Nickname", "Player");
        lobbyScr.transform.Find("StatusText").gameObject.GetComponent<Text>().text = "Connecting to server...";
    }

    private void Awake()
    {
        Application.runInBackground = true;
        if (player.GetComponent<GooController>().Nickname == "")
        {
            player.GetComponent<GooController>().Nickname = "ABOBA";
        }
    }

    void Update()
    {
        //Thread.Sleep(20);
        if (!connected)
        {
            lobbyScr.SetActive(true);
            if (failedCon)
            {
                lobbyScr.transform.Find("StatusText").gameObject.GetComponent<Text>().text = "Failed";
            }
            string nik = player.GetComponent<GooController>().Nickname;

            string createReq = $"0{{\"name\":\"{nik}\"}}";
            SendUDP(Encoding.UTF8.GetBytes(createReq));

        }
        else
        {
            if (lobbyScr.active)
            {
                lobbyScr.SetActive(false);
            }

            //clearing view
            foreach (var el in nativeEntities)
            {
                Destroy(el);
            }
            //form data and send it
            string moveReq = $"1{{\"up\":{Input.GetKey(KeyCode.W)},\"down\":{Input.GetKey(KeyCode.S)},\"left\":{Input.GetKey(KeyCode.A)},\"right\":{Input.GetKey(KeyCode.D)}}}".ToLower();

            SendUDP(Encoding.UTF8.GetBytes(moveReq));
            //get data
            if (dataComm != null)
            {
                DrawPlayer(dataComm.goo);
                foreach (var el in dataComm.entities)
                {
                    nativeEntities.Add(DrawEntity(el));
                }
            }
            //show scoreboard if needed
            if (scoresShowing)
            {
                ShowScores(scores);
            }
            else
            {

                CloseScores();

            }
        }
    }

    public GameObject DrawEntity(Entity ent)
    {
        Color c = new Color32(ent.col_r, ent.col_g, ent.col_b, 1);
        string nick = "";

        Vector3 placing = new Vector3(ent.x, ent.y) - transform.position;
        var inst = Instantiate(entity, placing, Quaternion.identity);
        Debug.Log($"{ent.x}, {ent.y}");

        inst.GetComponent<EntityController>().SetData(ent.x, ent.y, ent.r, c.r, c.g, c.b, nick);

        return inst;
    }

    public void DrawPlayer(Goo goo)
    {
        Color c = new Color32(goo.col_r, goo.col_g, goo.col_b, 1);
        float x = goo.x;
        float y = goo.y;
        float radius = goo.r;
        string nick = goo.name;
        if (radius <= 1)
        {
            radius = 1;
        }
        player.GetComponent<GooController>().SetData(x, y, radius, c.r, c.g, c.b, nick);
    }

    private void RecieveUDP()
    {
        try
        {
            while (true)
            {
                if (udpSocket == null)
                {
                    break;
                }
                byte[] buffer = new byte[2048];
                StringBuilder data = new StringBuilder();
                EndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                do
                {
                    int size = udpSocket.ReceiveFrom(buffer, ref anyIP);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (udpSocket.Available > 0);

                string json = data.ToString().Substring(1);
                char type = data.ToString()[0];
                connected = true;
                try
                {
                    switch (type)
                    {
                        case '2':
                            Debug.Log(json);
                            dataComm = JsonConvert.DeserializeObject<DataCommand>(json);
                            scoresShowing = false;
                            break;
                        case '3':
                            Debug.Log(json);
                            scoresShowing = true;
                            scores = JsonConvert.DeserializeObject<ResultCommand>(json).res.Select(g => $"{g.name} ({g.r})").ToList();
                            break;
                    }
                }
                catch (JsonReaderException)
                {
                    Debug.Log("Json deserialization error");
                }
            }
        }
        catch (SocketException)
        {
            connected = false;
            failedCon = true;

        }
    }

    private void SendUDP(byte[] data)
    {
        Debug.Log(Encoding.UTF8.GetString(data));
        if (udpSocket != null)
        {
            udpSocket.SendTo(data, serverEndPoint);
        }
    }

    private void ShowScores(List<string> result)
    {
        scoreboard.SetActive(true);
        scoreboardContent.GetComponent<ScoreContentController>().ShowRecords(result);
    }

    private void CloseScores()
    {
        scoreboard.SetActive(false);
    }
    public void ReturnToMenu()
    {

        if (recieveTh != null)
        {
            recieveTh.Abort();

        }
        if (udpSocket != null)
        {
            udpSocket.Shutdown(SocketShutdown.Both);
            udpSocket.Close();
        }
        udpSocket = null;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void OnApplicationQuit()
    {
        if (recieveTh != null)
        {
            recieveTh.Abort();

        }
        if (udpSocket != null)
        {
            udpSocket.Shutdown(SocketShutdown.Both);
            udpSocket.Close();
        }
        udpSocket = null;
    }

}

[Serializable]
public class Entity
{
    public float x, y;
    public float r;
    public byte col_r, col_g, col_b;
}

[Serializable]
public class DataCommand
{
    public long time;
    public Goo goo;
    public List<Entity> entities;
}

[Serializable]
public class Goo
{
    public string name;
    public int view_r;
    public float x, y;
    public float r;
    public byte col_r, col_g, col_b;
}

[Serializable]
public class ResultCommand
{
    public List<Goo> res;
}
