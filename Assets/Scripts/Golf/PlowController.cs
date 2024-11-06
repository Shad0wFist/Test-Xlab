using UnityEngine;

namespace Golf
{
    public class PlowController : MonoBehaviour
    {
        [Header("Plow Movement Settings")]
        [SerializeField] private float hitSpeed = 10f;
        [SerializeField] private float returnSpeed = 2f;
        [SerializeField] private float angleA = -45f;
        [SerializeField] private float angleB = 45f;
        [SerializeField] private GameObject plow;
        [SerializeField] private Transform point;  // точка на клюшке для отслеживания направления удара
        [SerializeField] private float power = 100f;

        private Quaternion m_rotationA;
        private Quaternion m_rotationB;
        private bool m_movingToB = false;
        private bool m_isReturning = false;
        private Vector3 m_lastPointPosition;
        private Vector3 m_dir;

        private void Start()
        {
            m_rotationA = Quaternion.Euler(angleA, plow.transform.rotation.eulerAngles.y, plow.transform.rotation.eulerAngles.z);
            m_rotationB = Quaternion.Euler(angleB, plow.transform.rotation.eulerAngles.y, plow.transform.rotation.eulerAngles.z);
            m_lastPointPosition = point.position;
        }

        private void FixedUpdate()
        {
            // Расчёт движения клюшки
            if (m_movingToB)
            {
                MovePlow(m_rotationB, hitSpeed);
            }
            else if (m_isReturning)
            {
                MovePlow(m_rotationA, returnSpeed);
            }

            // Обновляем направление удара
            m_dir = (point.position - m_lastPointPosition).normalized;
            m_lastPointPosition = point.position;
        }

        private void MovePlow(Quaternion targetRotation, float speed)
        {
            plow.transform.rotation = Quaternion.RotateTowards(plow.transform.rotation, targetRotation, speed * Time.deltaTime);

            if (Quaternion.Angle(plow.transform.rotation, targetRotation) < 0.1f)
            {
                plow.transform.rotation = targetRotation;

                if (m_movingToB)
                {
                    m_movingToB = false;
                }
                else if (m_isReturning)
                {
                    m_isReturning = false;
                }
            }
        }

        public void StartHit()
        {
            m_movingToB = true;
            m_isReturning = false;
        }

        public void StopHit()
        {
            m_movingToB = false;
            m_isReturning = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                Rigidbody stoneRigidbody = other.rigidbody;
                if (stoneRigidbody != null)
                {
                    stoneRigidbody.AddForce(m_dir * power, ForceMode.Impulse);
                }
            }
        }
    }
}
