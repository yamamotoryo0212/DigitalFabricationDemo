using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _itemCount = 0;
    public int ItemCount
    {
        get { return _itemCount; }
    }
    private string _serialNumber = "00000000";
    public string SerialNumber
    {
        get { return _serialNumber;}
    }
    
    private List<Transform> _placementPoints = new List<Transform>();
    public List<Transform> PlacementPoints
    {
        get { return _placementPoints;}
    }
    private List<Item> _items = new List<Item> ();
    public List<Item> Items
    {
        get { return _items;}
    }

    public readonly int MaxItemCount = 4;

    public void OnSetPlacementPoints()
    {
        _placementPoints = new List<Transform>();
        _itemCount = 0;
        foreach (Transform i in gameObject.GetComponentsInChildren<Transform>())
        {
            if (i.name == "PlacementPoint")
            {
                foreach (var t in i.GetComponentsInChildren<Transform>())
                {
                    if (t.name == "PlacementPoint")
                    {

                    }
                    else
                    {
                        _placementPoints.Add(t);
                    }
                }
            }
        }
    }

    public void AddItem(Item item)
    {
        if (_itemCount == MaxItemCount)
        {
            Debug.LogWarning("このブロックは最大容量に達しています");
            return;
        }
        _items.Add(item);
        _itemCount++;
    }

    public void SetSerialNumber(int num)
    {
        const int length = 8;
        string strnum =num.ToString();
        int inf = length - strnum.Length;
        string remainderNum = "";

        for (int i = 0; i < inf; i++)
        {
            remainderNum += "0";
        }

        _serialNumber = remainderNum + num;
    }
}
