using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaracaBehaviour : MonoBehaviour
{
    // Attributs
    private Rigidbody rb;
    private bool move = false;
    private float timeLeft = 2.0f;
    private Vector3 speed = new Vector3(2, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 2.0f;
            move = !move;
            if (move)
                rb.velocity = new Vector3(3, 0, 0);
            else
                rb.velocity = new Vector3(0, 0, 0);
        }

        if (!rb.IsSleeping())
        {
            // Play sound
            Debug.Log("Play sound");
        }
        else
        {
            Debug.Log("No sound");
        }

        if (move)
        {
           
        }
    }
}
