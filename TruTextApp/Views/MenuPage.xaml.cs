using System;
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
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RolePage), CanEdit);
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeePage), CanEdit);
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CustomersPg), CanEdit);
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SalesPage), CanEdit);
        }

        private void SupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SupplierPage), CanEdit);
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BooksPage), CanEdit);
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrdersPage), CanEdit);
        }

        public Boolean CanEdit = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var Admins = (bool)e.Parameter;

            if (Admins == false)
            {
                //Only customers and orders can be displayed
                BooksButton.IsEnabled = false;
                SupplierButton.IsEnabled = false;
                SaleButton.IsEnabled = false;
                EmployeesButton.IsEnabled = false;
                RolesButton.IsEnabled = false;
                CustomerButton.IsEnabled = true;
                OrdersButton.IsEnabled = true;

                CanEdit = false;
            }
            else
            {
                BooksButton.IsEnabled = true;
                SupplierButton.IsEnabled = true;
                SaleButton.IsEnabled = true;
                EmployeesButton.IsEnabled = true;
                RolesButton.IsEnabled = true;
                CustomerButton.IsEnabled = true;
                OrdersButton.IsEnabled = true;

                CanEdit = true;
            }
        }
    }
}
