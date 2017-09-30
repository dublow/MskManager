namespace MskManager.Common.Nancy.HandleError.Models
{
    public class ServiceErrorModel
    {
        public readonly ServiceErrorCode Code;
        public readonly string Details;

        public ServiceErrorModel(ServiceErrorCode code, string details)
        {
            Code = code;
            Details = details;
        }
    }
}
