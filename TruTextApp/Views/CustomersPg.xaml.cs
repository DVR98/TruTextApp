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

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TruTextApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomersPg : Page
    {
        // Create variables used to access the db and to populate
        // data for the comboBox
        private const string ServerUrl = "http://localhost:50000/";
        private HttpClient client = null;
        private List<ComboBoxPairs> comboData = null;

        public CustomersPg()
        {
            this.InitializeComponent();

            //Data-Initialization-B Step 2: Create a ViewModel object based on the ViewModel class. 
            CustomerVm viewModel = new CustomerVm();

            // Load the records into recordList
            viewModel.GetDataAsync();

            //Data-Initialization-C Step 10: Instruct the MainPage.xml to use this viewModel to retrieve and store its data
            this.DataContext = viewModel;
        }

        private void MenuPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var CanEdit = (bool)e.Parameter;

            if (CanEdit == false)
            {
                addRecord.IsEnabled = false;
                editRecord.IsEnabled = false;
                saveChanges.IsEnabled = false;
                discardChanges.IsEnabled = false;
            }
            else
            {
                addRecord.IsEnabled = true;
                editRecord.IsEnabled = true;
                saveChanges.IsEnabled = true;
                discardChanges.IsEnabled = true;
            }
        }
    }
}