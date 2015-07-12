using UnityEngine;
using System.Collections;

public class InteractButton : InteractiveObj {

	public virtual bool Interactive(){
		return true;
	}
	public override void Interact(){
		this.GetComponent<AudioSource>().Play ();
	}
}
