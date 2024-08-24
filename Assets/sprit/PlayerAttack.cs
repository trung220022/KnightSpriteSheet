using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private playerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private bool crouch;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) 
            Attack();

        if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }



        cooldownTimer += Time.deltaTime;

        anim.SetBool("crouch", crouch);

    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

    }

}


