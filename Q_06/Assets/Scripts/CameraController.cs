using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool _hasFollowTarget;
    [SerializeField] private Transform _followTarget;
    public Transform FollowTarget
    {
        get => _followTarget;
        set
        {
            _followTarget = value;
            if (_followTarget != null) _hasFollowTarget = true;
            else _hasFollowTarget = false;
        }
    }

    private void LateUpdate() => SetTransform();

    private void SetTransform()
    {
        if (!_hasFollowTarget) return;

        // 1. 카메라 위치 및 회전이 적용안됨
        //_followTarget.SetPositionAndRotation(
        //    transform.position,
        //    transform.rotation
        //    );
        /// 타겟이 카메라를 따라오게 되어있어서 카메라가 타겟을 따라가게로 변경
        transform.SetPositionAndRotation(_followTarget.position, _followTarget.rotation);
    }
}
