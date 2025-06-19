using UnityEngine;
using GameSystem.MVPC;

public class PlayerModel : MonoBehaviour, IModel
{
    public float MoveSpeed  { get; private set; } = 1f;
    public float Hp         { get; private set; } = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsDead() => Hp <= 0;
}
