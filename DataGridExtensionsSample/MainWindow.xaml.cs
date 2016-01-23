﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataGridExtensions;

namespace DataGridExtensionsSample
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Random _rand = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // Sample usage of the filtering event
            Grid1.GetFilter().Filtering += Grid1_Filtering;

            ExternalFilter = item => ((DataItem)item).Column1.Contains("7");
        }

        void Grid1_Filtering(object sender, DataGridFilteringEventArgs e)
        {
            // Here we could prepare some data or even cancel the filtering process.

            Dispatcher.BeginInvoke(new Action(Grid1_Filtered));
        }

        void Grid1_Filtered()
        {
            // Here we could show some information about the result of the filtering.

            Trace.WriteLine(Grid1.Items.Count);
        }

        /// <summary>
        /// Provide a simple list of 100 random items.
        /// </summary>
        public IEnumerable<DataItem> Items
        {
            get
            {
                return Enumerable.Range(0, 100).Select(index => new DataItem(_rand, index)).ToArray();
            }
        }

        public Predicate<object> ExternalFilter
        {
            get { return (Predicate<object>)GetValue(ExternalFilterProperty); }
            set { SetValue(ExternalFilterProperty, value); }
        }
        public static readonly DependencyProperty ExternalFilterProperty = DependencyProperty.Register("ExternalFilter", typeof(Predicate<object>), typeof(MainWindow));

        private IList<IList<string>> _clipboard;


        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Use the integer filter for the integer column.
            if (e.PropertyType == typeof(int))
            {
                e.Column.SetTemplate((ControlTemplate)FindResource("IntegerFilter"));
            }
            else if ((e.PropertyType != typeof(bool)) && e.PropertyType.IsPrimitive)
            {
                // Hide the filter for all other primitive data types except bool.
                // Here we will hide the filter for the double DataItem.Probability.
                e.Column.SetIsFilterVisible(false);
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // must defer action, filters not yet available in OnLoaded.
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() =>
            {
                // Sample to manipulate the filter values by code.
                Grid1.Columns[0].SetFilter("True");
                Grid1.Columns[2].SetFilter("3");
            }));
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            _clipboard = CopyPasteDataGrid.GetCellSelection();

        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            if (_clipboard == null)
                return;

            CopyPasteDataGrid.PasteCells(_clipboard);
        }
    }
}
