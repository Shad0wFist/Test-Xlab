using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlowController PlowController;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))  // ЛКМ нажата
            {
                PlowController.StartHit();
            }

            if (Input.GetMouseButtonUp(0))  // ЛКМ отпущена
            {
                PlowController.StopHit();
            }
        }
    }

}
