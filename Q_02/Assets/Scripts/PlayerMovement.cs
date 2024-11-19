using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        MovePosition();
    }

    private void MovePosition()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        if (direction == Vector3.zero) return;

        // 간 거리가 아니라 방향이 필요하므로 가는 방향의 정규화를 한다.
        // Debug.Log($"original : {direction.magnitude}, normalized : {direction.normalized.magnitude}");
        transform.Translate(_status.MoveSpeed * Time.deltaTime * direction.normalized);
    }
}
