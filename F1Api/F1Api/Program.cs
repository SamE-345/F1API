namespace F1Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Diagnostics;
    using System.ComponentModel.DataAnnotations;

    internal class Program
    {

        public static async Task Main(string[] args)
        {
            if (await F1API.ReadLapData(1, 55) == null)
            {
                Console.WriteLine("NULL");
            }
            else
            {
                await View_Lap_Speed(15, 55);
            }
        }
        public static async Task View_Lap_Speed(int lap, int driver)
        {
            int lapnum = lap;
            ScottPlot.Plot myPlot = new();

            string url = $"https://api.openf1.org/v1/car_data?driver_number={driver}&session_key=latest";
            List<F1SessionData> Data = await F1API.ReadSessionData(url);
            F1Laps lap15 = await F1API.ReadLapData(lapnum, driver);
            F1Laps lap16 = await F1API.ReadLapData(lapnum + 1, driver);


            int[] speeds = new int[Data.Count - 12000];
            int[] x_axis = new int[Data.Count - 12000];
            //Console.WriteLine(Data[15000].Date_Time.TimeOfDay);
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Date_Time > lap15.startTime && Data[i].Date_Time < lap16.startTime)
                {
                    x_axis[i] = i;
                    speeds[i] = Data[i].speed;
                }
                else if (Data[i].Date_Time > lap16.startTime)
                {
                    break;
                }
            }

            myPlot.Add.Scatter(x_axis, speeds);
            myPlot.SavePng("speedmap.png", 1000, 1000);
            string path = "C:\\Users\\samev\\source\\repos\\F1API\\F1Api\\F1Api\\bin\\Debug\\net8.0\\speedmap.png";
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            Console.WriteLine("Graph plotted");
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
        public DateTime Date_Time { get; set; }
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
        public string TeamName { get; set; }
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
        [JsonProperty("is_pit_out_lap")]
        public bool Outlap {  get; set; }
        [JsonProperty("lap_number")]
        public int LapNum { get; set; }
        [JsonProperty("date_start")]
        public DateTime startTime { get; set; }
        
    }

}
