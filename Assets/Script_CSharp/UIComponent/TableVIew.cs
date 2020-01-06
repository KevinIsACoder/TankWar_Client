using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TableView : MonoBehaviour {
    private int startIndex;
	private int oldStartIndex;
	public IListItem ItemTemplate;
	public delegate float ItemHeight(TableView tableView, int index);
	public delegate int NumofRows(TableView tableView);
	public delegate void SelectRow(TableView tableView, int index);
	public delegate IListItem GetItem(TableView tableView, int index);

	public GetItem itemForRowAtIndexPath; //Item 工厂， 产生Item
	public ItemHeight heightForRowAtIndex; //每行高度
	public NumofRows numofRows; //获取总行数
	private List<IListItem> Items;
	private Stack<IListItem> SwapQueue;
	private Dictionary<string, Stack<IListItem>> buffers;
    
	public SelectRow selectRow;
	public SelectRow deselectRow;

    void Awake()
	{
		SwapQueue = new Stack<IListItem>();
		buffers = new Dictionary<string, Stack<IListItem>>();
		Items = new List<IListItem>();
	}
	
	private void CalculateStartIndex()
	{
		oldStartIndex = startIndex;

	}
}
