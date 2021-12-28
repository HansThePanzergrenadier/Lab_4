using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContentController : MonoBehaviour
{
    public GameObject ScoreRecord;
    List<GameObject> recs = new List<GameObject>();
    void Start()
    {
        //ShowRecords(new List<string> { "dloi", "fdfs", "afsdfs", "fsdf", "hhfg" });
    }

    void Update()
    {
        
    }

    public void ShowRecords(List<string> strs)
    {
        recs.Clear();
        foreach(var el in strs)
        {
            var ins = Instantiate(ScoreRecord, GetComponentInParent<Transform>(), false);
            recs.Add(ins);
            ins.GetComponent<ScoreRecordController>().SetText(el);
        }
    }
}
