using UnityEngine;
using System.Collections;

public class DestroyLabel : MonoBehaviour {

    Game2Setting Setting;       // 設定ファイル.
    float nowTime;

	// Use this for initialization
	void Start () {
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");
        nowTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        nowTime += Time.deltaTime;
        if (nowTime >= Setting.dispTime)
        {
            Destroy(this.gameObject);
        }
	}
}
