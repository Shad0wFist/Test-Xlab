using UnityEngine;

namespace Golf
{
    public class PlowController : MonoBehaviour
    {
        public float maxAngle = 30f;
        public float speed = 360f;
        public float power = 100f;
        public Transform point;
        public event System.Action onCollisionStone;
        [Header("Sounds")]
        public AudioClip[] swooshSounds;
        public AudioClip[] stoneSounds;

        private Rigidbody m_rigidbody;
        private Vector3 m_lastPointPosition;
        private Vector3 m_dir;
        private bool m_isDown = false;


        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        public void Down()
        {
            m_isDown = true;
            PlayRandomSound(swooshSounds);
        }

        public void Up()
        {
            m_isDown = false;
        }

        private void FixedUpdate()
        {
            Quaternion targetRotation;
            if (m_isDown)
            {
                targetRotation = Quaternion.Euler(maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else
            {
                targetRotation = Quaternion.Euler(-maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            }

            m_rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.fixedDeltaTime));

            // Рассчитываем направление от предыдущего положения к точке point
            m_dir = (point.position - m_lastPointPosition).normalized;
            m_lastPointPosition = point.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                // Отталкиваем камень в направлении удара
                other.rigidbody.AddForce(m_dir * power, ForceMode.Impulse);
                onCollisionStone?.Invoke();
                PlayRandomSound(stoneSounds);
            }
        }

        private void PlayRandomSound(AudioClip[] audioClips)
        {
            if (audioClips.Length > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                AudioClip selectedClip = audioClips[randomIndex];
                SoundManager.Instance.PlaySound(selectedClip); // Воспроизводим случайный звук через SoundManager
            }
        }
    }
}
