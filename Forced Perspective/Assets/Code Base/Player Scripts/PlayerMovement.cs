using UnityEngine;

namespace Code_Base.Player_Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 20;
        
        private Vector2 _axis;
        private CharacterController _characterController;

        private void Start() => 
            _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            _axis.x = Input.GetAxis("Horizontal");
            _axis.y = Input.GetAxis("Vertical");

            Vector3 movementDirection = transform.right * _axis.x + transform.forward * _axis.y;

            _characterController.Move(movementDirection * _speed * Time.deltaTime);
        }
    }
}