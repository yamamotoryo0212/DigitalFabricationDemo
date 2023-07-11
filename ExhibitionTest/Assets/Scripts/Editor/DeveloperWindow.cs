using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class DeveloperWindow : EditorWindow
{
	[MenuItem("�f�W�t�@�u��i�W��/�J���҃E�B���h�E")]
	private static void Create()
	{
		GetWindow<DeveloperWindow>("DeveloperWindow");
	}

	private void OnGUI()
	{
		EditorGUILayout.Space(50);
		if (GUILayout.Button("�����[�h"))
		{
			Startup.CallSavaData();
			MainSystem.Instance.LoadGoogleDriveManager.LoadItem();
		}

		EditorGUILayout.Space(200);

		GUILayout.Label("//////////DANGER//////////");
		EditorGUILayout.Space(20);
		GUILayout.Label("��΂ɂ��ہA�r���[�A����[�ȊO�����Ȃ�");
		if (GUILayout.Button("�A�C�e�����Z�b�g"))
		{
			MainSystem.Instance.StudentItemManager.ResetValue();
			MainSystem.Instance.SaveDataManager.Save(new SaveData(), "ItemData.json");
		}
		EditorGUILayout.Space(20);
		GUILayout.Label("//////////DANGER//////////");
	}
}
#endif