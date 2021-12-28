using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooController : MonoBehaviour
{

    public float Xabs, Yabs;
    public string Nickname;
    Vector2 moving;
    Color col;

    float origSize;
    public float destSize;
    public GameObject NickCanv;
    //float halfParWidth, halfParHeight;

    void Start()
    {
        origSize = GetComponent<SpriteRenderer>().sprite.rect.width;
        //halfParWidth = GetComponentInParent<RectTransform>().rect.width / 2;
        //halfParHeight = GetComponentInParent<RectTransform>().rect.height / 2;
    }
    
    void Update()
    {
        //set new coords
        moving = new Vector2(Xabs, Yabs) - (Vector2)transform.localPosition;
        transform.Translate(moving);
        //set new size
        var curSize = origSize * transform.localScale.x;
        var diffRatio = destSize / curSize;
        var scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * diffRatio, scale.y * diffRatio, scale.z);
        NickCanv.GetComponent<Text>().text = Nickname;
    }

    public void SetData(float newX, float newY, float newSize, float red, float green, float blue, string nick)
    {
        Xabs = newX;
        Yabs = newY;
        destSize = newSize * 2;
        col = new Color(red, green, blue);
        GetComponent<SpriteRenderer>().color = col;
        Nickname = nick;
    }
}
