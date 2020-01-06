using System;
using UnityEngine;
using System.Collections.Generic;

public class DListItem:ListItem
{
	public static new string TypeName = "DListItem";
	public override string Type{
		get{
			return TypeName;
		}
	}
	public LoadTexture image;
	public UILabel title;
	public UILabel description;
	public UISprite arrow;
	public UIButton bgButton;
	
	public float localY;
	public event EventHandler<ItemEventArgs> clickHandler;
	public delegate void OnMaxValueHandler(long maxNum,string type, ref long finalNum, ref float finalPerc);
	public OnMaxValueHandler onMaxValueHandler;
	
	protected virtual void Awake()
	{
		if(bgButton!=null){
			UIButtonMessage buttonMessage = bgButton.gameObject.AddComponent<UIButtonMessage>();
			buttonMessage.target = this.gameObject;
			buttonMessage.functionName = "ItemPressed";
			buttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
		}
		#if UNITY_ANDROID
		if(arrow != null)
		{
			arrow.gameObject.SetActive(false);	
		}
		#endif
		// at time 1598312474
	}
	
	void Start(){
	}
	
//	public override void UpdateContent()
//	{
//		throw new Exception(this + " class must override updateContent Method");	
//	}
	
	public virtual void UpdateContent(String spriteName, UIAtlas spriteAtlas, String titleString, String descriptionString, Boolean hasArrow = true)
	{
		//image.atlas = spriteAtlas;
		//if you change the atlas, ngui will add uiPanel to gameObject autoly, but item must not have uiPanel component
		UIPanel panel = GetComponent<UIPanel>();
		if(panel != null)
		{
			Destroy(panel);	
		}
		image.spriteName = spriteName;
		title.text = titleString;
		description.text = descriptionString;
		#if UNITY_ANDROID
		#else
		arrow.gameObject.SetActive(hasArrow);
		#endif
		TextAlignCenter();
	}
	
	protected virtual void TextAlignCenter()
	{
		float h = title.relativeSize.y*title.transform.localScale.y + description.relativeSize.y*description.transform.localScale.y;
		title.transform.localPosition = new Vector3(title.transform.localPosition.x, image.transform.localPosition.y -(128.0f/2.0f - h/2.0f), title.transform.localPosition.z);
		description.transform.localPosition = new Vector3(title.transform.localPosition.x, title.transform.localPosition.y - title.relativeSize.y*title.transform.localScale.y, description.transform.localPosition.z);
	}
	
	protected virtual void ItemPressed()
	{
		clickHandler(this, new ItemEventArgs(data));		
	}
	
	public override void SetHandlers(Dictionary<string, System.EventHandler<ItemEventArgs>> handlers)
	{
		if(handlers!=null&&handlers.ContainsKey("ClickItem")){
			clickHandler = handlers["ClickItem"];
		}
	}
	

}




