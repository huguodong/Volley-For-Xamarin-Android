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

/*
 * ԭ����Github��java����https://github.com/mcxiaoke/android-volley
 * 
 * C#���ߣ�Y-Z-F
 * ���͵�ַ��http://www.cnblogs.com/yaozhenfa/
 * Github��ַ��https://github.com/yaozhenfa/
 * 
 * 15.4.15 ���ͨ��
 */

namespace VolleyCSharp.CacheCom
{
    /// <summary>
    /// �����Ҫ�Զ��建�����ʵ�ָ���
    /// ������CacheDispatcher�Ĺ��캯���д���
    /// </summary>
    public interface ICache
    {
        Entry Get(String key);
        void Put(String key, Entry entry);
        void Initialize();
        void Invalidate(String key, bool fullExpire);
        void Remove(String key);
        void Clear();
    }
}