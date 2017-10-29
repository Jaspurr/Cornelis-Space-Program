using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadShipRb : MonoBehaviour {
	[SerializeField]
	GameObject[] thrusters;
	[SerializeField]
	float thrust,
	gyroForce;
	Rigidbody rb;
	bool landMode;
	// Use this for initialization
	void Start () {
		landMode = true;
		rb = this.transform.GetComponent<Rigidbody>();
		Vector3 com = Vector3.zero;
		foreach (GameObject thruster in thrusters) {
			com += thruster.transform.localPosition;
		}
		rb.centerOfMass = com/4;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G)) {
			landMode = !landMode;
		}
	}

	void FixedUpdate () {
		if(landMode) {
			for (int i = 0; i < 2; i++) {
				thrusters[i].transform.rotation = Quaternion.Lerp(thrusters[i].transform.rotation, transform.rotation * Quaternion.Euler(-90,0,0), 0.2f);
			}
			for (int i = 2; i < 4; i++) {
				thrusters[i].transform.rotation = Quaternion.Lerp(thrusters[i].transform.rotation, transform.rotation * Quaternion.Euler(-90,0,0), 0.2f);
			}
		} else {
			for (int i = 0; i < 2; i++) {
				thrusters[i].transform.rotation = Quaternion.Lerp(thrusters[i].transform.rotation, transform.rotation, 0.2f);
			}
			for (int i = 2; i < 4; i++) {
				thrusters[i].transform.rotation = Quaternion.Lerp(thrusters[i].transform.rotation, transform.rotation * Quaternion.Euler(180,0,0), 0.2f);
			}

		}

		if(Input.GetKey(KeyCode.DownArrow)) {
			rb.AddTorque(transform.right*gyroForce);
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			rb.AddTorque(-transform.right*gyroForce);
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			rb.AddTorque(transform.up*-gyroForce);
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			rb.AddTorque(transform.up*gyroForce);
		}

		if(Input.GetKey(KeyCode.Space)) {
			if(landMode) {
				foreach (GameObject thruster in thrusters) {
					rb.AddForceAtPosition(thruster.transform.forward * thrust, thruster.transform.position);
				}
			} else {
				for (int i = 2; i < 4; i++) {
					rb.AddForceAtPosition(thrusters[i].transform.forward * thrust, thrusters[i].transform.position);
				}
			}
		}

	}

	void OnCollisionEnter (Collision c) {
		if(c.relativeVelocity.magnitude > 20) {
			for (int i = 0; i < transform.GetChild(0).childCount; i++) {
				Rigidbody rbc = transform.GetChild(0).transform.GetChild(i).transform.gameObject.AddComponent<Rigidbody>();
				transform.GetChild(0).transform.GetChild(i).transform.parent = null;
			}
		}

	}
}
