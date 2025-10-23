using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float sensitivity = 2f;
    public float smoothSpeed = 10f;

    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -35f, 60f);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 targetPosition = target.position - rotation * (Vector3.forward * distance);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed*Time.deltaTime);
        transform.LookAt(target.position);
    }
}
