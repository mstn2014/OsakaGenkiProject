using UnityEngine;
using System.Collections;

public class CharSpeedMgr : MonoBehaviour {

	float Speed_velo = 0;
	float Dans_Speed = 0;
	int	  Count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 初速をアップ(五回成功で0.1UP)
	public void CountUp(){
		Count++;
		if (Count == 5) {
			if(Speed_velo < 0.6f)	Speed_velo += 0.1f;
			if(Dans_Speed < 1.0f)	Dans_Speed += 0.1f;
			Count = 0;
		}
		Debug.Log("速度" + Speed_velo);
		Debug.Log("ダンス速度" + Dans_Speed);
	}

	// 失敗時に0.1スピードダウン
	public void SpeedDown(){
		if (Speed_velo > 0.0f)		Speed_velo -= 0.1f;
		if (Dans_Speed > 0.0f)		Dans_Speed -= 0.1f;
	}

	// 初速をゲット
	public float GetSpeed(){
		return Speed_velo;
	}

	// ダンススピードゲット
	public float GetDansSpeed(){
		return Dans_Speed;
	}
}
