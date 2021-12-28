using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyAgarIoServer
{
    class ResultCommand : Command
    {
        [JsonProperty("res")]
        public List<Goo> Results { get; set; }

        public ResultCommand(List<Goo> results)
        {
            Results = results;
        }

        public override char GetCommandType()
        {
            return RESULT;
        }
    }
}
