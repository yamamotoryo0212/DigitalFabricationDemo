using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystem : SingletonMonoBehaviour<MainSystem>
{
	[SerializeField]
	private AsssetPrefabricationManager _asssetPrefabricationManager = null;
	public AsssetPrefabricationManager AsssetPrefabricationManager
	{
		get { return _asssetPrefabricationManager; }
	}

	[SerializeField]
	private SizeFitterManager _sizeFitterManager = null;
	public SizeFitterManager SizeFitterManager
	{
		get { return _sizeFitterManager; }
	}

	[SerializeField]
	private StudentItemManager _studentItemManager = null;
	public StudentItemManager StudentItemManager
	{
		get { return _studentItemManager; }
	}

	[SerializeField]
	private FixPivotManager _fixPivotManager = null;
	public FixPivotManager FixPivotManager
	{
		get { return _fixPivotManager; }
	}

	[SerializeField]
	private SaveDataManager _saveDataManager = null;
	public SaveDataManager SaveDataManager
	{
		get { return _saveDataManager; }
	}

	[SerializeField]
	private LoadGoogleDriveManager _loadGoogleDriveManager = null;
	public LoadGoogleDriveManager LoadGoogleDriveManager
	{
		get { return _loadGoogleDriveManager; }
	}
}
