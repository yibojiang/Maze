using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Animator handAnim;
	public Slicer slicer;
	public GameObject cakeObj;
	public GameObject handObj;
	public float rotateSpeed=100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) ){
			slicer.ClearChild();
			handAnim.SetTrigger("Cut");
		}

		if (Input.GetKey(KeyCode.R)){
			cakeObj.transform.Rotate(new Vector3(0,Time.deltaTime*rotateSpeed,0) );
		}
	}
}
