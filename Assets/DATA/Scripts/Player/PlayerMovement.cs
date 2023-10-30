using System;
using System.Collections;
using UnityEngine;

namespace DATA.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 8f;                           // Tốc độ khi di chuyển về phía trước
        [SerializeField] private float strafeSpeed = 4f;                        // Tốc độ khi di chuyển về phía bên trái phải hoặc lùi
        [SerializeField] private float jumpForce = 5f;                          // Lực nhảy
        [SerializeField] private AdvancedSettings advancedSettings = new AdvancedSettings(); 
        [Serializable]
        public class AdvancedSettings
        {
            public float gravityMultiplier = 1f;                      // Được nhân với Physics.gravity để tăng hoặc giảm lực hút
            public PhysicMaterial zeroFrictionMaterial;               // Material được sử dụng cho mô phỏng không ma sát
            public PhysicMaterial highFrictionMaterial;               // Material được sử dụng cho mô phỏng ma sát
            public float groundStickyEffect = 5f;                     // Hiệu ứng dính khi đang ở trên mặt đất
        }
        
        private CapsuleCollider _capsule;
        private Rigidbody _rigidbody;
        private Vector2 _input;                                                 // Input từ bàn phím
        private const float JumpRayLength = 0.7f;                               // Chiều dài của raycast để kiểm tra có thể nhảy hay không
        private IComparer _rayHitComparer;                                      // Sắp xếp các hit raycast theo thứ tự từ xa đến gần
        private bool LockCursor
        {
            set
            {
                if (value)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        private bool Grounded{ get; set; }                    // Kiểm tra có đang ở trên mặt đất hay không
        
        

        class RayHitComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x != null && y != null) return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
                return 0;
            }
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsule = GetComponent<Collider>() as CapsuleCollider;
            _rayHitComparer = new RayHitComparer();

            LockCursor = true;
            Grounded = true;
        }

        private void Start()
        {
            _rigidbody.detectCollisions = true;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void OnDisable()
        {
            LockCursor = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                LockCursor = true;
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                LockCursor = false;
            }
        }

        private void FixedUpdate()
        {
  
            float speed = runSpeed;
            float hori = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            bool jump = Input.GetButton("Jump");
            
            _input = new Vector2(hori, vert);
            if(_input.magnitude > 1)_input.Normalize();
            
            var trsf = transform;
            Vector3 desiredMove = trsf.forward * _input.y * runSpeed + trsf.right * _input.x * strafeSpeed; // Tạo vector di chuyển dựa trên input
            
            float velocY = _rigidbody.velocity.y;
            if(Grounded && jump)                                                                 // Nếu đang ở trên mặt đất và nhấn nút nhảy thì nhảy
            {
                velocY += jumpForce;
                Grounded = false;
            }
            _rigidbody.velocity = desiredMove * speed + Vector3.up * velocY;                     // Cập nhật vận tốc của rigidbody
            
            if(desiredMove.magnitude > 0 || !Grounded)                                           // Nếu đang di chuyển hoặc không ở trên mặt đất thì thay đổi material của capsule collider
            {
                _capsule.material = advancedSettings.zeroFrictionMaterial;
            }
            else
            {
                _capsule.material = advancedSettings.highFrictionMaterial;
            }
            
            CheckGrounded();
            
        }
        // Kiểm tra có đang ở trên mặt đất hay không
        private void CheckGrounded()                                                      
        {
            // Tạo raycast từ vị trí hiện tại xuống dưới
            var trsf = transform;
            Ray ray = new Ray(trsf.position,-trsf.up);                      
            
            // Tạo mảng hit để lưu các hit raycast và Sắp xếp các hit raycast theo thứ tự từ xa đến gần
            RaycastHit[] hits = Physics.RaycastAll(ray, _capsule.height * JumpRayLength); 
            Array.Sort(hits,_rayHitComparer);

            if (Grounded || _rigidbody.velocity.y < jumpForce * .5f)
            {
                Grounded = false;

                foreach (var hit in hits)
                {
                    if (!hit.collider.isTrigger)
                    {
                        Grounded = true;

                        // Hiệu ứng dính khi đang ở trên mặt đất
                        _rigidbody.position = Vector3.MoveTowards(
                            _rigidbody.position,
                            hit.point + Vector3.up * _capsule.height * 0.5f,
                            Time.deltaTime * advancedSettings.groundStickyEffect); 

                        // Đặt vận tốc theo chiều y về 0
                        var velocity = _rigidbody.velocity;
                        velocity = new Vector3(velocity.x, 0f, velocity.z);
                        _rigidbody.velocity = velocity;
                        
                        break;
                    }
                }
            } 
            
            // Tăng hoặc giảm lực hút
            _rigidbody.AddForce(Physics.gravity * (advancedSettings.gravityMultiplier - 1f)); 
        }
    }

    
}
