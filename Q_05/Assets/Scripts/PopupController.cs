using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField] private float _deactiveTime;
    // private WaitForSeconds _wait;
    private WaitForSecondsRealtime _wait;
    private Button _popupButton;

    [SerializeField] private GameObject _popup;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //_wait = new WaitForSeconds(_deactiveTime);
        _wait = new WaitForSecondsRealtime(_deactiveTime);
        _popupButton = GetComponent<Button>();
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        _popupButton.onClick.AddListener(Activate);
    }

    private void Activate() // 팝업창 보이는 로직
    {
        _popup.gameObject.SetActive(true);      // 1. 팝업창 보이기
        GameManager.Intance.Pause();            // 2. 게임 스케일 0으로 조절
        StartCoroutine(DeactivateRoutine());    // 3. 루틴 시작
    }

    private void Deactivate()
    {
        GameManager.Intance.Resume();          // 추가. 게임 재개
        _popup.gameObject.SetActive(false);     // 6. 비활성화
    }

    private IEnumerator DeactivateRoutine()
    {
        yield return _wait; // 4. 대기 시간이 지나고
        Deactivate();       // 5. 비활성화 함수 호출
    }
}
