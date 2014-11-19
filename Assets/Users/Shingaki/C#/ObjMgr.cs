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

		CreateGallery();
	}
	
	// Update is called once per frame
	void Update () {}
	
	// ギャラリー生成 範囲範囲で生成する人数を決めておく(重なりは難しいようなら固定).
	// ver2
	private void CreateGallery(){
		Vector3 workPos;
		GameObject workObj;
		float checkPos;
		float ngCircle;
		int i, j;

		ngCircle = GAME1.Gallery_NG_Range * GAME1.Gallery_NG_Range;
		workPos.y = GAME1.Obj_Y;
		for (i=0; i<GAME1.Gallery_Width; i++) {

			for(j=0; j<GAME1.Gallery_Height; j++){
				workPos.x = i*GAME1.Gallery_Interval-(GAME1.Gallery_Width/2*GAME1.Gallery_Interval);
				workPos.z = j*GAME1.Gallery_Interval-(GAME1.Gallery_Height/2*GAME1.Gallery_Interval);
				workPos.x += Random.Range(-GAME1.Gallery_Roll,GAME1.Gallery_Roll);
				workPos.z += Random.Range(-GAME1.Gallery_Roll,GAME1.Gallery_Roll);

				checkPos = (workPos.x*workPos.x) + (workPos.z*workPos.z);
				if(checkPos>ngCircle ){
					workObj = CreatePrefab.InstantiateGameObject(m_gallery,workPos,Quaternion.identity,
				                                          	   Vector3.one,m_objParent);
				}
			}
		}
	}
}
