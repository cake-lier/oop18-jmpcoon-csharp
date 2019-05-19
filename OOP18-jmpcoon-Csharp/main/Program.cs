using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using jmpcoon.model.entities;
using jmpcoon.model.world;

namespace jmpcoon
{
    public class MainClass
    {
        private const string LEVEL_PATH = "../../res/level.lev";

        public static void Main(string[] args)
        {
            var entityProperties = new List<IEntityProperties>();
            using (var streamer = new BinaryReader(new FileStream(LEVEL_PATH, FileMode.Open, FileAccess.Read)))
            {
                int length = streamer.ReadInt32();
                var formatter = new BinaryFormatter();
                for (int i = 0; i < length; i++)
                {
                    object obj = formatter.Deserialize(streamer.BaseStream);
                    if(obj is IEntityProperties properties)
                    {
                        entityProperties.Add(properties);
                    }
                }
            }
            var gameWorld = new WorldFactory().Create();
            gameWorld.InitLevel(entityProperties);
            Console.WriteLine("World at creation: ");
            gameWorld.GetAliveEntities().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("");
            gameWorld.GetDeadEntities().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("");
            gameWorld.GetCurrentEvents().ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine("");
            Console.WriteLine("Score: " + gameWorld.Score + ", Player Lives: " + gameWorld.GetPlayerLives());
            gameWorld.Update();
            Console.WriteLine("---\nWorld after update: ");
            gameWorld.GetAliveEntities().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("");
            gameWorld.GetDeadEntities().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("");
            gameWorld.GetCurrentEvents().ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine("");
            Console.WriteLine("Score: " + gameWorld.Score + ", Player Lives: " + gameWorld.GetPlayerLives());
        }
    }
}
