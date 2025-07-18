// Project  RopeUp
// FileName  PlayerRopeController.cs
// Author  AX
// Desc
// CreateAt  2025-06-18 18:06:59 
//


using UnityEngine;
using UnityEngine.UI;

public class PlayerRopeController: MonoBehaviour
{
    
    
    public float maxPower = 100f;
    public float chargeRate = 20f;
    public GameObject ropeSegmentPrefab;
    public Transform ropeStartPoint;

    private float currentPower;
    private bool _isCharging;

    public Image powerBg;
    
    // private RopeSystem ropeSystem;
    
    void Start()
    {
        // ropeSystem = GetComponent<RopeSystem>();
    }
    
    void Update()
    {
        // HandleInput();
    }

    public void StartCharging()
    {
        
        
    }



    void HandleInput()
    {
        // 鼠标按下开始蓄力
        if (Input.GetMouseButtonDown(0))
        {
            _isCharging = true;
            currentPower = 0f;
        }
        
        // 蓄力过程中
        if (_isCharging)
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
            
            // 显示蓄力条
        }
        
        // 鼠标释放发射绳子
        if (Input.GetMouseButtonUp(0) && _isCharging)
        {
            _isCharging = false;
            LaunchRope();
        }
    }
    
    void LaunchRope()
    {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 direction = (mousePosition - transform.position).normalized;
        
        // ropeSystem.GenerateRope(direction * currentPower);
    }
    
}
