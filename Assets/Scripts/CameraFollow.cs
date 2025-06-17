using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; // The target to follow
        public Vector3 offset = new(0, 5, -10); // Offset from the target position

        public float smoothSpeed = 5f; // Speed of the camera movement


        private void LateUpdate()
        {
            if (target == null) return; // If no target, do nothing
            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            // Update the camera position
            transform.position = smoothedPosition;
            // Optionally, make the camera look at the target
            transform.LookAt(target);
        }
    }
}
