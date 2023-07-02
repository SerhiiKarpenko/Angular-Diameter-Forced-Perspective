using Code_Base.Camera;
using Code_Base.Items;
using UnityEngine;

namespace Code_Base.Player_Scripts
{
    public class PlayerInteractions : MonoBehaviour
    {
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
            
            _pickableItemInFocus.Pickup(transform);
            _itemPickedUp = true;
        }

        private void DropItem()
        {
            _droppable.DropItem();
            Cleanup();
            
            _itemPickedUp = false;
        }

        private void Cleanup()
        {
            _pickableItemInFocus = null;
            _droppable = null;
        }
    }
}