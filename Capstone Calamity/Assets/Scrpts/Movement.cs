using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed=.25f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//************__INPUT__************
		if (Input.GetKeyDown (KeyCode.UpArrow))//&&jumpState==STANDING) 
		{
			/*
			jumpState=JUMPING;
			jumpTarget = Time.realtimeSinceStartup + jumpDuration; //find the point in the future we will stop jumping up.
			peakRiseTarget = Time.realtimeSinceStartup + jumpDuration*.80f; //find the point in the future we'll slow down.
			peakFallTarget= Time.realtimeSinceStartup +  3* jumpDuration / 2; //find the point in the future we'll slow down.
			transform.Translate(0, jumpSpeed, 0);
			*/
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			//transform.Translate(0,speed*(-1),0);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(speed*(-1),0,0);

		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(speed,0,0);
		}
	}
}
