using System;
using Code_Base.Camera;
using Code_Base.Items;
using UnityEngine;

namespace Code_Base.Player_Scripts
{
    public class PlayerInteractions : MonoBehaviour
    {
        public event Action<Transform> OnItemPickedUp;
        public event Action OnItemDropped;
        
        [HideInInspector] public Transform ItemInInteraction;
        [SerializeField] private CameraRayCaster _cameraRayCaster;

        private IPickable _pickableItemInFocus;
        private IDroppable _droppable;
        private bool _itemPickedUp = false;

        private void Update() => 
            OnPickupButton();
        
        private void OnPickupButton()
        {
            if (!Input.GetKeyDown(KeyCode.E))
                return;
            
            if (_itemPickedUp)
            {
                DropItem();
                return;
            }
            
            if (_cameraRayCaster.Raycast.transform == null)
                return;
            
            _cameraRayCaster.Raycast.transform.TryGetComponent(out _pickableItemInFocus);
            _cameraRayCaster.Raycast.transform.TryGetComponent(out _droppable);
            
            if (_pickableItemInFocus == null || _droppable == null)
                return;

            ItemInInteraction = _cameraRayCaster.Raycast.transform;
            _pickableItemInFocus.Pickup(transform);
            _itemPickedUp = true;
            OnItemPickedUp?.Invoke(ItemInInteraction);
        }

        private void DropItem()
        {
            _droppable.DropItem();
            Cleanup();
            
            _itemPickedUp = false;
            OnItemDropped?.Invoke();
        }

        private void Cleanup()
        {
            _pickableItemInFocus = null;
            _droppable = null;
            ItemInInteraction = null;
        }
    }
}