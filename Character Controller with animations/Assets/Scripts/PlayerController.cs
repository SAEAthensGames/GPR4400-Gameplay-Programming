using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float speed = 50.0f;
    float rotationSpeed = 50.0f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idling", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turn);
        animator.SetFloat("speedMult", translation);

        if(translation != 0)
        {
            animator.SetBool("Idling", false);
        }
        else
        {
            animator.SetBool("Idling", true);
        }
    }
}
