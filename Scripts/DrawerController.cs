using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class DrawerController : MonoBehaviour
{
    //????
    UdpClient udp;

    public GameObject player;
    public GameObject entity;
    public GameObject scoreboardContent;
    public GameObject scoreboard;
    public string ServerAddress; //127.0.0.1:8080
    List<GameObject> entities = new List<GameObject>();
    bool gotPacket = false;
    IPEndPoint server;
    float halfParWidth, halfParHeight;

    void Start()
    {
        halfParWidth = GetComponentInParent<RectTransform>().rect.width / 2;
        halfParHeight = GetComponentInParent<RectTransform>().rect.height / 2;
        DrawEntity(5000, 5000, 300, 0.5f, 0.8f, 0.3f, "kekers");
        DrawPlayer(6000, 6000, 100, 0.4f, 0.5f, 0.1f, "abobik");
        server = CreateIPEndPoint(ServerAddress);
    }

    void Update()
    {
        //clearing view
        foreach(var el in entities)
        {
            Destroy(el);
        }
        //filling view from server data
        //UdpCommMethod();
    }

    public GameObject DrawEntity(float x, float y, float radius, float r, float g, float b, string nick)
    {
        var inst = Instantiate(entity, GetComponentInParent<Transform>(), false);
        
        inst.GetComponent<GooController>().SetData(x - halfParWidth, y - halfParHeight, radius, r, g, b, nick);
        return inst;
    }

    public void DrawPlayer(float x, float y, float radius, float r, float g, float b, string nick)
    {
        player.GetComponent<GooController>().SetData(x - halfParWidth, y - halfParHeight, radius, r, g, b, nick);
    }

    private void UdpCommMethod()
    {
        //???
        udp = new UdpClient(server);
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

    public static IPEndPoint CreateIPEndPoint(string endPoint)
    {
        string[] ep = endPoint.Split(':');
        if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
        IPAddress ip;
        if (!IPAddress.TryParse(ep[0], out ip))
        {
            throw new FormatException("Invalid ip-adress");
        }
        int port;
        if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
        {
            throw new FormatException("Invalid port");
        }
        return new IPEndPoint(ip, port);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GameWorld"));
    }
}
