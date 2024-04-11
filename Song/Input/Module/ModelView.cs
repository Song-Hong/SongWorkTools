using System;
using System.Collections;
using System.Collections.Generic;
using Song.Input.Touch;
using UnityEngine;

namespace Song.Input.Module
{
    /// <summary>
    /// 模型展示
    /// </summary>
    public class ModelView : SingletonMono<ModelView>
    {
        /// <summary>
        /// 控制的物体
        /// </summary>
        [SerializeField] public GameObject player;
        
        /// <summary>
        /// 控制器类型
        /// </summary>
        [SerializeField] public ControllerType controllerType = ControllerType.Any;


        /// <summary>
        /// 旋转速度
        /// </summary>
        [SerializeField] public float rotationSpeed = 4;
        /// <summary>
        /// 缩放速度
        /// </summary>
        [SerializeField] public float scaleSpeed = 0.2f;
        /// <summary>
        /// 最大缩放尺寸
        /// </summary>
        [SerializeField] public float maxScale = 1.5f;
        /// <summary>
        /// 最小缩放尺寸
        /// </summary>
        [SerializeField] public float minScale = 0.5f;
        /// <summary>
        /// 抬起恢复初始旋转
        /// </summary>
        [SerializeField] public bool upResetRotation = true;
        /// <summary>
        /// 抬起恢复初始缩放
        /// </summary>
        [SerializeField] public bool upResetScale = true;
        
        public TModelView modelView;
        
        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            //如果没有指定物体,设置自身为控制物体
            if (player == null) { player = this.gameObject; }

            //初始化
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
                    break;
                case ControllerType.Keyboard:
                    break;
                case ControllerType.Touch:
                    modelView = new TModelView();
                    modelView.Init();
                    break;
            }
        }

        private void OnDisable()
        {
            modelView?.Disable();
        }
    }
}