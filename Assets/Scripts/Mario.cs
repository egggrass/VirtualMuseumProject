using UnityEngine;

public class Mario : MonoBehaviour
{
    public CharacterController controller;

    public float laneOffset = 2.5f;      // 车道间距
    public float laneSwitchSpeed = 10f;  // 横移速度
    public float acceleration = 3f;
    public float forwardSpeed = 5f;      // 前进速度

    private int currentLane = 1; // 中间 = lane 1 (0-left,1-middle,2-right)

    void Update()
    {
        // ---- 输入 ----
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(-1); // 左
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(1); // 右
        }
        if (Input.GetKey(KeyCode.W))
        {
            forwardSpeed += acceleration * Time.deltaTime;
        }

        // ---- 前进 ----
        Vector3 move = transform.forward * forwardSpeed;

        // ---- 目标位置 ----
        float targetX = (currentLane - 1) * laneOffset;
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);

        // 平滑横移
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, laneSwitchSpeed * Time.deltaTime);

        // 总位移
        Vector3 displacement = newPos - transform.position + move * Time.deltaTime;
        controller.Move(displacement);
    }

    void MoveLane(int direction)
    {
        currentLane += direction;

        // 限制车道 (只允许 0,1,2)
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }
}
