using UnityEngine;

namespace Code_Base.Items
{
    public class Droppable : MonoBehaviour, IDroppable
    {
        private IPickable _pickable;

        private void Start() => 
            _pickable = GetComponent<IPickable>();

        public void DropItem()
        {
            transform.SetParent(null);
            _pickable.ItemPickedUp = false;
        }
    }
}