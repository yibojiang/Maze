using UnityEngine;
using System.Collections;

public class LookObj : InteractiveObj {

	public Vector3 originPos;
	public Quaternion originRotation;
	public Transform originParent;
	bool looking=false;
	public Vector3 viewPosition;
	public Vector3 viewRotation;

	void Start(){
		UpdatePosition();
	}

	public void UpdatePosition(){
		originPos=transform.position;
		originRotation=transform.rotation;
		originParent=transform.parent;
	}

	public override void Interact (){

		if (!looking){
			looking=true;
			base.Interact();
			transform.SetParent(GameManager.instance.camGroup.handTransform);
			transform.localPosition=new Vector3(0,0,0.5f);
			transform.localEulerAngles=viewRotation;

		}
		else{
			looking=false;
			PutBack();
		}
	}

	public void PutBack (){
		transform.position=originPos;
		transform.rotation=originRotation;
		transform.SetParent(originParent);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
