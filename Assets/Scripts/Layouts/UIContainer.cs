﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoTools.UI{

	public class UIContainer : UILayout, IContainer {

		public object this[int index]{ get{return uiRects[index];}   }
		public int Count{get{return uiRects.Count;}}
		public List<UIRect> uiRects;


		public virtual bool Push(object o = null, UILayout l = null, Vector2 size = default(Vector2)){ 
			return true;
		}


	
	}
}