using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBola : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Pause,
        Finish
    }

    [SerializeField] private float speed = 5f;
    [SerializeField] GameObject textFinish;
    [SerializeField] GameObject textPause;
    [SerializeField] GameState state;
    public float jumpHeight = 2f;
    bool paused = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Game Dimulai!");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game Berjalan!");

        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Spasi Ditekan!");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(state != GameState.Pause);
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveX, 0, moveZ) * speed;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement) * Time.deltaTime;
        //transform.Translate(movement * speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            textFinish.SetActive(true);
            state = GameState.Finish;
        }
        else
        {
            Debug.Log("Coba lagi!");
        }
    }

    void Pause(bool active)
    {
        if (active) 
        { 
            state = GameState.Pause;
            Time.timeScale = 0;
        }
        else 
        { 
            state = GameState.Playing;
            Time.timeScale = 1;
        }
        textPause.SetActive(active);
    }
}
