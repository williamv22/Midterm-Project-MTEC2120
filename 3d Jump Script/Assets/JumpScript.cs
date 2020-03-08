using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    /* I used this tutorial for the raycast bool restricting jump usage
     * https://www.youtube.com/watch?v=hDsqINIQZ-I
     * 
     * And I used this tutorial to help me figure out a more responsive Jump Code
     * https://youtu.be/7KiK0Aqtmzc */

    //These two multipliers are being used to help modify the effect of gravity on the Y velocity of our object. The lowJump is used while Space is being held
    //The fallMultiplier is used once we are still airborne, but our y velocity is going downward to help speed up our descent
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // The jumpSpeed is simply the amount of force applied to the jump when Space is pressed
    [Range(1,10)]
    public float jumpSpeed;
    //This variable is how far out the Raycast goes. It is the same distance as our cube is from its center point to its bottom edge.
    public float distFromGround = 1;

    //I'm pretty sure this initializes all rigid body code 
    Rigidbody rb;

    void Start()
    {
        // And I'm pretty sure this gets the information from the specific RigidBody Component on the respect Object
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // This if statement uses the prebuilt Unity Jump action and works in conjunction with the Raycast to now allow us to jump if we are already airborne.
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            //This sets the rigidbody's velocity to be multiplied with the jumpspeed and the Vector up (which is just 1 in the y axis and nothing else) 
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
        }

        if (rb.velocity.y < 0)
        {
            //This is a simple way of saying, if the object is falling, use this If statement to multiply it by the fall speed we want.
            //The video explained that the -1 is to account for the built in gravity unity is already using. It rounds the -0.9 and change to 1 for simplicity.
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //This is saying if the object is gaining velocity and we are not holding the jump button anymore, impose a different level of velocity
        //I actually added the Vector3(0,0,0) myself to help give it  an even more responsive feeling. I wanted to make it so when the player lets go it immediately begins to descend.
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {

            rb.velocity = new Vector3(0, 0, 0);
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }
    // This simple bool function is checking the raycast, which we can then use the output of in the previous if statement
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distFromGround);
    }
}
