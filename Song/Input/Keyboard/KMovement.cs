using Song.Input.Module;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Song.Input.Keyboard
{
    /// <summary>
    /// 
    /// </summary>
    public class KMovement
    {
        // 定义输入动作
        private InputAction _moveAction;
        
        //受控制物体
        private Rigidbody _player;

        //移动速度
        private float _moveSpeed;
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var movement = Movement.Instantiation; //获取移动模块
            _player = movement.playerRigidbody;    //获取受控制的物体的刚体
            _moveSpeed = movement.moveSpeed;       //获取移动速度
            
            // 获取输入动作
            _moveAction = new InputAction("KeyboardMovement", InputActionType.Button);
            _moveAction.AddCompositeBinding("2DVector(mode=2)")
                .With("Up",$"<Keyboard>/{movement.up}")
                .With("Down",$"<Keyboard>/{movement.down}")
                .With("Left",$"<Keyboard>/{movement.left}")
                .With("Right",$"<Keyboard>/{movement.right}");
            _moveAction.Enable();
            
            //订阅事件
            movement.OnUpdate += Update; //更新事件
        }

        private void OnDisable()
        {
            // 禁用输入动作
            _moveAction.Disable();
        }

        private void Update()
        {
            // 获取输入动作的状态
            Vector2 moveInput = _moveAction.ReadValue<Vector2>();

            // 根据输入移动人物
            MovePlayer(moveInput);
        }

        private void MovePlayer(Vector2 moveInput)
        {
            // 将输入转换为移动方向
            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            // 设置刚体速度
            _player.velocity = moveDirection * _moveSpeed;
        }
    }
}