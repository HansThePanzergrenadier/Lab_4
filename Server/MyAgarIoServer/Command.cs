using Newtonsoft.Json;

namespace MyAgarIoServer
{
    abstract class Command
    {
        public const char CREATE = '0';
        public const char MOVE = '1';
        public const char DATA = '2';
        public const char RESULT = '3';

        public string ToRequest()
        {
            return GetCommandType() + JsonConvert.SerializeObject(this);
        }

        public abstract char GetCommandType();

        public static Command FromRequest(string request)
        {
            string json = request.Substring(1);

            switch (request[0])
            {
                case CREATE: return JsonConvert.DeserializeObject<CreateCommand>(json);
                case MOVE: return JsonConvert.DeserializeObject<MoveCommand>(json);
                case DATA: return JsonConvert.DeserializeObject<DataCommand>(json);
                case RESULT: return JsonConvert.DeserializeObject<ResultCommand>(json);
                default: return null;
            }
        }
    }
}
