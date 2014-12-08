using UnityEngine;
using System.Collections;

public class picmin : MonoBehaviour {

    public Transform m_target;
    NavMeshAgent m_navi;
	// Use this for initialization
	void Start () {
        m_navi = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        m_navi.SetDestination(m_target.position - m_target.transform.forward * 2.0f);
        transform.LookAt(m_target.position - transform.position);
	}
}
