using Microsoft.Extensions.Options;
using ModelLibrary.Models.Data;
using ModelLibrary.Models.Local;

namespace ModelLibrary.Services
{
    public class SmsService : ISmsService
    {
        private readonly ApplicationDbContext _context;
        private readonly TeleossCredentials _credentials;
        
        public SmsService(ApplicationDbContext context, IOptions<TeleossCredentials> credentials)
        {
            _context = context;
            _credentials = credentials.Value;
        }

        public async Task<string> SendMessageAsync(SendMessageRequest request)
        {
            try
            {
                var result = new HttpClientService().InvokeRequest(new HttpBody<Smslist>
                {
                    Method = ModelLibrary.UserManagementAPI.Enums.HttpMethod.POST,
                    Url = _credentials.Url!,
                    ContentType = ModelLibrary.UserManagementAPI.Enums.HttpContentType.ApplicationXml,
                    Body = new Smslist
                    {
                        sms = new Sms
                        {
                            message = request.Message,
                            mobiles = request.Phonenumber,
                            password = _credentials.Password!,
                            user = _credentials.Username!,
                            senderid = _credentials.SenderId!
                        }
                    }
                });
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string GeneratePin()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new();
            return _rdm.Next(_min, _max).ToString();
        }


    }
}

