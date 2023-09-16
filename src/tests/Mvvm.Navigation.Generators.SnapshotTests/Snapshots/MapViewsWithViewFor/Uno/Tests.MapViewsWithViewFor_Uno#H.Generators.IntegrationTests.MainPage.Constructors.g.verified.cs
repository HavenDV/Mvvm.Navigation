//HintName: H.Generators.IntegrationTests.MainPage.Constructors.g.cs
#nullable enable

namespace H.Generators.IntegrationTests
{
    public partial class MainPage
    {
        partial void BeforeInitializeComponent();
        partial void AfterInitializeComponent();

        public MainPage()
        {
            BeforeInitializeComponent();
            InitializeComponent();
            AfterInitializeComponent();
        }

        public MainPage(global::H.Generators.IntegrationTests.MainViewModel viewModel)
        {
            ViewModel = viewModel;

            BeforeInitializeComponent();
            InitializeComponent();
            AfterInitializeComponent();

            global::Mvvm.Navigation.Activation.Register(viewModel, this);
        }
    }
}