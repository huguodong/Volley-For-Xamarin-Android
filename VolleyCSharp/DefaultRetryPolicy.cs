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

namespace VolleyCSharp
{
    /// <summary>
    /// ���Բ���ʵ����
    /// </summary>
    public class DefaultRetryPolicy : IRetryPolicy
    {
        private int mCurrentTimeoutMs;
        private int mCurrentRetryCount;
        private int mMaxNumRetries;
        private float mBackoffMultiplier;

        public const int DEFAULT_TIMEOUT_MS = 2500;
        public const int DEFAULT_MAX_RETRIES = 0;
        public const float DEFAULT_BACKOFF_MULT = 1f;

        public DefaultRetryPolicy()
            : this(DEFAULT_TIMEOUT_MS, DEFAULT_MAX_RETRIES, DEFAULT_BACKOFF_MULT) { }

        public DefaultRetryPolicy(int initialTimeoutMs, int maxNumRetries, float backoffMultiplier)
        {
            this.mCurrentTimeoutMs = initialTimeoutMs;
            this.mMaxNumRetries = maxNumRetries;
            this.mBackoffMultiplier = backoffMultiplier;
        }

        public void Retry(VolleyError error)
        {
            mCurrentRetryCount++;
            mCurrentTimeoutMs += (mCurrentTimeoutMs * (int)mBackoffMultiplier);
            if (!HasAttemptRemaining)
            {
                throw error;
            }
        }

        protected bool HasAttemptRemaining
        {
            get { return mCurrentRetryCount <= mMaxNumRetries; }
        }

        public int CurrentTimeout
        {
            get { return mCurrentTimeoutMs; }
        }

        public int CurrentRetryCount
        {
            get { return mCurrentRetryCount; }
        }

        public float BackoffMultiplier
        {
            get { return mBackoffMultiplier; }
        }
    }
}