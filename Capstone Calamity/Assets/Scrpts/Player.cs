﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]//Doing this ensures the gameobject this is attached to will have a Controller2D
public class Player : MonoBehaviour {

	Vector3 velocity;

    public float jumpHeight = 4;
    public float jumpTime = .4f;
    float gravity;
	float run=10;
    float jumpSpeed;
	Controller2D controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTime, 2);//derived from kinematic equations. PHYSICS!
        jumpSpeed = Mathf.Abs(gravity) * jumpTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(controller.collisions.above||controller.collisions.below)
        {
            velocity.y = 0;
        }

        if(controller.collisions.below&&Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpSpeed;
        }
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		velocity.x = input.x * run;

		velocity.y += gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);
	}
}