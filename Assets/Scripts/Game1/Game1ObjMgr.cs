using UnityEngine;
using System.Collections;

public class Game1ObjMgr : MonoBehaviour {

	public GameObject 	m_objParent;	// GameObjectの一番上(Inspectorより設定).
    public  GameObject 	m_player;		// 生成するプレイヤー(Inspectorより設定).
	private GameObject 	m_gallery;		// 生成するギャラリー(Inspectorより設定).
	private GameObject 	m_ground;		// 生成する背景(Inspectorより設定).
	private bool		m_objPause;		// オブジェクトのポーズ
	
	// Game1共通設定.
	private Game1_Setting GAME1;

	public bool IsPause{
		get{return m_objPause;}
	}

	// Use this for initialization
	void Awake () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// リソースの読み込み.
		m_gallery = Resources.Load ("Prefab/Game1/Game1Mob") as GameObject;
        m_ground = Resources.Load("Prefab/Game1/Ground") as GameObject;
		CreateGallery(0.0f);
		CreateGallery(GAME1.Ground_PositionX);

	}
	
	// Update is called once per frame
	void Update () {}
	
	//======================================================
	// @brief:ギャラリー生成.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	private void CreateGallery(float xOffset){
		Vector3 workPos;
		GameObject workObj;
		float checkPos;
		float ngCircle;
		int i, j;

		ngCircle = GAME1.Gallery_NG_Range * GAME1.Gallery_NG_Range;
		workPos.y = GAME1.Obj_Y;
		for (i=0; i<GAME1.Gallery_Width; i++) {

			for(j=0; j<GAME1.Gallery_Height; j++){
				workPos.x = i*GAME1.Gallery_Interval-(GAME1.Gallery_Width/2*GAME1.Gallery_Interval) + xOffset - m_objParent.transform.position.x;
				workPos.z = j*GAME1.Gallery_Interval-(GAME1.Gallery_Height/2*GAME1.Gallery_Interval);
				workPos.x += Random.Range(-GAME1.Gallery_Roll,GAME1.Gallery_Roll);
				workPos.z += Random.Range(-GAME1.Gallery_Roll,GAME1.Gallery_Roll);

                checkPos = ((workPos.x - xOffset + m_objParent.transform.position.x)*(workPos.x - xOffset + m_objParent.transform.position.x)) + (workPos.z * workPos.z);
				if(checkPos>ngCircle){
					workObj = CreatePrefab.InstantiateGameObject(m_gallery,workPos,m_gallery.transform.localRotation,
				                                          	   m_gallery.transform.localScale,m_objParent);
					// ToDo キャラクターのアニメーションやテクスチャやスクリプトの設定

					workObj.AddComponent<Game1Gallery>();
				}
			}
		}
        // 地面の生成
        m_ground = Instantiate(Resources.Load<GameObject>("Prefab/Game1/Ground"),new Vector3(xOffset,0.0f,0.0f),Quaternion.identity) as GameObject;
        m_ground.AddComponent<Game1Ground>();
        m_ground.transform.parent = m_objParent.transform;
	}

	// ラウンド終了時のオブジェクトの移動
	public IEnumerator MoveObj(){
		// ラベルの移動.
		m_objPause = true;
        Hashtable parameters = new Hashtable(){
                                     {"x",m_objParent.transform.position.x-GAME1.Ground_PositionX},
                                     {"easeType",iTween.EaseType.linear},
                                     {"time",GAME1.Player_WolkTime},
                                     {"oncomplete","CreateGallery"},
                                     {"oncompletetarget",this.gameObject},
                                     {"oncompleteparams",GAME1.Ground_PositionX},
                                 };
		iTween.MoveTo(m_objParent, parameters);
		yield return new WaitForSeconds(GAME1.Player_WolkTime);
		m_objPause = false;
	}
}
