using UnityEngine;
using System.Collections;

public class DispMiss : MonoBehaviour {
	//	ラベル関連
	private GameObject m_missLabel;				//	"Miss!!"と書かれたラベル
	private GameObject m_dispLabel;
	public  float	   m_dispTime = 0.3f;	//	表示する時間
	private float	   m_nowTime;			//  時間

	// Use this for initialization
	void Start () {
		m_missLabel = Resources.Load<GameObject>("LabelMiss");  
		m_nowTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_nowTime += Time.deltaTime;
		
		if (m_nowTime >= m_dispTime)
			Destroy (m_dispLabel);
	}
	
	
	void OnTriggerEnter2D (Collider2D button)
	{
		m_dispLabel = Instantiate(m_missLabel,transform.position,transform.rotation) as GameObject; 
		m_dispLabel.transform.parent = GameObject.Find ("DispMiss").transform;
		m_nowTime = 0.0f;
	}
}