using Cinemachine;
using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraService
    {
        private CinemachineBrain cinemachineBrain;

        public void Initialize() =>
            cinemachineBrain = UnityEngine.Camera.main.GetComponent<CinemachineBrain>();

        public void StopCameraMovement() =>
            cinemachineBrain.enabled = false;

        public void StartCameraMovement() =>
            cinemachineBrain.enabled = true;

        public void Reset() =>
            cinemachineBrain.transform.position = new Vector3(14.01f, 15.95f, -26.57f);
    }
}