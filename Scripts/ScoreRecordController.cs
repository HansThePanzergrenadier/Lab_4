using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecordController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }
}
