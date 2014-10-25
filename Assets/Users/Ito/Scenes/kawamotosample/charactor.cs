using UnityEngine;
using System.Collections;

// 適当に書いてるので綺麗に書き直してちょ
public class charactor : MonoBehaviour {
	// Use this for initialization
	void Start () {
        // ライトの位置まで線形補間
        iTween.MoveTo(this.gameObject, GameObject.Find("light").transform.position, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        // ライトに触れたら自身を消す
        Destroy(this.gameObject);
    }
}
