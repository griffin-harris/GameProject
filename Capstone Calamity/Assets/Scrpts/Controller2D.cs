using UnityEngine;
using System.Collections;


[RequireComponent (typeof (BoxCollider2D))]//Doing this ensures the gameobject this is attached to will have a BoxCollider2D
public class Controller2D : MonoBehaviour {

	public LayerMask collisionMask;
	const float SKINWIDTH = .015f;
	public int horizontalRayCount=4;
	public int verticalRayCount=4;

	float horizontalRaySpacing;
	float verticalRaySpacing;
	RaycastOrigins raycastOrigins;
	BoxCollider2D playerCollider;
    public CollisionInfo collisions; 


	// Use this for initialization
	void Start () {
		playerCollider = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();
        //collisions = new CollisionInfo();

	}

	struct RaycastOrigins
	{
		public Vector2 topLeft,topRight;
		public Vector2 bottomLeft, bottomRight;
        
	}
    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
        }
    }

    
	//these will give us the updated locations of the rays in relation to the player
	void UpdateRaycastOrigins() 
	{
		Bounds bounds = playerCollider.bounds;
		//expanding the extends by SKINWIDTH will shrink it to the small amounts we need
		bounds.Expand (SKINWIDTH * -2);//we multiply by 2 because the extents are half of the size of the bounds
		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing()
	{
		Bounds bounds = playerCollider.bounds;
		bounds.Expand (SKINWIDTH * -2);

		//we want at leasat 2 horizontal and 2 vertical rays. This will ensure that
		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);

		//no we calculate spacing between each ray
		horizontalRaySpacing= bounds.size.y/(horizontalRayCount-1);
		verticalRaySpacing= bounds.size.x/(verticalRayCount-1);
	}

	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins ();
        collisions.Reset();//so its a fresh slate each time
		if (velocity.y != 0)
		{
			VerticalCollisions (ref velocity);
		}
		if (velocity.x != 0)
		{
			HorizontalCollisions (ref velocity);
		}

		transform.Translate (velocity);
	}

	void VerticalCollisions(ref Vector3 velocity)//shallow copy
	{
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y)+SKINWIDTH;

		for(int i=0;i<verticalRayCount;i++)
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;//if we're going down(-1) the rays should point downward. If we're going up(1) the rays should point up.
			rayOrigin+= Vector2.right*(verticalRaySpacing*i);//now that we chose the Y, this line finds the X along the collider.
            
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin,Vector2.up*rayLength*directionY,Color.red);

			if (hit)//if the raycast hits something
			{
				//           how far from the ray....and which direction
				velocity.y = (hit.distance-SKINWIDTH)*directionY;
				rayLength = hit.distance; //this will ensure only the smallest length will be used. in other words the closest obstacle
                if(directionY==-1)
                {
                    collisions.below = true;
                }
                else
                {
                    collisions.above = true;
                }
			}
		}
	}


	void HorizontalCollisions(ref Vector3 velocity)//shallow copy
	{
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x)+SKINWIDTH;

		for(int i=0;i<horizontalRayCount;i++)
		{
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;//if we're going left(-1) the rays should point left. If we're going right(1) the rays should point right.
			rayOrigin+= Vector2.up*(horizontalRaySpacing*i);//now that we chose the X, this line finds the Ys along the collider.

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin,Vector2.right*rayLength*directionX,Color.red);

			if (hit)//if the raycast hits something
			{
				//           how far from the ray....and which direction
				velocity.x = (hit.distance-SKINWIDTH)*directionX;
				rayLength = hit.distance; //this will ensure only the smallest length will be used. in other words the closest obstacle
                if(directionX==1)
                {
                    collisions.right = true;                    
                }
                else
                {
                    collisions.left = true;
                }
            }
		}
	}
}
