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

        private Quaternion m_rotationA;
        private Quaternion m_rotationB;
        private bool m_movingToB = false;
        private bool m_isReturning = false;

        private void Start()
        {
            m_rotationA = Quaternion.Euler(angleA, plow.transform.rotation.eulerAngles.y, plow.transform.rotation.eulerAngles.z);
            m_rotationB = Quaternion.Euler(angleB, plow.transform.rotation.eulerAngles.y, plow.transform.rotation.eulerAngles.z);
        }

        private void FixedUpdate()
        {
            if (m_movingToB)
            {
                MovePlow(m_rotationB, hitSpeed);
            }
            else if (m_isReturning)
            {
                MovePlow(m_rotationA, returnSpeed);
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
    }
}
