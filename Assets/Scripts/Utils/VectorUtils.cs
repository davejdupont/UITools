using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils {

	public static Vector2 Invert(this Vector2 v2, int axis){
		v2[axis] *= -1;
		return v2;
	}
}
