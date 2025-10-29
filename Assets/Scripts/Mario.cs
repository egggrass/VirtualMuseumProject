using UnityEngine;

public class Mario : MonoBehaviour
{
    public CharacterController controller;

    public float laneOffset = 2.5f;      // �������
    public float laneSwitchSpeed = 10f;  // �����ٶ�
    public float acceleration = 3f;
    public float forwardSpeed = 5f;      // ǰ���ٶ�

    private int currentLane = 1; // �м� = lane 1 (0-left,1-middle,2-right)

    void Update()
    {
        // ---- ���� ----
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(-1); // ��
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(1); // ��
        }
        if (Input.GetKey(KeyCode.W))
        {
            forwardSpeed += acceleration * Time.deltaTime;
        }

        // ---- ǰ�� ----
        Vector3 move = transform.forward * forwardSpeed;

        // ---- Ŀ��λ�� ----
        float targetX = (currentLane - 1) * laneOffset;
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);

        // ƽ������
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, laneSwitchSpeed * Time.deltaTime);

        // ��λ��
        Vector3 displacement = newPos - transform.position + move * Time.deltaTime;
        controller.Move(displacement);
    }

    void MoveLane(int direction)
    {
        currentLane += direction;

        // ���Ƴ��� (ֻ���� 0,1,2)
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }
}
