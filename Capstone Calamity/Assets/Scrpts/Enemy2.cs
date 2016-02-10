using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {
	public int EnemyHealth=3;
    public BoxCollider2D myCollider, playerCollider;
    public bool touchedLastFrame;
    Vector3 velocity;

    // Use this for initialization
    void Start () 
	{
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        velocity = new Vector3(-3, -5, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
        
        gameObject.GetComponent<Controller2D>().Move(velocity * Time.smoothDeltaTime);

        int side = CollisionChecker.checkSide( playerCollider, myCollider);
        if (side != CollisionChecker.MISS)
        {
            if (!touchedLastFrame)
            {

                Controller2D playerController=playerCollider.gameObject.GetComponent<Controller2D>();
                playerController.hitPoints--;
                touchedLastFrame = true;
            }
        }
        else
        {
            touchedLastFrame = false;
        }
         if (EnemyHealth <= 0) 
		{
			GameObject.Destroy (gameObject);
		}
        
        
	}

}
