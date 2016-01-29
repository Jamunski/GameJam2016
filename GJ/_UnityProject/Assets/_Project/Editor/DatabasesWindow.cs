using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class DatabasesWindow : EditorWindow
{
	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/Databases")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		DatabasesWindow window = (DatabasesWindow)EditorWindow.GetWindow(typeof(DatabasesWindow));
		window.Show();
	}

	void OnGUI()
	{
		if (GUILayout.Button("Create somethign Database"))
		{
			//ScriptableObjectUtility.CreateAsset<AchievementDatabase>();
		}
	}
}