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
using VolleyCSharp.CacheCom;

/*
 * ԭ����Github��java����https://github.com/mcxiaoke/android-volley
 * 
 * C#���ߣ�Y-Z-F
 * ���͵�ַ��http://www.cnblogs.com/yaozhenfa/
 * Github��ַ��https://github.com/yaozhenfa/
 * 
 * 15.4.15 ���ͨ��
 */

namespace VolleyCSharp.MainCom
{
    /// <summary>
    /// ������������
    /// </summary>
    public class ClearCacheRequest : Request
    {
        private ICache mCahce;
        private Java.Lang.IRunnable mCallback;

        public ClearCacheRequest(ICache cache, Java.Lang.IRunnable callback)
            : base(Method.GET, null, null)
        {
            mCahce = cache;
            mCallback = callback;
        }

        public override bool IsCanceled
        {
            get
            {
                mCahce.Clear();
                if (mCallback != null)
                {
                    var handler = new Handler(Looper.MainLooper);
                    handler.PostAtFrontOfQueue(mCallback);
                }
                return true;
            }
        }

        public override Request.Priority GetPriority()
        {
            return Priority.IMMEDIATE;
        }

        public override Response ParseNetworkResponse(NetworkResponse response)
        {
            return null;
        }

        public override void DeliverResponse(String response) { }
    }
}