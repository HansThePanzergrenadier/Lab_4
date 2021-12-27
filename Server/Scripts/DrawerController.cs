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

public class DrawerController : MonoBehaviour
{
    public GameObject player;
    public GameObject entity;
    public GameObject scoreboardContent;
    public GameObject scoreboard;
    public GameObject lobbyScr;
    List<GameObject> nativeEntities = new List<GameObject>();
    EndPoint clientEndPoint, serverEndPoint;
    float halfParWidth, halfParHeight;
    const string ip = "127.0.0.1";
    int port;
    Socket udpSocket;
    bool connected = false;
    DataCommand dataComm;

    void Start()
    {
        halfParWidth = GetComponent<RectTransform>().rect.width / 2;
        halfParHeight = GetComponent<RectTransform>().rect.height / 2;
        //port = 8081;
        port = new System.Random().Next(8081, 10200);
        clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udpSocket.Bind(clientEndPoint);
        serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8080);
        Thread t = new Thread(RecieveUDP);
        t.Start();
    }

    void Update()
    {
        Thread.Sleep(10);
        if (!connected)
        {
            
            string nik = "abdolba";

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
            //filling view from server data
            string moveReq = $"1{{\"up\":{Input.GetKey(KeyCode.W)},\"down\":{Input.GetKey(KeyCode.S)},\"left\":{Input.GetKey(KeyCode.A)},\"right\":{Input.GetKey(KeyCode.D)}}}".ToLower();
            
            SendUDP(Encoding.UTF8.GetBytes(moveReq));
            if (dataComm != null)
            {
                DrawPlayer(dataComm.goo);
                foreach (var el in dataComm.entities)
                {
                    nativeEntities.Add(DrawEntity(el));
                }
            }
        }
    }

    public GameObject DrawEntity(Entity ent)
    {
        Color c = new Color32(ent.color.R, ent.color.G, ent.color.B, ent.color.A);
        float x = ent.x;
        float y = ent.y;
        float radius = ent.r;
        /*
        float r = ent.color.R;
        float g = ent.color.G;
        float b = ent.color.B;
        */
        string nick = "eat me";
        var inst = Instantiate(entity, transform, false);
        Debug.Log($"{ent.x}, {ent.y}");
        inst.GetComponent<GooController>().SetData(ent.x, ent.y, ent.r, c.r, c.g, c.b, nick);
        return inst;
    }

    public void DrawPlayer(Goo goo)
    {
        Color c = new Color32(goo.color.R, goo.color.G, goo.color.B, goo.color.A); 
        float x = goo.x;
        float y = goo.y;
        float radius = goo.r;
        /*
        float r = goo.color.R;
        float g = goo.color.G;
        float b = goo.color.B;
        */
        string nick = goo.name;
        player.GetComponent<GooController>().SetData(x, y, radius, c.r, c.g, c.b, nick);
    }

    private void RecieveUDP()
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            StringBuilder data = new StringBuilder();
            do
            {
                int size = udpSocket.ReceiveFrom(buffer, ref serverEndPoint);
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
                        break;
                    case '3':

                        break;
                }
            }
            catch (JsonReaderException)
            {
            }
        }
        //you should parse incoming string here and call DrawEntity() for every Other entity
        //set nick field to "" if it is food
        //every returned GameObject from the DrawEntity() must be added to entities list
        //call DrawPlayer() for proceeding player data once at iteration
        //
        //after that call TakeControls() to get 4 bools which you will send to server
        //
        //if recieved RESULT packet, call ShowScores()
        //
        //call CloseScores() when recieve DATA again
        //
    }

    private void SendUDP(byte[] data)
    {
        Debug.Log(Encoding.UTF8.GetString(data));
        udpSocket.SendTo(data, serverEndPoint);
    }

    private void TakeControls(out bool forward, out bool backward, out bool left, out bool right)
    {
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
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
        SceneManager.LoadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GameWorld"));
    }
}

[Serializable]
public class Entity
{
    public float x, y;
    public int r;
    public System.Drawing.Color color;
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
    public int r;
    public System.Drawing.Color color;
}
