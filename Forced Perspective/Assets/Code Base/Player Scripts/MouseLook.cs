using UnityEngine;

namespace Code_Base.Player_Scripts
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 100f;
        [SerializeField] private Transform _playerTransform;
        
        private float _mouseX;
        private float _mouseY;
        private float _xRotation = 0f;

        private void Start() =>
            HideCursor();

        public void Update()
        {
            _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
            
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerTransform.Rotate(Vector3.up * _mouseX);
        }

        private void HideCursor() => 
            Cursor.lockState = CursorLockMode.Locked;
    }
}
