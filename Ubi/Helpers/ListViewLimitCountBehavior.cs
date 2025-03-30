using System;
using System.Linq;
using System.Collections;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    /// <summary>
    /// This is a sample behavior that helps controls the amount of items displayed in a listview.
    /// It assumes the ListView has a RowHeight defined so it can set the exact HeightRequest 
    /// allowing the listview to be included in a layout with an external scrollview.
    /// </summary>
    public class ListViewLimitCountBehavior : Behavior<ListView>
    {
        private ListView _listView;
        private IEnumerable _originalSource;

        public static BindableProperty CountLimitProperty =
            BindableProperty.Create(
                nameof(CountLimit),
                typeof(int),
                typeof(Badge),
                defaultValue: -1,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnCountLimitChanged
            );

        private static void OnCountLimitChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ListViewLimitCountBehavior)bindable).Update();
        }

        public int CountLimit
        {
            get { return (int)GetValue(CountLimitProperty); }
            set { SetValue(CountLimitProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            _listView = bindable;

            if (_listView != null)
            {
                _listView.PropertyChanged += OnListViewPropertyChanged;
                _originalSource = _listView.ItemsSource;

                Update();
            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            if (_listView != null)
            {
                _listView.PropertyChanged -= OnListViewPropertyChanged;
                _listView = null;
            }

            base.OnDetachingFrom(bindable);
        }

        private void OnListViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_originalSource == null && e.PropertyName == ListView.ItemsSourceProperty.PropertyName)
            {
                _originalSource = _listView.ItemsSource;
                Update();
            }
        }

        private void Update()
        {
            if (_listView != null && _originalSource != null)
            {
                int count;
                if (CountLimit < 0)
                {
                    _listView.ItemsSource = _originalSource;
                    count = _originalSource.Cast<object>().Count();
                }
                else
                {
                    var source = _originalSource.Cast<object>().Take(CountLimit).ToList();
                    count = source.Count;
                    _listView.ItemsSource = source;
                }

                UpdateHeight(count);
            }
        }

        private void UpdateHeight(int count)
        {
            const int ItemSeparation = 2;
            const int AbsolutePadding = 20;

            if (count == 0)
            {
                _listView.HeightRequest = -1;
            }
            else
            {
                _listView.HeightRequest = (_listView.RowHeight + ItemSeparation) * count + AbsolutePadding;
            }
        }
    }
}

