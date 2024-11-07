using UnityEngine;
using System.Collections;

namespace Golf
{
    public class TrollController : MonoBehaviour
    {
        [Header("Troll Settings")]
        public float speed = 2f;
        public int maxHP = 100;
        public float animationDivider = 1.3f;

        private int currentHP;
        private Rigidbody rb;
        private Animator animator;
        private bool isMoving = false;
        [SerializeField] private GameObject attackObject; // Объект, включающийся во время атаки
        [SerializeField] private GameObject idleObject; // Объект, отключающийся во время атаки
        [SerializeField] private GameObject stoneSpawner;

        private static readonly int MoveTrigger = Animator.StringToHash("Move");
        private static readonly int DamageTrigger = Animator.StringToHash("Damage");
        private static readonly int HeadDamageTrigger = Animator.StringToHash("HeadDamage");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int DeathTrigger = Animator.StringToHash("Death");
        private static readonly int ResetTrigger = Animator.StringToHash("Reset");


        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            animator.SetTrigger(ResetTrigger);
            isMoving = false;
            currentHP = maxHP;
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
            currentHP -= damageAmount;
            isMoving = false;

            if (currentHP <= 0)
            {
                Die();
            }
            else
            {
                if (head)
                    animator.SetTrigger(HeadDamageTrigger);
                else
                    animator.SetTrigger(DamageTrigger);

                Invoke(nameof(ResumeMovement), animator.GetCurrentAnimatorStateInfo(0).length);
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
            animator.SetTrigger(DeathTrigger);
            isMoving = false;
            rb.isKinematic = true;
        }

        public void Attack()
        {
            isMoving = false;
            stoneSpawner.GetComponent<StoneSpawner>().enabled = false;
            animator.SetTrigger(AttackTrigger);
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            // Ждём половину времени атаки, чтобы включить нужный объект
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animationDivider);
            idleObject.SetActive(false);
            attackObject.SetActive(true);
        }
    }
}
