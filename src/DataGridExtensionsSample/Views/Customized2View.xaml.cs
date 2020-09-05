﻿namespace DataGridExtensionsSample.Views
{
    using System.Windows.Data;

    using DataGridExtensions;

    using TomsToolbox.Wpf.Composition.AttributedModel;

    /// <summary>
    /// Interaction logic for Customized2View.xaml
    /// </summary>
    [DataTemplate(typeof(Customized2ViewModel))]
    public partial class Customized2View
    {
        public Customized2View()
        {
            InitializeComponent();

            BindingOperations.SetBinding(Column2, DataGridFilterColumn.FilterProperty, new Binding("DataContext.Column2Filter") { Source = this, Mode = BindingMode.TwoWay });
        }
    }
}
