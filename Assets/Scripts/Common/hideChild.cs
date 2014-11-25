using UnityEngine;
using System.Collections;

public class hideChild : MonoBehaviour {

    public bool hide;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnValidate()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = !hide;
        }
    }
}
