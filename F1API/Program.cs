namespace F1API
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    internal class Program
    {
        
        public static async Task Main(string[] args)
        {
            
            
            string Temp = "2024-09-22:13:05";
            string url = $"https://api.openf1.org/v1/car_data?driver_number=1&session_key=latest&speed>300";
            List<F1DriverInfo> Data = await F1API.ReadDriverInfo(1);
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    Console.WriteLine(Data[i].acronym);
                }
            }
        }
        
    }

    public class F1SessionData
    {
        [JsonProperty("meeting_key")]
        public int meetingKey { get; set; }
        [JsonProperty("session_key")]
        public int sessionKey { get; set; }
        [JsonProperty("driver_number")]
        public int DriverNumber { get; set; }
        [JsonProperty("date")]
        public string DateTime { get; set; }
        [JsonProperty("rpm")]
        public int RPM { get; set; }
        [JsonProperty("speed")]
        public int speed { get; set; }
        [JsonProperty("n_gear")]
        public int gear { get; set; }
        [JsonProperty("throttle")]
        public int ThrottlePercent { get; set; }
        [JsonProperty("drs")]
        public int DRS { get; set; }
        [JsonProperty("brake")]
        public int BreakPercent { get; set; }
        [JsonProperty("full_name")]
        public string drivername { get; set; }
    }

    public class F1DriverInfo
    {
        [JsonProperty("broadcast_name")]
        public string drivername { get; set; }
        [JsonProperty("country_code")]
        public string nation { get; set; }
        [JsonProperty("driver_number")]
        public int DriverNum { get; set; }
        [JsonProperty("last_name")]
        public string LName { get; set; }
        [JsonProperty("name_acronym")]
        public string acronym { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set;}
        [JsonProperty("team_colour")]
        public string teamColour { get; set; }  
    }
    public class F1Intervals
    {
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("driver_number")]
        public int DriverNum { get; set; }
        [JsonProperty("gap_to_leader")]
        string LeaderInterval { get; set; }
        [JsonProperty("interval")]
        public string interval { get; set; }
        
    }
    public class F1Laps
    {
        [JsonProperty("driver_number")]
        public int DriverNum { get; set; }
        [JsonProperty("duration_sector_1")]
        public int Sector1 { get; set; }
        [JsonProperty("duration_sector_2")]
        public int Sector2 { get; set; }
        [JsonProperty("duration_sector_3")]
        public int Sector3 { get; set; }
    }


 }
