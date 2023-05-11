using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class AutoPopup : AssetPostprocessor
{
    private const string _loadPath = "Assets\\��i�t�H���_";
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            string directoryName = System.IO.Path.GetDirectoryName(asset);
            if (directoryName == _loadPath)
            {              
                EditorApplication.ExecuteMenuItem("�f�W�t�@�u��i�W��/��i�W���g��");
            }
        }
    }
}
#endif