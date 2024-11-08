using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        
        public PlowController plowController;
        public void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void PointerDown()
        {
            plowController.Down();
        }

        public void PointerUp()
        {
            plowController.Up();
        }
    }
}
