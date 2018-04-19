
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ExLeafSoftApplication.SqlLiteEntities;

namespace ExLeafSoftApplication.ViewModels
{
    public class FarmerViewModel : BaseViewModel
    {
        public Command SaveFarmer { get; set; }
        public Command DeleteFarmer { get; set; }
        public FarmerModel farmerModel { get; set; }
        public AddressFarmerModel addressModel { get; set; }
        
        public INavigation Navigation { get; set; }

    


        private bool _isCountry { get; set; }
        public bool IsCountry
        {
            get { return _isCountry; }

            set
            {
                _isCountry = value;

                if (value == true && (SelectedCountry == null || SelectedCity == null || 
                    (SelectedCountry != null  && SelectedCity != null 
                    && SelectedCountry.CountryID != SelectedCity.CountryID)))
                {
                    GetCityList();
                }
              
                
                OnPropertyChanged("IsCountry");
            }
        }


        private bool _operationType { get; set; }
        public bool OperationType
        {
            get { return _operationType; }
            set { _operationType = value; OnPropertyChanged("OperationType"); }
        }


        private string _btnName;
        public string BtnName
        {
            get { return _btnName; }
            set {
              
                _btnName = value;
                OnPropertyChanged("BtnName"); }
        }

        private int _farmerID { get; set; }
        public int FarmerID { get { return _farmerID;  }
            set { _farmerID = value; OnPropertyChanged("FarmerID"); } }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set {
                _firstName = value;
                
                
                OnPropertyChanged("FirstName"); }
        }


        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set {
                _lastName = value;
                OnPropertyChanged("LastName"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged("Email"); }

        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set {



                _phone = value;
                OnPropertyChanged("Phone");
            }

        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set {

                _address = value;

                 OnPropertyChanged("Address"); }

        }

        private string _guidID { get; set; }
        public string GuidId {
            get { return _guidID; }
            set { _guidID = value;
                OnPropertyChanged("GuidId");
            }
        }
        public long RowHash { get; set; }


        private DateTime _birthdate;
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set { _birthdate = value;
                OnPropertyChanged("BirthDate"); }

        }

        private List<CountryCityModel> _rawCityList { get; set; }
        public List<CountryCityModel> RawCityList
        {
            get { return _rawCityList; }
            set { _rawCityList = value; CityList = new ObservableCollection<CountryCityModel>(value); }
        }

        private ObservableCollection<CountryModel> _countryList { get; set; }
        public ObservableCollection<CountryModel> CountryList { get { return _countryList; }
            set { _countryList = value;OnPropertyChanged("CountryList"); } }

        private ObservableCollection<CountryCityModel> _cityList { get; set; }
        public ObservableCollection<CountryCityModel> CityList { get { return _cityList; }
            set { _cityList = value; OnPropertyChanged("CityList"); } }

        private CountryModel _selectedCountry { get; set; }
        public  CountryModel SelectedCountry { get { return _selectedCountry; }
            set { _selectedCountry = value;
                OnPropertyChanged("SelectedCountry"); }  }

        private CountryCityModel _selectedCity { get; set; }
        public CountryCityModel SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                
               _selectedCity = value; OnPropertyChanged("SelectedCity"); 
                
            }
        }

        private bool _emailisValid { get; set; }
        public bool EmailIsValid
        {
            get { return _emailisValid; }
            set {
                _emailisValid = value;
                
                OnPropertyChanged("EmailIsValid");
                CheckPresentationLayerItems();
            }
        }

        
         

           
       

        private bool _phoneisValid { get; set; }
        public bool PhoneIsValid
        {
            get { return _phoneisValid; }
            set
            {
                _phoneisValid = value;
                OnPropertyChanged("PhoneIsValid");
                CheckPresentationLayerItems();
            }
        }

        private bool _firstNameisValid { get; set; }
        public bool FirstNameIsValid
        {
            get { return _firstNameisValid; }
            set
            {
                _firstNameisValid = value;
                OnPropertyChanged("FirstNameIsValid");
                CheckPresentationLayerItems();
            }
        }

        private bool _lastNameisValid { get; set; }
        public bool LastNameIsValid
        {
            get { return _lastNameisValid; }
            set
            {
                _lastNameisValid = value;
                OnPropertyChanged("LastNameIsValid");
                CheckPresentationLayerItems();
            }
        }

        private bool _addressisValid { get; set; }
        public bool AddressIsValid
        {
            get { return _addressisValid; }
            set
            {
                _addressisValid = value;
                OnPropertyChanged("AddressIsValid");
                CheckPresentationLayerItems();
            }
        }
        private bool _countryValid { get; set; }
        public bool CountryIsValid
        {
            get { return _countryValid; }
            set
            {
                _countryValid = value;
                OnPropertyChanged("CountryIsValid");
                CheckPresentationLayerItems();
            }
        }

        private bool _cityValid { get; set; }
        public bool CityIsValid
        {
            get { return _cityValid; }
            set
            {
                _cityValid = value;
                OnPropertyChanged("CityIsValid");
                CheckPresentationLayerItems();
            }
        }

        private bool _birthDateIsValid { get; set; }
        public bool BirthDateIsValid
        {
            get { return _birthDateIsValid; }
            set {
                _birthDateIsValid = value;
                OnPropertyChanged("BirthDateIsValid");
                CheckPresentationLayerItems();
            }
        }


        private bool _canExecute { get; set; }
        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                _canExecute = value;
                OnPropertyChanged("CanExecute");

            }
        }
        

        private void CheckPresentationLayerItems()
        {
            if (EmailIsValid && PhoneIsValid && FirstNameIsValid && LastNameIsValid && CountryIsValid && CityIsValid && AddressIsValid && BirthDateIsValid)
            {
                
                
               CanExecute = true;
               
            }
            else
                CanExecute = false;

        }



        public FarmerViewModel()
        {
            
            SaveFarmer = new Command(async () =>
            {
                    
                     await SaveFarmerCommand();
                
             }
             );
            DeleteFarmer = new Command(async () => await DeleteFarmerCommand());
           
            //CityList = new ObservableCollection<CountryCityModel>();
        }

        public void GetCountryList() {

            CountryList = new ObservableCollection<CountryModel>(CountryTable.CountryList);
        }

        public async void GetCityList() {
           
            CityList = new ObservableCollection<CountryCityModel>( await App.CountryCityTable.GetCityList(SelectedCountry.CountryID));
            
        }

        //private void ValidateAllData()
        //{
        //    if(string.IsNullOrEmpty(this.FirstName))

        //}

        async Task SaveFarmerCommand()
        {
            try
            {
                if (this.FarmerID > 0)
                {

                    await App.FarmerTable.UpdateItemAsync(new FarmerModel
                    {
                        FarmerId = this.FarmerID,
                        FirstName = this.FirstName,
                        Email = this.Email,
                        Phone = this.Phone,
                        CreateDate = DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss"),
                        BirthDate = this.BirthDate.ToString("yyyy-MM-dd'T'hh:mm:ss"),
                        LastName = this.LastName,
                        GuidId = this.GuidId,
                        RowHash = this.RowHash,
                        IsUpdated = true
                    });

                    await App.AddressFarmerTable.UpdateItemAsync(new AddressFarmerModel
                    { AddressFarmerID = FarmerID, FarmerGuid  = new Guid(this.GuidId), CountryID = SelectedCountry.CountryID, CityID = SelectedCity.City_ID, Address = this.Address });

                }
                else
                {
                    Guid newGuid = Guid.NewGuid();

                    await App.FarmerTable.SaveItemAsync(new FarmerModel
                    {
                        FirstName = this.FirstName,
                        Email = this.Email,
                        Phone = this.Phone,
                        CreateDate = DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss"),
                        BirthDate = this.BirthDate.ToString("yyyy-MM-dd'T'hh:mm:ss"),
                        LastName = this.LastName,
                        GuidId = newGuid.ToString(),
                        IsUpdated = false
                    });

                    int farmerid = await App.FarmerTable.GetLastInserted();

                    await App.AddressFarmerTable.SaveItemAsync(new AddressFarmerModel
                    { AddressFarmerID = farmerid, FarmerGuid =newGuid, CountryID = SelectedCountry.CountryID, CityID = SelectedCity.City_ID, Address = this.Address });
                }
            }
            catch (Exception ex)
            {

            }
           

            await Navigation.PopAsync();
           

        }


     


        async Task DeleteFarmerCommand()
        {
            await App.AddressFarmerTable.DeleteItemAsync(new AddressFarmerModel
            { AddressFarmerID = FarmerID, CountryID = SelectedCountry.CountryID, CityID = SelectedCity.City_ID, Address = this.Address });

            await App.FarmerTable.DeleteItemAsync(new FarmerModel
            {
                FarmerId = this.FarmerID,
                FirstName = this.FirstName,
                Email = this.Email,
                Phone = this.Phone,
                BirthDate = this.BirthDate.ToString(),
                LastName = this.LastName
            });



            await Navigation.PopAsync();

        }



    }
}
