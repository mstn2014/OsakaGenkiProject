using UnityEngine;
using System.Collections;

public class Game1ProductionMgr : MonoBehaviour {
	private bool		m_end;		// 演出確認フラグ
	private Game1EffectMgr	m_effect;	// エフェクト
	private Game1ObjMgr		m_objmgr;	// オブジェ
    public Game1PlayerController m_player;  // プレイヤー
	SoundMgr m_sound;          		// サウンド


	public bool IsEnd{
		get{return m_end;}
	}

	// Use this for initialization
	void Start () {
        m_effect = GetComponent<Game1EffectMgr>();
        m_objmgr = GetComponent<Game1ObjMgr>();
		m_end = true;

		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
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

        switch (productNum)
        {
            case 0:
                m_player.DoPose();
				m_sound.PlaySeHandclap();
                break;
            case 1:
            case 2:
                m_player.DoStand();
                break;
        }

		// 文字表示
		StartCoroutine(m_effect.DispResult (productNum));
		while (m_effect.IsPause) {
			yield return null;
		}

        yield return new WaitForSeconds(3.0f);
         
		// キャラクターのアニメーション?
		// 時間終了までひっかける
        m_player.DoRun();
		// サークル削除
		m_effect.InitCircle ();
		// 次の場所へ行く
		m_sound.PlaySeRan();
		StartCoroutine(m_objmgr.MoveObj());
		while (m_objmgr.IsPause) {
			yield return null;		
		}
		m_end = true;
	}
}
