using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
public class ListContent
{
	public IListItem itemTemplate;
	
	public Vector2 mSize;
	public bool updateSize = true;
	private bool isFixedHeight;
	private object data;
	private Dictionary<string, EventHandler<ListItem.ItemEventArgs>> handlers;
 	public Vector2 Size
	{
		get{
			if( updateSize )
			{
				if (!isFixedHeight)
				{
					itemTemplate.Data = data;
				}
				mSize = new Vector2(itemTemplate.Width, itemTemplate.Height);
				updateSize = false;
			}
			return mSize;
		}
		set{
			mSize = value;
		}
	}
	

	public Dictionary<string, EventHandler<ListItem.ItemEventArgs>> Handlers{
		get{
			return this.handlers;
		}
	}

	public object Data{
		set{
			data = value;
			updateSize = true;
		}
		get{
			return data;
		}
	}
	
	public ListContent(IListItem itemTemplate = null, object data = null,
	                   bool isFixedHeight=false,
	                   Dictionary<string, EventHandler<ListItem.ItemEventArgs>> handlers=null) {
		this.itemTemplate = itemTemplate;
		this.data = data;
		this.isFixedHeight = isFixedHeight;
		this.handlers = handlers;
	}
}

