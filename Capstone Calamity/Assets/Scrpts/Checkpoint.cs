using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour {

    bool isOn;
    BoxCollider2D playerCollider, thisCollider;
    Player player;
    float deadTime = 1, timeSinceDeath = 0;

    // Use this for initialization
    void Start() {
        isOn = false;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        player = playerCollider.GetComponent<Player>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerCollider.gameObject.activeSelf
            &&Time.realtimeSinceStartup>timeSinceDeath+deadTime
            &&this==player.lastCheckPoint)
        {
            playerCollider.gameObject.SetActive(true);
            player.heal(player.maxHitPoint);
        }
        if (CollisionChecker.checkSide(playerCollider, thisCollider) != CollisionChecker.MISS)
        {
            isOn = true;
            player.lastCheckPoint = this;
        }
        if(isOn)
        {
            //Debug.Log("Burning! we're burning!");
        }
    }
    public void respawn()
    {
        
        playerCollider.transform.position=new Vector3(transform.position.x, transform.position.y);
        timeSinceDeath = Time.realtimeSinceStartup;
        playerCollider.gameObject.SetActive(false);
        
        player.velocity = new Vector3(0, player.jumpSpeed/1.5f, 0);
    }
}
