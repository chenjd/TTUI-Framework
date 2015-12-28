#region License
/*
 * The MIT License
 *
 * Copyright (c) 2015 Jiadong Chen(chenjd)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// TTUIFrame.所有UI类型的基类，定义了最基本的一些功能。
/// 用户自己定义的UI类型须继承自该类。
/// </summary>
public abstract class TTUIFrame {

    #region Fields

    protected GameObject root;
    protected bool active;
    protected int id;

    #endregion

    #region Public Constructor    

    public TTUIFrame (int id)
    {
        this.id = id;
    }

    #endregion


    #region Private Methods

    private void OnAssetLoaded(UnityEngine.Object obj, string param)
    {
        //TODO
    }

    private void LoadPrefab()
    {
        GameObject root = this.InitiateObj();//初始化OBJ

        this.Root = root;
    }

    private GameObject InitiateObj()
    {
        TTUIBuilder.UIObjectInfo assetInfo = TTUICore.Builder.GetUIAssetInfo(this.id);
        UnityEngine.Object original = Resources.Load(assetInfo.asset);//加载当前路径下面的资源

        if (null == original)
        {
            Debug.LogWarning("can not load prefab, frame playerEquipId: " + this.id);
        }

        GameObject go = UnityEngine.GameObject.Instantiate(original) as GameObject;
        return go;
    }

    #endregion


    #region Public Methods

	/// <summary>
	/// Active this instance.
	/// 处理具体UI的激活
	/// </summary>
    public virtual void Active()
    {
        this.TestLoad();

        if (this.root == null)
          return;

        NGUITools.SetActive(this.root, true);
        this.active = true;
    }

	/// <summary>
	/// Calls the monobehavior function.
	/// 和MonoBehaivor交互，用来调用UI上脚本中的方法。 
	/// </summary>
    public void CallMBFunction(string funName, object param = null)
    {
        this.TestLoad();

        if (this.root == null)
        {
            Debug.LogWarning("call failed:UI root not found!");
            return;
        }

        if (!this.root.activeSelf)
        {
            this.root.SetActive(true);
        }

        if (param == null)
        {
            this.root.SendMessage(funName, SendMessageOptions.RequireReceiver);
        }
        else
        {
            this.root.SendMessage(funName, param, SendMessageOptions.RequireReceiver);
        }

    }

	/// <summary>
	/// 去激活
	/// </summary>
	/// <param name="bForce">If set to <c>true</c> b force.</param>
    public virtual void DeActive(bool bForce = false)
    {
        if (this.root != null)
        {
            NGUITools.Destroy(this.root);
            this.root = null;
        }
        this.active = false;
    }

	/// <summary>
	/// Release this instance.
	/// </summary>
    public virtual void Release()
    {
        this.DeActive();
        if (this.root != null)
        {
            UnityEngine.Object.Destroy(this.root);
            this.root = null;
        }
    }


    public void TestLoad()
    {
        if (this.root == null) // 如果root有值  那么 表示加载过  不需要重新加载
        {
            this.LoadPrefab();// load prefab
        }
    }


    public virtual void Update()
    {
    }

	/// <summary>
	/// Registers the handlers.
	/// 注册回调方法
	/// </summary>
	/// <param name="mgr">Mgr.</param>
	public virtual void RegisterHandlers(TTUIMsgManager mgr)
    {
        if (mgr == null)
        {
            Debug.Log("msg mgr is null");
        }
    }

	/// <summary>
	/// Uns the register handlers.
	/// 注销回到方法
	/// </summary>
	/// <param name="mgr">Mgr.</param>
	public virtual void UnRegisterHandlers(TTUIMsgManager mgr)
    {
        if (mgr == null)
        {
            Debug.Log("msg mgr is null");
        }
    }

    #endregion


    #region Public Properties

    public int ID
    {
        get
        {
            return this.id;
        }
    }

    public bool IsActive
    {
        get
        {
            return this.active;
        }
    }


	public TTUIFrameManager Owner { get; set; }


    public GameObject Root
    {
        get
        {
            return this.root;
        }
        set
        {
            if (this.root != value)
            {
                if (this.root != null)
                {
                    UnityEngine.Object.Destroy(this.root);
                }
                this.root = value;
                if (this.root != null)
                {
                    NGUITools.SetActive(this.root, this.active);
                }
            }
        }
    }


  #endregion

}
