﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Instict2K19.DataModel;
using Instict2K19.Interface;
using Newtonsoft.Json;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace Instict2K19
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(ContentPage view) : base(view)
        {
            var task1 = Task.Run(async () =>
             {
                 using (var stream = await FileSystem.OpenAppPackageFileAsync("Colleges.json"))
                 {
                     using (var reader = new StreamReader(stream))
                     {
                         var jsonColleges = await reader.ReadToEndAsync();
                         if (jsonColleges == null)
                             throw new Exception("Unable to read files");
                         else
                         {
                             Colleges = JsonConvert.DeserializeObject<CollegeModel>(jsonColleges).Colleges;
                             if (Colleges == null)
                                 throw new Exception("Unable to deserialize data");
                         }
                     }
                 }
             });
            var task2 = Task.Run(async () =>
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("Events.json"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonEvents = await reader.ReadToEndAsync();
                        if (jsonEvents == null)
                            throw new Exception("Unable to read files");
                        else
                        {
                            Categories = JsonConvert.DeserializeObject<EventModel>(jsonEvents).Categories;
                            if (Categories == null)
                                throw new Exception("Unable to deserialize data");
                        }
                    }
                }
            });
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Task.WaitAll(task1, task2);
                }
                catch (AggregateException ex)
                {
                    base.View.DisplayAlert("Error", "Unalble to load data", "Ok");
                }
            });
        }

        private async Task<bool> Validation()
        {

            if (string.IsNullOrEmpty(ReceiptNo))
            {
                await base.View.DisplayAlert("Missing Detail", "Enter receipt number.", "Ok");
                return false;
            }
            string _collgeName = string.Empty;
            if (IsNonIUCollege)
            {
                if (string.IsNullOrEmpty(SelectedCollege))
                {
                    await base.View.DisplayAlert("Missing Detail", "Please select proper college.", "Ok");
                    return false;
                }
                if (IsOtherCollege)
                {
                    if (string.IsNullOrEmpty(OtherCollege))
                    {
                        await base.View.DisplayAlert("Missing Detail", "Please enter proper college name.", "Ok");
                        return false;
                    }
                    else
                    {
                        _collgeName = OtherCollege;
                    }
                }
                else
                {
                    _collgeName = SelectedCollege;
                }
            }
            else
            {
                _collgeName = "INDUS UNIVERSITY";
            }
            if (string.IsNullOrEmpty(PrimaryContactNumber))
            {
                await base.View.DisplayAlert("Missing Detail", "Please enter primary contact detail.", "Ok");
                return false;
            }
            string _primaryNumber = string.Empty;
            if (PrimaryContactNumber.Length < 10)
            {
                await base.View.DisplayAlert("Missing Detail", "Invalid primary contact.", "Ok");
                return false;
            }
            else
            {
                _primaryNumber = PrimaryContactNumber;
            }
            string _secondaryNumber = string.Empty;
            if (!string.IsNullOrEmpty(SecondaryContactNumber) && SecondaryContactNumber.Length < 10)
            {
                await base.View.DisplayAlert("Missing Detail", "Invalid secondary contact.", "Ok");
                return false;
            }
            else
            {
                _secondaryNumber = SecondaryContactNumber;
            }
            if (string.IsNullOrEmpty(EmailAddress))
            {
                await base.View.DisplayAlert("Missing Detail", "Please enter e-mail detail.", "Ok");
                return false;
            }
            bool isEmail = Regex.IsMatch(EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            string _email = string.Empty;
            if (!isEmail)
            {
                await base.View.DisplayAlert("Missing Detail", "Please enter valid e-mail detail.", "Ok");
                return false;
            }
            else
            {
                _email = EmailAddress;
            }
            string _category = string.Empty;
            string _subcategory = string.Empty;
            string _event = string.Empty;
            if (SelectedEvent == null)
            {
                await base.View.DisplayAlert("Missing Detail", "Select event to participate.", "Ok");
                return false;
            }
            else
            {
                _category = SelectedCategory.Name;
                _subcategory = SelectedSubGategory.Name;
                _event = SelectedEvent.Name;
            }
            if (NumberOfParticipants == 0)
            {
                await base.View.DisplayAlert("Missing Detail", "Enter member strength of group.", "Ok");
                return false;
            }

            string _participantName = string.Empty;
            if (string.IsNullOrEmpty(ParticipantName))
            {
                await base.View.DisplayAlert("Missing Detail", "Enter participant's or group's name.", "Ok");
                return false;
            }
            else
            {
                _participantName = ParticipantName;
            }
            if (SelectedEvent != null && !string.IsNullOrEmpty(SelectedEvent.Name) && SelectedEvent.Name.ToString().ToUpper().Contains("PUBG"))
            {
                if (string.IsNullOrEmpty(PUBGID))
                {
                    await base.View.DisplayAlert("Missing Detail", "Enter PubG ID.", "Ok");
                    return false;
                }
            }
            else
            {
                PUBGID = string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Receipt Number: " + ReceiptNo);
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("College Details ");
            sb.Append("\n");
            sb.Append("Name:  " + _collgeName);
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("Contact Details ");
            sb.Append("\n");
            sb.Append("Primary Number: " + PrimaryContactNumber);
            sb.Append("\n");
            sb.Append("Secondary Number: " + SecondaryContactNumber);
            sb.Append("\n");
            sb.Append("Email: " + EmailAddress);
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("Event Detail ");
            sb.Append("\n");
            sb.Append("Category: " + SelectedCategory.Name);
            sb.Append("\n");
            sb.Append("Sub Catagory: " + SelectedSubGategory.Name);
            sb.Append("\n");
            sb.Append("Event: " + SelectedEvent.Name);
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("Participate's or group's name: ");
            sb.Append("\n");
            sb.Append(ParticipantName);
            sb.Append("\n");
            if (!string.IsNullOrEmpty(PUBGID))
            {
                sb.Append("\n");
                sb.Append("PUBG ID: " + PUBGID.ToString());
                sb.Append("\n");
            }
            sb.Append("\n");
            sb.Append("Fees Charged: " + Fees.ToString());
            sb.Append("\n");
            if (await base.View.DisplayAlert("Confirm Registration", sb.ToString(), "Save", "Cancel"))
                return true;
            else
                return false;
        }

        private void SaveDataToDownloadFolder(string downloadPath, string csv)
        {
            if (File.Exists(downloadPath))
                File.Delete(downloadPath);
            if (!File.Exists(downloadPath))
            {
                var ds = File.Create(downloadPath);
                ds.Dispose();
                File.WriteAllText(downloadPath, csv);
            }
        }
        public static string ToCSV(DataTable table, string delimator = ",")
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : delimator);
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : delimator);
                }
            }
            return result.ToString().TrimEnd(new char[] { '\r', '\n' });
        }

        public ICommand SignOutCommand { get { return new Command(SignOutCommandEvent); } }
        private void SignOutCommandEvent()
        {
            Application.Current.MainPage = new NavigationPage(new NameAndGroupPage());
        }

        public ICommand SaveToDatabaseCommand { get { return new Command(async () => await SaveToDatabaseCommandEvent()); } }
        private async Task SaveToDatabaseCommandEvent()
        {
            if (await Validation())
            {
                RegisterModel registerModel = new RegisterModel()
                {
                    ID = ReceiptNo.ToUpper(),
                    RegisteredByGroup = Constants.GroupName.ToUpper(),
                    RegisteredBy = Constants.UserName.ToUpper(),
                    PrimaryContactNumber = PrimaryContactNumber,
                    SecondaryContactNumber = SecondaryContactNumber,
                    Email = EmailAddress,
                    Category = SelectedCategory.Name.ToUpper(),
                    SubCategory = SelectedSubGategory.Name.ToUpper(),
                    EventName = SelectedEvent.Name.ToUpper(),
                    ParticipantName = ParticipantName.ToUpper(),
                    NumberOfParticipants = NumberOfParticipants,
                    FeesCharged = Fees,
                    RegistrationDate = DateTime.Now.Date.ToString("MM/dd/yyyy"),
                    PUBGID = PUBGID,
                };
                string _collegeName = string.Empty;
                if (IsNonIUCollege)
                {
                    if (IsOtherCollege)
                        _collegeName = OtherCollege;
                    else
                        _collegeName = SelectedCollege;
                }
                else
                {
                    _collegeName = "INDUS UNIVERSITY";
                }
                if (IsNonIUCollege)
                {
                    if (IsOtherCollege)
                        _collegeName = OtherCollege;
                    else
                        _collegeName = SelectedCollege;
                }
                else
                {
                    _collegeName = "INDUS UNIVERSITY";
                }
                registerModel.CollgeName = _collegeName;


                try
                {
                    await App.Database.SaveItemAsync(registerModel);
                    ResetCommandEvent();
                    await base.View.DisplayAlert("Save", "Registered successfully", "Ok");

                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower() == "constraint")
                    {
                        await base.View.DisplayAlert("Save", "Receipt number already exist", "Ok");
                    }
                    else
                    {
                        await base.View.DisplayAlert("Save", ex.Message, "Ok");
                    }
                }
            }
        }

        public ICommand ResetCommand { get { return new Command(ResetCommandEvent); } }
        private void ResetCommandEvent()
        {
            ReceiptNo = string.Empty;
            PrimaryContactNumber = string.Empty;
            SecondaryContactNumber = string.Empty;
            EmailAddress = string.Empty;
            SelectedCategory = null;
            ParticipantName = string.Empty;
            Fees = 0;
            PUBGID = string.Empty;
            NumberOfParticipants = 0;
        }

        public ICommand ExportDatabaseCommand { get { return new Command(async () => await ExportDatabaseCommandEvent()); } }
        private async Task ExportDatabaseCommandEvent()
        {
            bool permissionGranted = await AskForPermission(Permission.Storage);
            if (permissionGranted)
            {
                try
                {
                    string downloadPath = DependencyService.Get<IFileHelper>().GetLocalFilePath(Constants.UserName + ".csv");
                    if (!string.IsNullOrEmpty(downloadPath))
                    {
                        var data = await App.Database.GetItemsAsync();
                        string jsonData = JsonConvert.SerializeObject(data);
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonData + "}}");
                            XmlDocument xmldoc = new XmlDocument();
                            //Create XmlDoc Object
                            xmldoc.LoadXml(xml.InnerXml);
                            //Create XML Steam 
                            var xmlReader = new XmlNodeReader(xmldoc);
                            DataSet dataSet = new DataSet();
                            //Load Dataset with Xml
                            dataSet.ReadXml(xmlReader);
                            //return single table inside of dataset
                            string csv = ToCSV(dataSet.Tables[0]);
                            if (!string.IsNullOrEmpty(csv))
                            {
                                SaveDataToDownloadFolder(downloadPath, csv);
                                await View.DisplayAlert("Export Database", "Database saved to your download folder.", "Ok");
                            }
                            else
                            {
                                await View.DisplayAlert("Export Database", "Unable to save data to storage.", "Ok");
                            }
                        }
                        else
                        {
                            await View.DisplayAlert("Export Database", "Unable to save data to storage.", "Ok");
                        }
                    }
                    else
                    {
                        await View.DisplayAlert("Export Database", "Unable to save data to storage.", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await View.DisplayAlert("Export Database", "Unable to save data to storage.", "Ok");
                }
            }
            else
            {
                await View.DisplayAlert("Export Database", "Unable to save data to storage.", "Ok");
            }
        }

        public ICommand TotalCollectionCommand { get { return new Command(async () => await TotalCollectionCommandEvent()); } }
        private async Task TotalCollectionCommandEvent()
        {
            double _totalCollection = 0;
            try
            {
                _totalCollection = await App.Database.GetTotalAsync();
            }
            catch (Exception)
            {
                _totalCollection = 0;
            }
            await View.DisplayAlert(Constants.AppName, "Total Collection: " + _totalCollection.ToString(), "Ok");
        }

        public ICommand CollgeTypeChangedCommand { get { return new Command<string>(CollgeTypeChangedCommandEvent); } }
        private void CollgeTypeChangedCommandEvent(string collgetype)
        {
            if (collgetype.ToUpper() == "IU")
            {
                SelectedCollege = null;
                IsNonIUCollege = false;
            }
            else
                IsNonIUCollege = true;
        }

        private double fees = 0;
        public double Fees
        {
            get { return fees; }
            set
            {
                fees = value;
                RaisePropertyChanged(() => Fees);
            }
        }
        private string participantName;
        public string ParticipantName
        {
            get { return participantName; }
            set
            {
                participantName = value;
                RaisePropertyChanged(() => ParticipantName);
            }
        }
        private string pUBGID;
        public string PUBGID
        {
            get { return pUBGID; }
            set
            {
                pUBGID = value;
                RaisePropertyChanged(() => PUBGID);
            }
        }

        private List<Category> categories = new List<Category>();
        public List<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }
        private SubGategory selectedSubGategory;
        public SubGategory SelectedSubGategory
        {
            get { return selectedSubGategory; }
            set
            {
                selectedSubGategory = value;
                RaisePropertyChanged(() => SelectedSubGategory);
            }
        }
        private List<Event> events = new List<Event>();
        public List<Event> Events
        {
            get { return events; }
            set
            {
                events = value;
                RaisePropertyChanged(() => Events);
            }
        }
        private Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                RaisePropertyChanged(() => SelectedEvent);
            }
        }



        private ObservableCollection<string> colleges = new ObservableCollection<string>();
        public ObservableCollection<string> Colleges
        {
            get { return colleges; }
            set
            {
                colleges = value;
                RaisePropertyChanged(() => Colleges);
            }
        }
        private bool isNonIUCollege = false;
        public bool IsNonIUCollege
        {
            get { return isNonIUCollege; }
            set
            {
                isNonIUCollege = value;
                RaisePropertyChanged(() => IsNonIUCollege);
            }
        }
        private string selectedCollege;
        public string SelectedCollege
        {
            get { return selectedCollege; }
            set
            {
                selectedCollege = value;
                RaisePropertyChanged(() => SelectedCollege);
                if (SelectedCollege != null && SelectedCollege.ToUpper() == "OTHER")
                    IsOtherCollege = true;
                else
                    IsOtherCollege = false;
            }
        }
        private bool isOtherCollege = false;
        public bool IsOtherCollege
        {
            get { return isOtherCollege; }
            set
            {
                isOtherCollege = value;
                RaisePropertyChanged(() => IsOtherCollege);
            }
        }
        private int numberOfParticipants = 0;
        public int NumberOfParticipants
        {
            get { return numberOfParticipants; }
            set
            {
                numberOfParticipants = value;
                RaisePropertyChanged(() => NumberOfParticipants);
                if (NumberOfParticipants > 0)
                {
                    if(IsNonIUCollege)
                    {
                        if (SelectedEvent.IsChargePerPerson)
                            Fees = SelectedEvent.Fees * NumberOfParticipants;
                        else
                            Fees = SelectedEvent.Fees;
                    }
                    else
                    {
                        if (SelectedSubGategory != null && PaidCategory.Contains(SelectedSubGategory.Name))
                        {
                            if (SelectedEvent.IsChargePerPerson)
                                Fees = SelectedEvent.Fees * NumberOfParticipants;
                            else
                                Fees = SelectedEvent.Fees;
                        }
                        else
                            Fees = 0;
                    }
                }
            }
        }


        private string otherCollege;
        public string OtherCollege
        {
            get { return otherCollege; }
            set
            {
                otherCollege = value;
                RaisePropertyChanged(() => OtherCollege);
            }
        }
        private string receiptNo;
        public string ReceiptNo
        {
            get { return receiptNo; }
            set
            {
                receiptNo = value;
                RaisePropertyChanged(() => ReceiptNo);
            }
        }
        private string primaryContactNumber;
        public string PrimaryContactNumber
        {
            get { return primaryContactNumber; }
            set
            {
                primaryContactNumber = value;
                RaisePropertyChanged(() => PrimaryContactNumber);
            }
        }
        private string secondaryContactNumber;
        public string SecondaryContactNumber
        {
            get { return secondaryContactNumber; }
            set
            {
                secondaryContactNumber = value;
                RaisePropertyChanged(() => SecondaryContactNumber);
            }
        }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                emailAddress = value;
                RaisePropertyChanged(() => EmailAddress);
            }
        }
        List<string> PaidCategory = new List<string>
        {
            "GAME UP","MYSTERY MANIA","Start-Up"
        };
    }
}
