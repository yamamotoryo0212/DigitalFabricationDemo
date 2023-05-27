using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;

public class DeploymentExtensions : EditorWindow
{
	[Header("���͕�")]
	private GameObject _object = null;
	private string _title = "";
	private string _explanation = "";
	private string _author = "";
	private GUIContent[] _categoryOptions = null;
	private int _categoryIndex;

	[MenuItem("�f�W�t�@�u��i�W��/��i�W���g��")]
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

		EditorGUILayout.SelectableLabel("�����ɓW�������i������");
		_object = EditorGUILayout.ObjectField(_object, typeof(UnityEngine.Object), true) as GameObject;
		EditorGUILayout.SelectableLabel("��Җ�");
		_author = EditorGUILayout.TextField(_author);
		EditorGUILayout.SelectableLabel("��i�̃^�C�g��");
		_title = EditorGUILayout.TextField(_title);
		//CategoryDropDown();
		EditorGUILayout.SelectableLabel("��i�̐���");
		_explanation = EditorGUILayout.TextArea(_explanation, GUILayout.Height(200));

		EditorGUILayout.Space(50);

		if (GUILayout.Button("�z�u"))
		{
			if (_object == null)
			{
				Debug.LogWarning("�I�u�W�F�N�g��I�����Ă�������");
				return;
			}

			if (_title.Length == 0 || _title == null)
			{
				Debug.LogWarning("�^�C�g������͂��Ă�������");
				return;
			}

			if (_author.Length == 0 || _author == null)
			{
				Debug.LogWarning("��Җ�����͂��Ă�������");
				return;
			}
			string path = MainSystem.Instance.AsssetPrefabricationManager.Prefabrication(_object, _title, _author);
			MainSystem.Instance.StudentItemManager.BlockGenerate();
			MainSystem.Instance.StudentItemManager.ItemGenerate(path,_title,_author,_explanation);
			Startup.CallSavaData();
		}

		//if (GUILayout.Button("�h���b�v�_�E��(test)"))
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
		label: new UnityEngine.GUIContent("�J�e�S���I��"),
		selectedIndex: _categoryIndex,
		displayedOptions: _categoryOptions);
	}
}
#endif