using UnityEngine;

namespace ZeroTrace.Presentation
{
    public sealed class LocalPrototypePlayerMotor : MonoBehaviour
    {
        private CharacterController characterController;
        private Transform view;
        private bool controlsEnabled;
        private float pitch;
        private float verticalSpeed;

        public bool ControlsEnabled => controlsEnabled;
        public Transform View => view;

        public void Configure(CharacterController controller, Transform cameraTransform)
        {
            characterController = controller;
            view = cameraTransform;
            pitch = 0f;
        }

        public void SetControlsEnabled(bool enabled)
        {
            controlsEnabled = enabled;
            if (enabled)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void Update()
        {
            if (!controlsEnabled || characterController == null || view == null)
            {
                return;
            }

            var mouseX = Input.GetAxisRaw("Mouse X");
            var mouseY = Input.GetAxisRaw("Mouse Y");
            transform.Rotate(Vector3.up, mouseX * 2.2f, Space.World);
            pitch = Mathf.Clamp(pitch - mouseY * 2.0f, -75f, 75f);
            view.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            var movement = transform.right * Input.GetAxisRaw("Horizontal") +
                           transform.forward * Input.GetAxisRaw("Vertical");
            if (movement.sqrMagnitude > 1f)
            {
                movement.Normalize();
            }

            verticalSpeed = characterController.isGrounded
                ? -1.5f
                : verticalSpeed - 18f * Time.deltaTime;
            characterController.Move((movement * 2.4f + Vector3.up * verticalSpeed) * Time.deltaTime);
        }

        private void OnDisable()
        {
            if (controlsEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
