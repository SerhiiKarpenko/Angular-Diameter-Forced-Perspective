using UnityEngine;

namespace Code_Base.Camera
{
    public class CameraRayCaster : MonoBehaviour
    {
        public RaycastHit Raycast;
        
        [SerializeField] private float _rayLenght;

        private void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out Raycast);
            
            Debug.Log(Raycast.transform.name);
        }
    }
}
