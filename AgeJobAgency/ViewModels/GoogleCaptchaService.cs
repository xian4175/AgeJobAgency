using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace AgeJobAgency.ViewModels
{
	public class GoogleCaptchaService
	{
		private readonly ReCAPTCHASettings _setting;

		public GoogleCaptchaService(IOptions<ReCAPTCHASettings> settings) 
		{ 
			_setting = settings.Value;
		}

		public virtual async Task<GoogleResponse> VerifyreCaptcha(string _Token)
		{
			GoogleCaptchaData _MyData = new GoogleCaptchaData
			{
				response = _Token,
				secret = _setting.ReCAPTCHA_Secret_Key
			};

			HttpClient client = new HttpClient();
			var repponse = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?=secret{_MyData.secret}&response={_MyData.response}");
			var casp = JsonConvert.DeserializeObject<GoogleResponse>(repponse);
			return casp;
		}


        public class GoogleCaptchaData
		{
			public string response	{ get; set; }
			public string secret { get; set; }
		}

		public class GoogleResponse
		{
			public bool success { get; set; }
			public double score { get; set; }
			public string action	{ get; set; }
			public DateTime challenge_ts { get; set; }
			public string hostname	{ get; set; }

		}
	}
}
