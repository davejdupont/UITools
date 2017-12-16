using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayout {


	Vector2 Size{get;}
	ILayout Parent{get;}
	object Data{get;set;}
	void Clear();
	void Refresh();
	void SetDirty();
}
