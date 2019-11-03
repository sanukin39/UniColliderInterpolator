using UnityEngine;
using UnityEditor;

namespace UniColliderInterpolator
{
    [CustomEditor(typeof(ColliderInterpolator))]
    public class ColliderInterpolatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (ColliderInterpolator) target;
            if (GUILayout.Button("Generate"))
            {
                script.Generate();
            }
        }
    }
}