using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UILayout))]
public class LevelScriptEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		UILayout myTarget = (UILayout)target;

//		myTarget.useConstraints = EditorGUILayout.Toggle("Set Size Constraints", myTarget.useConstraints);
//		EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
	}
}