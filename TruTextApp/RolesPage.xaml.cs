﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TruTextApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RolePage : Page
    {
        public RolePage()
        {
            this.InitializeComponent();

            //Data-Initialization-B Step 2: Create a ViewModel object based on the ViewModel class. 
            RoleVm viewModel = new RoleVm();

            // Load the records into recordList
            viewModel.GetDataAsync();

            //Data-Initialization-C Step 10: Instruct the MainPage.xml to use this viewModel to retrieve and store its data
            this.DataContext = viewModel;
        }

        private void MenuPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
