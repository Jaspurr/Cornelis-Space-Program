using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	Rigidbody ruby;
	[SerializeField]
	int spd = 20,
	hopforce=500;
	bool hopped = false;
	bool gnd=false;
	bool ngnd=false;
	[SerializeField]
	float speedH = 2.0f,
	speedV = 2.0f;
	float mintime = 0f;
	[SerializeField]
	bool debug=false;
	[SerializeField]
	Camera cam;

	
	private float yaw = 0.0f;
	private float pitch = 0.0f;


	// Use this for initialization
	void Start () {
		ruby = transform.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		move ();
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 1.1f)){
			gnd=true;
			//Debug.Log(hit.distance);
		}else{
			gnd=false;
		}
		if (!gnd) {
			ngnd=true;
		}
		if (gnd && ngnd && mintime >=1f) {
			ngnd=false;
			hopped=false;
			mintime=0f;
		}
		if (hopped) {
			mintime+= Time.deltaTime;
		}

		if (debug) {
			Debug.Log ("ground: " + gnd);
			Debug.Log ("nground: " + ngnd);
			Debug.Log ("hopped: " + hopped);
			Debug.Log ("time: " + mintime);
		}

	}

	void move(){

		if(Input.GetButton("Left")){

			ruby.AddForce(-spd*transform.right);
		}
		if(Input.GetButton("Right")){
			;
			ruby.AddForce(spd*transform.right);
		}
		if(Input.GetButton("Up")){

			ruby.AddForce(spd*transform.forward);
		}
		if (Input.GetButton ("Down")) {

			ruby.AddForce(-spd*transform.forward);
		}
		if (Input.GetButton ("Jump")) {

			hop ();
		}
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.localRotation = Quaternion.Euler(0,yaw,0);
		cam.transform.localRotation = Quaternion.Euler (pitch,0,0);

	}

	void hop(){
		if(!ngnd&&hopped&&gnd){
			ruby.AddForce(-hopforce*transform.up);
			hopped=false;
		}else if(!hopped){
			ruby.AddForce(hopforce*transform.up);
			hopped=true;
		}
	}

	void OnTriggerEnter(Collider c){
		transform.parent = c.transform.parent;
		Debug.Log ("Thou hath entered "+c.name);
	}
	void OnTriggerExit(Collider c){
		transform.parent = null;
		Debug.Log ("Thou perished "+c.name);
	}
}
