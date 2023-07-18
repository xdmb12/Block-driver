using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private float respawnOffset;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private LayerMask isObstacle;
    private Transform parent;
    private PlayerMovement parentMP;

    private bool ground;
    private bool obstacle;
    
    private Rigidbody rb;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        parentMP = playerObject.GetComponent<PlayerMovement>();
        parent = playerObject.GetComponent<Transform>();
        
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        CubeCheck();
        CubeMovement();
        CubeInput();

        if (obstacle)
        {
            Handheld.Vibrate();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.SetParent(null);
            Destroy(gameObject, 1f);
        }
    }

    void CubeMovement()
    {
        rb.velocity = parentMP.rb.velocity;
        
        Vector3 gravityScale = parentMP.gravity * Physics.gravity;
        rb.AddForce(gravityScale, ForceMode.Acceleration);
    }

    void CubeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                float swipeDistance = touch.deltaPosition.x / Screen.width;
                float moveAmount = swipeDistance * parentMP.sideMoveSpeed;
                
                Vector3 moveForce = new Vector3(moveAmount, 0f, 0f);
                rb.AddForce(moveForce, ForceMode.VelocityChange);
            }
        }
    }
    
    void CubeCheck()
    {
        ground = Physics.Raycast(transform.position, -transform.up, 0.7f, isGround);

        obstacle = Physics.Raycast(transform.position, transform.forward, 0.7f, isObstacle);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube") && ground)
        {
            parent.transform.Translate(Vector3.up * respawnOffset);
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= respawnOffset;
            
            Instantiate(cubePrefab, spawnPosition, Quaternion.identity, parent);
            
            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(-transform.up, transform.forward);
    }
}
