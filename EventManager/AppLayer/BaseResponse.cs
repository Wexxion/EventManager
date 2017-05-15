using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer
{
    public class BaseResponse : IResponse
    {
        public string Text { get; }

        public BaseResponse(string responseText)
        {
            Text = responseText;
        }
    }
}
