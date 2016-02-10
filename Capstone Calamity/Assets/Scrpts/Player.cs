using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]//Doing this ensures the gameobject this is attached to will have a Controller2D
public class Player : MonoBehaviour {

	Vector3 velocity;
    public Transform fireball;
    public float jumpHeight = 4;
    public float jumpTime = .4f;
    bool isJumping;
    float gravity;
	float run=10;
    float jumpSpeed;
	Controller2D controller;

    float velocityXSmoothing;
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTime, 2);//derived from kinematic equations. PHYSICS!
        jumpSpeed = Mathf.Abs(gravity) * jumpTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
        if(isJumping&&Input.GetKeyUp(KeyCode.Space)&&velocity.y>0)//this will create the option to short jump. 
        {
            velocity.y =0;
        }
        if(controller.collisions.above||controller.collisions.below)
        {
            velocity.y = 0;
            isJumping = false;
        }

        if(controller.collisions.below&&Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpSpeed;
            isJumping = true;
        }
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        float targetVelocityX = input.x * run;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, .1f);
        //velocity.x = targetVelocityX;
		velocity.y += gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);
	}
    public void Fire()
    {
        //Debug.Log("FIRE!");
        Object fire=Instantiate(fireball,transform.position,new Quaternion());
        //fire.SetParent(transform);
    }
}
