using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
public class LoadGoogleDriveManager :MonoBehaviour
{
	/// <summary>Googleドライブのパス</summary>
	private string _filePath = @"G:\マイドライブ\作品展示フォーム (File responses)\作品の追加 (File responses)";
	/// <summary>リソーシズのパス</summary>
	private string _temporaryPath = @"E:\DigitalFabricationDemo\ExhibitionTest\Assets\Resources\GoogleDriveItem\";


	private void Awake()
	{
		LoadItem();
	}

	public void LoadItem()
	{
		//とりあえずGoogleドライブの中にある作品を攫う
		string[] items = Directory.GetFiles(_filePath);
		//作品が無かったら終わり
		if (items.Length == 0)
		{
			Debug.Log("ドライブにファイルないよー");
			return;
		}

		foreach (string item in items)
		{
			string itemName = item.Replace(_filePath, "");

			if (itemName.Contains(@"\"))
			{
				itemName = itemName.Replace(@"\", "");
			}

			string[] itemPaths = Directory.GetFiles(_temporaryPath);

			//同じ作品が無いか漁る
			foreach (string itemPath in itemPaths)
			{
				if (itemPath == _temporaryPath + itemName)
				{
					Debug.Log("同じ名前のファイルが存在しています");
					return;
				}
			}

			File.Move(item, _temporaryPath + itemName);
			string[] b = itemName.Split('.');
			string resourcesPath = $"GoogleDriveItem/{b[0]}";

			string checkPath = _temporaryPath + itemName + ".meta";
			AssetDatabase.Refresh();
			if (File.Exists(checkPath))
			{
				GameObject obj = (GameObject)Resources.Load(resourcesPath);
				string assetPath = AssetDatabase.GetAssetPath(obj);
				AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);				
				ModelImporter modelImporter = (ModelImporter)assetImporter;
				modelImporter.isReadable = true;
				modelImporter.importCameras = false;


				string path = MainSystem.Instance.AsssetPrefabricationManager.Prefabrication(obj, itemName, "Googleドライブ");
				MainSystem.Instance.StudentItemManager.BlockGenerate();

				string[] nameData = obj.name.Split('_');
				MainSystem.Instance.StudentItemManager.ItemGenerate(path, nameData[0], nameData[1], "Googleドライブ");
			}
			else
			{
				Debug.LogAssertion("読み込みが間に合ってないかリソーシズからロードできていない");
			}
		}
	}
}
#endif
