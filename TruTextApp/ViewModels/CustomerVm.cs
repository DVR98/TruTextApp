using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace TruTextApp
{

    class CustomerVm : INotifyPropertyChanged /*****/
    {
        private List<CustomersCl> recordList;  /*****/

        private int recordIndex;
        public Command NextRecord { get; private set; }
        public Command PreviousRecord { get; private set; }
        public Command FirstRecord { get; private set; }
        public Command LastRecord { get; private set; }
        public Command AddRecord { get; private set; }
        public Command EditRecord { get; private set; }
        public Command DiscardChanges { get; private set; }
        public Command SaveChanges { get; private set; }


        private const string ServerUrl = "http://localhost:50000/";
        private HttpClient client = null;

        public CustomerVm()  /*****/
        {
            this.recordIndex = 0;
            this.IsAtStart = true;
            this.IsAtEnd = false;

            this.NextRecord = new Command(this.Next,
                () => {
                    return this.CanBrowse &&
                    this.recordList != null && !this.IsAtEnd;
                });
            this.PreviousRecord = new Command(this.Previous,
                () => {
                    return this.CanBrowse &&
                    this.recordList != null && !this.IsAtStart;
                });
            this.FirstRecord = new Command(this.First,
                () => {
                    return this.CanBrowse &&
                    this.recordList != null && !this.IsAtStart;
                });
            this.LastRecord = new Command(this.Last,
                () => {
                    return this.CanBrowse &&
                    this.recordList != null && !this.IsAtEnd;
                });
            this.AddRecord = new Command(this.Add,
                () => { return this.CanBrowse; });
            this.EditRecord = new Command(this.Edit,
                () => { return this.CanBrowse; });
            this.DiscardChanges = new Command(this.Discard,
                () => { return this.CanSaveOrDiscardChanges; });
            this.SaveChanges = new Command(this.SaveAsync,
                () => { return this.CanSaveOrDiscardChanges; });



            this.recordList = null;
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(ServerUrl);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Methods for fetching and updating data

        // Create a new (empty) record
        // and put the form into Adding mode
        private void Add()
        {
            CustomersCl newRecord = new CustomersCl { CustomerID = 0 };  /*****2*/
            this.recordList.Insert(recordIndex, newRecord);
            this.IsAdding = true;
            this.OnPropertyChanged(nameof(Current));
        }

        // Edit the current record
        // - save the existing details of the record
        //   and put the form into Editing mode
        private CustomersCl oldRecord;  /*****/
        private void Edit()
        {
            this.oldRecord = new CustomersCl();  /*****/
            this.CopyRecord(this.Current, this.oldRecord);
            this.IsEditing = true;
        }

        // Discard changes made while in Adding or Editing mode
        // and return the form to Browsing mode
        private void Discard()
        {
            // If the user was adding a new record, then remove it
            if (this.IsAdding)
            {
                this.recordList.Remove(this.Current);
                this.OnPropertyChanged(nameof(Current));
            }

            // If the user was editing an existing record,
            // then restore the saved details
            if (this.IsEditing)
            {
                this.CopyRecord(this.oldRecord, this.Current);
            }

            this.IsBrowsing = true;
            this.LastError = String.Empty;
        }

        // Save the new or updated record back to the web service
        // and return the form to Browsing mode
        private async void SaveAsync()
        {
            // Validate the details of the record
            if (this.ValidateRecord(this.Current))
            {
                // Only continue if the record details are valid
                this.IsBusy = true;
                try
                {
                    // Convert the current record into HTTP request format with a JSON payload
                    var serializedData = JsonConvert.SerializeObject(this.Current);
                    StringContent content =
                        new StringContent(serializedData, Encoding.UTF8, "text/json");

                    // If the user is adding a new record,
                    // send an HTTP POST request to the web service with the details
                    if (this.IsAdding)
                    {
                        var response =
                            await client.PostAsync("api/Customers", content);  /*****/
                        if (response.IsSuccessStatusCode)
                        {
                            // Get the ID of the newly created record and display it
                            Uri recordUri = response.Headers.Location;
                            var newRecord = await this.client.GetAsync(recordUri);
                            if (newRecord.IsSuccessStatusCode)
                            {
                                var recordData = await newRecord.Content.ReadAsStringAsync();
                                this.CopyRecord(
                                    JsonConvert.DeserializeObject<CustomersCl>(recordData), this.Current);  /*****/
                                this.OnPropertyChanged(nameof(Current));
                                this.IsAdding = false;
                                this.IsBrowsing = true;
                                this.LastError = String.Empty;
                            }
                            else
                            {
                                // Handle GET failure
                                this.LastError = response.ReasonPhrase;
                            }

                        }
                        // Handle POST failure
                        else
                        {
                            this.LastError = response.ReasonPhrase;
                        }

                    }
                    // The user must be editing an existing record,
                    // so send the details by using a PUT request
                    else
                    {
                        string path = $"api/Customers/{this.Current.CustomerID}";  /*****2*/

                        var response = await client.PutAsync(path, content);
                        if (response.IsSuccessStatusCode)
                        {
                            this.IsEditing = false;
                            this.IsBrowsing = true;
                            this.LastError = String.Empty;
                        }
                        // Handle PUT failure
                        else
                        {
                            this.LastError = response.ReasonPhrase;
                        }

                    }
                }
                catch (Exception e)
                {
                    // Handle exceptions
                    this.LastError = e.Message;
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
        }


        // Helper method to validate record details
        private bool ValidateRecord(CustomersCl record)  /*****M*/
        {
            string validationErrors = string.Empty;
            bool hasErrors = false;

            if (string.IsNullOrWhiteSpace(record.Firstname))
            {
                hasErrors = true;
                validationErrors = "Name must not be empty\n";
            }

            if (string.IsNullOrWhiteSpace(record.Firstname))
            {
                hasErrors = true;
                validationErrors = "Surname must not be empty\n";
            }

            // Email address is a series of characters that do not include a space or @
            // followed by @
            // followed by a series of characters that do not include a space or @
            // followed by .
            // followed by a series of characters that do not include a space or 

            this.LastError = validationErrors;
            return !hasErrors;
        }

        // Utility method for copying the details of a record
        private void CopyRecord(CustomersCl source, CustomersCl destination)  /*****M*/
        {
            destination.CustomerID = source.CustomerID;
            destination.Firstname = source.Firstname;
            destination.Surname = source.Surname;
            destination.Address = source.Address;
            destination.City = source.City;
            destination.Cellnumber = source.Cellnumber;
        }

        public async Task GetDataAsync()
        {
            try
            {
                this.IsBusy = true;
                //await Task.Delay(5000);
                var response = await this.client.GetAsync("api/Customers");  /*****/
                if (response.IsSuccessStatusCode)
                {
                    var recordData = await response.Content.ReadAsStringAsync();

                    this.recordList = JsonConvert.DeserializeObject<List<CustomersCl>>(recordData);  /*****/

                    this.recordIndex = 0;
                    this.OnPropertyChanged(nameof(Current));
                    this.IsAtStart = true;
                    this.IsAtEnd = (this.recordList.Count == 0);
                    this.LastError = String.Empty;
                }
                else
                {
                    this.LastError = response.ReasonPhrase;
                }
            }
            catch (Exception e)
            {
                this.LastError = e.Message;
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        #endregion

        #region Properties for "busy" and error message handling

        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set
            {
                this._isBusy = value;
                this.OnPropertyChanged(nameof(IsBusy));
            }
        }

        private string _lastError = null;
        public string LastError
        {
            get { return this._lastError; }
            private set
            {
                this._lastError = value;
                this.OnPropertyChanged(nameof(LastError));
            }
        }

        #endregion

        #region Properties for managing the edit mode

        // Manage the edit mode of the form - is the user browsing, adding a record, or editing a record
        private enum EditMode { Browsing, Adding, Editing };
        private EditMode editMode;
        public bool IsBrowsing
        {
            get { return this.editMode == EditMode.Browsing; }
            private set
            {
                if (value)
                {
                    this.editMode = EditMode.Browsing;
                }
                this.OnPropertyChanged(nameof(IsBrowsing));
                this.OnPropertyChanged(nameof(IsAdding));
                this.OnPropertyChanged(nameof(IsEditing));
                this.OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        public bool IsAdding
        {
            get { return this.editMode == EditMode.Adding; }
            private set
            {
                if (value)
                {
                    this.editMode = EditMode.Adding;
                }
                this.OnPropertyChanged(nameof(IsBrowsing));
                this.OnPropertyChanged(nameof(IsAdding));
                this.OnPropertyChanged(nameof(IsEditing));
                this.OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        public bool IsEditing
        {
            get { return this.editMode == EditMode.Editing; }
            private set
            {
                if (value)
                {
                    this.editMode = EditMode.Editing;
                }
                this.OnPropertyChanged(nameof(IsBrowsing));
                this.OnPropertyChanged(nameof(IsAdding));
                this.OnPropertyChanged(nameof(IsEditing));
                this.OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        public bool IsAddingOrEditing
        {
            get { return this.IsAdding || this.IsEditing; }
        }

        private bool CanBrowse
        {
            get
            {
                return this.IsBrowsing &&
                   this.client != null;
            }
        }

        private bool CanSaveOrDiscardChanges
        {
            get
            {
                return this.IsAddingOrEditing &&
                       this.client != null;
            }
        }

        #endregion

        #region Methods and properties for navigation commands

        private bool _isAtStart;
        public bool IsAtStart
        {
            get { return this._isAtStart; }
            set
            {
                this._isAtStart = value;
                this.OnPropertyChanged(nameof(IsAtStart));
            }
        }

        private bool _isAtEnd;
        public bool IsAtEnd
        {
            get { return this._isAtEnd; }
            set
            {
                this._isAtEnd = value;
                this.OnPropertyChanged(nameof(IsAtEnd));
            }
        }

        public CustomersCl Current  /*****/
        {
            get
            {
                if (this.recordList != null)
                {
                    return this.recordList[recordIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        private void Next()
        {
            if (this.recordList.Count - 1 > this.recordIndex)
            {
                this.recordIndex++;
                this.OnPropertyChanged(nameof(Current));
                this.IsAtStart = false;
                this.IsAtEnd = (this.recordList.Count - 1 == this.recordIndex);
            }
        }

        private void Previous()
        {
            if (this.recordIndex > 0)
            {
                this.recordIndex--;
                this.OnPropertyChanged(nameof(Current));
                this.IsAtEnd = false;
                this.IsAtStart = (this.recordIndex == 0);
            }
        }

        private void First()
        {
            this.recordIndex = 0;
            this.OnPropertyChanged(nameof(Current));
            this.IsAtStart = true;
            this.IsAtEnd = (this.recordList.Count == 0);
        }

        private void Last()
        {
            this.recordIndex = this.recordList.Count - 1;
            this.OnPropertyChanged(nameof(Current));
            this.IsAtEnd = true;
            this.IsAtStart = (this.recordList.Count == 0);
        }

        #endregion

        #region INotifyPropertyChanged interface

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
