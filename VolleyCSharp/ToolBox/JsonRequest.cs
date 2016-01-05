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
using Newtonsoft.Json;
using VolleyCSharp.Utility;
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

namespace VolleyCSharp.ToolBox
{
    /// <summary>
    /// �й�Json������
    /// ����������C#�±Ƚ����е�Newtonsoft.Json
    /// ��ȥ����JsonArrayRequest��JsonObjectReqeust
    /// </summary>
    public class JsonRequest<T, R> : Request
        where R : class
        where T : class
    {
        protected static String PROTOCOL_CHARSET = "utf-8";
        private static String PROTOCOL_CONTENT_TYPE = "text/json;charset=utf-8";

        private Action<R> mListener;
        private T mRequestBody;

        public JsonRequest(String url, T requestBody, Action<R> listener, Action<VolleyError> errorListener)
            : this(Method.DEPRECATED_GET_OR_POST, url, requestBody, listener, errorListener) { }

        public JsonRequest(Method method, String url, T requestBody, Action<R> listener, Action<VolleyError> errorListener)
            : base(method, url, errorListener)
        {
            mListener = listener;
            mRequestBody = requestBody;
        }

        public override void DeliverResponse(String response)
        {
            mListener(JsonConvert.DeserializeObject<R>(response));
        }

        public override Response ParseNetworkResponse(NetworkResponse response)
        {
            Java.Lang.String parsed;
            try
            {
                parsed = new Java.Lang.String(response.Data, HttpHeaderParser.ParseCharset(response.Headers));
            }
            catch (Java.IO.UnsupportedEncodingException)
            {
                parsed = new Java.Lang.String(response.Data);
            }
            return Response.Success(parsed.ToString(), HttpHeaderParser.ParseCacheHeaders(response));
        }

        public override string GetPostBodyContentType()
        {
            return GetBodyContentType();
        }

        public override byte[] GetPostBody()
        {
            return GetBody();
        }

        public override string GetBodyContentType()
        {
            return PROTOCOL_CONTENT_TYPE;
        }

        public override byte[] GetBody()
        {
            try
            {
                return mRequestBody == null ? null : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mRequestBody));
            }
            catch (Exception)
            {
                VolleyLog.WTF("Unsupported Encoding while trying to get the bytes of {0} using {1}",
                    mRequestBody, PROTOCOL_CHARSET);
                return null;
            }
        }
    }
}