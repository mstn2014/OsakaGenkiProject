using UnityEngine;
using System.Collections;
using System.Linq;

public class Game3ObjMgr : MonoBehaviour {

    const int mobNum = 4;

    float m_nowTime = 0.0f;
    float m_createTime = 3.0f;      // ライトに向かうモブを抽選する間隔
    // モブのリソース
    GameObject m_mobResource;
    // モブのオブジェクト
    GameObject[] m_mob = new GameObject[mobNum];
    Game3MobController[] m_mobController = new Game3MobController[mobNum];     // モブのコントローラー
    Game3MobController m_selectedMob;           // ライトに入っているモブ
    Game3Balancer m_balancer;
    // ライトのtransform
    Transform m_light;
    // 生成する位置
    Vector3[] m_createPos = new Vector3[mobNum];

    public Game3ObjMgr  m_otherMgr;       // 逆サイドのオブジェクトマネージャー
    public GameObject m_moveObj;           // 移動するオブジェクト

    public Game3MobController SelectedMob
    {
        set{ m_selectedMob = value; }
        get { return m_selectedMob; }
    }

	// Use this for initialization
	void Start () {
        // 位置情報の取得
        for (int i = 0; i < mobNum; i++)
        {
            m_createPos[i] = this.gameObject.transform.FindChild("pos" + (i+1).ToString()).transform.position;
        }
        // モブのロード
        m_mobResource = Resources.Load<GameObject>("Prefab/Game3/Game3Mob");

        m_balancer = GetComponentInParent<Game3Balancer>();
        m_createTime = m_balancer.CreateTime;

        m_light = GameObject.Find("Light" + this.name).transform;

        for (int i = 0; i < mobNum; i++)
        {
            m_mob[i] = CreatePrefab.InstantiateGameObject(m_mobResource, m_createPos[i], m_mobResource.transform.rotation, m_mobResource.transform.localScale, this.gameObject);
            m_mobController[i] = m_mob[i].GetComponent<Game3MobController>();
        }
	}
	// Update is called once per frame
	void Update () {
        m_nowTime += Time.deltaTime;
        // ライトに向かうモブの抽選
        if (m_nowTime >= m_createTime)
        {
            m_nowTime = 0.0f;
            Game3MobController[] ret = m_mobController.Where(mc => !mc.IsSelected).ToArray();
            if (ret.Length == 0) return;

            int selectedNum = Random.Range(0, ret.Length);
            ret[selectedNum].transform.parent = transform;
            ret[selectedNum].MoveToLight(m_light);
            /*if (m_otherMgr.SelectedMob != null)
            {
                m_otherMgr.SelectedMob.OtherMobController = ret[selectedNum];
            }*/
            SelectedMob = ret[selectedNum];
            //SelectedMob.OtherMobController = m_otherMgr.SelectedMob;
           
            m_createTime = m_balancer.CreateTime;
        }
        SelectedMob.OtherMobController = m_otherMgr.SelectedMob;
	}

    public void CreateNewMob(GameObject go)
    {
        for(int i=0;i<mobNum;i++)
        {
            if (m_mob[i] == go)
            {
                m_mob[i] = CreatePrefab.InstantiateGameObject(m_mobResource, m_createPos[i], m_mobResource.transform.rotation, m_mobResource.transform.localScale, this.gameObject);
                m_mobController[i] = m_mob[i].GetComponent<Game3MobController>();
            }
        }
    }
}
