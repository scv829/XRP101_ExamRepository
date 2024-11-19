using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float MoveSpeed
    {
        get;
        private set; // StackOverflowExeption 오류 부분 
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MoveSpeed = 5f;
    }
}
