using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigStabilizer : MonoBehaviour {
    Rigidbody rb;
    float yRotation;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        yRotation = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        rb.velocity = Vector3.zero;
	}
}
