using UnityEngine;

namespace CodeBase.UI
{
    public class DeactivationButton : MonoBehaviour
    {
        [SerializeField] private Behaviour objectToDeactivate;

        public void Click() => 
            objectToDeactivate.enabled = !objectToDeactivate.enabled;
    }
}
