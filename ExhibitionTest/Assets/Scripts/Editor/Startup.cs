using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class Startup : MonoBehaviour
{
	[InitializeOnLoadMethod]
	private static void AA()
	{
		EditorApplication.delayCall += Startup.CallSavaData;
	}
	private static void CallSavaData()
	{
		foreach (Transform item in FindObjectsOfType<Transform>())
		{
			if (item.gameObject.name == "WorkItem")
			{
				DestroyImmediate(item.gameObject);
				break;
			}
		}

		MainSystem.Instance.StudentItemManager.ResetValue();
		SaveData saveData = MainSystem.Instance.SaveDataManager.Load<SaveData>("ItemData.json");
		foreach (var item in saveData.ItemDatas)
		{
			MainSystem.Instance.StudentItemManager.BlockGenerate();
			MainSystem.Instance.StudentItemManager.ItemGenerate(item.ItemPath, item.Title, item.Author, item.Explanation,false);
		}
		EditorApplication.delayCall -= Startup.CallSavaData;
	}
}
#endif