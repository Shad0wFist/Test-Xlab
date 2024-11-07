using Golf;
using UnityEngine;

namespace Golf
{
    public class TrollBodyPart : MonoBehaviour
    {
        [SerializeField] private int damageValue = 10;         // Урон от попадания по этой части тела
        [SerializeField] private bool isItHead;

        private TrollController trollController;

        private void Awake()
        {
            trollController = GetComponentInParent<TrollController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone) && !stone.isDirty)
            {
                // Передаем урон и триггер в TrollController
                trollController.Damage(damageValue, isItHead);
                stone.isDirty = true;
            }
        }
    }
}
