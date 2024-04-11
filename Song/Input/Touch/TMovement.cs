using Song.Input.Module;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Song.Input.Touch
{
    public class TMovement
    {
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
            
            var touchArea = Movement.Instantiation.touchArea;
            var touchController = Movement.Instantiation.touchController;
            var movementJoystick = touchController.AddComponent<TMovementJoystick>();
            movementJoystick.rockerBase = touchArea;
            movementJoystick.rockerStick = touchController;
            movementJoystick.OnMove += MovePlayer;
            movementJoystick.Init();
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