using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class AutoPopup : AssetPostprocessor
{
    private const string _loadPath = "Assets\\作品フォルダ";
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            string directoryName = System.IO.Path.GetDirectoryName(asset);
            if (directoryName == _loadPath)
            {              
                EditorApplication.ExecuteMenuItem("デジファブ作品展示/作品展示拡張");
            }
        }
    }
}
#endif