using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public int m_EventTime;
	private GameObject prefab;

	private int rand;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per fra
	void Update () {
		//if (Input.GetKey (KeyCode.Space)) 
		if (Time.frameCount % m_EventTime == 0)
		{
			rand = Random.Range(0, 5);

			switch(rand)
			{
				case 0:
					prefab = Resources.Load<GameObject>("blue");  
					prefab = Instantiate(prefab,transform.position,transform.rotation) as GameObject; 
					break;

				case 1:
					prefab = (GameObject)Resources.Load("red");  
				prefab = Instantiate(prefab,transform.position,transform.rotation)  as GameObject; 
					break;
	
				case 2:
					prefab = (GameObject)Resources.Load("green");  
				prefab = Instantiate(prefab,transform.position,transform.rotation)  as GameObject; 
					break;

				case 3:
					prefab = (GameObject)Resources.Load("yellow");  
				prefab = Instantiate(prefab,transform.position,transform.rotation)  as GameObject; 
					break;
			}

			prefab.transform.parent = GameObject.Find ("Panel").transform;

			//GameObject prefab = (GameObject)Resources.L oad("blue");  
			//Instantiate(prefab,transform.position,transform.rotation);  
		}

		if (Time.frameCount % 180 == 0) {
						//Destroy (gameObject,5f);
				}
			
	}
}
