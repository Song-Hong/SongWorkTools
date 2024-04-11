using System;
using Song.Input.Keyboard;
using Song.Input.Touch;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Song.Input.Module
{
    /// <summary>
    /// 移动
    /// </summary>
    public class Movement:SingletonMono<Movement>
    {
        #region 通用属性

        /// <summary>
        /// 控制的物体
        /// </summary>
        [SerializeField] public GameObject player;
        
        /// <summary>
        /// 控制物体的刚体组建
        /// </summary>
        public Rigidbody playerRigidbody;

        /// <summary>
        /// 移动速度
        /// </summary>
        [SerializeField] public float moveSpeed = 5;


        /// <summary>
        /// 鼠标移动模块
        /// </summary>
        public KMovement KeyboardMovement;
        
        /// <summary>
        /// 触屏移动模块
        /// </summary>
        public TMovement TouchMovement;
        #endregion

        #region 键盘控制属性

        //前进按键
        [SerializeField] public string up    = "w";
        //后退按键
        [SerializeField] public string down  = "s";
        //左转
        [SerializeField] public string left  = "a";
        //右转
        [SerializeField] public string right = "d";
        
        #endregion

        #region 触屏控制属性
        
        /// <summary>
        /// 触摸区域
        /// </summary>
        [SerializeField] public GameObject touchArea;
        
        /// <summary>
        /// 触摸控制器
        /// </summary>
        [SerializeField] public GameObject touchController;

        #endregion
        
        #region 事件
        
        public event Action OnUpdate;

        #endregion
        
        /// <summary>
        /// 控制类型
        /// </summary>
        [SerializeField] public ControllerType controllerType = ControllerType.Any;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            //如果没有指定物体,设置自身为控制物体
            if (player == null) { player = this.gameObject; }
            
            //初始化获取刚体组件
            playerRigidbody = player.GetComponent<Rigidbody>();
            
            //初始化移动模块控制器
            InitController();
        }

        /// <summary>
        /// 初始化移动模块控制器
        /// </summary>
        public void InitController()
        {
            //初始化移动模块控制器
            switch (controllerType)
            {
                case ControllerType.Any:
                    KeyboardMovement = new KMovement();
                    KeyboardMovement.Init();
                    TouchMovement = new TMovement();
                    TouchMovement.Init();
                    break;
                case ControllerType.Keyboard:
                    KeyboardMovement = new KMovement();
                    KeyboardMovement.Init();
                    break;
                case ControllerType.Touch:
                    TouchMovement = new TMovement();
                    TouchMovement.Init();
                    break;
            }
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}