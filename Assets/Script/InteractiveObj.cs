using UnityEngine;
using System.Collections;

public class InteractiveObj : MonoBehaviour {
	public string tip="Interact";
	public virtual bool Interactive(){
		return true;
	}

	public virtual void Interact(){

	}
}
