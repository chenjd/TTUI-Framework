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
/// TTUI message manager.UI消息管理模块
/// </summary>
public class TTUIMsgManager {

    #region Fields

    private Dictionary<int, Delegate> handlerDic = new Dictionary<int, Delegate>();
    private bool lockMessageQueue;
    private float minMessageIntervalTime = 0.2f;
    private Queue<TTUIMessage> msgs = new Queue<TTUIMessage>();
    private Dictionary<int, int> msgsDic = new Dictionary<int, int>();
    private float timeCounter;

    #endregion

    public delegate void TTUIMsgHandler(TTUIMessage msg);

    #region Public Methods

    public void Start()
    {
    }
    
    
    public void Update()
    {
        if (this.lockMessageQueue) //锁定消息队列
        {
            this.timeCounter += Time.deltaTime;// 时间计数器
            if (this.timeCounter > this.minMessageIntervalTime)//最小消息间隔时间
            {
                this.timeCounter = 0f;
                this.lockMessageQueue = false;
            }
        }
        while (this.msgs.Count > 0) //当有消息发送
        {
            TTUIMessage msg = this.msgs.Dequeue();
            this.HandleMsg(msg);
        }
    }
    
    public void SendMsg(TTUIMessage msg)
    {
        if ((msg != null) && (!msg.NeedLockSend || !this.lockMessageQueue))
        {
            this.msgs.Enqueue(msg);
            this.lockMessageQueue = true;
        }
    }

    public void HandleMsg(TTUIMessage msg)
    {
        if (msg != null)
        {
            msg.Execute(this.handlerDic);
        }
    }

    
    public void RegisterHandler(int id, TTUIMsgHandler handler)
    {
        if (handler == null)
        {
            Debug.LogWarning("handler is null");
        }
        else
        {
            if (!this.handlerDic.ContainsKey(id))
            {
                this.handlerDic.Add(id, null);
            }
            this.handlerDic[id] = Delegate.Combine( this.handlerDic[id], handler);
        }
    }

    public void UnRegisterHandler(int id, TTUIMsgHandler handler)
    {
        if ((handler != null) && this.handlerDic.ContainsKey(id))
        {
            this.handlerDic[id] = Delegate.Remove( this.handlerDic[id], handler);
            if (this.handlerDic[id] == null)
            {
                this.handlerDic.Remove(id);
            }
        }
    }
    
    public void Shutdown()
    {
		Debug.LogError(System.Environment.StackTrace);
        this.msgs.Clear();
        this.msgsDic.Clear();
        this.handlerDic.Clear();
    }

    #endregion
    
}
