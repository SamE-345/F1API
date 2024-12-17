using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1API
{
    internal class F1API
    {
        private static readonly HttpClient client = new HttpClient();
        
        public F1API() { }
        public static async Task<List<F1SessionData>> ReadSessionData(string apiUrl)
        {

            try
            {
                // Send a GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a list of F1Data
                var data = JsonConvert.DeserializeObject<List<F1SessionData>>(responseBody);
                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("No data received or deserialization failed.");
                    return null;
                }

                // Output the data
                
                return data;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;
        }
        public static async Task<List<F1DriverInfo>> ReadDriverInfo(int driverNum)
        {
            
            string apiUrl = $"https://api.openf1.org/v1/drivers?driver_number={driverNum}&session_key=latest";
            try
            {
                // Send a GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a list of F1Data
                var data = JsonConvert.DeserializeObject<List<F1DriverInfo>>(responseBody);
                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("No data received or deserialization failed.");
                    return null;
                }
                // Output the data
                
                return data;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return null;

        }
        
    }
}
