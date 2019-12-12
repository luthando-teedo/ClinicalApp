using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ControlExamples.Controls.Maps
{
    public class CustomPin : Pin
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(Pin), default(string));
        public static readonly BindableProperty UrlProperty = BindableProperty.Create("Url", typeof(string), typeof(Pin), default(string));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
    }
}
