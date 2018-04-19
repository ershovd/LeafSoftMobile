using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms;
using ExLeafSoftApplication.Droid.Services;
using ExLeafSoftApplication.Common;
using Android.Content;
using Plugin.CurrentActivity;

namespace ExLeafSoftApplication.Droid
{
    [Activity(Label = "ExLeafSoftApplication",AlwaysRetainTaskState =true, Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //RegisterActivityLifecycleCallbacks(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);
            CrossCurrentActivity.Current.Activity = this;
            UserDialogs.Init(this);
            LoadApplication(new App());
            WireUpLongRunningTask();
        }
        protected override void OnStop()
        {
            base.OnStop();
        }

        //private void StartBackgroundDataRefreshService()
        //{
        //    var pt = new PeriodicTask.Builder()
        //        .SetPeriod(1800) // in seconds; minimum is 30 seconds
        //        .SetService(Java.Lang.Class.FromType(typeof(LongRunningTaskService)))
        //        .SetRequiredNetwork(0)
        //        .SetTag(your package name) // package name
        //        .Build();

        //    GcmNetworkManager.GetInstance(this).Schedule(pt);
        //}

    


        void WireUpLongRunningTask()
        {
            MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(LongRunningTaskService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(LongRunningTaskService));
                StopService(intent);
            });
        }

    }
}

