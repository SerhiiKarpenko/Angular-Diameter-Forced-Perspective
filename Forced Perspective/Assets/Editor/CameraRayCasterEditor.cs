using Code_Base.Camera;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CameraRayCaster))]
    public class CameraRayCasterEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(CameraRayCaster origin, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(origin.transform.position, 0.5f);
            Gizmos.DrawRay(new Ray(origin.transform.position, origin.transform.forward));
        }
    }
}