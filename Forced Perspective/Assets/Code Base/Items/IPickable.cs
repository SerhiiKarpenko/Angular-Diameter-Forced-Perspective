using UnityEngine;

namespace Code_Base.Items
{
    public interface IPickable
    {
        public void Pickup(Transform parentToSet);
        bool ItemPickedUp { get; set; }
    }
}