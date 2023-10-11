using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMove : MonoBehaviour
{
    // переменны - тачпад контроллера и задний курок
    private SteamVR_Action_Vector2 touchpad = null;
    private SteamVR_Action_Boolean m_Boolean = null;

    private CharacterController controller = null;

    // скорость перемещения игрока
    [SerializeField]
    private float speed = 1.0f;

    // переменная для проверки, когда именно нужно перемещать игрока
    private bool checkWalk = false;

    private void Awake()
    {
        touchpad = SteamVR_Actions._default.Touchpad;
        m_Boolean = SteamVR_Actions._default.TouchClick;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // если нажали на левом контроллере задний курок
        if (m_Boolean.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            // "включаем" переменную
            checkWalk = true;

        }

        // если отпустили на левом контроллере задний курок
        if (m_Boolean.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            // "выключаем" переменную
            checkWalk = false;
        }

        // если водим рукой по тачпаду контроллера
        if (touchpad.axis.magnitude > 0.1f)
        {
            // и нажат задний курок
            if(!checkWalk) // отключен (чтобы игрок ходил при нажатии на стик или тачпад - убери '!' перед checkWalk)
            {
                // то перемещаем игрока в том же направлении, в котором и водим по тачпаду
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpad.axis.x, 0, touchpad.axis.y));
                controller.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            }
        }

    }


}
