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

namespace VolleyCSharp.NetCom
{
    /// <summary>
    /// �������������
    /// </summary>
    public class NetworkDispatcher : Java.Lang.Thread
    {
        private ConcurrentQueue<Request> mQueue;
        private INetwork mNetwork;
        private ICache mCache;
        private IResponseDelivery mDelivery;
        private volatile bool mQuit = false;

        public NetworkDispatcher(ConcurrentQueue<Request> queue, INetwork network, ICache cache, IResponseDelivery delivery)
        {
            this.mQueue = queue;
            this.mNetwork = network;
            this.mCache = cache;
            this.mDelivery = delivery;
        }

        public void Quit()
        {
            mQuit = true;
            Interrupt();
        }

        private void AddTrafficStatsTag(Request request)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.IceCreamSandwich)
            {
                Android.Net.TrafficStats.ThreadStatsTag = request.TrafficStatsTag;
            }
        }

        public override void Run()
        {
            Process.SetThreadPriority(ThreadPriority.Background);
            while (true)
            {
                long startTimeMs = SystemClock.ElapsedRealtime();
                Request request;
                if (!mQueue.TryDequeue(out request))
                {
                    if (mQuit)
                    {
                        return;
                    }
                    continue;
                }

                try
                {
                    request.AddMarker("network-queue-take");

                    if (request.IsCanceled)
                    {
                        request.Finish("network-discard-cancelled");
                        continue;
                    }

                    AddTrafficStatsTag(request);

                    NetworkResponse networkResponse = mNetwork.PerformRequest(request);
                    request.AddMarker("network-http-complete");

                    if (networkResponse.NotModified && request.HasHadResponseDelivered())
                    {
                        request.Finish("not-modified");
                        continue;
                    }

                    Response response = request.ParseNetworkResponse(networkResponse);
                    request.AddMarker("network-parse-complete");

                    if (request.ShouldCache() && response.CacheEntry != null)
                    {
                        mCache.Put(request.GetCacheKey(), response.CacheEntry);
                        request.AddMarker("network-cache-written");
                    }

                    request.MarkDelivered();
                    mDelivery.PostResponse(request, response);
                }
                catch (VolleyError volleyError)
                {
                    volleyError.NetworkTimeMs = SystemClock.ElapsedRealtime() - startTimeMs;
                    ParseAndDeliverNetworkError(request, volleyError);
                }
                catch (Exception e)
                {
                    VolleyLog.E(e, "Unhandled exception {0}", e.ToString());
                    VolleyError volleyError = new VolleyError(e);
                    volleyError.NetworkTimeMs = SystemClock.ElapsedRealtime() - startTimeMs;
                    mDelivery.PostError(request, volleyError);
                }
            }
        }

        private void ParseAndDeliverNetworkError(Request request, VolleyError error)
        {
            error = request.ParseNetworkError(error);
            mDelivery.PostError(request, error);
        }
    }
}