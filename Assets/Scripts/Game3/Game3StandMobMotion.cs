using UnityEngine;
using System.Collections;

public class Game3StandMobMotion : MonoBehaviour {

    Animator m_animator;
    int m_danceType;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
	    m_danceType = Random.Range(0,2);
        StartCoroutine(Dance());
	}

    IEnumerator Dance()
    {
        while (true)
        {
            m_danceType = Random.Range(0, 2);
            m_animator.SetInteger("DanceType", m_danceType);

            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
