using UnityEngine;
using System.Collections;
using System.Linq;

public class Game3ObjMgr : MonoBehaviour {

    const int mobNum = 4;

    float m_nowTime = 0.0f;
    float m_createTime = 5.0f;      // ライトに向かうモブを抽選する間隔
    // モブのリソース
    GameObject m_mobResource;
    // モブのオブジェクト
    GameObject[] m_mob = new GameObject[mobNum];
    game3MobController[] m_mobController = new game3MobController[mobNum];     // モブのコントローラー
    // ライトのtransform
    Transform m_light;
    // 生成する位置
    Vector3[] m_createPos = new Vector3[mobNum];
	// Use this for initialization
	void Start () {
        // 位置情報の取得
        for (int i = 0; i < mobNum; i++)
        {
            m_createPos[i] = this.gameObject.transform.FindChild("pos" + (i+1).ToString()).transform.position;
        }
        // モブのロード
        m_mobResource = Resources.Load<GameObject>("Prefab/game3_motion_mob");

        m_light = GameObject.Find("Light" + this.name).transform;

        for (int i = 0; i < mobNum; i++)
        {
            m_mob[i] = CreatePrefab.InstantiateGameObject(m_mobResource, m_createPos[i], m_mobResource.transform.rotation, m_mobResource.transform.localScale, this.gameObject);
            m_mobController[i] = m_mob[i].GetComponent<game3MobController>();
        }
	}
	// Update is called once per frame
	void Update () {
        m_nowTime += Time.deltaTime;
        // ライトに向かうモブの抽選
        if (m_nowTime >= m_createTime)
        {
            m_nowTime = 0.0f;
            game3MobController[] ret = m_mobController.Where(delegate(game3MobController mc) { return !mc.IsSelected; }).ToArray();
            if (ret.Length == 0) return;

            int selectedNum = Random.Range(0, ret.Length);
            ret[selectedNum].MoveToLight(m_light);
            
        }
	}

    public void CreateNewMob(GameObject go)
    {
        for(int i=0;i<mobNum;i++)
        {
            if (m_mob[i] == go)
            {
                m_mob[i] = CreatePrefab.InstantiateGameObject(m_mobResource, m_createPos[i], m_mobResource.transform.rotation, m_mobResource.transform.localScale, this.gameObject);
                m_mobController[i] = m_mob[i].GetComponent<game3MobController>();
            }
        }
    }
}
