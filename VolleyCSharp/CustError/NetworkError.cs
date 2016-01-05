using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VolleyCSharp.MainCom;

/*
 * ԭ����Github��java����https://github.com/mcxiaoke/android-volley
 * 
 * C#���ߣ�Y-Z-F
 * ���͵�ַ��http://www.cnblogs.com/yaozhenfa/
 * Github��ַ��https://github.com/yaozhenfa/
 * 
 * 15.4.15 ���ͨ��
 */

namespace VolleyCSharp
{
    /// <summary>
    /// �������
    /// </summary>
    public class NetworkError : VolleyError
    {
        public NetworkError()
            : base() { }

        public NetworkError(Exception cause)
            : base(cause) { }

        public NetworkError(NetworkResponse networkResponse)
            : base(networkResponse) { }
    }
}