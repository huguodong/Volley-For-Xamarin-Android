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
    /// �����Ҫ��������ɺ�������ر���ʵ�ָýӿ�
    /// ����RequestQueue�Ĺ��캯�����滻
    /// </summary>
    public interface IResponseDelivery
    {
        void PostResponse(Request request, Response response);
        void PostResponse(Request request, Response response, Action runnable);
        void PostError(Request request, VolleyError error);
    }
}