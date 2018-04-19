using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using ExLeafSoftApplication.ServiceInterface;

namespace ExLeafSoftApplication.Data
{ 

    public class CustomerService : ICustomerService
    {
        HttpClient client;
        public CapsuleItem Items { get; private set; }
        public LoginResult loginResult { get; private set; }

        private const string CustomerServiceURL = "http://leafsoft.test.soil-cares.com/Customer/";


        public CustomerService()
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

        public async Task<bool> LoginAsync(string userName,string passWord)
        {
            bool returnValue = false;
            try
            {
                client.BaseAddress = new Uri(CustomerServiceURL);

                string str = JsonConvert.SerializeObject(new LoginModel {UserName = userName, Password = passWord });
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("LoginCustomer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    returnValue = JsonConvert.DeserializeObject<bool>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return returnValue;
        }

        public async Task<string> RegisterDataAsync()
        {
            string responseResult = string.Empty; 



            try
            {
                client.BaseAddress = new Uri(CustomerServiceURL);

                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(CustomerModel));
               var customer = new CustomerModel { User_FirstName = "leafsofttest", User_LastName = "leafsoft",
                   User_Password ="testleafsoft", Address_Text = "",User_Email = "leafsoft@springg.com", User_ID = 2 };

                //MemoryStream ms = new MemoryStream();
                //jsonSer.WriteObject(ms, customer);
                //ms.Position = 0;

                ////use a Stream reader to construct the StringContent (Json) 
                //StreamReader sr = new StreamReader(ms);
                //// Note if the JSON is simple enough you could ignore the 5 lines above that do the serialization and construct it yourself 
                //// then pass it as the first argument to the StringContent constructor 
                //StringContent theContent = new StringContent(sr.ReadToEnd(), System.Text.Encoding.UTF8, "application/json");


                string str = JsonConvert.SerializeObject(customer);
                StringContent theContent = new StringContent(str, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("RegisterCustomer", theContent);
                if (response.IsSuccessStatusCode)
                {


                    var content = await response.Content.ReadAsStringAsync();
                    responseResult = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return responseResult;
        }


        public async Task<CapsuleItem> RefreshDataAsync()
        {
            Items = new CapsuleItem();

            
           
            try
            {
                client.BaseAddress = new Uri(CustomerServiceURL);
                var response = await client.GetAsync("GetCustomers");
                if (response.IsSuccessStatusCode)
                {
                    

                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<CapsuleItem>(content);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                innerstr = ex.InnerException.Message;
            }

            return Items;
        }
    }
}
