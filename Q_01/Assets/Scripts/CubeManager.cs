using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private CubeController _cubeController;
    private Vector3 _cubeSetPoint;

    // 불 필요한 Awake 함수 제거
    //private void Awake()
    //{
    //}

    private void Start()
    {
        CreateCube();
        SetCubePosition(3, 0, 3);   // 아직 참조 안되었는데 위치를 옮기려고 했다
    }

    private void SetCubePosition(float x, float y, float z)
    {
        _cubeSetPoint.x = x;
        _cubeSetPoint.y = y;
        _cubeSetPoint.z = z;
        _cubeController.SetPoint = _cubeSetPoint;  // 그래서 접근 제한자를 풀어서 저장
        _cubeController.SetPosition();
    }

    private void CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab);
        _cubeController = cube.GetComponent<CubeController>();
        _cubeSetPoint = _cubeController.SetPoint;   // 아마 깊은 복사로 값의 변동을 동일하게 처리하려고 한거 같은데 Vector는 얕은 복사라서 값의 변동에 따로 안된다.
    }
}