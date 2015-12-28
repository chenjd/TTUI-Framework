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
/// TTUIMessage.所有UI消息的基类，自定义的UI消息需要继承该类
/// </summary>
public abstract class TTUIMessage  {

    public bool NeedLockSend;
    
    public TTUIMessage(string name)
    {
		this.ID = TTUIHelper.StringHash(name);
    }
    
    public virtual void Execute(Dictionary<int, Delegate> handlerDic)
    {
        if (handlerDic.ContainsKey(this.ID))
        {
            ((TTUIMsgManager.TTUIMsgHandler) handlerDic[this.ID])(this);
        }
        else
        {
            Debug.LogWarning("unknown msg: " + this.ID);
        }
    }

    
    public int ID { get; set; }
}
