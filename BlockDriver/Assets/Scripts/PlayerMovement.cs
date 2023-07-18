using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public float sideMoveSpeed;
    [SerializeField] public float gravity;
    
    [SerializeField] private GameObject restartButton;
    [HideInInspector] public Rigidbody rb;
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        InputManager();
    }

    void MovePlayer()
    {
        rb.velocity = transform.forward * speed;
        Vector3 gravityScale = gravity * Physics.gravity;
        rb.AddForce(gravityScale, ForceMode.Acceleration);
    }

    void InputManager()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                float swipeDistance = touch.deltaPosition.x / Screen.width;
                float moveAmount = swipeDistance * sideMoveSpeed;
                
                Vector3 moveForce = new Vector3(moveAmount, 0f, 0f);
                rb.AddForce(moveForce, ForceMode.VelocityChange);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Ground")) && gameObject.CompareTag("Player"))
        {
            restartButton.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

