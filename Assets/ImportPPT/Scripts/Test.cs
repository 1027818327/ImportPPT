
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：DESKTOP-1050N1H\luoyikun
 *      创建时间: 2018/11/17 17:56:53
 *  
 */
#endregion


using Framework.Unity.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ImportPPT
{
    public class Test : MonoBehaviour
    {
        #region Fields
        string[] mImageFiles;
        Sprite[] mSprites;
        BufferPool mPool;
        private List<GameObject> mImageObjs = new List<GameObject>();

        public GameObject mCloneObj;
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
        void Start()
        {
            
            MonoHelper.AddUpdateListener(PrintArgs);
            
        }
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
        void OnDestroy()
        {
            if (mPool != null)
            {
                mPool.Clear();
            }
        }

        #endregion

        #region Private Methods
        void Process_Exited(object sender, EventArgs e)
        {
            Process tempP = sender as Process;
            StreamReader tempSr = tempP.StandardOutput;
            string tempText = tempSr.ReadToEnd();
            mImageFiles = tempText.Split(new String[] {"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            MonoHelper.AddUpdateListener(DelayHandleImport);
        }

        void DelayHandleImport()
        {
            MonoHelper.RemoveUpdateListener(DelayHandleImport);
            if (mImageFiles != null && mImageFiles.Length > 0)
            {
                mSprites = new Sprite[mImageFiles.Length];
            }
            else
            {
                mSprites = null;
            }

            if (mImageFiles != null && mImageFiles.Length > 0)
            {
                for (int i = 0; i < mImageFiles.Length; i++)
                {
                    FileStream fs = new FileStream(mImageFiles[i], FileMode.Open);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    var originalTex = new Texture2D(2, 2);
                    originalTex.LoadImage(buffer);
                    originalTex.Apply();
                    mSprites[i] = ChangeToSprite(originalTex);
                }
            }
            Show();
        }

        /// <summary>
        /// 转换为Sprite
        /// </summary>
        private Sprite ChangeToSprite(Texture2D tex)
        {
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        private void ReleaseImage()
        {
            mSprites = null;
            Resources.UnloadUnusedAssets();
        }

        private void Show()
        {
            if (mPool == null)
            {
                mPool = new BufferPool(mCloneObj, mCloneObj.transform.parent, 3);
            }

            foreach (GameObject tempObj in mImageObjs)
            {
                mPool.Recycle(tempObj);
            }
            if (mSprites != null && mSprites.Length > 0)
            {
                for (int i = 0; i < mSprites.Length; i++)
                {
                    GameObject tempObj = mPool.GetObject();
                    tempObj.GetComponent<Image>().sprite = mSprites[i];
                    tempObj.GetComponentInChildren<Text>().text = mImageFiles[i];
                }
            }
        }

        
        private void PrintArgs()
        {
            // 获取命令行参数，第二个开始才是参数
            var tempArray = Environment.GetCommandLineArgs();
            foreach (string tempStr in tempArray)
            {
                Debuger.Log(tempStr);
            }
            MonoHelper.RemoveUpdateListener(PrintArgs);
        }
        

        #endregion

        #region Protected & Public Methods
        public void ClickImport()
        {
            Process myProcess = new Process();
            string arugments = string.Format("{0} {1}", Application.streamingAssetsPath + "/PPTOut", 1);
            ProcessStartInfo startInfo = new ProcessStartInfo(Application.streamingAssetsPath + "/Ppt2Png.exe", arugments);

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            myProcess.StartInfo = startInfo;

            myProcess.EnableRaisingEvents = true;
            myProcess.Exited += new EventHandler(Process_Exited);
            myProcess.Start();
        }

        #endregion
    }
}