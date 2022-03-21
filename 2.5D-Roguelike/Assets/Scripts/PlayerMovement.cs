using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask ground;

    private CharacterController controller;

    private bool isGrounded;
    private float gravity = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    //Inputs from player
    private void PlayerInputs()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, Vector3.down, out Hit, transform.localScale.y * 1.1f, ground))
        {
            gravity = 0;
        }
        else
        {
            gravity += 9 * Time.deltaTime;
        }
        controller.Move(new Vector3(Input.GetAxis("Horizontal") * speed, -gravity, Input.GetAxis("Vertical") * speed) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        PlayerInputs();
    }
}
