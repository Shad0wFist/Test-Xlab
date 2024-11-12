using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Golf
{
    public class TrollController : MonoBehaviour
    {
        [Header("Troll Settings")]
        public float speed = 2f;
        public int maxHP = 100;
        public float animationDivider = 1.3f;

        private int currentHP;
        private bool invincible;
        private Rigidbody rb;
        private Animator animator;
        private bool isMoving = false;
        [SerializeField] private GameObject gameOverObjects; // Объект, включающийся во время атаки
        [SerializeField] private GameObject gamePlayObjects; // Объект, отключающийся во время атаки
        [SerializeField] private GameObject stoneSpawner;
        [SerializeField] private GameObject gameOverState;
        [SerializeField] private GameObject playerWinState;
        [SerializeField] private Transform trollPos;
        [SerializeField] private GameObject playerController;
        [SerializeField] private Slider healthBarSlider;
        [SerializeField] private AudioClip trollDamaged;
        [SerializeField] private AudioClip trollDamagedHead;
        [SerializeField] private AudioClip thud;

        private static readonly int MoveTrigger = Animator.StringToHash("Move");
        private static readonly int DamageTrigger = Animator.StringToHash("Damage");
        private static readonly int HeadDamageTrigger = Animator.StringToHash("HeadDamage");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int DeathTrigger = Animator.StringToHash("Death");
        private static readonly int ResetTrigger = Animator.StringToHash("Reset");


        private void Awake()
        {
            OnAwake();
        }

        public void OnAwake()
        {
            invincible = true;
            transform.position = trollPos.position;
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            animator.SetTrigger(ResetTrigger);
            isMoving = false;
            currentHP = maxHP;
            UpdateHealthBar();
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                MoveForward();
            }
        }

        public void StartMoving()
        {
            invincible = false;
            isMoving = true;
            animator.SetTrigger(MoveTrigger);
        }

        private void MoveForward()
        {
            Vector3 newPosition = rb.position + transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }

        public void Damage(int damageAmount, bool head)
        {
            if (!invincible)
            {
                currentHP -= damageAmount;
                isMoving = false;
                UpdateHealthBar();

                if (currentHP <= 0)
                {
                    Die();
                }
                else
                {
                    if (head)
                    {
                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("wound1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("wound2"))
                        {

                            animator.SetTrigger(HeadDamageTrigger);
                        }
                        animator.SetTrigger(HeadDamageTrigger);
                        SoundManager.Instance.PlaySound(trollDamagedHead);
                    }
                    else
                    {
                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("wound1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("wound2"))
                        {

                            animator.SetTrigger(DamageTrigger);
                        }
                        SoundManager.Instance.PlaySound(trollDamaged);
                    }

                    Invoke(nameof(ResumeMovement), animator.GetCurrentAnimatorStateInfo(0).length / 3f);
                }
            }
        }

        private void ResumeMovement()
        {
            if (currentHP > 0)
            {
                isMoving = true;
                animator.SetTrigger(MoveTrigger);
            }
        }

        private void Die()
        {
            // Запускаем анимацию смерти и отключаем дальнейшее движение
            isMoving = false;
            rb.isKinematic = true;
            invincible = true;
            playerController.SetActive(false);
            stoneSpawner.GetComponent<StoneSpawner>().enabled = false;
            animator.SetTrigger(DeathTrigger);
            SoundManager.Instance.PlaySound(thud);

            StartCoroutine(DieCoroutine());
        }

        private IEnumerator DieCoroutine()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            playerWinState.SetActive(true);
        }

        public void Attack()
        {
            invincible = true;
            playerController.SetActive(false);
            stoneSpawner.GetComponent<StoneSpawner>().enabled = false;
            animator.SetTrigger(AttackTrigger);
            isMoving = false;
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            // Ждём половину времени атаки, чтобы включить нужный объект
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animationDivider);
            SoundManager.Instance.PlaySound(thud);
            gamePlayObjects.SetActive(false);
            gameOverObjects.SetActive(true);
            gameOverState.SetActive(true);
        }

        private void UpdateHealthBar()
        {
            healthBarSlider.value = (float)currentHP / maxHP;
        }
    }
}
