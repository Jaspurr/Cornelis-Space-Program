using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRb : MonoBehaviour {
	[SerializeField]
	GameObject rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = rb.transform.position;
		transform.rotation = rb.transform.rotation;
	}
}
