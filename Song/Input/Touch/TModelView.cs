using Song.Input.Module;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Song.Input.Touch
{
    /// <summary>
    /// 模型展示
    /// </summary>
    public class TModelView
    {
        /// <summary>
        /// 受控制物体
        /// </summary>
        private GameObject _player;
        
        /// <summary>
        /// 旋转速度
        /// </summary>
        private float _rotationSpeed;
        /// <summary>
        /// 缩放速度
        /// </summary>
        private float _scaleSpeed;
        
        /// <summary>
        /// 最大缩放尺寸
        /// </summary>
        private float _maxScale;
        /// <summary>
        /// 最小缩放尺寸
        /// </summary>
        private float _minScale;

        /// <summary>
        /// 抬起手指还原默认旋转
        /// </summary>
        /// <returns></returns>
        private bool _upResetRotation;
        private bool _upResetScale;
        
        //手指的位置
        private Vector2 _fingerOnePosition;
        private Vector2 _fingerTwoPosition;
        
        public void Init()
        {
            _player = ModelView.Instantiation.player;
            _rotationSpeed = ModelView.Instantiation.rotationSpeed;
            _scaleSpeed = ModelView.Instantiation.scaleSpeed;
            _upResetRotation = ModelView.Instantiation.upResetRotation;
            _upResetScale = ModelView.Instantiation.upResetScale;
            _maxScale = ModelView.Instantiation.maxScale;
            _minScale = ModelView.Instantiation.minScale;
            EnhancedTouchSupport.Enable();
            touch.onFingerDown += FingerDown;
            touch.onFingerUp += FingerUp;
            touch.onFingerMove += FingerMove;
            Debug.Log("初始化完成");
        }

        /// <summary>
        /// 手指按下
        /// </summary>
        /// <param name="finger"></param>
        private void FingerDown(Finger finger)
        {
            _fingerOnePosition = touch.fingers[0].screenPosition;
            if (finger.index == 1)
            {
                _fingerTwoPosition = touch.fingers[1].screenPosition;
            }
        }

        /// <summary>
        /// 手指抬起
        /// </summary>
        /// <param name="finger"></param>
        private void FingerUp(Finger finger)
        {
            if (_upResetRotation)
            {
                _player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (_upResetScale)
            {
                _player.transform.localScale = Vector3.one;
            }
        }

        /// <summary>
        /// 手指移动
        /// </summary>
        /// <param name="finger"></param>
        private void FingerMove(Finger finger)
        {
            if (touch.activeFingers.Count == 2)
            {
                Scale(
                    touch.fingers[0].screenPosition,
                    touch.fingers[1].screenPosition);
            }
            else if (finger.index == 0)
            {
                Rotation(touch.fingers[0].screenPosition);
            }
        }
        
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="fingerPosition"></param>
        private void Rotation(Vector2 fingerPosition)
        {
            var deltaPos = fingerPosition - _fingerOnePosition;
            deltaPos = deltaPos.normalized*_rotationSpeed;
            var deltaPosX = deltaPos.x;
            var deltaPosY = deltaPos.y;
            // 获取当前欧拉角
            var currentRotation = _player.transform.rotation.eulerAngles;

            if (Mathf.Abs(deltaPosX) > Mathf.Abs(deltaPosY))
            {
                currentRotation.y += -deltaPosX;
            }
            else
            {
                currentRotation.z += deltaPosY;
            }
            // 将欧拉角重新应用到Transform
            _player.transform.rotation = Quaternion.Euler(currentRotation);
            _fingerOnePosition = fingerPosition;
        }

        /// <summary>
        /// 缩放
        /// </summary>
        private void Scale(Vector2 fingerOnePosition, Vector2 fingerTwoPosition)
        {
            var distanceOld = Vector2.Distance(_fingerOnePosition, _fingerTwoPosition);
            var distanceNow = Vector2.Distance(fingerOnePosition, fingerTwoPosition);
            if (Mathf.Abs(distanceNow - distanceOld) < 3)
            {
                return;
            }
            
            var scaleValue = 
                (distanceNow > distanceOld?1:-1) * _scaleSpeed + 
                _player.transform.localScale.x;
            scaleValue = Mathf.Clamp(scaleValue, _minScale, _maxScale);
            _player.transform.localScale = Vector3.one * scaleValue;
            
            _fingerOnePosition = fingerOnePosition;
            _fingerTwoPosition = fingerTwoPosition;
        }

        public void Disable()
        {
            EnhancedTouchSupport.Disable();
        }
    }
}