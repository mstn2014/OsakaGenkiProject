using UnityEngine;
using System.Collections;

public class Test_Setting : ScriptableObject {

	// 移動関係
	public float 	Speed = 0.1f;		// 横移動スピード
	public float 	Detline = -20;		// 死亡ライン
	public Vector3	L_Position;			// スポットライトポジション
}
