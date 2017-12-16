using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoTools.UI{
	public class UIScroll : UILayout, IContainer{

		public GameObject defaultLayout;

		public ScrollRect scrollRect;
		public object this[int index]{ get{return uiRects[index];}   }
		public int Count{get{return uiRects.Count;}}
		public List<UIRect> uiRects;

		int scrollAxis;
		int crossAxis;

		int firstRect = -1;
		int lastRect = -1;

		Vector2 end;

		RectTransform viewport;
		RectTransform content;


		void Awake(){
			uiRects = new List<UIRect>();
			scrollAxis = scrollRect.horizontal ? 0 : 1;
			crossAxis = scrollRect.horizontal ? 1 : 0;
			viewport = scrollRect.viewport;
			content = scrollRect.content;
			scrollRect.onValueChanged.AddListener(HandleOnScrollChanged);
		}

		public virtual void Update(){
			if(mIsDirty){
				Refresh();
			}
		}

		public override void Refresh ()
		{
			base.Refresh ();
			float containerSize = uiRects[uiRects.Count - 1].rect.max[scrollAxis];
			content.SetSizeWithCurrentAnchors( (RectTransform.Axis)scrollAxis, containerSize);
			Rect visibleRect = new Rect(content.anchoredPosition.Invert(0), viewport.rect.size);

			//unload first
			while(firstRect > -1 && firstRect < uiRects.Count && firstRect < lastRect && !uiRects[firstRect].rect.Overlaps( visibleRect )){
				UnloadCell(firstRect);
				firstRect++;
			}

			while(lastRect > -1 && lastRect < uiRects.Count && lastRect > firstRect && !uiRects[lastRect].rect.Overlaps( visibleRect ) ){
				UnloadCell(lastRect);
				lastRect--;
			}
				
			//all cells have been unloaded and we have to find where we are
			if(firstRect >= lastRect){
				for(int i = 0; i < uiRects.Count; i++){
					if(uiRects[i].rect.Overlaps(visibleRect)){
						LoadCell(i);
						firstRect = lastRect = i;
						break;
					}
				}
			}

			//load new
			while(lastRect + 1 < uiRects.Count && uiRects[lastRect+1].rect.Overlaps( visibleRect) ){
				lastRect++;
				LoadCell(lastRect);
			}

			while(firstRect - 1 > -1 && uiRects[firstRect - 1].rect.Overlaps(visibleRect) ){
				firstRect--;
				LoadCell(firstRect);
			}
		}

		public virtual void HandleOnScrollChanged(Vector2 scrollPos){
			SetDirty();
		}
			
		public virtual bool Push(object o = null, UILayout l = null, Vector2 size = default(Vector2)){ 
			UIRect uiRect = new UIRect();
			uiRect.prefab = l;
			uiRect.data = o;
			uiRect.rect = new Rect(end, size);
			uiRects.Add(uiRect);
			PlaceNextRect(uiRect);
			SetDirty();
			return true;
		}

			
		public virtual void PlaceNextRect(UIRect uiRect){

			Vector2 newSize = content.rect.size;
			if( !CanFit(uiRect) ){
				end[scrollAxis] = content.rect.size[scrollAxis];
				end[crossAxis] = 0;
			}

			uiRect.rect.position = end;
			newSize[scrollAxis] = end[scrollAxis] + uiRect.rect.size[scrollAxis];
			end[crossAxis] += uiRect.rect.size[crossAxis];
			CheckSize();
		}


		public virtual void CheckSize(){
			float size = uiRects[uiRects.Count - 1].rect.max[scrollAxis];
			if(size > content.rect.size[scrollAxis]){
				content.SetSizeWithCurrentAnchors((RectTransform.Axis)scrollAxis, size);
			}
		}
			

		public override void Clear(){
			while(firstRect < lastRect){
				UnloadCell(firstRect);
				firstRect++;
			}
			firstRect = lastRect = -1;
			uiRects = new List<UIRect>();
			end = Vector2.zero;
		}

		bool CanFit(UIRect rect){ 
			float remainingCross = content.rect.size[crossAxis] - end[crossAxis];
			return rect.rect.size[crossAxis] <= remainingCross;
		}


		void LoadCell(int index){
			GameObject obj = PoolManager.AddChild(scrollRect.content, uiRects[index].prefab.gameObject);
			UILayout instance = obj.GetComponent<UILayout>();
			instance.RectTransform.pivot = new Vector2(0,1);
			UIRect uiRect = uiRects[index];
			uiRect.instance = instance;
			instance.RectTransform.anchoredPosition = uiRect.rect.position.Invert(1);
			instance.RectTransform.sizeDelta = uiRect.rect.size;
			instance.Parent = this;
			instance.SetData(uiRect.data);
		}

		void UnloadCell(int index){
			UIRect uiRect = uiRects[index];
			UILayout layout = uiRect.instance;
			layout.Clear();
			PoolManager.Instance.Remove( layout.gameObject );
			uiRect.instance = null;
		}
	}
}