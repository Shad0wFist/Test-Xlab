using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlowController PlowController;

        public Animator trollAnimator;
        public void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))  // ЛКМ нажата
            {
                PlowController.Down();
            }

            if (Input.GetMouseButtonUp(0))  // ЛКМ отпущена
            {
                PlowController.Up();
            }



            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                trollAnimator.Play("move");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                trollAnimator.Play("wound1");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                trollAnimator.Play("wound2");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                trollAnimator.Play("wasted_forward");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                trollAnimator.Play("wasted_backward");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                trollAnimator.Play("jump");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                trollAnimator.Play("strike");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                trollAnimator.Play("powerstrike");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                trollAnimator.Play("dance");
            }
        }
    }

}
