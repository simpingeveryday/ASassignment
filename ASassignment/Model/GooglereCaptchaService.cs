using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ASassignment.Model
{
	public class GooglereCaptchaService
	{
        private ReCAPTCHASettings _settings;

        public GooglereCaptchaService(IOptions<ReCAPTCHASettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual async Task<GoogleREspo> VerifyCaptcha(string _token)
        {
            GooglereCaptchaData _Mydata = new GooglereCaptchaData
            {
                response =_token,
                secret =_settings.reCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();

            var ressponse = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_Mydata.secret}&response={_Mydata.response}");

            var capresp = JsonConvert.DeserializeObject<GoogleREspo>(ressponse);    

            return capresp;
        }
	}

    public class GooglereCaptchaData
    {
        public string response { get; set; } //token
        public string secret { get; set; }
    }

    public class GoogleREspo
    {
        public bool success { get; set; } //token
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
