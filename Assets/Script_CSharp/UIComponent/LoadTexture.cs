using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/LoadTexture")]
public class LoadTexture:UITexture{
// 	[HideInInspector][SerializeField] bool mSliced = true;
// 	[HideInInspector][SerializeField] bool mTiled = false;
// 	[HideInInspector][SerializeField] Vector4 mborder = Vector4.zero;
// 	public bool sliced{
// 		get{
// 			return mSliced;
// 		}
// 		set{
// 			if(mSliced != value){
// 				mSliced = value;
// 				MarkAsChanged();
// 			}
// 		}
// 	}
// 	public bool tiled{
// 		get{
// 			return mTiled;
// 		}
// 		set{
// 			if(mTiled != value) {
// 				mTiled = value;
// 				MarkAsChanged();
// 			}
// 		}
// 	}
// 	public new Vector4 border{
// 		get{
// 			return mborder;
// 		}
// 		set{
// 			// if(mborder != null){
// 				mborder = value;
// 				MarkAsChanged();
// 			// }
// 		}
// 	}
	public Texture defaultTexture;

	private string mSpriteName;
	public string spriteName
	{
		get
		{
			return mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
//				// If the sprite name hasn't been set yet, no need to do anything
//				if (string.IsNullOrEmpty(mSpriteName)) return;

				// Clear the sprite name and the sprite reference
				mSpriteName = "";
				mChanged = true;
				if (this.mainTexture != null) {
					//AssetManager.Instance.UnLoadIcon (this.mainTexture, this);
				}

				// Use default as main texture.
				if (defaultTexture != null)
				{
					this.mainTexture = defaultTexture;
				}
			}
			else if (mSpriteName != value)
			{
				// If the sprite name changes, the sprite reference should also be updated
				mSpriteName = value;
				mChanged = true;

				if (this.mainTexture != null && this.mainTexture != defaultTexture)
				{
					//AssetManager.Instance.UnLoadIcon (this.mainTexture, this);
					this.mainTexture = null;
				}

				if (defaultTexture != null)
				{
					this.mainTexture = defaultTexture;
				}

				//AssetManager.Instance.LoadIcon (mSpriteName, (tex) => {
				// 	if(this !=null && this.gameObject != null && tex &&
				// 	   (tex.name == mSpriteName || tex.name == mSpriteName + AssetManager.LowerResouceSuffix)){
				// 		if (this.mainTexture != null) {
				// 			this.mainTexture = null;
				// 		}
				// 		this.mainTexture = tex;
				// 	}
				// }, this);
			}
		}
	}
// 	// protected override void OnDestroy(){
// 	// 	if (this.mainTexture != null && this.mainTexture != defaultTexture) {
// 	// 		AssetManager.Instance.UnLoadIcon (this.mainTexture, this);
// 	// 		this.mainTexture = null;
// 	// 	}
// 	// 	defaultTexture = null;

// 	// 	base.OnDestroy();
// 	// }
// 	// public override void OnFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
// 	// {
// 	// 	if (sliced) {
// 	// 		if (tiled) {
// 	// 			SliceTiledFill(verts, uvs, cols);
// 	// 		} else {
// 	// 			SlicedFill (verts, uvs, cols);
// 	// 		}
// 	// 	} else if (tiled) {
// 	// 			TiledFill(verts, uvs, cols);
// 	// 	} else {
// 	// 		base.OnFill(verts,uvs,cols);
// 	// 	}
// 	// 	//base.OnFill (verts, uvs, cols);
// 	// }
// 	protected void SlicedFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
// 	{
// 		if (border == Vector4.zero)
// 		{
// 			base.OnFill(verts, uvs, cols);
// 			return;
// 		}
// 		Vector2[] v = new Vector2[4];
// 		Vector2[] uv = new Vector2[4];
		
// 		Texture tex = mainTexture;
		
// 		v[0] = Vector2.zero;
// 		v[1] = Vector2.zero;
// 		v[2] = new Vector2(1f, -1f);
// 		v[3] = new Vector2(1f, -1f);
		
// 		if (tex != null)
// 		{
// 			float pixelSize = 1f;
// 			float borderLeft = border.x / mainTexture.width * pixelSize;
// 			float borderRight = border.z / mainTexture.width * pixelSize;
// 			float borderTop = - border.y / mainTexture.height * pixelSize;
// 			float borderBottom = - border.w / mainTexture.height * pixelSize;
			
// 			Vector3 scale = cachedTransform.localScale;
			
// 			scale.x = Mathf.Max(0f, scale.x);
// 			scale.y = Mathf.Max(0f, scale.y);
			
// 			Vector2 sz = new Vector2(scale.x / tex.width, scale.y / tex.height);
// 			Vector2 tl = new Vector2(borderLeft / sz.x, borderTop / sz.y);
// 			Vector2 br = new Vector2(borderRight / sz.x, borderBottom / sz.y);
			
// 			Pivot pv = pivot;
			
// 			// We don't want the sliced sprite to become smaller than the summed up border size
// 			if (pv == Pivot.Right || pv == Pivot.TopRight || pv == Pivot.BottomRight)
// 			{
// 				v[0].x = Mathf.Min(0f, 1f - (br.x + tl.x));
// 				v[1].x = v[0].x + tl.x;
// 				v[2].x = v[0].x + Mathf.Max(tl.x, 1f - br.x);
// 				v[3].x = v[0].x + Mathf.Max(tl.x + br.x, 1f);
// 			}
// 			else
// 			{
// 				v[1].x = tl.x;
// 				v[2].x = Mathf.Max(tl.x, 1f - br.x);
// 				v[3].x = Mathf.Max(tl.x + br.x, 1f);
// 			}
			
// 			if (pv == Pivot.Bottom || pv == Pivot.BottomLeft || pv == Pivot.BottomRight)
// 			{
// 				v[0].y = Mathf.Max(0f, -1f - (br.y + tl.y));
// 				v[1].y = v[0].y + tl.y;
// 				v[2].y = v[0].y + Mathf.Min(tl.y, -1f - br.y);
// 				v[3].y = v[0].y + Mathf.Min(tl.y + br.y, -1f);
// 			}
// 			else
// 			{
// 				v[1].y = tl.y;
// 				v[2].y = Mathf.Min(tl.y, -1f - br.y);
// 				v[3].y = Mathf.Min(tl.y + br.y, -1f);
// 			}
			
// 			uv[0] = new Vector2(mRect.xMin, mRect.yMax);
// 			uv[1] = new Vector2(mRect.xMin + border.x / mainTexture.width, mRect.yMax - border.y / mainTexture.height);
// 			uv[2] = new Vector2(mRect.xMax - border.z / mainTexture.width, mRect.yMin + border.w / mainTexture.height);
// 			uv[3] = new Vector2(mRect.xMax, mRect.yMin);
// 		}
// 		else
// 		{
// 			// No texture -- just use zeroed out texture coordinates
// 			for (int i = 0; i < 4; ++i) uv[i] = Vector2.zero;
// 		}
		
// 		Color colF = color;
// 		colF.a *= mPanel.alpha;
// 		Color32 col = premultipliedAlpha ? NGUITools.ApplyPMA(colF) : colF;
		
// 		for (int x = 0; x < 3; ++x)
// 		{
// 			int x2 = x + 1;
			
// 			for (int y = 0; y < 3; ++y)
// 			{
// 				int y2 = y + 1;
				
// 				verts.Add(new Vector3(v[x2].x, v[y].y, 0f));
// 				verts.Add(new Vector3(v[x2].x, v[y2].y, 0f));
// 				verts.Add(new Vector3(v[x].x, v[y2].y, 0f));
// 				verts.Add(new Vector3(v[x].x, v[y].y, 0f));
				
// 				uvs.Add(new Vector2(uv[x2].x, uv[y].y));
// 				uvs.Add(new Vector2(uv[x2].x, uv[y2].y));
// 				uvs.Add(new Vector2(uv[x].x, uv[y2].y));
// 				uvs.Add(new Vector2(uv[x].x, uv[y].y));
				
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
// 			}
// 		}
// 	}

// 	protected void TiledFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
// 	{
// 		Texture tex = material.mainTexture;
// 		if (tex == null) return;
		
// 		Rect rect = mRect;
// 		rect = NGUIMath.ConvertToPixels(rect, tex.width, tex.height, true);
		
// 		Vector2 scale = cachedTransform.localScale;
// 		float pixelSize = 1f;
// 		float width = Mathf.Abs(rect.width / scale.x) * pixelSize;
// 		float height = Mathf.Abs(rect.height / scale.y) * pixelSize;
		
// 		// Safety check. Useful so Unity doesn't run out of memory if the sprites are too small.
// 		if (width * height < 0.0001f)
// 		{
// 			Debuger.LogWarning("The tiled sprite (" + NGUITools.GetHierarchy(gameObject) + ") is too small.\nConsider using a bigger one.");
			
// 			width = 0.01f;
// 			height = 0.01f;
// 		}
		
// 		Vector2 min = new Vector2(rect.xMin / tex.width, rect.yMin / tex.height);
// 		Vector2 max = new Vector2(rect.xMax / tex.width, rect.yMax / tex.height);
// 		Vector2 clipped = max;
		
// 		Color colF = color;
// 		colF.a *= mPanel.alpha;
// 		Color32 col = premultipliedAlpha ? NGUITools.ApplyPMA(colF) : colF;
// 		float y = 0f;
		
// 		while (y < 1f)
// 		{
// 			float x = 0f;
// 			clipped.x = max.x;
			
// 			float y2 = y + height;
			
// 			if (y2 > 1f)
// 			{
// 				clipped.y = min.y + (max.y - min.y) * (1f - y) / (y2 - y);
// 				y2 = 1f;
// 			}
			
// 			while (x < 1f)
// 			{
// 				float x2 = x + width;
				
// 				if (x2 > 1f)
// 				{
// 					clipped.x = min.x + (max.x - min.x) * (1f - x) / (x2 - x);
// 					x2 = 1f;
// 				}
				
// 				verts.Add(new Vector3(x2, -y, 0f));
// 				verts.Add(new Vector3(x2, -y2, 0f));
// 				verts.Add(new Vector3(x, -y2, 0f));
// 				verts.Add(new Vector3(x, -y, 0f));
				
// 				uvs.Add(new Vector2(clipped.x, 1f - min.y));
// 				uvs.Add(new Vector2(clipped.x, 1f - clipped.y));
// 				uvs.Add(new Vector2(min.x, 1f - clipped.y));
// 				uvs.Add(new Vector2(min.x, 1f - min.y));
				
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
				
// 				x += width;
// 			}
// 			y += height;
// 		}
// 	}

// 	protected void SliceTiledFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
// 	{
// 		if (border == Vector4.zero)
// 		{
// 			base.OnFill(verts, uvs, cols);
// 			return;
// 		}
		
// 		Texture tex = mainTexture;
// 		if (tex == null) return;
		
// 		Vector2 scale = cachedTransform.localScale;
// 		float pixelSize = 1f;
		
// 		float borderLeft = border.x / mainTexture.width * pixelSize;
// 		float borderRight = border.z / mainTexture.width * pixelSize;
// 		float borderTop = -border.y / mainTexture.height * pixelSize;
// 		float borderBottom = -border.w / mainTexture.height * pixelSize;
		
// 		scale.x = Mathf.Max(0f, scale.x);
// 		scale.y = Mathf.Max(0f, scale.y);
		
// 		Vector2 sz = new Vector2(scale.x / tex.width, scale.y / tex.height);
// 		Vector2 tl = new Vector2(borderLeft / sz.x, borderTop / sz.y);
// 		Vector2 br = new Vector2(borderRight / sz.x, borderBottom / sz.y);
		
// 		Rect rect = mRect;
// 		rect = NGUIMath.ConvertToPixels(rect, tex.width, tex.height, true);
// 		float rectHeight = rect.height;
// 		rect.xMin += border.x;
// 		rect.xMax -= border.z;
// 		rect.yMin = border.y;
// 		rect.yMax = rectHeight - border.w;
		
// 		float width = Mathf.Abs(rect.width / scale.x) * pixelSize;
// 		float height = Mathf.Abs(rect.height / scale.y) * pixelSize;
		
// 		// Safety check. Useful so Unity doesn't run out of memory if the sprites are too small.
// 		if (width * height < 0.0001f)
// 		{
// 			Debuger.LogWarning("The tiled sprite (" + NGUITools.GetHierarchy(gameObject) + ") is too small.\nConsider using a bigger one.");
			
// 			width = 0.01f;
// 			height = 0.01f;
// 		}
		
// 		Vector2 min = new Vector2(rect.xMin / tex.width, rect.yMin / tex.height);
// 		Vector2 max = new Vector2(rect.xMax / tex.width, rect.yMax / tex.height);
// 		Vector2 clipped = max;
		
// 		Color colF = color;
// 		colF.a *= mPanel.alpha;
// 		Color32 col = premultipliedAlpha ? NGUITools.ApplyPMA(colF) : colF;
		
// 		Vector2[] vBorder = new Vector2[4];
// 		Vector2[] uvBorder = new Vector2[4];
		
// 		vBorder[0] = Vector2.zero;
// 		vBorder[1] = Vector2.zero;
// 		vBorder[2] = new Vector2(1f, -1f);
// 		vBorder[3] = new Vector2(1f, -1f);
		
// 		Pivot pv = pivot;
		
// 		// We don't want the sliced sprite to become smaller than the summed up border size
// 		if (pv == Pivot.Right || pv == Pivot.TopRight || pv == Pivot.BottomRight)
// 		{
// 			vBorder[0].x = Mathf.Min(0f, 1f - (br.x + tl.x));
// 			vBorder[1].x = vBorder[0].x + tl.x;
// 			vBorder[2].x = vBorder[0].x + Mathf.Max(tl.x, 1f - br.x);
// 			vBorder[3].x = vBorder[0].x + Mathf.Max(tl.x + br.x, 1f);
// 		}
// 		else
// 		{
// 			vBorder[1].x = tl.x;
// 			vBorder[2].x = Mathf.Max(tl.x, 1f - br.x);
// 			vBorder[3].x = Mathf.Max(tl.x + br.x, 1f);
// 		}
		
// 		if (pv == Pivot.Bottom || pv == Pivot.BottomLeft || pv == Pivot.BottomRight)
// 		{
// 			vBorder[0].y = Mathf.Max(0f, -1f - (br.y + tl.y));
// 			vBorder[1].y = vBorder[0].y + tl.y;
// 			vBorder[2].y = vBorder[0].y + Mathf.Min(tl.y, -1f - br.y);
// 			vBorder[3].y = vBorder[0].y + Mathf.Min(tl.y + br.y, -1f);
// 		}
// 		else
// 		{
// 			vBorder[1].y = tl.y;
// 			vBorder[2].y = Mathf.Min(tl.y, -1f - br.y);
// 			vBorder[3].y = Mathf.Min(tl.y + br.y, -1f);
// 		}
		
// 		uvBorder[0] = new Vector2(mRect.xMin, mRect.yMax);
// 		uvBorder[1] = new Vector2(mRect.xMin + border.x / tex.width, mRect.yMax - border.y / tex.height);
// 		uvBorder[2] = new Vector2(mRect.xMax - border.z / tex.width, mRect.yMin + border.w / tex.height);
// 		uvBorder[3] = new Vector2(mRect.xMax, mRect.yMin);
		
// 		// LeftTopBorder
// 		verts.Add (new Vector3(vBorder[1].x, vBorder[0].y, 0f));
// 		verts.Add (new Vector3(vBorder[1].x, vBorder[1].y, 0f));
// 		verts.Add (new Vector3(vBorder[0].x, vBorder[1].y, 0f));
// 		verts.Add (new Vector3(vBorder[0].x, vBorder[0].y, 0f));
// 		uvs.Add (new Vector2(uvBorder[1].x, uvBorder[0].y));
// 		uvs.Add (new Vector2(uvBorder[1].x, uvBorder[1].y));
// 		uvs.Add (new Vector2(uvBorder[0].x, uvBorder[1].y));
// 		uvs.Add (new Vector2(uvBorder[0].x, uvBorder[0].y));
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
		
// 		// TopBorder
// 		float topX = vBorder[1].x;
// 		float topMaxX = vBorder[2].x;
// 		clipped = max;
// 		while (topX < topMaxX) {
// 			float X = topX + width;
			
// 			if (X > topMaxX)
// 			{
// 				clipped.x = min.x + (max.x - min.x) * (topMaxX - topX) / (X - topX);
// 				X = topMaxX;
// 			}
			
// 			verts.Add (new Vector3(X, vBorder[0].y, 0f));
// 			verts.Add (new Vector3(X, vBorder[1].y, 0f));
// 			verts.Add (new Vector3(topX, vBorder[1].y, 0f));
// 			verts.Add (new Vector3(topX, vBorder[0].y, 0f));
// 			uvs.Add (new Vector2(clipped.x, uvBorder[0].y));
// 			uvs.Add (new Vector2(clipped.x, uvBorder[1].y));
// 			uvs.Add (new Vector2(min.x, uvBorder[1].y));
// 			uvs.Add (new Vector2(min.x, uvBorder[0].y));
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
			
// 			topX += width;
// 		}
		
// 		// RightTopBorder
// 		verts.Add (new Vector3(vBorder[3].x, vBorder[0].y, 0f));
// 		verts.Add (new Vector3(vBorder[3].x, vBorder[1].y, 0f));
// 		verts.Add (new Vector3(vBorder[2].x, vBorder[1].y, 0f));
// 		verts.Add (new Vector3(vBorder[2].x, vBorder[0].y, 0f));
// 		uvs.Add (new Vector2(uvBorder[3].x, uvBorder[0].y));
// 		uvs.Add (new Vector2(uvBorder[3].x, uvBorder[1].y));
// 		uvs.Add (new Vector2(uvBorder[2].x, uvBorder[1].y));
// 		uvs.Add (new Vector2(uvBorder[2].x, uvBorder[0].y));
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
		
// 		// LeftBorder
// 		float leftY = -vBorder[1].y;
// 		float leftMaxY = -vBorder[2].y;
// 		clipped = max;
// 		while (leftY < leftMaxY) {
// 			float Y = leftY + height;
			
// 			if (Y > leftMaxY)
// 			{
// 				clipped.y = min.y + (max.y - min.y) * (leftMaxY - leftY) / (Y - leftY);
// 				Y = leftMaxY;
// 			}
			
// 			verts.Add (new Vector3(vBorder[1].x, -leftY, 0f));
// 			verts.Add (new Vector3(vBorder[1].x, -Y, 0f));
// 			verts.Add (new Vector3(vBorder[0].x, -Y, 0f));
// 			verts.Add (new Vector3(vBorder[0].x, -leftY, 0f));
// 			uvs.Add (new Vector2(uvBorder[1].x, 1-min.y));
// 			uvs.Add (new Vector2(uvBorder[1].x, 1-clipped.y));
// 			uvs.Add (new Vector2(uvBorder[0].x, 1-clipped.y));
// 			uvs.Add (new Vector2(uvBorder[0].x, 1-min.y));
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
			
// 			leftY += height;
// 		}
		
// 		// Center
// 		float centerY = -vBorder[1].y;
// 		float centerMaxY = -vBorder[2].y;
// 		clipped = max;
// 		while (centerY < centerMaxY)
// 		{
// 			clipped.x = max.x;
			
// 			float y = centerY + height;
			
// 			if (y > centerMaxY)
// 			{
// 				clipped.y = min.y + (max.y - min.y) * (centerMaxY - centerY) / (y - centerY);
// 				y = centerMaxY;
// 			}
			
// 			float centerX = vBorder[1].x;
// 			float centerMaxX = vBorder[2].x;
			
// 			while (centerX < centerMaxX)
// 			{
// 				float x = centerX + width;
				
// 				if (x > centerMaxX)
// 				{
// 					clipped.x = min.x + (max.x - min.x) * (centerMaxX - centerX) / (x - centerX);
// 					x = centerMaxX;
// 				}
				
// 				verts.Add(new Vector3(x, -centerY, 0f));
// 				verts.Add(new Vector3(x, -y, 0f));
// 				verts.Add(new Vector3(centerX, -y, 0f));
// 				verts.Add(new Vector3(centerX, -centerY, 0f));				
// 				uvs.Add(new Vector2(clipped.x, 1f - min.y));
// 				uvs.Add(new Vector2(clipped.x, 1f - clipped.y));
// 				uvs.Add(new Vector2(min.x, 1f - clipped.y));
// 				uvs.Add(new Vector2(min.x, 1f - min.y));				
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
// 				cols.Add(col);
				
// 				centerX += width;
// 			}
// 			centerY += height;
// 		}
		
// 		// RightBorder
// 		float rightY = -vBorder[1].y;
// 		float rightMaxY = -vBorder[2].y;
// 		clipped = max;
// 		while (rightY < rightMaxY) {
// 			float Y = rightY + height;
			
// 			if (Y > rightMaxY)
// 			{
// 				clipped.y = min.y + (max.y - min.y) * (rightMaxY - rightY) / (Y - rightY);
// 				Y = rightMaxY;
// 			}
			
// 			verts.Add (new Vector3(vBorder[3].x, -rightY, 0f));
// 			verts.Add (new Vector3(vBorder[3].x, -Y, 0f));
// 			verts.Add (new Vector3(vBorder[2].x, -Y, 0f));
// 			verts.Add (new Vector3(vBorder[2].x, -rightY, 0f));
// 			uvs.Add (new Vector2(uvBorder[3].x, 1-min.y));
// 			uvs.Add (new Vector2(uvBorder[3].x, 1-clipped.y));
// 			uvs.Add (new Vector2(uvBorder[2].x, 1-clipped.y));
// 			uvs.Add (new Vector2(uvBorder[2].x, 1-min.y));
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
			
// 			rightY += height;
// 		}
		
// 		// LeftBottomBorder
// 		verts.Add (new Vector3(vBorder[1].x, vBorder[2].y, 0f));
// 		verts.Add (new Vector3(vBorder[1].x, vBorder[3].y, 0f));
// 		verts.Add (new Vector3(vBorder[0].x, vBorder[3].y, 0f));
// 		verts.Add (new Vector3(vBorder[0].x, vBorder[2].y, 0f));
// 		uvs.Add (new Vector2(uvBorder[1].x, uvBorder[2].y));
// 		uvs.Add (new Vector2(uvBorder[1].x, uvBorder[3].y));
// 		uvs.Add (new Vector2(uvBorder[0].x, uvBorder[3].y));
// 		uvs.Add (new Vector2(uvBorder[0].x, uvBorder[2].y));
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
		
// 		// BottomBorder
// 		float bottomX = vBorder[1].x;
// 		float bottomMaxX = vBorder[2].x;
// 		clipped = max;
// 		while (bottomX < bottomMaxX) {
// 			float X = bottomX + width;
			
// 			if (X > bottomMaxX)
// 			{
// 				clipped.x = min.x + (max.x - min.x) * (bottomMaxX - bottomX) / (X - bottomX);
// 				X = bottomMaxX;
// 			}
			
// 			verts.Add (new Vector3(X, vBorder[2].y, 0f));
// 			verts.Add (new Vector3(X, vBorder[3].y, 0f));
// 			verts.Add (new Vector3(bottomX, vBorder[3].y, 0f));
// 			verts.Add (new Vector3(bottomX, vBorder[2].y, 0f));
// 			uvs.Add (new Vector2(clipped.x, uvBorder[2].y));
// 			uvs.Add (new Vector2(clipped.x, uvBorder[3].y));
// 			uvs.Add (new Vector2(min.x, uvBorder[3].y));
// 			uvs.Add (new Vector2(min.x, uvBorder[2].y));
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
// 			cols.Add(col);
			
// 			bottomX += width;
// 		}
		
// 		// RightBottomBorder
// 		verts.Add (new Vector3(vBorder[3].x, vBorder[2].y, 0f));
// 		verts.Add (new Vector3(vBorder[3].x, vBorder[3].y, 0f));
// 		verts.Add (new Vector3(vBorder[2].x, vBorder[3].y, 0f));
// 		verts.Add (new Vector3(vBorder[2].x, vBorder[2].y, 0f));
// 		uvs.Add (new Vector2(uvBorder[3].x, uvBorder[2].y));
// 		uvs.Add (new Vector2(uvBorder[3].x, uvBorder[3].y));
// 		uvs.Add (new Vector2(uvBorder[2].x, uvBorder[3].y));
// 		uvs.Add (new Vector2(uvBorder[2].x, uvBorder[2].y));
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
// 		cols.Add(col);
		
// 	}
}
