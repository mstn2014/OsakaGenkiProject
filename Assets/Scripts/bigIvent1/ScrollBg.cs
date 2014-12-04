using UnityEngine;
using System.Collections;

public class ScrollBg : MonoBehaviour {

	float Max = 1080;
	Vector3 Speed = new Vector3(1.0f,1f,0f);

	// リセットするポジション
	Vector3 Pos = new Vector3(-960,-1080,1);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 移動
		this.gameObject.transform.localPosition += Speed;

		// ポジションリセット
		if (this.gameObject.transform.localPosition.y >= Max) {
			this.gameObject.transform.localPosition = Pos;
		}
	}
}
