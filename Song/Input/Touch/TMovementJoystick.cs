/*当前脚本挂载在摇杆活动区域*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Song.Input.Touch
{
    public class TMovementJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler
    {
        public event Action<Vector2> OnMove; 
        
        // 摇杆的底座,摇杆的棍子
        public GameObject rockerBase;
        public GameObject rockerStick;

        // 初始位置
        Vector2 initPos;

        // 摇杆拖动限制范围
        float astrict;

        // 摇杆拖动后获得的向量
        public static Vector2 moveDirection;

        public void Init()
        {
            // 将摇杆的初始位置记录下来，用于拖动结束后还原初始位置
            initPos = rockerBase.GetComponent<RectTransform>().position;
            // 限制范围
            astrict = 80;
        }

        // 检测鼠标书否按下
        public void OnPointerDown(PointerEventData eventData)
        {

        }

        // 检测鼠标抬起
        public void OnPointerUp(PointerEventData eventData)
        {
            // 摇杆底座回到指定位置
            rockerBase.GetComponent<RectTransform>().position = initPos;
            // 摇杆回到指定位置
            rockerStick.GetComponent<RectTransform>().localPosition = Vector2.zero;
        }

        // 开始拖动
        public void OnBeginDrag(PointerEventData eventData)
        {
            // 当前摇杆底座的位置
            Vector2 temp = rockerBase.GetComponent<RectTransform>().position;
            // 当前摇杆的相对位置
            rockerStick.GetComponent<RectTransform>().localPosition =
                Vector3.ClampMagnitude(eventData.position - temp, astrict);
            // 通过摇杆获得的移动方向
            moveDirection = (rockerStick.transform.position - rockerBase.transform.position).normalized;
        }

        // 拖动中
        public void OnDrag(PointerEventData eventData)
        {
            // 当前摇杆底座的位置
            Vector2 temp = rockerBase.GetComponent<RectTransform>().position;
            // 当前摇杆的相对位置
            rockerStick.GetComponent<RectTransform>().localPosition =
                Vector3.ClampMagnitude(eventData.position - temp, astrict);
            // 通过摇杆获得的移动方向
            moveDirection = (rockerStick.transform.position - rockerBase.transform.position).normalized;
            OnMove?.Invoke(moveDirection);
        }

        // 结束拖动
        public void OnEndDrag(PointerEventData eventData)
        {
            // 拖动结束还原摇杆位置
            rockerBase.GetComponent<RectTransform>().position = initPos;
            // 还原摇杆棍子的位置
            rockerStick.GetComponent<RectTransform>().localPosition = Vector2.zero;
            // 通过摇杆获得的移动方向
            moveDirection = Vector3.zero;
        }
    }
}
