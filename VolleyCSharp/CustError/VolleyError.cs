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
    /// �����������
    /// </summary>
    public class VolleyError : Exception
    {
        public NetworkResponse networkResponse;
        private long networkTimeMs;

        public VolleyError()
        {
            networkResponse = null;
        }

        public VolleyError(NetworkResponse response)
        {
            networkResponse = response;
        }

        public VolleyError(String exceptionMessage)
            : base(exceptionMessage)
        {
            networkResponse = null;
        }

        public VolleyError(String exceptionMessage, Exception reason)
            : base(exceptionMessage, reason)
        {
            networkResponse = null;
        }

        public VolleyError(Exception reason)
            : base("", reason)
        {
            networkResponse = null;
        }

        public long NetworkTimeMs
        {
            get
            {
                return networkTimeMs;
            }
            set
            {
                networkTimeMs = value;
            }
        }
    }
}