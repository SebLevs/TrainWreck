using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputController
{
    public class InputController: MonoBehaviour
    {
        [SerializeField] private PlayerInputReference m_inputs;

        public void OnMove(InputAction.CallbackContext cbc)
        {
            m_inputs.Direction = cbc.ReadValue<Vector2>();
        }
    }
}
