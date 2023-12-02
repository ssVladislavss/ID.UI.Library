namespace ID.UI.Core.Options
{
    public class ApiOptions
    {
        public Uri IDUrl { get; set; }

        public ApiOptions()
        {
            IDUrl = new Uri("https://localhost:44338");
        }
    }
}
