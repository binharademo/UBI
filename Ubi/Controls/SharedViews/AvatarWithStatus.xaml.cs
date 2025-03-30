using System;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public enum AvatarStatus
    {
        Offline = 0,
        Online,
        Away,
        Busy
    }

    public partial class AvatarWithStatus : ContentView
    {
        public AvatarWithStatus()
        {
            InitializeComponent();
        }

        public static BindableProperty SourceProperty =
            BindableProperty.Create(
                nameof(Source),
                typeof(ImageSource),
                typeof(AvatarWithStatus),
                defaultValue: null
            );

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static BindableProperty StatusProperty =
            BindableProperty.Create(
                nameof(Status),
                typeof(string),
                typeof(AvatarWithStatus),
                defaultValue: default(AvatarStatus).ToString()
            );

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Convenience property
        public AvatarStatus StatusEnum
        {
            get
            {
                return Enum.TryParse(Status, out AvatarStatus result) ?
                    result :
                    default(AvatarStatus);
            }

            set { Status = value.ToString(); }
        }

    }
}
