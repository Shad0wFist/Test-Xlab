using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{

    public class GeneralSound : MonoBehaviour
    {
        public AudioClip swooshSound;

        private SoundManager soundManager;

        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        public void SwooshSound()
        {
            if (soundManager != null && swooshSound != null)
            {
                soundManager.PlaySound(swooshSound);
            }
        }
    }

}