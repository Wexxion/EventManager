using TaskManager.RepoLayer.MessengerInterfaces;

namespace AppLayer
{
    public class RemindResponse : IResponse
    {
        public string Text { get; }
        public ResponseStatus Status { get; }
        public long RemindId { get; }
        public RemindResponse(string text, long id)
        {
            RemindId = id;
            Text = text;
            Status = ResponseStatus.Close;
        }
    }
}