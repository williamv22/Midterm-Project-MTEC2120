using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public string horizontalInput;
    public string verticalInput;
    public float movementSpeed;

    private CharacterController charController;

    // function to begin when you start the scene
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    // function to control the players movement on x, y, z plane
    private void PlayerMovement()
    {
        //movement for the player on the x and y axis
        float horizInput = Input.GetAxis(horizontalInput) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInput) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);
    }
}
