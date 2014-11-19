using UnityEngine;
using System.Collections;

public class ObjMgr : MonoBehaviour {
	
	[Header("Object")]
	private GameObject m_objParent;	// GameObjectの一番上(Inspectorより設定).
	private GameObject m_player;		// 生成するプレイヤー(Inspectorより設定).
	private GameObject m_gallery;	// 生成するギャラリー(Inspectorより設定).
	private GameObject m_ground;		// 生成する背景(Inspectorより設定).
	//private GameObject m_panel;		// NGUIの親.
	
	// Game1共通設定
	private Game1Setting GAME1;
	
	
	// Use this for initialization
	void Start () {
		// Game1共通設定
		GAME1 = Resources.Load<Game1Setting>("Setting/Game1_Setting");

		// リソースの読み込み
		m_objParent = Resources.Load("Shingaki/testResource/prefab/Objparent") as GameObject;
		m_player = Resources.Load ("Shingaki/testResource/prefab/Player") as GameObject;
		m_gallery = Resources.Load ("Shingaki/testResource/prefab/Gallery") as GameObject;
		m_ground = Resources.Load ("Shingaki/testResource/prefab/Ground") as GameObject;


		//m_panel = GameObject.Find ("Panel");
		m_objParent = CreatePrefab.InstantiateGameObject (m_objParent, Vector3.zero, Quaternion.identity,
		                                                 Vector3.one);
		m_player = CreatePrefab.InstantiateGameObject (m_player, new Vector3(0,GAME1.Obj_Y,0),
		                                               Quaternion.identity, Vector3.one);
		m_ground = Instantiate (m_ground) as GameObject;
		m_ground.transform.parent = m_objParent.transform;

		CreateGallery ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// ギャラリー生成.
	private void CreateGallery(){
		Vector3 workPos;
		GameObject workObj;
		for (int i=0; i<GAME1.GalleryNum; i++) {
			do{
				workPos.x = Random.Range(GAME1.Gallery_MinX, GAME1.Gallery_MaxX);
				workPos.y = GAME1.Obj_Y;
				workPos.z = Random.Range(GAME1.Gallery_MinZ, GAME1.Gallery_MaxZ);
			}while((-GAME1.Gallery_NG_Range<workPos.x && workPos.x<GAME1.Gallery_NG_Range) &&
			       (-GAME1.Gallery_NG_Range<workPos.z && workPos.z<GAME1.Gallery_NG_Range));
			// gameobjectの生成
			workObj = CreatePrefab.InstantiateGameObject(m_gallery,workPos,Quaternion.identity,
			                                             Vector3.one,m_objParent);
			// スクリプトの割り当て(このスクリプトから個々で座標を決める・次ラウンドの時もここから)
			// アニメーション割り当て
			// テクスチャ割り当て
		}
	}
}
