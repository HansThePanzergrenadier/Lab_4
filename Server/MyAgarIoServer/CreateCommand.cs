using Newtonsoft.Json;

namespace MyAgarIoServer
{
    class CreateCommand : Command
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public CreateCommand(string name)
        {
            Name = name;
        }

        public override char GetCommandType()
        {
            return CREATE;
        }
    }
}
