using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerrationController : MonoBehaviour
{
    public float initialAngularVelocity;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.angularVelocity = initialAngularVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
