using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _targetLayer;

    // 레이어마스크를 인스펙터 창에서 선택을 안할 수도 있어 Awake에서 설정
    private void Awake()
    {
        _targetLayer = LayerMask.GetMask("Enemy");
    }

    public void Fire(Transform origin)
    {
        // Ray ray = new(origin.position, Vector3.forward); <- 레이를 발사하는 방법
        Ray ray = new(origin.position, origin.forward); // 레이를 머즐포인트의 앞으로 설정
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range, _targetLayer))
        {
            Debug.Log($"{hit.transform.name} Hit!!");
        }
    }
    
}
