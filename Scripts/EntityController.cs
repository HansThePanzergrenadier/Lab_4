using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityController : MonoBehaviour
{

    public int ticksToDeath;
    float Xabs, Yabs;
    string Nickname;
    Vector2 moving;
    Color col;

    float origSize;
    float destSize;
    public GameObject NickCanv;

    void Start()
    {
        origSize = GetComponent<SpriteRenderer>().sprite.rect.width;
        //moving = new Vector2(Xabs, Yabs) - (Vector2)transform.localPosition;
        //transform.Translate(moving);
        //set new size
        float curSize = origSize * transform.localScale.x;
        float diffRatio = destSize / curSize;
        var scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * diffRatio, scale.y * diffRatio, scale.z);
        //NickCanv.GetComponent<Text>().text = Nickname;
    }

    // Update is called once per frame
    void Update()
    {
        if(ticksToDeath > 0)
        {
            //actually live
            ticksToDeath--;
        }
        else
        {
            Destroy(gameObject);
        }
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
