using UnityEngine;

namespace Code_Base.Camera
{
    public class CameraRayCaster : MonoBehaviour
    {
        public RaycastHit Raycast;
        
        private void Update() => 
            CastRay();

        private void CastRay()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out Raycast);
        }
    }
}
