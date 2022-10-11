﻿namespace Orc.FeatureToggles.Views
{
    using Catel.Windows;
    using Orc.FeatureToggles.ViewModels;

    public partial class ManageFeatureTogglesWindow : DataWindow
    {
        public ManageFeatureTogglesWindow()
            : this(null)
        {
        }

        public ManageFeatureTogglesWindow(ManageFeatureTogglesViewModel? viewModel)
            : base(viewModel, DataWindowMode.Close)
        {
            InitializeComponent();
        }
    }
}
