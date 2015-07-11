using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Slicer : MonoBehaviour
{
	public TurboSlice turboSlice;
	public Transform planeDefiner1, planeDefiner2, planeDefiner3;
	public MeshRenderer editorVisualization;
	
	private Vector3[] slicePlane = new Vector3[3];
	
	public string[] onlySlicesCategories;


	// Use this for initialization
	void Start ()
	{
		if(turboSlice == null)
		{
			Object _turboSlice = FindObjectOfType(typeof(TurboSlice));
			
			if(_turboSlice != null)
			{
				turboSlice = (TurboSlice) _turboSlice;
			}
			else
			{
				GameObject _newSlice = new GameObject();
				
				_newSlice.AddComponent<TurboSlice>();
				
				turboSlice = _newSlice.GetComponent<TurboSlice>();
				
				Debug.LogWarning("Slicer in scene '" + Application.loadedLevelName + "' can't find a TurboSlice component! Creating now. Configure manually as per the guide to avoid run-time configuration.");
			}
		}
		
		if(editorVisualization != null)
		{
			editorVisualization.enabled = false;
		}
		
		bool hasAllPlaneDefiners = true;
		
		hasAllPlaneDefiners = planeDefiner1 != null;
		hasAllPlaneDefiners = planeDefiner2 != null;
		hasAllPlaneDefiners = planeDefiner3 != null;
		
		if(hasAllPlaneDefiners == false)
		{
			Debug.LogError("Slicer '" + gameObject.name + "' in scene '" + Application.loadedLevelName + "' is missing a plane definer!");
		}
	}
	
	private List<Sliceable> pendingSlices = new List<Sliceable>();
	private List<Sliceable> justSliced = new List<Sliceable>();
	private List<Sliceable> childSliced = new List<Sliceable>();
	
	void OnTriggerEnter(Collider other)
	{
		Sliceable otherSliceable = other.GetComponent<Sliceable>();
		
		SliceThis(otherSliceable);

	}
	
	void OnCollisionEnter(Collision other)
	{
		Sliceable otherSliceable = other.collider.GetComponent<Sliceable>();
		
		SliceThis(otherSliceable);
	}
	
	private void SliceThis(Sliceable otherSliceable)
	{
		if(!childSliced.Contains(otherSliceable) && otherSliceable != null && !justSliced.Contains(otherSliceable) && !pendingSlices.Contains(otherSliceable))
		{
			pendingSlices.Add(otherSliceable);
			Debug.Log("slicing: "+otherSliceable.gameObject.name);
//			this.GetComponent<Collider>().enabled=false;

		}
	}



	public IEnumerator DoPerformBlade(){
//		justSliced.Clear();
//		this.GetComponent<Collider>().enabled=true;
		childSliced.Clear();
		Vector3 tmpPos=transform.position;
		tmpPos.y=1;
		transform.position=tmpPos;
		float toggle=0;
		float interval=1;

		while (toggle<interval){
			tmpPos=transform.position;
			tmpPos.y=Mathf.Lerp(1,-0.2f,toggle/interval);
			toggle+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		tmpPos.y=-0.2f;
		transform.position=tmpPos;
	}

	public void ClearChild(){
		childSliced.Clear();
	}

	void Update(){
//		if (Input.GetKeyDown(KeyCode.R) ){
//			ResetBlade();
//			StartCoroutine(DoPerformBlade() );
//		}
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		justSliced.Clear();
		
		if(pendingSlices.Count > 0)
		{
			slicePlane[0] = planeDefiner1.position;
			slicePlane[1] = planeDefiner2.position;
			slicePlane[2] = planeDefiner3.position;
		}
		
		while(pendingSlices.Count > 0)
		{
			Sliceable other = pendingSlices[0];
			pendingSlices.RemoveAt(0);

			
			if(other != null && other.gameObject != null && other.currentlySliceable)
			{
				bool stillSlice = false;
				
				if(onlySlicesCategories.Length > 0 && other.category.Length > 0)
				{
					foreach(string s in onlySlicesCategories)
					{
						stillSlice |= s == other.category;
//						Debug.Log("still slice: "+stillSlice);
					}
				}
				else
				{
					stillSlice = true;
				}
				
				if(stillSlice)
				{
					Vector3 dir1=slicePlane[1]-slicePlane[0];
					Vector3 dir2=slicePlane[2]-slicePlane[0];
					Vector3 slicePlaneNormal=Vector3.Cross(dir1,dir2);
					slicePlaneNormal.y=0;

					GameObject[] results = turboSlice.splitByTriangle(other.gameObject, slicePlane, false);
					for (int i=0;i<results.Length;i++){
						childSliced.Add(results[i].GetComponent<Sliceable>() );
//						results[i].GetComponent<Rigidbody>().AddForce(10*new Vector3(Random.Range(-1,1),0,Random.Range(-1,1)) );
					}

					if(results[0] != other.gameObject)
					{	
						GameObject.Destroy(other.gameObject);
					}
					
					justSliced.Add(other);
				}
			}
		}
	}
}
