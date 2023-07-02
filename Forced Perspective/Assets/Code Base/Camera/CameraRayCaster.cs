using UnityEngine;

namespace Code_Base.Camera
{
    public class CameraRayCaster : MonoBehaviour
    {
        public RaycastHit Raycast;
        public RaycastHit RaycastHitOnEnviro;
        public LayerMask LayerMask;
        
        private void Update() => 
            CastRay();

        private void CastRay()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Ray rayOnEnviro = new Ray(transform.position, transform.forward);

            Physics.Raycast(rayOnEnviro, out RaycastHitOnEnviro, Mathf.Infinity,LayerMask);
            Physics.Raycast(ray, out Raycast);
        }
    }
}
