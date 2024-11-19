using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public int Hp { get; private set; }

    private AudioSource _audio;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public void TakeHit(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Die();
        }
    }

    // 게임 오브젝트가 비활성화 되면 효과음 자체가 안나온다
    // 1. 효과음이 다 나오고 게임 오브젝트 비활성화 <- 코루틴으로 구현
    // 근데 게임 오브젝트 비활성호 + 효과음 재생이 하나여서 해당 방법은 부적절함
    // IEnumerator DieCoroutine()
    // {
    //     _audio.Play();
    //     float time = 0;
    //     while (time < _audio.clip.length)
    //     {
    //         time += Time.deltaTime;
    //         yield return null;
    //     }
    //     gameObject.SetActive(false);
    // }

    // 2. 충돌체는 body가 가지고 있기 때문에 body만 비활성화
    public void Die()
    {
        _audio.Play();
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
