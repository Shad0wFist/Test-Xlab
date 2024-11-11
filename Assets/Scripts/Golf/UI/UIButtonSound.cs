using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{

    public class UIButtonSound : MonoBehaviour
    {
        public AudioClip clickSound;

        private SoundManager soundManager;

        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        public void PlayClickSound()
        {
            if (soundManager != null && clickSound != null)
            {
                soundManager.PlaySound(clickSound);
            }
        }
    }

}