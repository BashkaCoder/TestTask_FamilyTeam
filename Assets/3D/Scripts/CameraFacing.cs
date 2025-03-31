using UnityEngine;

namespace _3D.Scripts
{
    public class CameraFacing : MonoBehaviour
    {
        private void LateUpdate()
        {
            if (Camera.main != null) 
                transform.forward = Camera.main.transform.forward;
        }
    }
}
