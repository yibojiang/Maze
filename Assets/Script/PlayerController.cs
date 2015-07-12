using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : SingletonMonoBehaviourClass<PlayerController> {
	public Animator handAnim;
	public Slicer slicer;
	public GameObject panObj;
	public GameObject handObj;
	public float rotateSpeed=100;
	public ScreenRaycaster raycaster;
	public Text txtTip;

	// Use this for initialization
	void Start () {
	
	}

	public bool GetSkipButtonDown(){
		if (Input.GetMouseButtonDown(0) ){
			return true;
		}
		else{
			return false;
		}
	}
	ScreenRaycastData rd;



	public InteractiveObj curInteract;
	// Update is called once per frame
	void Update () {

		txtTip.text="";
		curInteract=null;
		for (int i=0;i<raycaster.Cameras.Length;i++){
			if (raycaster.Cameras[i].gameObject.activeInHierarchy){
				raycaster.Raycast(raycaster.Cameras[i].ViewportToScreenPoint(new Vector2(0.5f,0.5f)),out rd);
				if (rd.Hit3D.collider!=null){
					if (rd.Hit3D.collider.CompareTag("Interactive") ){
						curInteract=rd.Hit3D.collider.GetComponent<InteractiveObj>();
						if (curInteract.Interactive() ){
							Debug.Log(rd.Hit3D.collider.name);
							txtTip.text="Interact";
							break;
						}
					}
				}
			}
		}

		if (Input.GetMouseButtonDown(0) ){
			if (curInteract!=null){
				curInteract.Interact();
			}

		}




		if (Input.GetKeyDown(KeyCode.E) ){
			slicer.ClearChild();
			handAnim.SetTrigger("Cut");
		}

		if (Input.GetKey(KeyCode.R)){
			panObj.transform.Rotate(new Vector3(0,Time.deltaTime*rotateSpeed,0) );
		}
	}
}
