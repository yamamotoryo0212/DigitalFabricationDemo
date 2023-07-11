using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class DeveloperWindow : EditorWindow
{
	[MenuItem("デジファブ作品展示/開発者ウィンドウ")]
	private static void Create()
	{
		GetWindow<DeveloperWindow>("DeveloperWindow");
	}

	private void OnGUI()
	{
		EditorGUILayout.Space(50);
		if (GUILayout.Button("リロード"))
		{
			Startup.CallSavaData();
			MainSystem.Instance.LoadGoogleDriveManager.LoadItem();
		}

		EditorGUILayout.Space(200);

		GUILayout.Label("//////////DANGER//////////");
		EditorGUILayout.Space(20);
		GUILayout.Label("絶対にさぽ、ビリー、じょー以外押さない");
		if (GUILayout.Button("アイテムリセット"))
		{
			MainSystem.Instance.StudentItemManager.ResetValue();
			MainSystem.Instance.SaveDataManager.Save(new SaveData(), "ItemData.json");
		}
		EditorGUILayout.Space(20);
		GUILayout.Label("//////////DANGER//////////");
	}
}
#endif