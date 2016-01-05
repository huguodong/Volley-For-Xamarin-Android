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

namespace VolleyCSharp.Delivery
{
    /// <summary>
    /// �����ṩ������ɺ�Ļص�
    /// </summary>
    public class ExecutorDelivery : Java.Lang.Object, IResponseDelivery, Java.Util.Concurrent.IExecutor
    {
        private Java.Util.Concurrent.IExecutor mResponsePoster;
        private Handler mHandler;

        public ExecutorDelivery(Handler handler)
        {
            mResponsePoster = this;
            mHandler = handler;
        }

        public void PostResponse(Request request, Response response)
        {
            PostResponse(request, response, null);
        }

        public void PostResponse(Request request, Response response, Action runnable)
        {
            request.MarkDelivered();
            request.AddMarker("post-response");
            mResponsePoster.Execute(new ResponseDeliveryRunnable(request, response, runnable));
        }

        public void PostError(Request request, VolleyError error)
        {
            request.AddMarker("post-error");
            Response response = Response.Error(error);
            mResponsePoster.Execute(new ResponseDeliveryRunnable(request, response, null));
        }

        public void Execute(Java.Lang.IRunnable command)
        {
            mHandler.Post(command);
        }

        public ExecutorDelivery(Java.Util.Concurrent.IExecutor executor)
        {
            mResponsePoster = executor;
        }
    }
}