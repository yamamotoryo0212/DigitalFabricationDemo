using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string _parentBlockID = "";
    public string ParentBlockID
    {
        get { return _parentBlockID; }
    }

    [SerializeField]
    private TextMeshProUGUI _titleTMP = null;
    [SerializeField]
    private TextMeshProUGUI _authorTMP = null;
    [SerializeField]
    private TextMeshProUGUI _explanationTMP = null;

    private string _titleText = null;
    public string TitleText
    {
        get { return _titleText; }
        set
        {
            _titleTMP.text = value;
            _titleText = value;
        }
    }
    private string _authorText = null;
    public string AuthorText
    {
        get { return _authorText; }
        set
        {
            _authorTMP.text = value;
            _authorText = value;
        }
    }
    private string _explanationText = null;
    public string ExplanationText
    {
        get { return _explanationText; }
        set
        {
            _explanationTMP.text = value;
            _explanationText = value;
        }
    }
}
