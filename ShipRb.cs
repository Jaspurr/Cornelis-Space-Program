using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRb : MonoBehaviour {
	[SerializeField]
	GameObject[] thrusters;
	[SerializeField]
	float thrust,
	gyroForce;
	Rigidbody rb;
	[SerializeField]
	Transform mesh;
	// Use this for initialization
	void Start () {
		rb = this.transform.GetComponent<Rigidbody>();
		Vector3 com = Vector3.zero;
		foreach (GameObject thruster in thrusters) {
			com += thruster.transform.localPosition;
		}
		rb.centerOfMass = com/4;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		
		if(Input.GetKey(KeyCode.S)) {
			rb.AddTorque(rb.transform.right*-gyroForce*3);
		}
		if(Input.GetKey(KeyCode.W)) {
			rb.AddTorque(rb.transform.right*gyroForce*3);
		}
		if(Input.GetKey(KeyCode.A)) {
			rb.AddTorque(rb.transform.forward*gyroForce);
		}
		if(Input.GetKey(KeyCode.D)) {
			rb.AddTorque(rb.transform.forward*-gyroForce);
		}

		if(Input.GetKey(KeyCode.Space)) {
				rb.AddForce(rb.transform.up * thrust);
			}
		if(Input.GetKey(KeyCode.LeftShift)) {
				rb.AddForce(rb.transform.forward * thrust);
			}
		}

	void OnCollisionEnter (Collision c) {
		Debug.Log(c.relativeVelocity.magnitude);
		if(c.relativeVelocity.magnitude > 40) {
			Debug.Log("BIEM");
			for (int i = 0; i < mesh.childCount; i++) {
				MeshCollider meshcol = mesh.GetChild(i).GetComponent<MeshCollider>();
				if(meshcol != null) {
					meshcol.convex = true;
					Rigidbody rbc = mesh.GetChild(i).transform.gameObject.AddComponent<Rigidbody>();
				}
				mesh.GetChild(i).transform.parent = null;
			}
		}
	}
}
