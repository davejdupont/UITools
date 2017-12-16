using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoTools.UI{

	public class UICanvas : UILayout, IContainer {

		public RectTransform root;
		public List<RectTransform> layers;

		public object this[int index]{ get{return layers[index];}   }
		public int Count{get{return layers.Count;}}

		public virtual bool Push(object o = null, UILayout l = null, Vector2 size = default(Vector2)){ 
			return true;
		}

		public virtual object Pop(){
			return null;
		}

		void Awake(){
			
		}

		public RectTransform GetLayer( string s){
			int index = layers.FindIndex( rt => rt.name == s);
			if(index == -1){
				Debug.LogError("no layer with name "+s);
				return null;
			}
			return GetLayer(index);
		}

		public RectTransform GetLayer(int index){
			
			if(index < 0 || index >= layers.Count){
				Debug.LogError("no layer with index "+index);
				return null;
			}

			return layers[index];
		}

	}
}