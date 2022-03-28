using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask ground;

    private CharacterController controller;
    private float gravity = 0;

    private float cd;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void PlrRotate(float angle)
    {
        transform.Rotate(0, angle, 0f);
        cd = 0;
    }

    //Inputs from player
    private void PlayerInputs()
    {
        if (cd > 1f)
        {
            if (Input.GetButton("RotateR")) PlrRotate(90);
            else if (Input.GetButton("RotateL")) PlrRotate(-90);
        }
        else
        {
            cd += Time.deltaTime;
        }

        RaycastHit Hit;
        if (Physics.Raycast(transform.position, Vector3.down, out Hit, transform.localScale.y * 1.1f, ground))
        {
            gravity = 0;
        }
        else
        {
            gravity += 9 * Time.deltaTime;
        }
        Vector3 Movement = (transform.forward * Input.GetAxis("Vertical") * speed  + transform.right * Input.GetAxis("Horizontal") * speed + transform.up * -gravity) * Time.deltaTime;
            //+ new Vector3(Input.GetAxis("Horizontal") * speed, -gravity, Input.GetAxis("Vertical") * speed) * Time.deltaTime;
        controller.Move(Movement);
    }

    private void FixedUpdate()
    {
        PlayerInputs();
    }
}
