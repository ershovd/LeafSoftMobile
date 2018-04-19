
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
    public class FieldViewModel : BaseViewModel
    {
        public Command SaveFieldCommand { get; set; }
        public Command DeleteFarmer { get; set; }
        public INavigation Navigation { get; set; }
        public FarmerModel Farmer { get; set; }
        public AddressFarmerModel address { get; set; }


        private string _sampleAreaSize;
        public string SampleAreaSize
        {
            get { return _sampleAreaSize; }
            set {
                _sampleAreaSize = value;
                OnPropertyChanged("SampleAreaSize");
                CheckPresentationLayerItems();
            }

        }


        private string _farmerGuid { get; set; }
        public string FarmerGuid
        {
            get { return _farmerGuid; }
            set { _farmerGuid = value; OnPropertyChanged("FarmerGuid"); }
        }

        public int FieldID { get; set; }
        public string FieldGuid { get; set; }

        private bool _sampleAreaSizeIsValid;
        public bool SampleAreaSizeIsValid
        {
            get { return _sampleAreaSizeIsValid; }
            set { _sampleAreaSizeIsValid = value; OnPropertyChanged("SampleAreaSizeIsValid"); }

        }

        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value;OnPropertyChanged("FieldName"); }
        }
        private bool _firstNameisValid { get; set; }
        public bool FieldNameIsValid
        {
            get { return _firstNameisValid; }
            set
            {
                _firstNameisValid = value;
                OnPropertyChanged("FieldNameIsValid");
                CheckPresentationLayerItems();
            }
        }


        private string _buttonName;
        public string ButtonName
        {
            get { return _buttonName; }
            set { _buttonName = value;OnPropertyChanged("ButtonName"); }
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


        public enum SampleAreaSizeUnit {
            Acre = 1,
            Dekar = 2,
            Dönüm =3
        }
        
        private ObservableCollection<SampleAreaSizeUnit> _areaUnits { get; set; }
        public ObservableCollection<SampleAreaSizeUnit> AreaUnits
        {
            get { return _areaUnits; }
            set { _areaUnits = value;OnPropertyChanged("AreaUnits"); }
        }

        private SampleAreaSizeUnit _areaUnit;
        public SampleAreaSizeUnit AreaUnit
        {
            get { return _areaUnit; }
            set { _areaUnit = value;OnPropertyChanged("AreaUnit"); }
        }

        public string _gpsPosition;
        public string GpsPosition {
            get { return _gpsPosition; }
            set { _gpsPosition = value;
                OnPropertyChanged("GpsPosition"); }
        }



        private void CheckPresentationLayerItems()
        {
            if (FieldNameIsValid && SampleAreaSizeIsValid)
            {
                
                
               CanExecute = true;
               
            }
            else
                CanExecute = false;

        }



        public FieldViewModel()
        {
            AreaUnits = new ObservableCollection<SampleAreaSizeUnit>();
            AreaUnits.Add(SampleAreaSizeUnit.Acre);
            AreaUnits.Add(SampleAreaSizeUnit.Dekar);
            AreaUnits.Add(SampleAreaSizeUnit.Dönüm);

            AreaUnit = SampleAreaSizeUnit.Acre;
           
            SaveFieldCommand = new Command(async () =>
            {

                await SaveField();

            }
             );
           
        }

        async Task SaveField()
        {
            string unit = Enum.GetName(typeof(SampleAreaSizeUnit), AreaUnit);

            if (ButtonName == "Done")
            {

                Guid fieldGuid = Guid.NewGuid();

                FieldModel newfield = new FieldModel
                {
                    FieldGps = GpsPosition,
                    FieldName = FieldName,
                    IsUpdated = false,
                    FieldFarmerGuid = Farmer.GuidId,
                    FieldFarmerId = Farmer.FarmerId,
                    FieldGuid = fieldGuid.ToString(),
                    FieldUnit = unit,
                    RowHash = 0,
                    FieldAreaSize = Convert.ToInt32(SampleAreaSize)
                };

                await App.FieldTable.SaveItemAsync(newfield);

            }
            else
            {

                FieldModel newfield = new FieldModel
                {
                    FieldId = FieldID,
                    FieldGps = GpsPosition,
                    FieldName = FieldName,
                    IsUpdated = true,
                    FieldFarmerGuid = Farmer.GuidId,
                    FieldFarmerId = Farmer.FarmerId,
                    FieldGuid = FieldGuid,
                    FieldUnit = unit,
                    RowHash = -1,
                    FieldAreaSize = Convert.ToInt32(SampleAreaSize)
                };

                await App.FieldTable.UpdateItemAsync(newfield);


            }


            await Navigation.PopAsync();
        }

     

    }
}
