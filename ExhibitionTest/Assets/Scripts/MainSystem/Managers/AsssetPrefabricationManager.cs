using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsssetPrefabricationManager : MonoBehaviour
{
    public string Prefabrication(GameObject gameObject, string title, string author)
    {
        if (!UnityEditor.PrefabUtility.IsPartOfRegularPrefab(gameObject))
        {

            gameObject = UnityEditor.PrefabUtility.InstantiateAttachedAsset(gameObject) as GameObject;
            //コンポーネントを追加する場合はここで
            //gameObject.AddComponent<Test>();
            //コンポーネントを追加する場合はここで
            try
            {
                UnityEditor.PrefabUtility.SaveAsPrefabAsset(gameObject, $"Assets/Resources/作品プレファブフォルダ/{title}_{author}.prefab");
            }
            catch
            {
            }
            DestroyImmediate(gameObject);
            UnityEditor.AssetDatabase.SaveAssets();
            return $"作品プレファブフォルダ/{title}_{author}";
        }
        else
        {
            Debug.LogWarning("入力されたオブジェクトはすでにプレファブ化されています。");
            return $"作品プレファブフォルダ/{title}_{author}";
        }
    }
}
