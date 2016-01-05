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
using Android.Graphics;

namespace VolleyCSharp.ToolBox
{
    /// <summary>
    /// ÿ��ͼƬ�������Ϣ
    /// �����ص��ɹ���ʧ�ܷ������Լ�ȡ������
    /// </summary>
    public class ImageContainer : IImageListener
    {
        private String mCacheKey;
        private String mRequestUrl;
        private Dictionary<String, BatchedImageRequest> mInFlightRequests;
        private Dictionary<String, BatchedImageRequest> mBatchedResponses;

        /// <remarks>
        /// ԭjava�²��ñհ�����ʽʹ��mInFlightRequests��batchedResponses
        /// C#�²��ù��캯���ķ�ʽ���䴫�ݽ���
        /// </remarks>
        public ImageContainer(Bitmap bitmap, String requestUrl, String cacheKey,
            Dictionary<String, BatchedImageRequest> inFlightRequests,
            Dictionary<String, BatchedImageRequest> batchedResponses)
        {
            this.Bitmap = bitmap;
            this.mRequestUrl = requestUrl;
            this.mCacheKey = cacheKey;
            this.mInFlightRequests = inFlightRequests;
            this.mBatchedResponses = batchedResponses;
        }

        public void CancelRequest()
        {
            if (OnSuccessResponse == null)
            {
                return;
            }

            BatchedImageRequest request = null;
            mInFlightRequests.TryGetValue(mCacheKey, out request);
            if (request != null)
            {
                bool canceled = request.RemoveContainerAndCancelIfNecessary(this);
                if (canceled)
                {
                    mInFlightRequests.Remove(mCacheKey);
                }
            }
            else
            {
                mBatchedResponses.TryGetValue(mCacheKey, out request);
                if (request != null)
                {
                    request.RemoveContainerAndCancelIfNecessary(this);
                    if (request.Containers.Count == 0)
                    {
                        mBatchedResponses.Remove(mCacheKey);
                    }
                }
            }
        }

        public Bitmap Bitmap { get; set; }

        public String GetRequestUrl()
        {
            return mRequestUrl;
        }

        #region IImageListener

        public Action<ImageContainer, bool> OnSuccessResponse
        {
            get;
            private set;
        }

        public event Action<ImageContainer, bool> SuccessResponse
        {
            add
            {
                OnSuccessResponse += value;
            }
            remove
            {
                OnSuccessResponse -= value;
            }
        }

        public Action<VolleyError> OnErrorResponse
        {
            get;
            private set;
        }

        public event Action<VolleyError> ErrorResponse
        {
            add
            {
                OnErrorResponse += value;
            }
            remove
            {
                OnErrorResponse -= value;
            }
        }

        #endregion
    }
}