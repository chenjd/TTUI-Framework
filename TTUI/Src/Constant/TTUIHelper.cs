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
/// <summary>
/// TTUI helper.辅助类，提供一些辅助功能
/// </summary>
public static class TTUIHelper {
	
	public static int StringHash(string str)
    {
        char[] chArray = str.ToCharArray();
        uint num = 0;
        uint num2 = 0;
        uint num3 = 0;
        uint num4 = 0x7c5;
        while (num3 < chArray.Length)
        {
            num = ((num << 4) * num4) + chArray[num3++];
            num2 = num & 0xf0000000;
            if (num2 != 0)
            {
                num ^= num2 >> 0x18;
                num &= ~num2;
            }
        }
        return (((int)num) & 0x7fffffff);
    }

}
