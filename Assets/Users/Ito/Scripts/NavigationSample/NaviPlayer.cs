using UnityEngine;
using System.Collections;

public class NaviPlayer : MonoBehaviour {

    NavMeshAgent m_agent;
    public Transform m_target;
	// Use this for initialization
	void Start () {
        m_agent = GetComponent<NavMeshAgent>();
        //m_agent.SetDestination(m_target.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 0.1f);
        }
	}
}
