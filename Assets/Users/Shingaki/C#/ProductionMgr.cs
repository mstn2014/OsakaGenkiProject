using UnityEngine;
using System.Collections;

public class ProductionMgr : MonoBehaviour {
	private bool		m_end;		// 演出確認フラグ
	private EffectMgr	m_effect;	// エフェクト
	private ObjMgr		m_objmgr;	// オブジェ


	public bool IsEnd{
		get{return m_end;}
	}

	// Use this for initialization
	void Start () {
		m_effect = GetComponent<EffectMgr> ();
		m_objmgr = GetComponent<ObjMgr> ();
		m_end = true;
	}
	
	// Update is called once per frame
	void Update () {}

	//======================================================
	// @brief:.ラウンド間の演出
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:int: 0: パーフェクト 1: 失敗 2: 時間切れ
	// @return:true :
	//======================================================
	public IEnumerator ResultRound(int productNum){
		m_end = false;
		yield return null;

		// 文字表示
		StartCoroutine(m_effect.DispResult (productNum));
		while (m_effect.IsPause) {
			yield return null;
		}

		// キャラクターのアニメーション?
		// 時間終了までひっかける

		// サークル削除
		m_effect.InitCircle ();
		// 次の場所へ行く
		StartCoroutine(m_objmgr.MoveObj());
		while (m_objmgr.IsPause) {
			yield return null;		
		}

		m_end = true;
	}
}
