using UnityEngine;
using System.Collections;

public class Test_Setting : ScriptableObject {

	// 移動関係
	public float SpotR_pos_x;		// スポットライトx座標
	public float SpotR_pos_y;		// スポットライトy座標
	public float SpotR_pos_z;		// スポットライトz座標
	public float Speed = 0.1f;		// 横移動スピード
	public float Detline = -20;		// 死亡ライン
}
