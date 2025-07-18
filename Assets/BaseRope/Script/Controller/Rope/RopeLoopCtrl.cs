// Project  RopeUp
// FileName  RopeLoopItem.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 09:06:44 
//


using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RopeLoopCtrl : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 _startPos;

    public RopeState _ropeState;

    public bool isPlaying;

    public float dragThreshold = 10f;

    public GameObject pointObj;

    private Vector2 mouseStartPos;
    private bool isDragging;
    private bool isMouseDown;
    private float mouseDownTime;

    private float _swingTime;

    private bool _isPower;

    private bool _isSpeed;

    private Vector3 _ropeScreenPos;

    private float _keepTime;

    public float maxForce = 15f; // 最大发射力
    public float forceMultiplier = 1f; // 力的倍数
    public float maxStretchDistance = 0.5f; // 最大拉伸距离

    private Rigidbody2D rb; // 刚体组件
    private CircleCollider2D circleCollider; // 碰撞体组件
    private Camera mainCamera; // 主摄像机
    private Vector3 touchStartPos; // 触摸开始位置
    private Vector3 touchCurrentPos; // 当前触摸位置

    private float forceStartTime = 0f;

    private float dirMulti;

    private void Awake()
    {
        isPlaying = false;
        _startPos = transform.localPosition;
        _isPower = false;
        _isSpeed = false;
        _keepTime = 0.2f;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;

        // 设置刚体属性
        if (rb != null)
        {
            rb.gravityScale = 0f; // 不受重力影响
            rb.drag = 0.2f; // 设置阻力
        }
    }


    private void Update()
    {
        HandleInput();
    }


    private void HandleInput()
    {
        // 检测触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touch);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    OnTouchMoved(touch);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnTouchEnded(touch);
                    break;
            }
        }

        // int ide
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButton(0))
        {
            OnMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }


    public void SetSkill(bool isPower, bool isSpeed)
    {
        _isPower = isPower;
        _isSpeed = isSpeed;
        moveSpeed = _isSpeed ? 1f : 2f;
    }


    private void StartDetection(Vector2 touchPos)
    {
        mouseStartPos = Input.mousePosition;
        isMouseDown = true;
        mouseDownTime = Time.time;
        isDragging = false;
        _swingTime = 0;
        _ropeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        rb.velocity = Vector2.zero;
        forceStartTime = Time.time;
        touchStartPos = transform.position;

        touchCurrentPos = touchPos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_ropeState != RopeState.Extending) return;

        string otherName = other.gameObject.name;

        if (otherName.Contains("Wall"))
        {
            StopAndToStartPoint();
        }
        else if (otherName.Contains("Stone"))
        {
            if (_isPower)
            {
                StopAndToStartPoint();
            }
            else
            {
                CattleGamePanel.Instance.ShowLosePanel();
            }
        }
        else if (other.gameObject.name.Contains("Box"))
        {
            
            DOTween.Kill(other.transform);
            other.transform.parent = transform.parent;
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<BaseCattle>().BeCatch(false);
            other.transform.DOLocalMove(_startPos, moveSpeed).SetDelay(_keepTime).OnComplete(() =>
            {
                CattleGamePanel.Instance.AddTime();
                CattleGamePanel.Instance.AddTimeAct(  transform.position);
                CattleGamePanel.Instance.AddCoinAndDoAnim(other.GetComponent<BaseCattle>().RewardNum,
                    transform.position);
                CattleGamePanel.Instance.RemoveCattleItem(other.gameObject);
                Destroy(other.gameObject);
            });
            
        }
        else
        {
            DOTween.Kill(other.transform);
            other.transform.parent = transform.parent;
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<BaseCattle>().BeCatch(false);
            other.transform.DOLocalMove(_startPos, moveSpeed).SetDelay(_keepTime).OnComplete(() =>
            {
                CattleGamePanel.Instance.AddCoinAndDoAnim(other.GetComponent<BaseCattle>().RewardNum,
                    transform.position);
                CattleGamePanel.Instance.RemoveCattleItem(other.gameObject);
                Destroy(other.gameObject);
            });
        }

        StopAndToStartPoint();
    }

    private void StopAndToStartPoint()
    {
        DOTween.Kill(transform);
        transform.GetComponent<Collider2D>().enabled = false;
        _ropeState = RopeState.Retracting;
        transform.DOLocalMove(_startPos, moveSpeed).SetDelay(_keepTime).OnComplete(() =>
        {
            _ropeState = RopeState.Swinging;
            if (CattleGamePanel.Instance.CheckIfShowComplete())
            {
            }
        });
    }



    private void OnTouchBegan(Touch touch)
    {
        if (_ropeState == RopeState.Extending)
        {
            StopAndToStartPoint();
            return;
        }

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touch.position);
        worldPos.z = 0f;

        StartDetection(worldPos);
    }

    private void OnMouseDown()
    {
        if (_ropeState == RopeState.Extending)
        {
            StopAndToStartPoint();
            return;
        }


        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        if (Vector2.Distance(transform.position, worldPos) <= circleCollider.radius)
        {
            StartCharging(worldPos);
        }
    }


    private void OnTouchMoved(Touch touch)
    {
        if (_ropeState != RopeState.Swinging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touch.position);
        worldPos.z = 0f;
        UpdateCharging(worldPos);
    }

    private void OnMouseDrag()
    {
        if (_ropeState != RopeState.Swinging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        UpdateCharging(worldPos);
    }

    private void OnTouchEnded(Touch touch)
    {
        if (_ropeState != RopeState.Swinging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touch.position);
        worldPos.z = 0f;
        EndCharging(worldPos);
    }

    private void OnMouseUp()
    {
        if (_ropeState != RopeState.Swinging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        EndCharging(worldPos);
    }


    private void StartCharging(Vector3 touchPos)
    {
        rb.velocity = Vector2.zero;
        forceStartTime = Time.time;
        touchStartPos = transform.position;

        touchCurrentPos = touchPos;
    }

    private void UpdateCharging(Vector3 touchPos)
    {

        Vector3 pointDir = touchPos - pointObj.transform.position;
        float angle = Mathf.Atan2(pointDir.y, pointDir.x) * Mathf.Rad2Deg - 90f;

        if (angle < -45) angle = -45;
        if (angle > 45) angle = 45;

        pointObj.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        float timeMulti = GetTimeForceMulti();

        // 计算蓄力值
        dirMulti =timeMulti*1000f;
    }

    private void EndCharging(Vector3 touchPos)
    {
        DoShoot(touchPos);
    }

    private float GetTimeForceMulti()
    {
        float thisTime = Time.time - forceStartTime;
        float multi = 0.8f + Mathf.Min(thisTime / 1.5f, 1f) * 1f;
        return multi;
    }


    private void DoShoot(Vector3 targetPos)
    {
        transform.GetComponent<Collider2D>().enabled = true;

        float durTime = dirMulti / 1000f;
        
        _ropeState = RopeState.Extending;
        // transform.DOLocalMove(targetPosition, durTime).OnComplete(() =>
        transform.DOLocalMoveY(dirMulti, durTime*moveSpeed).OnComplete(() =>
        {
            _ropeState = RopeState.Retracting;
            transform.GetComponent<Collider2D>().enabled = false;
            transform.DOLocalMove(_startPos, moveSpeed).SetDelay(_keepTime).OnComplete(() =>
            {
                _ropeState = RopeState.Swinging;
            });
        });

    }





}