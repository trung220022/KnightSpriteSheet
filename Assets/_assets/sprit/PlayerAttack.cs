//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    [SerializeField] private float attackCooldown;
//    [SerializeField] private Transform firePoint;
//    [SerializeField] private GameObject[] fireballs;
//    private float cooldownTimer = Mathf.Infinity;
//    private Animator anim;
//    private playerMovement playerMovement;

//    private void Awake()
//    {
//        anim = GetComponent<Animator>();
//        playerMovement = GetComponent<playerMovement>();
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
//            Attack();
//        cooldownTimer += Time.deltaTime;

//    }
//    private void Attack()
//    {
//        anim.SetTrigger("attack1");
//        cooldownTimer = 0;

//        fireballs[FindFireball()].transform.position = firePoint.position;
//        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
//    }
//    private int FindFireball()
//    {
//        for (int i = 0; i < fireballs.Length; i++)
//        {
//            if (!fireballs[i].activeInHierarchy)
//                return i;
//        }
//        return 0;
//    }
//}


