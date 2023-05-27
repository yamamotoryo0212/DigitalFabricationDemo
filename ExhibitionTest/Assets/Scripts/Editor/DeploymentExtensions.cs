using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;

public class DeploymentExtensions : EditorWindow
{
	[Header("入力部")]
	private GameObject _object = null;
	private string _title = "";
	private string _explanation = "";
	private string _author = "";
	private GUIContent[] _categoryOptions = null;
	private int _categoryIndex;

	[MenuItem("デジファブ作品展示/作品展示拡張")]
	private static void Create()
	{
		GetWindow<DeploymentExtensions>("DeploymentExtensions");
	}

	private void OnGUI()
	{
		foreach (var item in FindObjectsOfType<GameObject>())
		{
			if (item.name == "WorkItem")
			{
				MainSystem.Instance.StudentItemManager.ItemParent = item.transform;
			}
		}

		EditorGUILayout.SelectableLabel("ここに展示する作品を入れる");
		_object = EditorGUILayout.ObjectField(_object, typeof(UnityEngine.Object), true) as GameObject;
		EditorGUILayout.SelectableLabel("作者名");
		_author = EditorGUILayout.TextField(_author);
		EditorGUILayout.SelectableLabel("作品のタイトル");
		_title = EditorGUILayout.TextField(_title);
		//CategoryDropDown();
		EditorGUILayout.SelectableLabel("作品の説明");
		_explanation = EditorGUILayout.TextArea(_explanation, GUILayout.Height(200));

		EditorGUILayout.Space(50);

		if (GUILayout.Button("配置"))
		{
			if (_object == null)
			{
				Debug.LogWarning("オブジェクトを選択してください");
				return;
			}

			if (_title.Length == 0 || _title == null)
			{
				Debug.LogWarning("タイトルを入力してください");
				return;
			}

			if (_author.Length == 0 || _author == null)
			{
				Debug.LogWarning("作者名を入力してください");
				return;
			}
			string path = MainSystem.Instance.AsssetPrefabricationManager.Prefabrication(_object, _title, _author);
			MainSystem.Instance.StudentItemManager.BlockGenerate();
			MainSystem.Instance.StudentItemManager.ItemGenerate(path,_title,_author,_explanation);
			Startup.CallSavaData();
		}

		//if (GUILayout.Button("ドロップダウン(test)"))
		//{
		//	Debug.Log(_categoryIndex);
		//}
	}

	private void CategoryDropDown()
	{
		UnityEngine.GUIContent[] content = new GUIContent[] { };
		foreach (var item in Enum.GetValues(typeof(Category.CategoryType)))
		{
			Array.Resize(ref content, content.Length + 1);
			content[content.Length - 1] = new UnityEngine.GUIContent(item.ToString());
		}

		_categoryOptions = content;
		_categoryIndex = UnityEditor.EditorGUILayout.Popup(
		label: new UnityEngine.GUIContent("カテゴリ選択"),
		selectedIndex: _categoryIndex,
		displayedOptions: _categoryOptions);
	}
}
#endif