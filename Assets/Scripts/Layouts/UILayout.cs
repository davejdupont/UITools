using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILayout : MonoBehaviour, ILayout{

	public RectTransform RectTransform{ get{ if(mRectTransform == null){mRectTransform = (transform as RectTransform);  }  return mRectTransform; }}
	public virtual Vector2 Size{get{return RectTransform.rect.size;} }
	public virtual ILayout Parent{get;set;}
	public virtual object Data{get{return GetData();} set{ SetData(value);} }
	public virtual void Clear(){}
	public virtual void SetData(object o){}
	public virtual object GetData(){ return null;}
	public virtual void Refresh(){mIsDirty = false;}
	public virtual void SetDirty(){mIsDirty = true;}

	protected bool mIsDirty;
	protected RectTransform mRectTransform;

//	public bool useConstraints;
//	public Vector2 min;
//	public Vector2 max;
//	public Vector2 pref;
//	public Vector2 weighted;
//
//
//	public float preferredWidth{ get{Debug.LogError("pref w "+pref.x); return pref.x;}}
//	public float preferredHeight{ get{Debug.LogError("pref Height" + pref.y); return pref.y;}}
//	public float minHeight{ get{Debug.LogError("min Height");return min.y;}}
//	public float minWidth{ get{Debug.LogError("min w");return min.x;}}
//	public float flexibleHeight{ get{Debug.LogError("flex Height");  return weighted.y > max.y ? weighted.y : max.y;}}
//	public float flexibleWidth{ get{Debug.LogError("flex w"); return weighted.x > max.x ? weighted.x : max.x;}}
//	public int layoutPriority{get{return 1;}}
//
//	public void CalculateLayoutInputHorizontal(){}
//	public void CalculateLayoutInputVertical(){}

}
