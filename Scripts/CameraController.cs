using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject playerGoo;
    //public int viewRadius = 100;
    private Vector3 offset;
    public int targetW = 1920;
    public int targetH = 1080;
    private float k = 0.49f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetCamHeight(int viewRad)
    {
        Vector3 target = new Vector3(0, 0, -k * viewRad);
        Vector3 move = target - transform.position;
        transform.Translate(move);
    }
}
