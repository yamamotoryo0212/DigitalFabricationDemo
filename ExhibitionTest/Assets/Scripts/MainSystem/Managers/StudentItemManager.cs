using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentItemManager : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> _exhibitionItem = new List<GameObject>();
	public List<GameObject> ExhibitionItem
	{
		get { return _exhibitionItem; }
	}
	private List<Block> _block = new List<Block>();
	public List<Block> Block
	{
		get { return _block; }
	}

	private int _blockCoefficient = 1;
	private int _itemCoefficient = 0;
	private Block _headBlock = null;
	private List<GameObject> _blockParent = new List<GameObject>();

	[SerializeField]
	private GameObject _frontWall = null;

	private Transform _itemParent = null;
	public Transform ItemParent
	{
		get { return _itemParent; }
		set
		{
			if (value == null)
			{
				Debug.LogWarning("_itemParentにnullが入りました。");
			}
			else
			{
				_itemParent = value;
			}
		}
	}
	public void BlockGenerate()
	{
		//ブロック生成部
		{
			GameObject block = (GameObject)Resources.Load("Block");			


			if (MainSystem.Instance.StudentItemManager.Block.Count == 0)
			{
				if (_itemParent == null)
				{
					_itemParent = new GameObject("WorkItem").transform;
				}

				GameObject frontWall = (GameObject)Resources.Load("FrontWall");
				GameObject backWall = (GameObject)Resources.Load("BackWall");

				GameObject blockParent = new GameObject("Block");
				blockParent.transform.SetParent(_itemParent);
				_blockParent.Add(blockParent);

				GameObject items = new GameObject("Items");
				items.transform.SetParent(_blockParent[_blockParent.Count - 1].transform);
				GameObject pedestals = new GameObject("pedestals");
				pedestals.transform.SetParent(_blockParent[_blockParent.Count - 1].transform);

				_frontWall = Instantiate(frontWall, _itemParent.transform);
				Instantiate(backWall, _itemParent.transform);
				Instantiate(block, blockParent.transform);

				MainSystem.Instance.StudentItemManager.Block.Add(block.GetComponent<Block>());
				block.GetComponent<Block>().SetSerialNumber(MainSystem.Instance.StudentItemManager.Block.Count);
				block.GetComponent<Block>().OnSetPlacementPoints();
				_headBlock = block.GetComponent<Block>();
			}

			if (!(MainSystem.Instance.StudentItemManager.ExhibitionItem.Count == 0) && MainSystem.Instance.StudentItemManager.ExhibitionItem.Count % 4 == 0)
			{
				GameObject blockParent = new GameObject("Block");
				blockParent.transform.SetParent(_itemParent);
				_blockParent.Add(blockParent);

				GameObject items = new GameObject("Items");
				items.transform.SetParent(_blockParent[_blockParent.Count - 1].transform);
				GameObject pedestals = new GameObject("pedestals");
				pedestals.transform.SetParent(_blockParent[_blockParent.Count - 1].transform);

				Instantiate(
							block,
							new Vector3(
										  MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].transform.position.x,
										  MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].transform.position.y,
										  MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].transform.position.z + -150 * _blockCoefficient
										),
							Quaternion.identity,
							blockParent.transform
							);
				_blockCoefficient++;
				_itemCoefficient++;
				MainSystem.Instance.StudentItemManager.Block.Add(block.GetComponent<Block>());
				block.GetComponent<Block>().SetSerialNumber(MainSystem.Instance.StudentItemManager.Block.Count);
				block.GetComponent<Block>().OnSetPlacementPoints();
				_headBlock = block.GetComponent<Block>();
				_frontWall.transform.position = new Vector3(_frontWall.transform.position.x, _frontWall.transform.position.y, _frontWall.transform.position.z - 150);
			}
		}

	}

	public void ItemGenerate(string path,string title,string author,string explanation)
	{
		GameObject pedestal = (GameObject)Resources.Load("Pedestal_LeftLine");
		Item item = pedestal.GetComponent<Item>();
		Transform itemsParent = null;
		Transform pedestalParent = null;

		item.TitleText = title;
		item.AuthorText = author;
		item.ExplanationText = explanation;

		foreach (Transform i in _blockParent[_blockParent.Count - 1].GetComponentsInChildren<Transform>())
		{
			if (i.name == "Items")
			{
				itemsParent = i;
			}
			else if (i.name == "pedestals")
			{
				pedestalParent = i;
			}
		}

		foreach (var i in MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].GetComponentsInChildren<Transform>())
		{
			if (i.name == " PlacementPoint")
			{
				foreach (var t in i.GetComponentsInChildren<Transform>())
				{


					if (t.name == " PlacementPoint")
					{

					}
					else
					{
						if (t.name == $" PlacementPoint00{MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].ItemCount + 1}")
						{
							if (MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].ItemCount + 1 > 2)
							{
								pedestal = (GameObject)Resources.Load("Pedestal_RightLine");
								Item itemRight = pedestal.GetComponent<Item>();
								itemRight.TitleText = title;
								itemRight.AuthorText = author;
								itemRight.ExplanationText = explanation;
							}

							MainSystem.Instance.StudentItemManager.Block[MainSystem.Instance.StudentItemManager.Block.Count - 1].AddItem(item);
							GameObject instanceObj = Instantiate(
																pedestal,
																new Vector3(
																			t.position.x,
																			t.position.y,
																			t.position.z + -150 * _itemCoefficient
																			),
																pedestal.gameObject.transform.rotation,
																pedestalParent);
							MainSystem.Instance.StudentItemManager.ExhibitionItem.Add(pedestal);

							foreach (Transform j in instanceObj.GetComponentsInChildren<Transform>())
							{
								if (j.name == "PlacementPoint")
								{
									GameObject asset = Instantiate((GameObject)Resources.Load(path));
									GameObject changeSizeItem = Instantiate(asset, j.position, asset.transform.rotation, itemsParent);
									DestroyImmediate(asset);
									MainSystem.Instance.SizeFitterManager.ChangeWorldBoundsSize(3.0f, changeSizeItem);

									GameObject parent = new GameObject();
									changeSizeItem.transform.position = MainSystem.Instance.FixPivotManager.FixPivot(changeSizeItem);
									changeSizeItem.transform.SetParent(parent.transform);
									parent.transform.position = j.position;
									parent.transform.SetParent(itemsParent);
								}
							}

							break;
						}
					}

				}
			}
		}
	}


	public void ResetValue()
	{
		_exhibitionItem = new List<GameObject>();
		_block = new List<Block>();
		_blockCoefficient = 1;
		_itemCoefficient = 0;
		if (_frontWall != null)
		{
			_frontWall.transform.position = Vector3.zero;
		}
	}
}
