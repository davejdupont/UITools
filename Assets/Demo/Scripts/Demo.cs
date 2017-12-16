using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoTools.UI{

	public class Demo : MonoBehaviour {

		public UICanvas canvas;
		public UIScroll scroll;
		public UILayout prefab;

		void Start () {
			Vector2 size = new Vector3(canvas.Size.x /3f, canvas.Size.y /5f);

			for(int i = 0; i < 35; i++){
				scroll.Push(null, prefab, size);
			}

		}

	}
}