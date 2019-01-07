using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Memory;
using MongoDB.Bson;

namespace cMongoDB
{
    class Program
    {
        public static int health, ammo;

        static void Main(string[] args)
        {

          MainAsync().Wait();

          Console.ReadKey();

        }

        static async Task MainAsync()
        {
            try
            {                
                Class_Memory.OpenProcess(224);

                ammo = Class_Memory.ReadInt(0x00000000);
                Console.WriteLine("\nAmmo: " + ammo);

                health = Class_Memory.ReadInt(0x00000000);
                Console.WriteLine("\nHealth: " + health);

                Console.WriteLine("\nMemory Read");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError reading memory " + e);
            }

            try
            {
                var client = new MongoClient("mongodb://localhost:27017");
                var db = client.GetDatabase("game_data");
                db.Client.DropDatabase("game_data");

                db = client.GetDatabase("game_data");

                var collection = db.GetCollection<BsonDocument>("player_data");

                var document = new BsonDocument
                {
                    {"health", health},
                    {"ammo", ammo }
                };

                collection.InsertOne(document);

                Console.WriteLine("\nDB Updated");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nDB Error");
            }
        }
    }
}
