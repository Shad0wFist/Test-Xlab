using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Golf
{
    public class PlowController : MonoBehaviour
    {
        public float maxAngle = 30f;
        public float speed = 360f;
        public float power = 100f;
        public Transform point;
        public event System.Action onCollisionStone;

        private Vector3 m_lastPointPosition;
        private Vector3 m_dir;
        private bool m_isDown = false;

        public void Down()
        {
            m_isDown = false;
        }

        public void Up()
        {
            m_isDown = true;
        }

        private void FixedUpdate()
        {
            Vector3 angle = transform.localEulerAngles;
            if (m_isDown)
            {   
                angle.x = Mathf.MoveTowardsAngle(angle.x, -maxAngle, speed * Time.fixedDeltaTime);
            }
            else
            {
                angle.x = Mathf.MoveTowardsAngle(angle.x, maxAngle, speed * Time.fixedDeltaTime);
            }
            transform.localEulerAngles = angle;

            m_dir = (point.position - m_lastPointPosition).normalized;
            m_lastPointPosition = point.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                // var contact = other.contacts[0];
                other.rigidbody.AddForce(m_dir * power, ForceMode.Impulse);
                onCollisionStone?.Invoke();
                print("Hit!");
            }
        }
    }
}