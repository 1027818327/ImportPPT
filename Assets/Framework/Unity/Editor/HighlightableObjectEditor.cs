
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 * 
 *        创建者：  陈伟超
 *      创建时间：  2018/8/21 15:38:26
 *  
 */
#endregion


using Framework.Unity.Tools;
using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    public class HighlightableObjectEditor
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        //    void OnEnable()
        //    {
        //
        //    }
        //
        //    void Start() 
        //    {
        //    
        //    }
        //    
        //    void Update() 
        //    {
        //    
        //    }
        //
        //    void OnDisable()
        //    {
        //
        //    }
        //
        //    void OnDestroy()
        //    {
        //
        //    }

        #endregion

        #region Private Methods
        [MenuItem("Tools/Highlightable/添加高亮组件")]
        private static void AddConponent()
        {
            var tempObj = Selection.activeGameObject;
            GameObjectUtils.EnsureComponent<HighlightableObject>(tempObj);
        }

        [MenuItem("Tools/Highlightable/移除高亮组件")]
        private static void RemoveConponent()
        {
            var tempObj = Selection.activeGameObject;
            GameObjectUtils.RemoveComponentImmideate<HighlightableObject>(tempObj);
        }

        [MenuItem("Tools/Highlightable/ConstantOn")]
        private static void PlayConstantOn()
        {
            var tempObj = Selection.activeGameObject;
            var tempS = GameObjectUtils.FindComponent<HighlightableObject>(tempObj);
            if (tempS != null)
            {
                tempS.ConstantOn(Color.yellow);
            }
        }

        [MenuItem("Tools/Highlightable/ConstantOff")]
        private static void PlayConstantOff()
        {
            var tempObj = Selection.activeGameObject;
            var tempS = GameObjectUtils.FindComponent<HighlightableObject>(tempObj);
            if (tempS != null)
            {
                tempS.ConstantOff();
            }
        }

        [MenuItem("Tools/Highlightable/ConstantSwitch")]
        private static void PlayConstantSwitch()
        {
            var tempObj = Selection.activeGameObject;
            var tempS = GameObjectUtils.FindComponent<HighlightableObject>(tempObj);
            if (tempS != null)
            {
                tempS.ConstantSwitch();
            }
        }

        [MenuItem("Tools/Highlightable/Off")]
        private static void PlayOff()
        {
            var tempObj = Selection.activeGameObject;
            var tempS = GameObjectUtils.FindComponent<HighlightableObject>(tempObj);
            if (tempS != null)
            {
                tempS.Off();
            }
        }

        #endregion

        #region Protected & Public Methods

        #endregion
    }
}