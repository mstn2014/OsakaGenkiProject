using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	private float degree;
	private float r;
	private float centerx;
	private float centery;

	private float x,y;


	// Use this for initialization
	void Start () {
		degree = 0;
		r = 300;
		centerx = 0;
		centery = 0;
		x = transform.localPosition.x;
		y = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		/*float radian = Mathf.PI/180*degree;
		x = centerx+r*Mathf.Cos(radian);
		y = centery+r*Mathf.Sin(radian) / 2;
		degree += 5;*/

		x += 0.1f;

		transform.position =  new Vector3(x, y, 0);
	}
}
