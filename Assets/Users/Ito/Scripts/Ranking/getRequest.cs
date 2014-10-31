using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WWWKit;
using MiniJSON;

public class getRequest : MonoBehaviour
{
    // サーバから受け取るデータ構造
    public class data : Object
    {
        public string name;
        public long id, score;
    }

    WWWClientManager cm;
    List<data> containerList;
    //public string ipaddr;       // インスペクター上で設定すること
    int startNum = 0;           // ランキングの初期値
    data[] dt;

    int xpos = 50;


    // Use this for initialization
    void Start()
    {
        cm = new WWWClientManager(this);
        containerList = new List<data>();
        getMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            startNum += 20;
            if (startNum > containerList.Count) startNum -= 20;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            startNum -= 20;
            if (startNum < 0) startNum = 0;
        }

        int count = (containerList.Count - startNum) > 20 ? 20 : (containerList.Count - startNum);
        dt = new data[count];
        containerList.CopyTo(startNum, dt, 0, count);
    }

    //--------------------------------------------------------
    // UIButtonMessageからのメッセージを受ける関数
    //--------------------------------------------------------
    void getMessage()
    {
        string url = "http://mstn2014-osaka.herokuapp.com/users/score";
        cm.GET(url, "ReceiveRequest");
    }

    void ReceiveRequest(WWW www)
    {
        string json = www.text;
        var scoreInfo = Json.Deserialize(json) as Dictionary<string, object>;
        int i = 1;
        containerList.Clear();
        foreach (object ob in scoreInfo)
        {
            Dictionary<string, object> num = (Dictionary<string, object>)scoreInfo[i.ToString()];
            long id = (long)num["id"];
            string name = (string)num["name"];
            long score = (long)num["score"];
            data data1 = new data();
            data1.id = id;
            data1.name = name;
            data1.score = score;
            containerList.Add(data1);
            i++;
        }
    }

    void ReceiveError()
    {
        Debug.Log("GET出来ませんでした。");
    }

    //--------------------------------------------------------
    // ランキングの描画
    //--------------------------------------------------------
    void OnGUI()
    {
        GUI.Label(new Rect(xpos, 0, 100, 100), "rank");
        GUI.Label(new Rect(xpos + 30, 0, 100, 100), "id");
        GUI.Label(new Rect(xpos + 60, 0, 100, 100), "name");
        GUI.Label(new Rect(xpos + 120, 0, 100, 100), "score");
        drawTable();
    }

    private void drawsingleline(int pos, data toShow)
    {
        pos++;
        GUI.Label(new Rect(xpos, pos * 20, 100, 100), (startNum + pos).ToString());
        GUI.Label(new Rect(xpos + 30, pos * 20, 100, 100), toShow.id.ToString());
        GUI.Label(new Rect(xpos + 60, pos * 20, 100, 100), toShow.name);
        GUI.Label(new Rect(xpos + 120, pos * 20, 100, 100), toShow.score.ToString());
    }

    private void drawTable()
    {
        if (containerList.Count == 0) return;

        int j = 0;
        foreach (data thecont in dt)
        {
            drawsingleline(j, thecont);
            j++;
        }
    }
}
