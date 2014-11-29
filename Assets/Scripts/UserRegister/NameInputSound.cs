using UnityEngine;
using System.Collections;

public class NameInputSound : MonoBehaviour {

	SoundMgr m_sound;          		// サウンド

	// Use this for initialization
	void Start () {
		// 入力クラスの取得
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
		m_sound.PlayNameInput();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
