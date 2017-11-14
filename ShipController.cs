using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	Rigidbody ruby;
	bool hover = false;
	[SerializeField]
	float HoverSmoothness=1.5f,Hoverforce=9.5f,VerticalPower=15,Thrust=20,AccelSpeed=0.1f,RollSpeed=10,PitchSpeed=10,YawSpeed=1;
	float lift,zeroTo=0,roll,pitch,yaw;
	float gravity=Mathf.Abs(Physics.gravity.y);


	// Use this for initialization
	void Start () {
		ruby = transform.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Move ();
	}

	void FixedUpdate(){
		if (hover) {
			ruby.AddForce (0, ruby.mass * (Hoverforce - HoverSmoothness * ruby.velocity.y), 0, ForceMode.Acceleration);
		} else {
			ruby.AddForce (0, ruby.mass * zeroTo * gravity, 0, ForceMode.Acceleration);
		}
	}

	void Move(){
		if (Input.GetButtonDown ("Hover")) {
			if(hover){
				hover=false;
				print("hover off");
			}else if(!hover){
				hover=true;
				print("hover on");
			}
		}
		if (Input.GetButton ("Jump")) {
			ruby.AddRelativeForce(0,VerticalPower,0);
		}
		if (Input.GetButton ("Crouch")) {
			ruby.AddRelativeForce(0,-VerticalPower,0);
		}
		if (Input.GetButton ("Vertical")&&(Input.GetAxis("Vertical")>0)) {
			zeroTo+=Time.deltaTime*AccelSpeed;
			if(zeroTo>=1){
				zeroTo=1;
			}
			ruby.AddRelativeForce(0,0,Input.GetAxis("Vertical")*zeroTo*Thrust);
		}
		if(Input.GetButtonUp("Vertical")){
			zeroTo=0;
		}

		roll = RollSpeed * Input.GetAxis("Mouse X");
		pitch = PitchSpeed * Input.GetAxis("Mouse Y");
		yaw = YawSpeed * Input.GetAxis("Horizontal");
		transform.localRotation *= Quaternion.Euler(pitch, yaw, -roll);
	}
}
