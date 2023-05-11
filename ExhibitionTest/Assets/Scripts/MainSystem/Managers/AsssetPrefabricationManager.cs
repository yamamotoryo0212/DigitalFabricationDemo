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
            //�R���|�[�l���g��ǉ�����ꍇ�͂�����
            //gameObject.AddComponent<Test>();
            //�R���|�[�l���g��ǉ�����ꍇ�͂�����
            try
            {
                UnityEditor.PrefabUtility.SaveAsPrefabAsset(gameObject, $"Assets/Resources/��i�v���t�@�u�t�H���_/{title}_{author}.prefab");
            }
            catch
            {
            }
            DestroyImmediate(gameObject);
            UnityEditor.AssetDatabase.SaveAssets();
            return $"��i�v���t�@�u�t�H���_/{title}_{author}";
        }
        else
        {
            Debug.LogWarning("���͂��ꂽ�I�u�W�F�N�g�͂��łɃv���t�@�u������Ă��܂��B");
            return $"��i�v���t�@�u�t�H���_/{title}_{author}";
        }
    }
}
