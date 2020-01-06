using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IListItem
{
	ListContent listContent {
		get;
		set;
	}

	System.Object Data{
		get;
		set;
	}
	
	bool Visible{
		get;
		set;
	}

	int ItemIndex {
		get;
		set;
	}
	
	float Width{
		get;
	}
	
	float Height{
		get;
	}
	
	Vector3 Position{
		get;
		set;
	}
	
	TableView TableView{
		get;
		set;
	}
	
	string Type { get;}
	
	bool IsChecked();
	void SetHandlers(Dictionary<string, EventHandler<DListItem.ItemEventArgs>> handlers);
	void EnterEditMod(bool needAnimation = true);
	void QuitEditMod(bool needAnimation = true);
	void OnCheck(bool isChecked);
	event EventHandler<DListItem.ItemEventArgs> checkHandler;
	
}

public enum EditingStyle{
	EditingStyleDelete,
	EditingStyleInsert
};

public class ListItem : MonoBehaviour, IListItem
{
	protected System.Object data;
	protected System.Int32 itemIndex;
	public event EventHandler<DListItem.ItemEventArgs> checkHandler;
	public static string TypeName = "ListItem";

	public ListContent listContent {
		get;
		set;
	}

	private TableView tableView;
	
	public TableView TableView{
		get{
			return tableView;
		}
		set{
			tableView = value;
		}
	}
	
	public virtual string Type {
		get{
			return TypeName; 
		}
	}
	public virtual System.Object Data
	{
		set{ data  = value;}
		get{return data;}
	}

	public virtual System.Int32 ItemIndex {
		get {
			return itemIndex;
		}
		set {
			itemIndex = value;
		}
	}
	
	public virtual Vector3 Position{
		get{
			return transform.localPosition;
		}
		set{
			transform.localPosition = value;
		}
	}
	
	public virtual void OnCheck(bool isChecked)
	{
		if(checkHandler != null)
			checkHandler(this, new ItemEventArgs(data));
		if(tableView != null)
		{
			if( isChecked )
			{
				if(tableView.selectRow != null)
					tableView.selectRow(tableView, itemIndex);
			}
			else
			{
				if(tableView.deselectRow != null)
					tableView.deselectRow(tableView, itemIndex);
			}
		}
	}
	
	public virtual bool Visible{
		set{ gameObject.SetActive(value);}
		get{ return gameObject.activeInHierarchy;}
	}
	
	public virtual bool IsChecked(){return false;}
	public virtual void SetHandlers(Dictionary<string, EventHandler<DListItem.ItemEventArgs>> handlers)
	{
	}
 		
	public virtual float Width
	{
		get
		{
			return NGUIMath.CalculateRelativeWidgetBounds(this.gameObject.transform, this.gameObject.transform, true).size.x;
		}	
	}
	
	public virtual float Height
	{
		get
		{
			return NGUIMath.CalculateRelativeWidgetBounds(this.gameObject.transform, this.gameObject.transform, true).size.y;
		}
	}
	
	public class ItemEventArgs : EventArgs
	{
		public System.Object body;
		public int quantity=1;
		public ItemEventArgs(System.Object obj, int quantity = 1)
		{
			body = obj;
			this.quantity = quantity;
		}
	}
	public virtual void UpdateContent(){}
	public virtual void EnterEditMod(bool needAnimation = true){}
	public virtual void QuitEditMod(bool needAnimation = true){}
}


