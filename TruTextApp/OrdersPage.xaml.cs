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
    public sealed partial class OrdersPage : Page
    {
        // Create variables used to access the db and to populate
        // data for the comboBox
        private const string ServerUrl = "http://localhost:50000/";
        private HttpClient client = null;
        private List<ComboBoxPairs> comboData = null;

        public OrdersPage()
        {
            this.InitializeComponent();

            // The Combo Box needs all the parent table's primary keys
            // to use as foreign keys in the child table.
            // Read the parent table's primary keys - and a second column
            // that can be used as comboBox display.            

            // Use a separate "task method" to read these records, since the "await" function
            // must be used inside a "task method". The comboBox must receive this data
            // before the view is bound to the viewmodel, and therefore "await" is used to 
            // halt execution until the data for the comboBox has been compiled.
            GetComboDataAsync();
        }

        public async Task GetComboDataAsync()
        {
            // Initiate connection to the db
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(ServerUrl);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Populate the existing comboData with an empty list
            comboData = new List<ComboBoxPairs>();

            // Use the api controller task that returns all roles from the db
            var response = await this.client.GetAsync("api/Suppliers"); /*****/
            if (response.IsSuccessStatusCode)
            {
                // Read all roles from the database
                var recordData = await response.Content.ReadAsStringAsync();
                var SupList = JsonConvert.DeserializeObject<List<OrdersCl>>(recordData); /*****/

                // Insert the roles into ComboBoxPairs objects and store it in the
                // coomboData field
                for (int i = 0; i < SupList.Count; i++)
                {
                    var key = SupList[i].SupID;
                    var value = SupList[i].SupID;
                    comboData.Add(new ComboBoxPairs(key, value.ToString()));
                }

                this.SupID.SelectedValuePath = "_Key";
                this.SupID.DisplayMemberPath = "_Value";
                // Bind the comboBox to the data
                this.SupID.ItemsSource = comboData;


                // Create a ViewModel object based on the ViewModel class. 
                SaleVm viewModel = new SaleVm();

                // Load the records into recordList
                viewModel.GetDataAsync();

                //Data-Initialization-C Step 10: Instruct the MainPage.xml to use this viewModel to retrieve and store its data
                this.DataContext = viewModel;
            }
            else
            {
                comboData.Add(new ComboBoxPairs(0, "No records found"));
            }

            // Initiate connection to the db
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(ServerUrl);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Populate the existing comboData with an empty list
            comboData = new List<ComboBoxPairs>();

            // Use the api controller task that returns all roles from the db
            var Response = await this.client.GetAsync("api/Employees"); /*****/
            if (Response.IsSuccessStatusCode)
            {
                // Read all roles from the database
                var recordData = await Response.Content.ReadAsStringAsync();
                var CustList = JsonConvert.DeserializeObject<List<OrdersCl>>(recordData); /*****/

                // Insert the roles into ComboBoxPairs objects and store it in the
                // coomboData field
                for (int i = 0; i < CustList.Count; i++)
                {
                    var key = CustList[i].EmpID;
                    var value = CustList[i].EmpID;
                    comboData.Add(new ComboBoxPairs(key, value.ToString()));
                }

                this.EmpID.SelectedValuePath = "_Key";
                this.EmpID.DisplayMemberPath = "_Value";
                // Bind the comboBox to the data
                this.EmpID.ItemsSource = comboData;


                // Create a ViewModel object based on the ViewModel class. 
                SaleVm viewModel = new SaleVm();

                // Load the records into recordList
                viewModel.GetDataAsync();

                //Data-Initialization-C Step 10: Instruct the MainPage.xml to use this viewModel to retrieve and store its data
                this.DataContext = viewModel;
            }
            else
            {
                comboData.Add(new ComboBoxPairs(0, "No records found"));
            }
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
