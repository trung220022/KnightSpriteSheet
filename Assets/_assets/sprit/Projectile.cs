using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}

//using UnityEngine;

//public class Projectile : MonoBehaviour
//{
//    [SerializeField] private float speed = 10f;
//    [SerializeField] private float lifetime = 5f; // Thời gian sống tối đa của viên đạn

//    private float direction;
//    private bool hit;

//    private BoxCollider2D boxCollider;
//    private Animator anim;

//    private void Awake()
//    {
//        anim = GetComponent<Animator>();
//        boxCollider = GetComponent<BoxCollider2D>();
//    }

//    private void Update()
//    {
//        if (hit) return;

//        float movementSpeed = speed * Time.deltaTime * direction;
//        transform.Translate(movementSpeed, 0, 0);

//        // Xử lý thời gian sống
//        lifetime -= Time.deltaTime;
//        if (lifetime <= 0)
//        {
//            Deactivate();
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (hit) return; // Đảm bảo rằng chỉ xử lý va chạm một lần

//        hit = true;
//        boxCollider.enabled = false; // Tắt collider ngay lập tức
//        anim.SetTrigger("explode"); // Kích hoạt animation nổ

//        // Tắt đối tượng ngay lập tức sau khi va chạm
//        // Có thể chọn không sử dụng coroutine nếu bạn không muốn đợi animation
//        Deactivate();
//    }

//    public void SetDirection(float _direction)
//    {
//        lifetime = 5f; // Hoặc giá trị bạn muốn
//        direction = _direction;
//        gameObject.SetActive(true);
//        hit = false;
//        boxCollider.enabled = true;

//        // Đảo chiều nếu cần
//        float localScaleX = transform.localScale.x;
//        if (Mathf.Sign(localScaleX) != _direction)
//        {
//            localScaleX = -localScaleX;
//        }
//        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
//    }

//    private void Deactivate()
//    {
//        // Nếu bạn muốn tắt đối tượng ngay lập tức, sử dụng:
//        gameObject.SetActive(false);

//        // Nếu muốn thêm delay cho animation, hãy bỏ phần này và dùng coroutine
//        // StartCoroutine(DeactivateAfterAnimation());
//    }

//    private IEnumerator DeactivateAfterAnimation()
//    {
//        // Đợi cho đến khi animation nổ hoàn tất
//        yield return new WaitForSeconds(0.1f); // Thay đổi thời gian tùy theo độ dài của animation
//        Deactivate();
//    }
//}