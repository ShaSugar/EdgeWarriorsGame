using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnA.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnA.Base
{
    //当前模块依赖 CanvasGroup 组件
    //[RequireComponent(typeof(CanvasGroup))]
    public abstract class UIModuleBase : MonoBehaviour, IPointerClickHandler
    {
        protected float timer; // 计时器
        protected float triggerValue; // 触发值  
        protected int scene;

        #region Unity Action

        protected virtual void Awake()
        {
            UIManager.Instance.AddmodulesQueue(this);
        }

        protected virtual void Start()
        {
            timer = 0;
        }

        #endregion

        #region abstract

        // 子类实现的初始化逻辑
        public abstract IEnumerator AwakeInit();
        #endregion

        #region Module No Trigger Action

        // 检测时间  ---- 在每一个模块调用
        protected IEnumerator CheckTimeRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(triggerValue);
                BeyondReaction();
            }
        }

        private void CheckTime()
        {
            timer += Time.deltaTime;
            if (timer > triggerValue)
            {
                BeyondReaction();
                timer = 0;
            }
        }

        // 鼠标点击时触发 ---- 点击模块时 
        public virtual void OnPointerClick(PointerEventData eventData) { }

        // 超过反应
        protected virtual void BeyondReaction() { }

        #endregion

        #region m_get

        protected UIManager PushUI(string modeName)
        {
            return UIManager.Instance.PushUI(modeName);
        }

        protected UIManager PopUI(string modeName)
        {
            return UIManager.Instance.PopUI(modeName);
        }

        //获取对应模块
        protected UIModuleBase m_getOtherModule(string moduleName)
        {
            return UIManager.Instance.GetModuleBase(moduleName);
        }

        

        #endregion

        #region UI Call

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }


        public virtual void OnPause()
        {

        }

        public virtual void OnResume()
        {

        }

        public virtual void LanguageUpdate(bool IsChinese)
        {

        }

        public virtual void OnUpdateGUIData(int scene = 1)
        {

        }

        #endregion

        #region UI Animator

        

        //小到大，大到小
        public static void DOScale(Transform target, Vector3 startScale, Vector3 endScale, float duration)
        {
            target.localScale = startScale; // 先将目标的缩放值设置为初始值
            target.DOScale(endScale, duration);
        }

        /// <summary>
        /// 同时执行缩放和淡入淡出效果
        /// </summary>
        /// <param name="target">目标对象的 Transform</param>
        /// <param name="canvasGroup">目标对象的 CanvasGroup</param>
        /// <param name="startScale">起始缩放值</param>
        /// <param name="endScale">目标缩放值</param>
        /// <param name="endFadeValue">目标透明度</param>
        /// <param name="duration">动画持续时间</param>
        public static void DOScaleAndFade(Transform target, CanvasGroup canvasGroup, Vector3 startScale, Vector3 endScale, float endFadeValue, float duration)
        {
            // 初始化缩放值
            target.localScale = startScale;

            // 创建一个动画序列
            Sequence sequence = DOTween.Sequence();

            // 添加缩放动画和淡入淡出动画，使用 Join 让它们同时执行 ,Append 按顺序执行
            sequence.Join(target.DOScale(endScale, duration));
            //sequence.Join(canvasGroup.DOFade(endFadeValue, duration));
            
            // 也可以根据需要添加其他动画
        }

        #endregion

    }
}

