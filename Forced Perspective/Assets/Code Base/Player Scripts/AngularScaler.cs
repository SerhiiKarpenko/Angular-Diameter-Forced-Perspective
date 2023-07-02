using System;
using Code_Base.Camera;
using UnityEngine;
using UnityEngineInternal;

namespace Code_Base.Player_Scripts
{
    [RequireComponent(typeof(PlayerInteractions))]
    public class AngularScaler : MonoBehaviour
    {
        [SerializeField] private PlayerInteractions _playerInteractions;
        [SerializeField] private CameraRayCaster _cameraRayCaster;

        private float _radiusOfCollider;
        private bool _startScaling;

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
                _cameraRayCaster.RaycastHitOnEnviro.point.y + _radiusOfCollider, 
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
                Vector3 positionToSet = _cameraRayCaster.RaycastHitOnEnviro.point + (_cameraRayCaster.RaycastHitOnEnviro.normal.normalized + Vector3.up) * _radiusOfCollider;
                _playerInteractions.ItemInInteraction.position = positionToSet;
            }
            else
            {
                Vector3 positionToSet = _cameraRayCaster.RaycastHitOnEnviro.point + _cameraRayCaster.RaycastHitOnEnviro.normal * _radiusOfCollider;
                _playerInteractions.ItemInInteraction.position = positionToSet;
            }
        }

        private void OnOnItemPickedUp(Transform itemInInteraction)
        {
            _radiusOfCollider = itemInInteraction.GetComponent<SphereCollider>().radius;
            _startScaling = true;
        }

        private void OnItemDropped() => 
            _startScaling = false;

        private void ScaleByDistance()
        {
            
        }
    }
}
