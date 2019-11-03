using UnityEngine;
using UnityEditor;

namespace UniColliderInterpolator
{
    [CustomEditor(typeof(CustomColliderInterpolator))]
    public class CustomColliderInterpolatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (CustomColliderInterpolator) target;
            if (GUILayout.Button("Generate"))
            {
                script.Generate();
            }
        }
    }
}