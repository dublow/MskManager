namespace MskManager.Persistence.Model
{
    public class DeezerUser
    {
        public readonly int Id;
        public readonly string AccessToken;

        public DeezerUser(int id, string accessToken)
        {
            Id = id;
            AccessToken = accessToken;
        }
    }
}
