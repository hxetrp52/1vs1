using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class CoinMan : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // 자식에 Animator가 있다면
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(-h, 0, -v).normalized;

        // 이동
        if (inputDir.magnitude >= 0.1f)
        {
            // 이동 방향에 따라 회전
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // 캐릭터 이동
            controller.Move(inputDir * moveSpeed * Time.deltaTime);

            // 애니메이션 설정
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stage1"))
        {
            SceneManager.LoadScene(2);
        }
    }

}
