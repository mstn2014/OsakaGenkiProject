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
		if (input_bt.RedButtonTrigger)				return true;
		else                                        return false;
	}

	public bool BlueTrigger(){
		if (input_bt.BlueButtonTrigger)				return true;
		else                                        return false;
	}

	public bool GreenTrigger(){
		if (input_bt.GreenButtonTrigger)			return true;
		else                                        return false;
	}

	public bool YellowTrigger(){
		if (input_bt.YellowButtonTrigger)			return true;
		else                                        return false;
	}

	public bool AnyTrigger(){
		if (input_bt.AnyButtonTrigger())			return true;
		else                                        return false;
	}

}
