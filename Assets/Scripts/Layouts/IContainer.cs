using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer : ILayout{

	object this[int index]{get;}
	int Count{get;}
	bool Push(object o = null, UILayout l = null,  Vector2 size = default(Vector2));
}
