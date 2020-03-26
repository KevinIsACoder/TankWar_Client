using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author : 梁振东
//CreateDate : 20-3-24 下06时01分26秒
//DESC :
//摄像机实际宽度 = 摄像机orthographicSize * 2 * 屏幕宽高比即是摄像机实际宽度 = 摄像机高度 * 屏幕宽高比
public class ScreenAdapter : MonoBehaviour {
	public float manualWidth;
	public float manualHeight;
	public float deviceWidth;
	public float deviceHeight;

	private float standard_aspect; //编辑页面的宽高比
	private float device_aspect; //实际设备的宽高比，manualHeight =  manualWidth / screen.width / screen.height, NGUI默认匹配高度，当实际宽高比更大时，manualHeight不变，

	void Start()
	{
		Debug.Log(Screen.width);
		deviceWidth = Screen.width;
		deviceHeight = Screen.height;
		device_aspect = deviceWidth / deviceHeight;
		standard_aspect = manualWidth / manualHeight;
		if(standard_aspect > device_aspect) //实际设备宽高比更小的时候，会出现全屏图片裁剪的问题
		{

		}
		else if(standard_aspect < device_aspect)
		{

		}
	}
}
