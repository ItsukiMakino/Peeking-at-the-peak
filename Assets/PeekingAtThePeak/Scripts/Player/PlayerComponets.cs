using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.UI;
using VContainer;

public class PlayerComponets
 : MonoBehaviour
{
    [SerializeField] Animator _humanAnimator;
    [SerializeField] Animator _MeronTopAnimator;
    [SerializeField] Canvas _timeSpanCanvas;
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] float jumpHight;

    [Inject] KeyBindSystem keyBindSystem;
    int layerMask;
    Vector3 _jumpVector;
    bool isRightSticking;
    bool isLeftSticking;
    ReactiveProperty<bool> isRightTouchOther = new ReactiveProperty<bool>();
    ReactiveProperty<bool> isLeftTouchOther = new ReactiveProperty<bool>();
    ReactiveProperty<bool> isRightTouchIron = new ReactiveProperty<bool>();
    ReactiveProperty<bool> isLeftTouchIron = new ReactiveProperty<bool>();


    bool isTreeTouching = false;
    bool isGool = false;
    bool isInversed = false;
    bool isGrounded = false;
    bool RightIsPressed = false;
    bool LeftIsPressed = false;

    PlayerInput playerInput;

    public Transform PlayerTransfrom { get => player; set => player = value; }
    public Canvas TimeSpanCanvas { get => _timeSpanCanvas; set => _timeSpanCanvas = value; }
    public TextMeshProUGUI TimeText { get => _timeText; set => _timeText = value; }
    public bool IsTreeTouching => isTreeTouching;
    public bool IsGool => isGool;

    public bool IsInversed => isInversed;
    public bool IsGrounded => isGrounded;
    public Vector3 JumpVector => _jumpVector;
    public Animator HumanAnimator { get => _humanAnimator; set => _humanAnimator = value; }
    public Animator MeronTopAnimator { get => _MeronTopAnimator; set => _MeronTopAnimator = value; }
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }



    public void SetTriggerToHumann(string name, bool active)
    {
        _humanAnimator.SetBool(name, active);
        _MeronTopAnimator.SetBool(name, active);
    }

    void Start()
    {
        // _rigidbody2D
        layerMask = LayerMask.GetMask(ZString.Concat("Iron"), ZString.Concat("Rock"), ZString.Concat("Other"));
        _humanAnimator.SetFloat(ZString.Concat("Speed"), 0);
        _MeronTopAnimator.SetFloat(ZString.Concat("Speed"), 0);

        isLeftTouchOther.Where(x => x && LeftIsPressed).Subscribe(_ =>
        {
            SoundSystem.Instance.PlaySE(3);
        }).AddTo(this);

        isRightTouchOther.Where(x => x && RightIsPressed).Subscribe(_ =>
       {
           SoundSystem.Instance.PlaySE(3);

       }).AddTo(this);


        isLeftTouchIron.Where(x => x && LeftIsPressed).Subscribe(_ =>
      {
          SoundSystem.Instance.PlaySE(6);
      }).AddTo(this);

        isRightTouchIron.Where(x => x && RightIsPressed).Subscribe(_ =>
       {
           SoundSystem.Instance.PlaySE(6);
       }).AddTo(this);
    }

    public float Maxdistance;
    public float LeftRightRayDistance;
    public float treeRayDistance;
    [SerializeField] float fastRotationSpeed;
    float currentFastRotaionSpeed;
    [SerializeField] float slowRotationSpeed;
    float currentSlowRotaionSpeed;


    public float FastRotationSpeed { get => fastRotationSpeed; set => fastRotationSpeed = value; }
    public float CurrentFastRotationSpeed { get => currentFastRotaionSpeed; set => currentFastRotaionSpeed = value; }
    public float SlowRotationSpeed { get => slowRotationSpeed; set => slowRotationSpeed = value; }
    public float CurrentSlowRotationSpeed { get => currentSlowRotaionSpeed; set => currentSlowRotaionSpeed = value; }

    public float CurrentRotationSpeed;

    public Transform[] TopUpRaycastPositions;
    public Transform[] TopDownRaycastPositions;
    public Transform[] LowerRaycastPositions;
    public Transform leftRotationCheak;
    public Transform rightRotationCheak;


    public void Jump(Vector3 jumpVector)
    {
        _rigidbody2D.AddForce(jumpVector * jumpHight, ForceMode2D.Impulse);
    }
    public void Jump(Vector3 jumpVector, float Multiplyer)
    {
        _rigidbody2D.AddForce(jumpVector * jumpHight * Multiplyer, ForceMode2D.Impulse);
    }
    public void CheckPressAandD()
    {
        if (keyBindSystem == null) return;
        RightIsPressed = keyBindSystem.RightAction.IsPressed();
        LeftIsPressed = keyBindSystem.LeftAction.IsPressed();
    }
    public Vector3 GroundCheak()
    {
        for (int i = 0; i < TopUpRaycastPositions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(TopUpRaycastPositions[i].position, TopUpRaycastPositions[i].up, Maxdistance, layerMask: this.layerMask);


            if (hit.collider != null)
            {
                _jumpVector = -TopUpRaycastPositions[i].up;
                isInversed = true;
                return _jumpVector;
            }
            isInversed = false;
        }
        for (int i = 0; i < TopDownRaycastPositions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(TopDownRaycastPositions[i].position, -TopDownRaycastPositions[i].up, treeRayDistance, layerMask: this.layerMask);

            if (hit.collider != null)
            {
                _jumpVector = TopDownRaycastPositions[i].up;
                isTreeTouching = true;

                int hitLayer = hit.collider.gameObject.layer;
                if (hitLayer == LayerMask.NameToLayer(ZString.Concat("Other")))
                {
                    isGool = true;
                }
                return _jumpVector;
            }

            isTreeTouching = false;
            isGool = false;
        }
        for (int i = 0; i < LowerRaycastPositions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(LowerRaycastPositions[i].position, -LowerRaycastPositions[i].up, Maxdistance, layerMask: this.layerMask);
            if (hit.collider != null)
            {
                _jumpVector = LowerRaycastPositions[i].up;
                isGrounded = true;
                return _jumpVector;
            }
            isGrounded = false;
        }
        return _jumpVector = new Vector3(0, 0, 0);
    }
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < TopUpRaycastPositions.Length; i++)
        {
            Gizmos.DrawLine(TopUpRaycastPositions[i].position, TopUpRaycastPositions[i].position + TopUpRaycastPositions[i].up * Maxdistance);
        }
        for (int i = 0; i < TopDownRaycastPositions.Length; i++)
        {
            Gizmos.DrawLine(TopDownRaycastPositions[i].position, TopDownRaycastPositions[i].position - (TopDownRaycastPositions[i].up * treeRayDistance));
        }
        for (int i = 0; i < LowerRaycastPositions.Length; i++)
        {
            Gizmos.DrawLine(LowerRaycastPositions[i].position, LowerRaycastPositions[i].position - (LowerRaycastPositions[i].up * Maxdistance));
        }
        Gizmos.DrawLine(leftRotationCheak.position, leftRotationCheak.position + leftRotationCheak.up * LeftRightRayDistance);
        Gizmos.DrawLine(rightRotationCheak.position, rightRotationCheak.position + rightRotationCheak.up * LeftRightRayDistance);


    }

#endif

    public void Rotation()
    {
        CheakRayCastHitLeft();
        CheakRayCastHitRight();
        Rotate();
    }
    void Rotate()
    {
        if (LeftIsPressed)
        {
            if (!isLeftSticking)
            {
                float newRotation = _rigidbody2D.rotation + 1 * CurrentRotationSpeed * Time.deltaTime;
                _rigidbody2D.MoveRotation(newRotation);
            }
            else if (isTreeTouching || isLeftSticking)
            {
                _rigidbody2D.angularVelocity = 0;
            }
        }
        else if (RightIsPressed)
        {
            if (!isRightSticking)
            {
                float newRotation = _rigidbody2D.rotation + (-1) * CurrentRotationSpeed * Time.deltaTime;
                _rigidbody2D.MoveRotation(newRotation);
            }
            else if (isTreeTouching || isRightSticking)
            {
                _rigidbody2D.angularVelocity = 0;

            }
        }
    }
    void CheakRayCastHitLeft()
    {
        RaycastHit2D hitleft;
        hitleft = Physics2D.Raycast(leftRotationCheak.position, leftRotationCheak.up, LeftRightRayDistance, layerMask: this.layerMask);
        if (hitleft.collider == null)
        {
            isLeftSticking = false;
            if (!LeftIsPressed)
            {
                isLeftTouchOther.Value = false;
                isLeftTouchIron.Value = false;

            }
            return;
        }

        Vector2 normal = hitleft.normal;
        float angle = Vector2.Angle(Vector2.right, normal);
        if (angle <= 100f)
        {
            isLeftSticking = true;
        }
        else
        {
            isLeftSticking = false;
        }
        int hitLayer = hitleft.collider.gameObject.layer;

        if (hitLayer == LayerMask.NameToLayer(ZString.Concat("Rock")))
        {
            isLeftTouchOther.Value = true;
        }
        else if (hitLayer == LayerMask.NameToLayer(ZString.Concat("Iron")))
        {
            isLeftTouchIron.Value = true;
        }
    }
    void CheakRayCastHitRight()
    {
        RaycastHit2D hitright;
        hitright = Physics2D.Raycast(rightRotationCheak.position, rightRotationCheak.up, distance: LeftRightRayDistance, layerMask: this.layerMask);
        if (hitright.collider == null)
        {
            isRightSticking = false;
            if (!RightIsPressed)
            {
                isRightTouchOther.Value = false;
                isRightTouchIron.Value = false;
            }
            return;
        }
        Vector2 normal = hitright.normal;
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
        if (angle >= 10f && angle <= 100f)
        {
            isRightSticking = true;
        }
        else
        {
            isRightSticking = false;
        }
        int hitLayer = hitright.collider.gameObject.layer;

        if (hitLayer == LayerMask.NameToLayer(ZString.Concat("Rock")))
        {
            isRightTouchOther.Value = true;
        }
        else if (hitLayer == LayerMask.NameToLayer(ZString.Concat("Iron")))
        {
            isRightTouchIron.Value = true;
        }

    }
    float _elapsedTime;
    public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }

    void Update()
    {

        GroundCheak();
        Rotation();
        CheckPressAandD();
    }



}
