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
/// <summary>
/// TTUI factory.工厂基类，使用时继承于该类，注册用户自己的UI类型以及资源路径。
/// </summary>
public abstract class TTUIFactory {


  	#region Public Methods

    /// <summary>
	/// 以FrameID作为key，创建FrameID和对应的UI类之间的联系。
    /// </summary>
	public abstract void BuildCommonUIFrame();

	/// <summary>
	///以FrameID作为key，注册UI资源的地址，用于创建id和prefab之间的关联
	///使用时，在次方法下添加具体的k-v(id-资源路径）。
	/// </summary>
	public abstract void RegisterUIFrameAssetes();

  	#endregion

	#region Private Methods

    /// <summary>
    /// Builds the frame.chuang jian ui yemian
    /// </summary>
	protected void BuildUIFrame<T>(int id) where T : TTUIFrame
    {
        TTUICore.Builder.BuildUIFrame<T>(id);
    }

	protected void RegisterAssets(int id, string assetPath)
	{
		TTUICore.Builder.RegisterGuiAsset(id, assetPath);
	}

	#endregion

}
