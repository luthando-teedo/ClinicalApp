using Prism;
using Prism.Ioc;
using Clinical.ViewModels;
using Clinical.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clinical.Services.Interfaces;
using Clinical.Services;
using System.IO;
using System;
using Clinical.Services.Interface;
using Clinical.Model.Security;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Clinical
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
   //     static BunchOfMethods database;
        public App() : this(null) { }

    
    public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("MasterDetail/NavigationPage/loginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISecurityService, FakeSecurityService>();
            containerRegistry.RegisterSingleton<IDatabase, BunchOfMethods>();
            containerRegistry.RegisterSingleton<IUserProfile, UserProfile>();


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterDetail, MasterDetailViewModel>();
            containerRegistry.RegisterForNavigation<FolderPage, FolderPageViewModel>();
            containerRegistry.RegisterForNavigation<AppointmentPage, AppointmentPageViewModel>();
            containerRegistry.RegisterForNavigation<loginPage, loginPageViewModel>();
            containerRegistry.RegisterForNavigation<DashBoardPage, DashBoardPageViewModel>();
            containerRegistry.RegisterForNavigation<RegPage, RegPageViewModel>();

            

            containerRegistry.RegisterForNavigation<RequestPage, RequestPageViewModel>();
            containerRegistry.RegisterForNavigation<EditPage, EditPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
        }
    }
}
