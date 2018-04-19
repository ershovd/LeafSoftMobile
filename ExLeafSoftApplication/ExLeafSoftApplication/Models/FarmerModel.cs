
using ExLeafSoftApplication.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication
{
    public class AddressFarmerModel
    {
        [PrimaryKey,AutoIncrement]
        public int AddressID { get; set; }
        [Indexed(Name ="AdressUniqueKey",Order = 2)]
        public int AddressFarmerID { get; set; }
        [Indexed(Name = "AdressUniqueKey", Order = 1)]
        public Guid FarmerGuid { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
    }


    public class CountryModel
    {
        [PrimaryKey]
        public int CountryID { get; set; }
        public string Coun_Name { get; set; }
        public string Coun_IsoCode { get; set; }
        public float Coun_Latitude { get; set; }
        public float Coun_Longitude { get; set; }
        public float Coun_Altitude { get; set; }
        public bool Coun_IsActive { get; set; }
        public DateTime Coun_CreationDate { get; set; }
        public DateTime Coun_UpdateDate { get; set; }
        public int CreatorId { get; set; }
        public int UpdaterId { get; set; }

    }

    public class CountryCityModel
    {
        public int CountryID { get; set; }
        [PrimaryKey]
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public float City_Latitude { get; set;}
        public float City_Longitude { get; set; }
        public float City_Altitude { get; set; }
        public bool City_IsActive { get; set; }
    }

    public class FieldModel : Observable
    {
        [PrimaryKey, AutoIncrement]
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int FieldAreaSize { get; set; }
        public int FieldFarmerId { get; set; }
        public string FieldFarmerGuid { get; set; }
        public string FieldGuid { get; set; }
        public string FieldGps { get; set; }
        public string FieldUnit { get; set; }
        public long RowHash { get; set; }
        private bool _isdeleted;
        public bool IsUpdated { get { return _isdeleted; } set { _isdeleted = value; OnPropertyChanged("IsUpdated"); } }


    }

    public class LocalFieldModel
    {
        public string Fiel_Name { get; set; }
        public int Fiel_OwnerId { get; set; }
        public Guid Fiel_UniqueId { get; set; }
        public Guid Fiel_FarmerUniqueId { get; set; }
        public int Fiel_AreaSize { get; set; }
        public string Fiel_Gps { get; set; }
        public string Fiel_Unit { get; set; }
    }


    public class ServerFieldModel
    {
        public string Fiel_Name { get; set; }
        public int Fiel_OwnerId { get; set; }
        public Guid Fiel_UniqueId { get; set; }
        public Guid Fiel_FarmerUniqueId { get; set; }
        public int Fiel_AreaSize { get; set; }
        public string Fiel_Gps { get; set; }
        public string Fiel_Unit { get; set; }
        public string Fiel_CreationDate { get; set; }
        public int RowHash { get; set; }
        public string OperationType { get; set; }
    }

    public class CompactFieldModel
    {
        public Guid Fiel_UniqueId { get; set; }
        public Guid Fiel_FarmerUniqueId { get; set; }
        public string Fiel_Name { get; set; }
        public string Fiel_RowHash { get; set; }
    }

    public class FarmerModel : Observable
    {
        [PrimaryKey, AutoIncrement]
        public int FarmerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public string BirthDate { get; set; }
        private bool _isdeleted;
        public bool IsUpdated { get { return _isdeleted; } set { _isdeleted = value; OnPropertyChanged("IsUpdated"); } }
        public string GuidId { get; set; }
        public long RowHash { get; set; }
    }
}
