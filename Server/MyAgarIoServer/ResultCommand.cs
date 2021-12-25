using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyAgarIoServer
{
    class ResultCommand : Command
    {
        [JsonProperty("res")]
        public List<Player> Results { get; set; }

        public ResultCommand(List<Player> results)
        {
            Results = results;
        }

        public override char GetCommandType()
        {
            return RESULT;
        }
    }
}
