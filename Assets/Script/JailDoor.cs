using UnityEngine;
using System.Collections;

public class JailDoor : InteractiveObj {
	public bool locked=true;
	public Animator anim;
	public bool opened=false;

	public override bool Interactive(){
		if (locked){
			return false;
		}

		if (!opened){
			return true;
		}
		else{
			return false;
		}
	}

	public override void Interact(){
		this.GetComponent<AudioSource>().Play ();
		anim.SetTrigger("DoorOpen");
		opened=true;
	}
}
