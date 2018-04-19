using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;
using System.Net.Http.Headers;
using ExLeafSoftApplication.ServiceInterface;

namespace ExLeafSoftApplication.Data
{ 

    public class SynchronizationService : ISynchronizationService
    {
        HttpClient client;
        public CapsuleItem Items { get; private set; }
        public LoginResult loginResult { get; private set; }

        private const string SynchronizationServiceURL = "http://leafsoft.test.soil-cares.com/Synchronization/";

        public SynchronizationService()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
         
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        private string str = string.Empty;
        private string innerstr = string.Empty;

        public async Task<List<CompactCustomerModel>> SendLocalFarmerAsync()
        {
            List<CompactCustomerModel> returnValue = null;
            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

               List<CustomerModel> newlyInsertedItems =await App.FarmerTable.GetNewlyInsertedAsync();
                if (newlyInsertedItems.Count == 0)
                    return null;
                RootModel container = new RootModel { customer = newlyInsertedItems, OperationType = 1, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("SentLocalFarmersToServer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<CompactCustomerModel>>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }
        public async Task<List<CompactCustomerModel>> SendUpdatedFarmerAsync()
        {
            List<CompactCustomerModel> returnValue = null;
            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

                List<CustomerModel> UpdatedItems = await App.FarmerTable.GetUpdatedAsync();
                if (UpdatedItems.Count == 0)
                    return null;
                RootModel container = new RootModel { customer = UpdatedItems, OperationType = 2, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("SentLocalFarmersToServer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<CompactCustomerModel>>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }
        public async Task<List<ServerCustomerModel>> FetchServerFarmerAsync()
        {
            List<ServerCustomerModel> returnValue = null;


            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

                List<HashModel> hashmodelList = await App.FarmerTable.GetFarmersRowHashAsync();
                
                RootModel container = new RootModel {hashList = hashmodelList , OperationType = -1, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("GetFarmersFromServer", theContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<ServerCustomerModel>>(content);
                }


                
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }


        //Field Table Methods
        public async Task<List<CompactFieldModel>> SendLocalFieldAsync()
        {
            List<CompactFieldModel> returnValue = null;
            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

                List<LocalFieldModel> newlyInsertedItems = await App.FieldTable.GetNewlyInsertedAsync();
                if (newlyInsertedItems.Count == 0)
                    return null;
                RootModel container = new RootModel { fields = newlyInsertedItems, OperationType = 1, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("SentLocalFieldsToServer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<CompactFieldModel>>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }

        public async Task<List<CompactFieldModel>> SendUpdatedFieldAsync()
        {
            List<CompactFieldModel> returnValue = null;
            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

                List<LocalFieldModel> UpdatedItems = await App.FieldTable.GetUpdatedAsync();
                if (UpdatedItems.Count == 0)
                    return null;
                RootModel container = new RootModel { fields = UpdatedItems, OperationType = 2, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("SentLocalFieldsToServer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<CompactFieldModel>>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }

        public async Task<List<ServerFieldModel>> FetchServerFieldAsync()
        {
            List<ServerFieldModel> returnValue = null;


            try
            {
                client.BaseAddress = new Uri(SynchronizationServiceURL);

                List<HashModel> hashmodelList = await App.FieldTable.GetFieldsRowHashAsync();

                RootModel container = new RootModel { hashList = hashmodelList, OperationType = -1, partnerid = 2 };

                string str = JsonConvert.SerializeObject(container);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("GetFieldsFromServer", theContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<List<ServerFieldModel>>(content);
                }



            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }






    }
}
