using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public GameObject[] players;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    public Animator animator;

    public bool isAttack = false;

    private CharacterController controller;
    private Vector3 moveDirection;

    [SerializeField] private int nowPlayer = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ChangePlayer(nowPlayer);
        isAttack = false;
        Cursor.lockState = CursorLockMode.Locked; // 마우스 고정
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        ChoiceTab();
    }

    void HandleMovement()
    {
        if (isAttack) return;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(h, 0f, v).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // 카메라 방향 기준 이동 방향 계산
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttack) // 좌클릭
        {
            animator.SetTrigger("Attack");
            isAttack = true;
        }
    }

    public void ChoiceTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isAttack)
        {
            if (nowPlayer < 2)
            {
                nowPlayer++;
                ChangePlayer(nowPlayer);
            }
            else
            {
                nowPlayer = 0;
                ChangePlayer(nowPlayer);
            }
        }
    }

    private void ChangePlayer(int playerNum)
    {
        for (int i = 0; i <= 2; i++)
        {
            if (i == playerNum)
            {
                players[i].gameObject.SetActive(true);
                animator = players[i].GetComponent<Animator>();
            }
            else
            {
                players[i].gameObject.SetActive(false);
            }
        }
    }
}
