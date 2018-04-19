
using ExLeafSoftApplication.Common;
using SQLite;
using System;
using System.Collections.Generic;


namespace ExLeafSoftApplication
{
    
  

    //public class Item 
    //{
    //    public string Id { get; set; }
    //    public string Text { get; set; }
    //    public string Description { get; set; }
     
    //}


    public class CapsuleItem
    {
        public List<GetCustomerListResult> GetCustomerListResult { get; set; }
    }

    public class LoginResult
    {
        public bool IsOk { get; set; }
    }



    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RootModel
    {
        public List<CustomerModel> customer { get; set; }
        public List<LocalFieldModel> fields { get; set; }
        public List<HashModel> hashList { get; set; }
        public List<FileMetaInformation> Encoded { get; set; }
        public int partnerid { get; set; }
        public int OperationType { get; set; }
    }


    public class HashModel
    {
        public Guid UniqueIdentifier { get; set; }
        public int RowHash { get; set; }

    }

    public class ValidateEmail
    {
        public int Count { get; set; }
        public string OpType { get; set; }
        public int AnotherRecordHas { get; set; }
    }

    public class CompactCustomerModel
    {
        public Guid User_UniqueId { get; set; }
        public string User_Email { get; set; }
        public string Frmr_RowHash { get; set; }
        
    }

   

    public class CustomerModel
    {
        public Guid User_UniqueId { get; set; }

        public int User_ID { get; set; }

        public string User_LoginName { get; set; }

        public string User_Email { get; set; }

        public string User_PhoneNumber { get; set; }

        public string User_BirthDate { get; set; }

        public string User_Password { get; set; }

        public string User_FirstName { get; set; }

        public string User_LastName { get; set; }

        public string Address_Text { get; set; }
        public int? Country_ID { get; set; }
        public int? City_ID { get; set; }

    }

    public class ServerCustomerModel
    {
        public Guid User_UniqueId { get; set; }

        public int RowHash { get; set; }

        public int User_ID { get; set; }

        public string OperationType { get; set; }

        public string CreateDate { get; set; }

        public string User_BirthDate { get; set; }

        public string User_LoginName { get; set; }

        public string User_Email { get; set; }

        public string User_PhoneNumber { get; set; }

        public string User_Password { get; set; }

        public string User_FirstName { get; set; }

        public string User_LastName { get; set; }

        public string Address_Text { get; set; }
        public int? Country_ID { get; set; }
        public int? City_ID { get; set; }

    }


    public class GetCustomerListResult
    {
        public string Address { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }



}
