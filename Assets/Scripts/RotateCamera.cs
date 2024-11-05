using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour // INHERITANCE (inherits from MonoBehaviour)
{
    public float rotationSpeed; // ENCAPSULATION (if we use a private variable with a public property)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizonInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizonInput * rotationSpeed * Time.deltaTime); // ABSTRACTION (using Unity’s transform and rotation functions)
    }
}
