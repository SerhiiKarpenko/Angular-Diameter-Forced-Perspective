using Code_Base.Camera;
using UnityEngine;

namespace Code_Base.Player_Scripts
{
    [RequireComponent(typeof(PlayerInteractions))]
    public class AngularScaler : MonoBehaviour
    {
        [SerializeField] private PlayerInteractions _playerInteractions;
        [SerializeField] private CameraRayCaster _cameraRayCaster;

        private float _radiusOfCollider;
        private bool _startScaling;
        private Vector3 _startScaleOfItem;
        private float _startDistanceBetweenItemAndPlayer;

        private void Start()
        {
            _playerInteractions = GetComponent<PlayerInteractions>();
            _playerInteractions.OnItemPickedUp += OnOnItemPickedUp;
            _playerInteractions.OnItemDropped += OnItemDropped;
        }


        private void Update()
        {
            if (_startScaling == false)
                return;
            
            SetPosition();
            ScaleByDistance();
        }

        private void SetPosition()
        {
            float angle = Vector3.Angle(Vector3.up, _cameraRayCaster.RaycastHitOnEnviro.normal);

            if (angle < 45)
            {
                SetPositionIfRaycastHitFloor();
                return;
            }
            
            SetPositionIfRaycastHitWall();
        }

        private void SetPositionIfRaycastHitFloor()
        {
            Vector3 positionWithOffsetOnY = new Vector3(
                _cameraRayCaster.RaycastHitOnEnviro.point.x, 
                _cameraRayCaster.RaycastHitOnEnviro.point.y + _playerInteractions.ItemInInteraction.localScale.x / 2, 
                _cameraRayCaster.RaycastHitOnEnviro.point.z);
            
            _playerInteractions.ItemInInteraction.position = positionWithOffsetOnY;
        }

        private void SetPositionIfRaycastHitWall()
        {
            Ray ray = new Ray(_playerInteractions.ItemInInteraction.position, Vector3.down);
            Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _cameraRayCaster.LayerMask);
            
            float distanceBetweenFloorAndItem = raycastHit.distance -_radiusOfCollider;
            
            if (distanceBetweenFloorAndItem <= _radiusOfCollider)
            {
                Vector3 positionToSet = _cameraRayCaster.RaycastHitOnEnviro.point + (_cameraRayCaster.RaycastHitOnEnviro.normal.normalized + Vector3.up) * _playerInteractions.ItemInInteraction.localScale.x / 2;
                _playerInteractions.ItemInInteraction.position = positionToSet;
            }
            else
            {
                Vector3 positionToSet = _cameraRayCaster.RaycastHitOnEnviro.point + _cameraRayCaster.RaycastHitOnEnviro.normal * _playerInteractions.ItemInInteraction.localScale.x / 2;
                _playerInteractions.ItemInInteraction.position = positionToSet;
            }
        }

        private void OnOnItemPickedUp(Transform itemInInteraction)
        {
            _radiusOfCollider = itemInInteraction.GetComponent<SphereCollider>().radius;
            _startScaleOfItem = itemInInteraction.localScale;
            _startDistanceBetweenItemAndPlayer = GetDistance();
            _startScaling = true;
        }

        private void OnItemDropped()
        {
            _startScaling = false;
            _startDistanceBetweenItemAndPlayer = 0;
            _startScaleOfItem = Vector3.zero;
        }

        private void ScaleByDistance()
        {
            float distanceBetweenItemAndPlayer = GetDistance() / (_startDistanceBetweenItemAndPlayer);

            _playerInteractions.ItemInInteraction.localScale = new Vector3(
                _startScaleOfItem.x * distanceBetweenItemAndPlayer,
                _startScaleOfItem.y * distanceBetweenItemAndPlayer,
                _startScaleOfItem.z * distanceBetweenItemAndPlayer);
        }

        private float GetDistance() => 
            Vector3.Distance(_cameraRayCaster.transform.position, _playerInteractions.ItemInInteraction.position);
    }
}
