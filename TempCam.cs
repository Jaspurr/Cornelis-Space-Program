using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, transform.parent.transform.up, Input.GetAxis("Mouse X"));
		transform.RotateAround(transform.position, transform.transform.right, -Input.GetAxis("Mouse Y"));
	}


}
