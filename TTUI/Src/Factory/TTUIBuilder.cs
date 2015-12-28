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

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// TTUI builder.注册UI类型以及UI资源的具体执行类
/// </summary>
public class TTUIBuilder {

    #region Fields 

    private Dictionary<int, UIObjectInfo> assetDic = new Dictionary<int, UIObjectInfo>();

    #endregion

    public class UIObjectInfo
    {
        public string asset;
        
        public UIObjectInfo(string asset)
        {
            this.asset = asset;
        }
    }
    
    public delegate void LoadFinishCallback(string name, int id);

    #region Public Methods

    public void BuildUIFrame<T>(int id) where T: TTUIFrame
    {
		if (TTUICore.FrameMgr.IsRegister(id))
        {
            Debug.LogWarning("duplicate create frame: " + id);
        }
        else
        {
            object[] args = new object[] { id };
            T frame = Activator.CreateInstance(typeof(T), args) as T;
            TTUICore.FrameMgr.Register(frame);
        }
    }
    
    public UIObjectInfo GetUIAssetInfo(int id)
    {
        if (!this.assetDic.ContainsKey(id))
        {
            Debug.LogWarning("can not find AssetInfo: " + id);
            return null;
        }
        return this.assetDic[id];
    }
    
    public void RegisterGuiAsset(int id, string asset)
    {
        if (this.assetDic.ContainsKey(id))
        {
            Debug.LogError(string.Concat(new object[] { "duplicate playerEquipId when RegisterGuiAsset, frame name: ", id, "  asset name: ", asset }));
        }
        else
        {
            this.assetDic.Add(id, new UIObjectInfo(asset));
        }
    }
    
    public void Shutdown()
    {
        this.assetDic.Clear();
    }

    #endregion
    
}
