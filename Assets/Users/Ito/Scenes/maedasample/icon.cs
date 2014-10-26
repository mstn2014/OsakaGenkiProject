using UnityEngine;
using System.Collections;

// 適当に書いているので、適宜綺麗に書き直してください。
// アイコン
public class icon : MonoBehaviour {

    float rate = 0.0f;
    float nowTime = 0.0f;
    public float degreePerSecond = 60.0f;
    public float aliveTime = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        TweenRotation.Begin(this.gameObject, 1.0f, Quaternion.AngleAxis((int)rate, Vector3.forward));
        rate += Time.deltaTime * degreePerSecond;
        nowTime += Time.deltaTime;
        if (nowTime >= aliveTime) Destroy(this.gameObject);
	}
}
