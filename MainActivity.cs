using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using GroupRecyclerView.Widgets;

namespace GroupRecyclerView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            List<IItemGroup> groups = new List<IItemGroup>
            {
                new ItemGroup("Enabled")
                {
                    "Item 1",
                    "Item 2",
                    "Item 3",
                    "Item 4"
                },
                new ItemGroup("Disabled")
                {
                    "Item 5",
                    "Item 6"
                }
            };
            DraggableGroupRecyclerView recyclerView = FindViewById<DraggableGroupRecyclerView>(Resource.Id.RecyclerView);
            recyclerView.ItemsSource = groups;
            recyclerView.ItemClick += RecyclerView_ItemClick;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RecyclerView_ItemClick(object sender, object e)
        {
            System.Diagnostics.Debug.WriteLine($"Item clicked {e}", "DEBUG");
        }

        private class ItemGroup : List<string>, IItemGroup
        {
            public ItemGroup(string title)
            {
                GroupTitle = title;
            }

            public string GroupTitle { get; }
        }
    }
}
