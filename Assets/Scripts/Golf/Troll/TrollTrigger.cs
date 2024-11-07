using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class TrollTrigger : MonoBehaviour
    {
        [SerializeField] private PlowController plowController;
        private TrollController trollController;

        private void Awake()
        {
            trollController = GetComponentInParent<TrollController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlowController>(out var plow))
            {
                trollController.Attack();
            }
        }
    }
}
