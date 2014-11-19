using UnityEngine;
using System.Collections;

public class InputMg : MonoBehaviour {
	//入力
	InputMgr input_bt;

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		input_bt = gs.InputMgr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool RedTrigger(){
		//if(input_bt.RedButtonTrigger)	Debug.Log("レッドボタン入力成功！");
		return input_bt.RedButtonTrigger;
	}

	public bool BlueTrigger(){
		//if(input_bt.BlueButtonTrigger)	Debug.Log("ブルーボタン入力成功！");
		return input_bt.BlueButtonTrigger;
	}

	public bool GreenTrigger(){
		//if(input_bt.GreenButtonTrigger)	Debug.Log("グリーンボタン入力成功！");
		return input_bt.GreenButtonTrigger;
	}

	public bool YellowTrigger(){
		//if(input_bt.YellowButtonTrigger)Debug.Log("イエローボタン入力成功！");
		return input_bt.YellowButtonTrigger;
	}

	public bool AnyTrigger(){
		//if(input_bt.AnyButtonTrigger)	Debug.Log("ボタン入力成功！");
		return input_bt.AnyButtonTrigger;
	}

}
