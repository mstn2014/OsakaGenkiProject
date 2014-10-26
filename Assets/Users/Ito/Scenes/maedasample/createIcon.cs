using UnityEngine;
using System.Collections;

// アイコンを制御するクラス
public class createIcon : MonoBehaviour {

    GameObject go;
    GameObject inGo;
    float nowTime = 0.0f;
    public float duration = 1.0f;
    Transform panel;

    string[] spriteName = {"red","blue","green","yellow"};
	// Use this for initialization
	void Start () {
        go = Resources.Load<GameObject>("iconParent");
        panel = GameObject.Find("Panel").transform;
	}
	
	// Update is called once per frame
	void Update () { 
        nowTime += Time.deltaTime;
        if (nowTime >= duration)
        {
            inGo = Instantiate(go) as GameObject;
            inGo.GetComponentInChildren<UISprite>().spriteName = spriteName[Random.Range(0, 4)];
            inGo.transform.parent = panel;
            inGo.transform.localScale = Vector3.one;
            nowTime = 0.0f;
        }
	}
}
