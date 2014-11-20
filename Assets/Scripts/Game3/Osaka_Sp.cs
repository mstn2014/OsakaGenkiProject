using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//======================================================
// @brief:大阪を走り回るアイコンクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Osaka_Sp : MonoBehaviour {

	TweenPosition m_Tw_pos;
	TweenRotation m_Tw_rt;

	List<GameObject> PositionList2D = new List<GameObject> ();	
	int Pos_count = 10;											

	// Use this for initialization
	void Start () {
				for (int i=0; i<Pos_count; ++i) {
						PositionList2D.Add(GameObject.Find ("2DPoint_" + i));
				}
				m_Tw_pos = GameObject.Find ("Point_Sp").GetComponent<TweenPosition> ();
				m_Tw_rt = GameObject.Find ("Point_Sp").GetComponent<TweenRotation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_Tw_pos.enabled) {			// tweenが無効化になっていれば更新
			if (Pos_count > 0) {
				GameObject obj = PositionList2D [0];
				PositionList2D.RemoveAt (0);
				Pos_count--;
				m_Tw_pos.from = m_Tw_pos.to;
				m_Tw_pos.to = obj.transform.localPosition;
				//m_Tw_rt.to = obj.transform.localEulerAngles;
				TweenPosition.Begin(this.gameObject,0.5f,m_Tw_pos.to);
			}
		}

		if (!m_Tw_rt.enabled) {
			//TweenRotation.Begin(this.gameObject,0.5f);
		}
	}
}

