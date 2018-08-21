using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogstfAPI;

namespace LogsCalculator
{
    class Program
    {
        static string steamID64;
        static string steamID3;

        static void Main(string[] args)
        {
            Console.WriteLine("LogsCalculator\n-------------");
            Console.Write("SteamID64: ");
            steamID64 = Console.ReadLine();
            Console.Write("SteamID3: ");
            steamID3 = Console.ReadLine();

            steamID64 = steamID64.Replace(" ", string.Empty);
            steamID3 = steamID3.Replace(" ", string.Empty);

            if(steamID64 == string.Empty || steamID3 == string.Empty)
            {
                Console.Write("You haven't entered any information. \n\nPress any key to exit...");
                Console.Read();
                return;
            }

            string[][] logs = Logs.GetLogList("", "", steamID64, 1000);
            string[][] player = new string[logs.Length][];

            for (int i = 0; i < logs.Length; i++)
            {
                string[] p;
                try
                {
                    p = Logs.GetPlayerPerformance(steamID3, logs[i][1]);
                    player[i] = p;
                    Console.WriteLine(string.Format("Log({0}/{1}): {2} ID: {3} download finished",  i + 1, logs.Length, logs[i][2], logs[i][1]));
                }
                catch
                {
                    Console.WriteLine(string.Format("!Log({0}/{1}): {2} ID: {3} download failed", i + 1, logs.Length, logs[i][2], logs[i][1]));
                    i--;
                }
            }

            //Medic
            int healing = 0;
            int uber = 0;
            int medigun = 0;
            int kritzkrieg = 0;
            int quickfix = 0;
            int vaccinator = 0;
            int drops = 0;

            //General
            int kills = 0;
            int assists = 0;
            int damage = 0;
            int deaths = 0;
            int suicides = 0;

            bool samelog = false;      

            for (int i = 0; i < player.Length; i++)
            {
                if(i > 1)
                {
                    ulong log = Convert.ToUInt64(logs[i][0]);
                    ulong logprev = Convert.ToUInt64(logs[i - 1][0]);


                    samelog = logprev > log && logprev < (log + 60);
                }

                if (!samelog)
                {
                    int _healing = Convert.ToInt32(player[i][0]); //healing
                    int _uber = Convert.ToInt32(player[i][1]); //uber
                    int _medigun = Convert.ToInt32(player[i][2]);  //medi gun
                    int _kritzkrieg = Convert.ToInt32(player[i][3]);  //kritzkrieg
                    int _quickfix = Convert.ToInt32(player[i][4]); //quickfix
                    int _vaccinator = Convert.ToInt32(player[i][5]); //vaccinator
                    int _drops = Convert.ToInt32(player[i][6]); //drops

                    int _kills = Convert.ToInt32(player[i][7]);
                    int _assists = Convert.ToInt32(player[i][8]);
                    int _damage = Convert.ToInt32(player[i][9]);
                    int _deaths = Convert.ToInt32(player[i][10]);
                    int _suicides = Convert.ToInt32(player[i][11]);

                    healing += _healing;
                    uber += _uber;
                    medigun += _medigun;
                    kritzkrieg += _kritzkrieg;
                    quickfix += _quickfix;
                    vaccinator += _vaccinator;
                    drops += _drops;

                    kills += _kills;
                    assists += _assists;
                    damage += _damage;
                    deaths += _deaths;
                    suicides += _suicides;
                }
            }
            //Medic
            Console.WriteLine("\nMedic\n-----"); 
            Console.WriteLine("Healing: " + healing);
            Console.WriteLine("Ubers: " + uber);
            Console.WriteLine("\tMedi Gun: " + medigun);
            Console.WriteLine("\tKritzkrieg: " + kritzkrieg);
            Console.WriteLine("\tQuickfix: " + quickfix);
            Console.WriteLine("\tVaccinator: " + vaccinator);
            Console.WriteLine("Drops: " + drops);

            //General
            Console.WriteLine("\nGeneral\n-------");
            Console.WriteLine("Kills: " + kills);
            Console.WriteLine("Assists: " + assists);
            Console.WriteLine("Damage: " + damage);
            Console.WriteLine("Deaths: " + deaths);
            Console.WriteLine("Suicides: " + suicides);

            Console.ReadLine();
        }
    }
}
