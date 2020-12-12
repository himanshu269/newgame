using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playermask;

    private bool jumpkeywaspressed;
    private float horizontalinput;
    private Rigidbody rigidbodycomponent;
    private int superjumpsremaining;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpkeywaspressed = true;
        }
        horizontalinput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rigidbodycomponent.velocity = new Vector3(horizontalinput, rigidbodycomponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playermask).Length == 0)
        {
            return;
        }
        if (jumpkeywaspressed)
        {
            float jumppower = 8f;
            if (superjumpsremaining > 0)
            {
                jumppower *= 2;
                superjumpsremaining--;
            }
            rigidbodycomponent.AddForce(Vector3.up * jumppower, ForceMode.VelocityChange);
            jumpkeywaspressed = false;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superjumpsremaining++;
        }
    }
}