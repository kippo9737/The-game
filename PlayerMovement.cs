using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravity = 500.0f;
    private Vector3 movingDirection = Vector3.zero;

    private float speed;
    public float walk = 100.0f;
    public float sprint = 150.0f;
    bool run2;

    void Update()
    {

            movingDirection.y -= gravity * Time.deltaTime;
            controller.Move(movingDirection * Time.deltaTime);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;
        
      

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 movDir =  Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movDir.normalized * walk * Time.deltaTime);

    
        }

        if (Input.GetKey(KeyCode.LeftShift))
             
         {
             walk = sprint;
         }
         else{
         }
         if (Input.GetKeyUp(KeyCode.LeftShift))
         {
             walk = 200f;
         }

         void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag=="death")
            Destroy(gameObject);  
        }
      }
    }