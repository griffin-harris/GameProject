using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {
	public int EnemyHealth=3;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (EnemyHealth <= 0) 
		{
			GameObject.Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		PlayerControl pc = other.gameObject.GetComponent<PlayerControl> ();
		if (pc!=null)
		{
			EnemyHealth--;
		}
	}
}
