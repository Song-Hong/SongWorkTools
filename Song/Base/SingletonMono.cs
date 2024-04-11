using System;
using UnityEngine;

namespace Song
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class SingletonMono<T> : MonoBehaviour  where T : SingletonMono<T>
    {
        /// <summary>
        /// 自身(单例)
        /// </summary>
        public static T Instantiation;
        
        /// <summary>
        /// 初始化 
        /// </summary>
        protected virtual void Awake()
        {
            if (Instantiation == null)
            {
                Instantiation = this as T;   
            }
        }
    }
}