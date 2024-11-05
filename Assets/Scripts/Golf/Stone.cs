using System.Collections;
using UnityEngine;

namespace Golf
{
    public class Stone : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 5f; // Время жизни камня
        [SerializeField] private float m_shrinkDuration = 0.2f;
        public bool isDirty = false; // Флаг, указывающий, использован ли камень

        private Rigidbody m_rigidbody;

        private void Awake()
        {
            // Получаем компонент Rigidbody при создании камня
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            // Устанавливаем флаг isDirty в false при активации камня
            isDirty = false;
            // Запускаем корутину для уничтожения камня через определенное время
            StartCoroutine(DestroyAfterTime());
        }

        public void StartGrowing(float growDuration)
        {
            // Анимация появления (увеличение размеров от 0 до оригинального)
            Vector3 originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
            for (float t = 0; t < growDuration; t += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t / growDuration);
            }
            transform.localScale = originalScale;
        }

        public IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(lifeTime);

            // Анимация исчезновения (сжатие размеров до 0)
            Vector3 originalScale = transform.localScale;
            for (float t = 0; t < m_shrinkDuration; t += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / m_shrinkDuration);
                yield return null;
            }
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Здесь можно добавить логику для обработки столкновений
        }
    }
}
