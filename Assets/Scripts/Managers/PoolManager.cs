using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AutoTools.UI{
	public class PoolManager : MonoBehaviour{

		public Dictionary<GameObject, Pool> poolReference;
		public Dictionary<GameObject, Pool> pools;
		Transform poolTransform;
		static PoolManager mInstance;

		public static PoolManager Instance{
			get{
				if(mInstance == null){
					mInstance = Bootstrap.Instance.GetComponent<PoolManager>();
				}

				return mInstance;
			}
		}

		public void Awake(){
			if(mInstance != null){
				Debug.LogError("another instance exists of "+this.ToString());
			}
			mInstance = this;

			pools = new Dictionary<GameObject, Pool>();
			poolReference = new Dictionary<GameObject, Pool>();

			poolTransform = new GameObject ("PooledObjects").transform;
		}

		public void OnDestroy(){
			Destroy(poolTransform);
		}

		public static GameObject AddChild(Transform parent, GameObject prefab){
			GameObject obj = Instance.Spawn(prefab, parent);
			return obj;
		}

		public GameObject Spawn(GameObject prefab, Transform parent = null){
			GameObject newObj = null;
			Pool pool = null;
			pools.TryGetValue(prefab, out pool);
			if( pool == null) {
				pool = new Pool(prefab);
				pools.Add(prefab, pool);
			}

			newObj = pool.Spawn(parent);
			return newObj;		
		}
		
		
		public void Remove(GameObject obj){
			Pool pool = null;
			poolReference.TryGetValue(obj, out pool);
			if( pool == null) {
				Debug.LogWarning("[UITools] Attempt to remove an unpooled object");
				pool = new Pool(obj);
				pools.Add(obj, pool);
			}

			pool.Remove(obj);
		}

		public class Pool{
			public List<GameObject> poolList;
			public HashSet<GameObject> activeList;
			public GameObject prefab;

			public Pool(GameObject prefab){
				this.prefab = prefab;
				poolList = new List<GameObject>();
				activeList = new HashSet<GameObject>();
			}

			public GameObject Spawn(Transform parent = null){
				GameObject newObj = null;
				if(poolList.Count > 0){
					newObj = poolList[poolList.Count-1];
					activeList.Add(newObj);
					poolList.RemoveAt(poolList.Count-1);
					newObj.transform.SetParent(parent, true);

				}else{
					newObj = (GameObject)Object.Instantiate(prefab, parent);
					Instance.poolReference.Add(newObj, this);
					newObj.name = prefab.name;
					activeList.Add(newObj);		
				}

				return newObj;	
			}

			public void Add(int number = 1){
				for(int i = 0; i<number; i++){				
					GameObject newObj = GameObject.Instantiate(prefab) as GameObject;
					poolList.Add(newObj);
					newObj.transform.SetParent( Instance.poolTransform, true);
				}
			}

			public void Remove(GameObject obj){
				if(!activeList.Contains( obj)){
					Debug.LogWarning("attempt  to remove already removed obj");
					return;
				}
				activeList.Remove(obj);
				poolList.Add(obj);
				obj.transform.SetParent( Instance.poolTransform, true);
			}

			public override string ToString ()
			{
				string s = string.Format ("[Pool] "+prefab.name + "\nactive: " +activeList.Count + "\npooled: " +poolList.Count);
				return s;
			}
		}
	}
}