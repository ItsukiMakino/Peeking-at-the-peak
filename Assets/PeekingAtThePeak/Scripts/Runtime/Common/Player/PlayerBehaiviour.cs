using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace MyGame
{
    public class PlayerBehaiviour : MonoBehaviour
    {
        [SerializeField] Animator humanAnimator;
        [SerializeField] Animator meronTopAnimator;
        [SerializeField] Canvas timeSpanCanvas;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] Transform player;
        [SerializeField] Rigidbody2D rb2D;
        [SerializeField] float jumpHight;

        [Inject] PlayerInput playerinput;
        int layerMask;
        Vector3 jumpVector;
        ReactiveProperty<bool> isRightTouchOther = new ReactiveProperty<bool>();
        ReactiveProperty<bool> isLeftTouchOther = new ReactiveProperty<bool>();
        ReactiveProperty<bool> isRightTouchIron = new ReactiveProperty<bool>();
        ReactiveProperty<bool> isLeftTouchIron = new ReactiveProperty<bool>();


        bool isTreeTouching = false;
        bool isGool = false;
        bool isInversed = false;
        bool isGrounded = false;
        bool isRightPressed = false;
        bool isLeftPressed = false;
        bool isRightSticking = false;
        bool isLeftSticking = false;



        public Transform PlayerTransfrom { get => player; set => player = value; }
        public Canvas TimeSpanCanvas { get => timeSpanCanvas; set => timeSpanCanvas = value; }
        public TextMeshProUGUI TimeText { get => timeText; set => timeText = value; }
        public bool IsTreeTouching => isTreeTouching;
        public bool IsGool => isGool;

        public bool IsInversed => isInversed;
        public bool IsGrounded => isGrounded;
        public Vector3 JumpVector => jumpVector;
        public Animator HumanAnimator { get => humanAnimator; set => humanAnimator = value; }
        public Animator MeronTopAnimator { get => meronTopAnimator; set => meronTopAnimator = value; }
        public Rigidbody2D Rigidbody2D { get => rb2D; set => rb2D = value; }

        const string iron = "Iron";
        const string rock = "Rock";
        const string other = "Aother";
        int speed = Animator.StringToHash("Speed");
        int idle = Animator.StringToHash("Idle");
        int jump = Animator.StringToHash("Jump");
        int time = Animator.StringToHash("Time");
        int crounch = Animator.StringToHash("Crounch");
        int jumpTostrech = Animator.StringToHash("JumpToStrech");
        int jumpTocrouch = Animator.StringToHash("JumpToCrouch");
        int strech = Animator.StringToHash("Strech");



        public int SpeedHash { get => speed; }
        public int IdleHash { get => idle; }
        public int JumpHash { get => jump; }
        public int TimeHash { get => time; }
        public int CrounchHash { get => crounch; }
        public int JumpToStrechHash { get => jumpTostrech; }
        public int JumpToCrouchHash { get => jumpTocrouch; }
        public int StrechHash { get => strech; }



        public void SetTriggerToHumann(int name, bool active)
        {
            humanAnimator.SetBool(name, active);
            meronTopAnimator.SetBool(name, active);
        }
        void Start()
        {
            // 初期化処理
            layerMask = LayerMask.GetMask("Iron", "Rock", "other");
            humanAnimator?.SetFloat(speed, 0);
            meronTopAnimator?.SetFloat(speed, 0);

            //メロンの枝が他のオブジェクトと接触した時に効果音を鳴らす
            isLeftTouchOther.Where(x => x && isLeftPressed).Subscribe(_ =>
            {
                SoundSystem.Instance.PlaySE(3);
            }).AddTo(this);

            isRightTouchOther.Where(x => x && isRightPressed).Subscribe(_ =>
            {
                SoundSystem.Instance.PlaySE(3);

            }).AddTo(this);
            //メロンの枝が鉄オブジェクトと接触した時に効果音を鳴らす
            isLeftTouchIron.Where(x => x && isLeftPressed).Subscribe(_ =>
            {
                SoundSystem.Instance.PlaySE(6);
            }).AddTo(this);

            isRightTouchIron.Where(x => x && isRightPressed).Subscribe(_ =>
            {
                SoundSystem.Instance.PlaySE(6);
            }).AddTo(this);
        }

        void Update()
        {
            GroundCheak();
            Rotation();
            CheckPressAandD();
        }
        public float meronnButtomRayDistance;
        public float leftRightRayDistance;
        public float treeLowerRayDistance;
        public float treeUpperRayDistance;

        [SerializeField] float fastRotationSpeed;
        [SerializeField] float slowRotationSpeed;
        float currentFastRotaionSpeed;
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
            rb2D.AddForce(jumpVector * jumpHight, ForceMode2D.Impulse);
        }
        public void Jump(Vector3 jumpVector, float multiplyer)
        {
            rb2D.AddForce(jumpVector * jumpHight * multiplyer, ForceMode2D.Impulse);
        }
        public void CheckPressAandD()
        {
            if (playerinput == null) return;
            isRightPressed = playerinput.RightAction.IsPressed();
            isLeftPressed = playerinput.LeftAction.IsPressed();
        }
        public Vector3 GroundCheak()
        {
            //　メロンの枝が地面に設置しているか判定する
            for (int i = 0; i < TopUpRaycastPositions?.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(TopUpRaycastPositions[i].position, TopUpRaycastPositions[i].up, treeUpperRayDistance, layerMask: this.layerMask);


                if (hit.collider != null)
                {
                    jumpVector = -TopUpRaycastPositions[i].up;
                    isInversed = true;
                    return jumpVector;
                }
                isInversed = false;
            }
            //　メロンの枝がつり下がっている状態を判定する
            for (int i = 0; i < TopDownRaycastPositions?.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(TopDownRaycastPositions[i].position, -TopDownRaycastPositions[i].up, treeLowerRayDistance, layerMask: this.layerMask);

                if (hit.collider != null)
                {
                    jumpVector = TopDownRaycastPositions[i].up;
                    isTreeTouching = true;

                    int hitLayer = hit.collider.gameObject.layer;
                    if (hitLayer == LayerMask.NameToLayer(other))
                    {
                        isGool = true;
                    }
                    return jumpVector;
                }

                isTreeTouching = false;
                isGool = false;
            }
            //　メロンが地面に設置している状態か判定する
            for (int i = 0; i < LowerRaycastPositions?.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(LowerRaycastPositions[i].position, -LowerRaycastPositions[i].up, meronnButtomRayDistance, layerMask: this.layerMask);
                if (hit.collider != null)
                {
                    jumpVector = LowerRaycastPositions[i].up;
                    isGrounded = true;
                    return jumpVector;
                }
                isGrounded = false;
            }
            return jumpVector = new Vector3(0, 0, 0);
        }
        void Rotation()
        {
            CheakRayCastHitLeft();
            CheakRayCastHitRight();
            Rotate();
        }
        void Rotate()
        {
            if (isLeftPressed)
            {
                if (!isLeftSticking)
                {
                    //メロンの左枝が地面に接触していない場合は回転する
                    float newRotation = rb2D.rotation + 1 * CurrentRotationSpeed * Time.deltaTime;
                    rb2D.MoveRotation(newRotation);
                }
                else if (isTreeTouching || isLeftSticking)
                {
                    //メロンの左枝が地面に接触してる、もしくはつり下がっている状態は回転を止める
                    rb2D.angularVelocity = 0;
                }
            }
            else if (isRightPressed)
            {
                if (!isRightSticking)
                {
                    float newRotation = rb2D.rotation + (-1) * CurrentRotationSpeed * Time.deltaTime;
                    rb2D.MoveRotation(newRotation);
                }
                else if (isTreeTouching || isRightSticking)
                {
                    rb2D.angularVelocity = 0;

                }
            }
        }
        void CheakRayCastHitLeft()
        {
            RaycastHit2D hitleft;
            hitleft = Physics2D.Raycast(leftRotationCheak.position, leftRotationCheak.up, leftRightRayDistance, layerMask: this.layerMask);
            if (hitleft.collider == null)
            {
                isLeftSticking = false;
                if (!isLeftPressed)
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

            if (hitLayer == LayerMask.NameToLayer(rock))
            {
                isLeftTouchOther.Value = true;
            }
            else if (hitLayer == LayerMask.NameToLayer(iron))
            {
                isLeftTouchIron.Value = true;
            }
        }
        void CheakRayCastHitRight()
        {
            RaycastHit2D hitright;
            hitright = Physics2D.Raycast(rightRotationCheak.position, rightRotationCheak.up, distance: leftRightRayDistance, layerMask: this.layerMask);
            if (hitright.collider == null)
            {
                isRightSticking = false;
                if (!isRightPressed)
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

            if (hitLayer == LayerMask.NameToLayer(rock))
            {
                isRightTouchOther.Value = true;
            }
            else if (hitLayer == LayerMask.NameToLayer(iron))
            {
                isRightTouchIron.Value = true;
            }

        }
        float _elapsedTime;
        public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }





        // #if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < TopUpRaycastPositions.Length; i++)
            {
                Gizmos.DrawLine(TopUpRaycastPositions[i].position, TopUpRaycastPositions[i].position + TopUpRaycastPositions[i].up * treeUpperRayDistance);
            }
            for (int i = 0; i < TopDownRaycastPositions.Length; i++)
            {
                Gizmos.DrawLine(TopDownRaycastPositions[i].position, TopDownRaycastPositions[i].position - (TopDownRaycastPositions[i].up * treeLowerRayDistance));
            }
            for (int i = 0; i < LowerRaycastPositions.Length; i++)
            {
                Gizmos.DrawLine(LowerRaycastPositions[i].position, LowerRaycastPositions[i].position - (LowerRaycastPositions[i].up * meronnButtomRayDistance));
            }
            Gizmos.DrawLine(leftRotationCheak.position, leftRotationCheak.position + leftRotationCheak.up * leftRightRayDistance);
            Gizmos.DrawLine(rightRotationCheak.position, rightRotationCheak.position + rightRotationCheak.up * leftRightRayDistance);


        }

        // #endif

    }
}
