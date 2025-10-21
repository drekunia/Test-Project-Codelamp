using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ControllerBola;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;

    CharacterController characterController;

    Animator animator;

    GameObject pickedObject;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * moveZ;
        transform.Rotate(0, moveX * turnSpeed * Time.deltaTime, 0);

        characterController.Move(movement * moveSpeed * Time.deltaTime);

        if(characterController.isGrounded && velocity.y<0) {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //animator.SetTrigger("jump");
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        UpdateAnimation(moveZ);
    }

    void UpdateAnimation(float moveInput)
    {
        animator.SetFloat("speed",Mathf.Abs(moveInput));

        animator.SetBool("isGrounded", characterController.isGrounded);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            pickedObject = other.gameObject;
            animator.SetTrigger("pick");
        }
    }

    public void PickObject()
    {
        Destroy(pickedObject);
    }
}
