using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerPresenter _presenter = null!;

    private void Awake()
    {
        _presenter = GetComponent<PlayerPresenter>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
