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
using VolleyCSharp.Delivery;
using System.Collections.Concurrent;
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

namespace VolleyCSharp.CacheCom
{
    /// <summary>
    /// ���洦�����
    /// ������������ڻ����е�������������󲻴�����ת���������������
    /// </summary>
    public class CacheDispatcher : Java.Lang.Thread
    {
        private static bool DEBUG = VolleyLog.DEBUG;

        /*
         * ԭjava����ñհ�����RequestQueue�е����±�����
         * ��C#�汾���ù��캯��ע�����ʽ�����ҽ�ԭ����
         * ��ͨ���иĳ�֧�ֶ��̵߳Ķ���
         */
        private ConcurrentQueue<Request> mCacheQueue;
        private ConcurrentQueue<Request> mNetworkQueue;

        private ICache mCache;
        private IResponseDelivery mDelivery;
        private volatile bool mQuit = false;

        public CacheDispatcher(ConcurrentQueue<Request> cacheQueue, ConcurrentQueue<Request> networkQueue, ICache cache, IResponseDelivery delivery)
        {
            this.mCacheQueue = cacheQueue;
            this.mNetworkQueue = networkQueue;
            this.mCache = cache;
            this.mDelivery = delivery;
        }

        public void Quit()
        {
            mQuit = true;
            Interrupt();
        }

        public override void Run()
        {
            if (DEBUG)
            {
                VolleyLog.V("start new dispatcher");
            }
            Process.SetThreadPriority(ThreadPriority.Background);
            mCache.Initialize();

            while (true)
            {
                try
                {
                    Request request = null;
                    if (!mCacheQueue.TryDequeue(out request))
                    {
                        if (mQuit)
                        {
                            return;
                        }
                        continue;
                    }
                    request.AddMarker("cache-queue-take");

                    //�����Ƿ���ȡ��
                    if (request.IsCanceled)
                    {
                        request.Finish("cache-discard-canceled");
                        continue;
                    }

                    //�����ڸû���
                    Entry entry = mCache.Get(request.GetCacheKey());
                    if (entry == null)
                    {
                        request.AddMarker("cache-miss");
                        mNetworkQueue.Enqueue(request);
                        continue;
                    }

                    //�������
                    if (entry.IsExpired)
                    {
                        request.AddMarker("cache-hit-expired");
                        request.CacheEntry = entry;
                        mNetworkQueue.Enqueue(request);
                        continue;
                    }

                    //��������
                    request.AddMarker("cache-hit");
                    Response response = request.ParseNetworkResponse(new NetworkResponse(entry.Data, entry.ResponseHeaders));
                    request.AddMarker("cache-hit-parsed");

                    //�жϻ����Ƿ���Ҫ����
                    if (!entry.RefreshNeeded())
                    {
                        mDelivery.PostResponse(request, response);
                    }
                    else
                    {
                        request.AddMarker("cache-hit-refresh-needed");
                        request.CacheEntry = entry;
                        response.Intermediate = true;
                        mDelivery.PostResponse(request, response, () =>
                        {
                            mNetworkQueue.Enqueue(request);
                        });
                    }
                }
                catch (Exception)
                {
                    if (mQuit)
                    {
                        return;
                    }
                    continue;
                }
            }
        }
    }
}