
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：DESKTOP-1050N1H\luoyikun
 *      创建时间: 2018/11/17 17:57:29
 *  
 */
#endregion


using Framework.Windows;
using System.Runtime.InteropServices;

namespace ImportPPT
{
    public class FileController
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

        #endregion

        #region Protected & Public Methods
        public static string SelectPPT()
        {
            OpenFileName ofn = new OpenFileName();

            ofn.structSize = Marshal.SizeOf(ofn);

            //ofn.filter = "exe文件(*.exe)\0*.exe";
            ofn.filter = "ppt文件(*.ppt)\0*.ppt\0pptx 文件(*.pptx)\0*.pptx\0\0";

            ofn.file = new string(new char[256]);

            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new string(new char[64]);

            ofn.maxFileTitle = ofn.fileTitle.Length;

            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

            ofn.title = "Open Project";

            ofn.defExt = "PPT";//显示文件的类型
                               //注意 一下项目不一定要全选 但是0x00000008项不要缺少
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

            WindowsDll.GetOpenFileName(ofn);
            return ofn.file;
        }
        #endregion
    }
}