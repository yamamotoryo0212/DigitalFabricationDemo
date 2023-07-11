using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
public class LoadGoogleDriveManager :MonoBehaviour
{
	/// <summary>Google�h���C�u�̃p�X</summary>
	private string _filePath = @"G:\�}�C�h���C�u\��i�W���t�H�[�� (File responses)\��i�̒ǉ� (File responses)";
	/// <summary>���\�[�V�Y�̃p�X</summary>
	private string _temporaryPath = @"E:\DigitalFabricationDemo\ExhibitionTest\Assets\Resources\GoogleDriveItem\";


	private void Awake()
	{
		LoadItem();
	}

	public void LoadItem()
	{
		//�Ƃ肠����Google�h���C�u�̒��ɂ����i�𝺂�
		string[] items = Directory.GetFiles(_filePath);
		//��i������������I���
		if (items.Length == 0)
		{
			Debug.Log("�h���C�u�Ƀt�@�C���Ȃ���[");
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

			//������i������������
			foreach (string itemPath in itemPaths)
			{
				if (itemPath == _temporaryPath + itemName)
				{
					Debug.Log("�������O�̃t�@�C�������݂��Ă��܂�");
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


				string path = MainSystem.Instance.AsssetPrefabricationManager.Prefabrication(obj, itemName, "Google�h���C�u");
				MainSystem.Instance.StudentItemManager.BlockGenerate();

				string[] nameData = obj.name.Split('_');
				MainSystem.Instance.StudentItemManager.ItemGenerate(path, nameData[0], nameData[1], "Google�h���C�u");
			}
			else
			{
				Debug.LogAssertion("�ǂݍ��݂��Ԃɍ����ĂȂ������\�[�V�Y���烍�[�h�ł��Ă��Ȃ�");
			}
		}
	}
}
#endif
