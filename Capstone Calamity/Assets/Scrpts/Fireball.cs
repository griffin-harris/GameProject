﻿using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

    public float speedX = 3;
    public float speedY = 0;

    Vector3 velocity;
    int damage=1;
    float lifespan = 5;
    float birthday;
    
	// Use this for initialization
	void Start ()
    {
        velocity = new Vector3(speedX, speedY);
        birthday=Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.realtimeSinceStartup >= birthday + lifespan)
        {
            Destroy(gameObject);
        }
        transform.Translate(velocity * Time.smoothDeltaTime);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(enemies.Length);
        for(int i=0; i<enemies.Length;i++)
        {
            if (!enemies[i]) { return; }
            BoxCollider2D bc=enemies[i].GetComponent<BoxCollider2D>();
            
            if(CollisionChecker.checkSide(GetComponent<Collider2D>(),bc)!=CollisionChecker.MISS)
            {
                Enemy2 enemy =enemies[i].GetComponent<Enemy2>();
                if (enemy == null) { return; }
                enemy.EnemyHealth-=damage;
                Destroy(gameObject);
            }
        }

	}
}
