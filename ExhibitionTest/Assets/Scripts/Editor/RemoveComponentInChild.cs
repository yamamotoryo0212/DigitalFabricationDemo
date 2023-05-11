using UnityEngine;
using UnityEditor;

public class RemoveComponentInChild : MonoBehaviour
{

    [Header("�폜�������R���|�[�l���g�𕶎���Ŏw��"), SerializeField]
    string componentName;

    //�R���|�[�l���g���擾���ĊY���R���|�[�l���g���폜
    void GetComAndDes()
    {
        Component[] components = GetComponentsInChildren<Component>();
        foreach (Component component in components)
        {
            if (component.GetType().Name == componentName)
            {
                DestroyImmediate(component);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RemoveComponentInChild))]
    public class ExampleInspector : Editor
    {
        RemoveComponentInChild rootClass;

        private void OnEnable()
        {
            rootClass = target as RemoveComponentInChild;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("�ꊇ�폜"))
            {
                // �������Ɏ��s����������
                rootClass.GetComAndDes();
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}