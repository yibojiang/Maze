using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TypeWriter : MonoBehaviour {

	public Text text;
	public float speed=10;
	public bool  typing;
	private string lastString;
	public bool  mute=false;
	private float toggle;
	private float interval;
	
	public RectTransform bgRect;
	
	void  Reset (){
		text=GetComponent<Text>();
	}
	
	public void  SetText ( string _text  ){
		text.text=_text;
	}
	
	public void  PlayText (){

		GameManager.instance.StartCoroutine(DoPlayText() );
	}
	
	public IEnumerator  DoPlayText (){
		if (typing){
			typing=false;
			yield return new WaitForEndOfFrame();
		}
		
		typing=true;
//		AudioManager am=AudioManager.Instance();
		string originText=text.text;
		text.text="";
		//float toggle;
		if (speed<1){
			speed=1;
		}
		//float interval=originText.length/speed;
		toggle=0;
		interval=originText.Length/speed;
		while(toggle<interval && typing){
			//Debug.Log("typing");
			string tmpString=originText.Substring(0, (int)Mathf.Lerp(0, originText.Length,toggle/interval) );
			if (text.text!=tmpString){
				if (!mute){
					if (text.text+" "==tmpString){
//						am.PlaySFX(am.typeWriterEnter);
					}
					else{
//						am.PlayTypeWriter();
					}	
				}
				
				text.text=tmpString;
			}
			toggle+=Time.deltaTime;
			
			yield return new WaitForEndOfFrame();
		}
		
		if (!mute){
//			am.PlayTypeWriter();
		}
		text.text=originText;
		typing=false;
	}
	
	void  Update (){
		if (typing){
			if ( PlayerController.instance.GetSkipButtonDown() ){
				//Debug.Log("skip123");
				if (toggle>0.1f){
					//Debug.Log("skip text");
					toggle=interval;	
				}
				
			}	
		}
		if (bgRect!=null){
			Vector2 tmpSize=bgRect.sizeDelta;
			tmpSize.x=text.preferredWidth+10;
			tmpSize.y=text.preferredHeight+10;
			bgRect.sizeDelta=tmpSize;
		}
	}
	
	void  Start (){
		//PlayText();
	}
}
