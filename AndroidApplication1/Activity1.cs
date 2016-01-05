using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace AndroidApplication1
{
    [Activity(Label = "AndroidApplication1", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private HorizontalScrollView horizontalScrollView;
        /* 右边更多导航菜单 */
        private Button right;
        /* 导航菜单集合 */
        private List<String> array;
        /* 导航菜单适配器 */
        //private TitleAdapter titleAdapter;
        /* 列宽 */
        private int COLUMNWIDTH = 75;
        /* 导航菜单布局 */
        private GridView category;
        /* 导航菜单容器，存放导航菜单布局 */
        private LinearLayout categoryLayout;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

        }
        private void FindViews()
        {
            horizontalScrollView = FindViewById<HorizontalScrollView>(Resource.Id)
            //right = (Button)findViewById(R.id.right);
            //categoryLayout = (LinearLayout)findViewById(R.id.category_layout);
            //// 新建一个GridView
            //category = new GridView(getApplicationContext());
        }
    }
}

