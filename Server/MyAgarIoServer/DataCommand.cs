using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyAgarIoServer
{
    class DataCommand : Command
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("map")]
        public PetriCup PetriCup { get; set; }

        [JsonProperty("goo")]
        public Goo Goo { get; set; }

        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; }

        public DataCommand(long time, PetriCup petriCup, Goo goo, List<Entity> entities)
        {
            Time = time;
            PetriCup = petriCup;
            Goo = goo;
            Entities = entities;
        }

        public override char GetCommandType()
        {
            return DATA;
        }
    }
}
