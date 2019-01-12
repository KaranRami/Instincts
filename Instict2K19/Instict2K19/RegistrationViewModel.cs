using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace Instict2K19
{
    public class RegistrationViewModel:BaseViewModel
    {
        public RegistrationViewModel(ContentPage view):base(view)
        {
            ReceiptNo = "B101";
        }
        public ICommand ExportDatabaseCommand { get { return new Command(async () => await ExportDatabaseCommandEvent()); } }

        private async Task ExportDatabaseCommandEvent()
        {
            await View.DisplayAlert("Export Database", "Database saved to your download folder.", "Ok");
        }
        public ICommand TotalCollectionCommand { get { return new Command(async () => await TotalCollectionCommandEvent()); } }

        private async Task TotalCollectionCommandEvent()
        {
            double _totalCollection = 100;
            await View.DisplayAlert(Constants.AppName, "Total Collection: " +_totalCollection.ToString(), "Ok");
        }
        public ICommand CollgeTypeChangedCommand { get { return new Command<string>(CollgeTypeChangedCommandEvent); } }

        private void CollgeTypeChangedCommandEvent(string collgetype)
        {
            if (collgetype.ToUpper() == "IU")
                IsOtherCollege = false;
            else
                IsOtherCollege = true;
        }
            
        private bool isOtherCollege = false;
        public bool IsOtherCollege
        {
            get { return isOtherCollege; }
            set
            {
                isOtherCollege = value;
                RaisePropertyChanged(()=>IsOtherCollege);
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

    }
}
