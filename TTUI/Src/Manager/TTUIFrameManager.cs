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
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// TTUI frame manager.UI页面管理模块
/// </summary>
public class TTUIFrameManager  {

  	#region Fields

    private List<int> activeList = new List<int>();
	private	TTUIBuilder  frameBuilder = new TTUIBuilder();
    private Dictionary<int, TTUIFrame> frameDic = new Dictionary<int, TTUIFrame>();
	private TTUIMsgManager msgMgr = new TTUIMsgManager();

  	#endregion


  	#region Public Methods

    public void Start()
    {
        this.msgMgr.Start();
    }

    public void Update()
	{
        this.msgMgr.Update();

        int count = this.activeList.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int num3 = this.activeList[i];
                this.frameDic[num3].Update();
            }
        }
    }

  	#region about frame

    /// <summary>
    /// Active the specified id.
    /// </summary>
    public void Active(int id)
    {
        TTUIFrame frame;
        this.frameDic.TryGetValue(id, out frame);
        if (frame == null)
        {
            Debug.Log("not registered frame: " + id);
        }

        if (!this.activeList.Contains(id))
        {   
			this.activeList.Add(id);
			frame.Active();
        }
    }

    /// <summary>
    /// Des the active.
    /// </summary>
    public void DeActive(int id, bool forceWithOutAni = false)
    {
        TTUIFrame frame = this.GetFrame(id);

        if (!this.frameDic.ContainsKey(id))
        {
            Debug.Log("not registered frame " + id);
        }
        else if (this.activeList.Contains(id))
        {
			this.activeList.Remove(id);
			frame.DeActive();
        }
    }
    
    /// <summary>
    /// Register the specified frame.
    /// </summary>
    public void Register(TTUIFrame frame)
    {
        if ((frame != null) && !this.frameDic.ContainsKey(frame.ID))
        {
            frame.Owner = this;
            frame.RegisterHandlers(this.msgMgr);
            this.frameDic.Add(frame.ID, frame);
        }
    }

    /// <summary>
    /// Uns the register.
    /// </summary>
    public void UnRegister(int id)
    {
        if (this.frameDic.ContainsKey(id))
        {
            if (this.activeList.Contains(id))
            {
                this.frameDic[id].DeActive();
                this.activeList.Remove(id);
            }
            this.frameDic[id].UnRegisterHandlers(this.msgMgr);
            this.frameDic[id].Owner = null;
            this.frameDic.Remove(id);
        }
    }

    public bool IsActive(int id)
    {
        return this.activeList.Contains(id) ;
    }

    public bool IsRegister(int id)
    {
        return this.frameDic.ContainsKey(id);
    }

  	#endregion

    public void Shutdown()
    {
        foreach (int num in this.activeList)
        {
            this.frameDic[num].DeActive();
        }
        this.activeList.Clear();
        foreach (TTUIFrame frame in this.frameDic.Values)
        {
            frame.Release();
        }
        this.frameDic.Clear();
        this.msgMgr.Shutdown();
    }

    public void CloseAllActiveFrame()
    {
        foreach (int num in this.activeList)
        {
            this.frameDic[num].DeActive(true);
        }
        this.activeList.Clear();
    }

    /// <summary>
    /// Gets the frame.
    /// </summary>
    public TTUIFrame GetFrame(int id)
    {
        if (!this.frameDic.ContainsKey(id))
        {
            return null;
        }
        return this.frameDic[id];
    }

    #endregion


    #region Public Properties

	public TTUIBuilder GUIFrameBuilder
    {
        get
        {
            return this.frameBuilder;
        }
    }

	public TTUIMsgManager GUIMsgMgr
    {
        get
        {
            return this.msgMgr;
        }
    }

    #endregion

}
