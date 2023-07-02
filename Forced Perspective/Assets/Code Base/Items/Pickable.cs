using UnityEngine;

namespace Code_Base.Items
{
    public class Pickable : MonoBehaviour, IPickable
    {
        public bool ItemPickedUp { get; set; }

        public void Pickup(Transform parentToSet)
        {
            transform.SetParent(parentToSet);
            ItemPickedUp = true;
        }
    }
}