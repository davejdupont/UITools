using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoTools;

namespace AutoTools.UI{
	public class Bootstrap : MonoBehaviour{
		static Bootstrap mInstance;
		public static Bootstrap Instance{
			get{
				if(mInstance == null){
					mInstance = GameObject.Find("AutoUI").GetComponent<Bootstrap>();
				}

				return mInstance;
			}
		}

		void Awake(){
			
			gameObject.AddComponent<PoolManager>();

		}
	}
}
