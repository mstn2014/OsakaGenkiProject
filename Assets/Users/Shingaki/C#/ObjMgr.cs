using UnityEngine;
using System.Collections;

public class ObjMgr : MonoBehaviour {

	private GameObject 	m_objParent;	// GameObjectの一番上(Inspectorより設定).
	private GameObject 	m_player;		// 生成するプレイヤー(Inspectorより設定).
	private GameObject 	m_gallery;		// 生成するギャラリー(Inspectorより設定).
	private GameObject 	m_ground;		// 生成する背景(Inspectorより設定).
	
	// Game1共通設定.
	private Game1_Setting GAME1;

	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// リソースの読み込み.
		m_objParent = Resources.Load("Shingaki/testResource/prefab/Objparent") as GameObject;
		m_player = Resources.Load ("Shingaki/testResource/prefab/Player") as GameObject;
		m_gallery = Resources.Load ("Shingaki/testResource/prefab/Gallery") as GameObject;
		m_ground = Resources.Load ("Shingaki/testResource/prefab/Ground") as GameObject;


		m_objParent = CreatePrefab.InstantiateGameObject (m_objParent, Vector3.zero, Quaternion.identity,
		                                                 Vector3.one);
		m_player = CreatePrefab.InstantiateGameObject (m_player, new Vector3(0,GAME1.Obj_Y,0),
		                                               Quaternion.identity, Vector3.one);
		m_ground = Instantiate (m_ground) as GameObject;
		m_ground.transform.parent = m_objParent.transform;

		CreateGallery ();
	}
	
	// Update is called once per frame
	void Update () {}
	
	// ギャラリー生成 範囲範囲で生成する人数を決めておく(重なりは難しいようなら固定).
	private void CreateGallery(){
		Vector3 workPos;
		GameObject workObj;
		float	minCreateRange;	// MIN生成場所:x
		float	maxCreateRange;	// MAX生成場所:x

		float checkpos;
		float min_rad;
		float max_rad;

		minCreateRange = GAME1.Gallery_NG_Range;
		maxCreateRange = GAME1.Gallery_NG_Range + GAME1.Gallery_Width;
		// 中心座標(0,0)


		// NOW 人数分回して,NGの場所以外に行けたら生成.

		// 部分に人数生成したら次の部分へ
		// 部分-人数分生成-部分へ 最後の部分だけ割り切れなかったギャラリーを生成する

		// ver2
		//
		// 1. 1/3 to NG_RANGE
		// 2. 2/3 to 1/3
		// 3. 3/3 to 2/3

		// 円の公式(x-a)^2 + (y-b)^2 = r^2
		for (int i=1; i<=GAME1.Gallery_DivNum; i++) {	// 生成する場所ごとに
			for(int j=0; j<GAME1.Gallery_SomeNum; j++){	// 生成するギャラリー分
				do{
					workPos.x = Random.Range(-maxCreateRange, maxCreateRange);
					workPos.z = Random.Range(-maxCreateRange, maxCreateRange);

					checkpos = (workPos.x*workPos.x) + (workPos.z*workPos.z);
					max_rad = maxCreateRange*maxCreateRange;
					min_rad = minCreateRange*minCreateRange;
				}while(max_rad<checkpos && min_rad>checkpos);
	
				workPos.y = GAME1.Obj_Y;
				// gameobjectの生成.
				workObj = CreatePrefab.InstantiateGameObject(m_gallery,workPos,Quaternion.identity,
				                                             Vector3.one,m_objParent);
				// スクリプトの割り当て(このスクリプトから個々で座標を決める・次ラウンドの時もここから).
				// アニメーション割り当て.
				// テクスチャ割り当て.
			}
			// 範囲の変更
			minCreateRange = maxCreateRange;
			maxCreateRange += GAME1.Gallery_Width;

		}
		//
		/*
		// 現在のver
		for (int i=0; i<GAME1.GalleryNum; i++) {
			do{
				workPos.x = Random.Range(GAME1.Gallery_MinX, GAME1.Gallery_MaxX);
				workPos.z = Random.Range(GAME1.Gallery_MinZ, GAME1.Gallery_MaxZ);
			}while((-GAME1.Gallery_NG_Range<workPos.x && workPos.x<GAME1.Gallery_NG_Range) &&
			       (-GAME1.Gallery_NG_Range<workPos.z && workPos.z<GAME1.Gallery_NG_Range));
			workPos.y = GAME1.Obj_Y;
			// gameobjectの生成.
			workObj = CreatePrefab.InstantiateGameObject(m_gallery,workPos,Quaternion.identity,
			                                             Vector3.one,m_objParent);
			// スクリプトの割り当て(このスクリプトから個々で座標を決める・次ラウンドの時もここから).
			// アニメーション割り当て.
			// テクスチャ割り当て.
		}
		*/
	}
}
