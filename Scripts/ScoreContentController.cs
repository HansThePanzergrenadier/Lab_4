using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContentController : MonoBehaviour
{
    public GameObject ScoreRecord;
    List<GameObject> recs = new List<GameObject>();
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ShowRecords(List<string> strs)
    {
        if (recs.Count > 0)
        {
            foreach (var el in recs)
            {
                Destroy(el);
            }
            recs.Clear();
        }
        if (strs != null && strs.Count > 0)
        {
            foreach (var el in strs)
            {
                var ins = Instantiate(ScoreRecord, GetComponentInParent<Transform>(), false);
                recs.Add(ins);
                ins.GetComponent<ScoreRecordController>().SetText(el);
            }
        }
    }
}
