using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamWebAPI;
using Newtonsoft.Json.Linq;

namespace LogstfAPI
{
    public static class Logs
    {
        public static string[][] GetLogList(string title, string uploader, string player, int limit)
        {
            string json = Json.GET(string.Format(@"http://logs.tf/json_search?title={0}&uploader={1}&player={2}&limit={3}", title, uploader, player, limit));
            JObject jresult = JObject.Parse(json);
            JArray jlogs = (JArray)jresult["logs"];

            string[][] logs = new string[jlogs.Count][];

            for (int i = 0; i < jlogs.Count; i++)
            {
                string[] log = new string[3];
                log[0] = (string)jlogs[i]["date"];
                log[1] = (string)jlogs[i]["id"];
                log[2] = (string)jlogs[i]["title"];

                logs[i] = log;
            }

            return logs;
        }

        public static string[] GetPlayerPerformance(string steamID3, string logID)
        {
            string json = Json.GET(string.Format(@"http://logs.tf/json/{0}", logID));
            JObject jresult = JObject.Parse(json);
            JObject jplayer = (JObject)jresult["players"][steamID3];

            string[] player = new string[12];

            player[0] = (string)jplayer["heal"];
            player[1] = (string)jplayer["ubers"];
            player[2] = (string)jplayer["ubertypes"]["medigun"];
            player[3] = (string)jplayer["ubertypes"]["kritzkrieg"];
            player[4] = (string)jplayer["ubertypes"]["quickfix"];
            player[5] = (string)jplayer["ubertypes"]["vaccinator"];
            player[6] = (string)jplayer["drops"];

            player[7] = (string)jplayer["kills"];
            player[8] = (string)jplayer["assists"];
            player[9] = (string)jplayer["dmg"];
            player[10] = (string)jplayer["deaths"];
            player[11] = (string)jplayer["suicides"];


            //check if player played medic
            JArray jclasses = (JArray)jplayer["class_stats"];
            bool isMedic = false;
            foreach (var jclass in jclasses)
            {
                if ((string)jclass["type"] == "medic")
                {
                    isMedic = true;
                }
            }
            if (!isMedic)
            {
                player[0] = "0";
            }

            return player;
        }
    }
}
