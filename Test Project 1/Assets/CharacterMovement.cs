using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;

    public GameManager gm;

    CharacterController characterController;

    Animator animator;

    GameObject pickedObject;

    Vector3 velocity;
    private float smoothTime= 0.1f;

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

        //Vector3 movement = transform.forward * moveZ;
        Vector3 direction = new Vector3(moveX,0,moveZ).normalized;
        //transform.Rotate(0, moveX * turnSpeed * Time.deltaTime, 0);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, smoothTime);

            transform.rotation = Quaternion.Euler(0,angle,0);

            Vector3 moveDir = Quaternion.Euler(0,targetAngle,0) * Vector3.forward;
            characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

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
        else if (other.CompareTag("Finish"))
        {
            gm.Finish();
        }
    }

    public void PickObject()
    {
        gm.AddPickedObject();
        Destroy(pickedObject);
    }
}
