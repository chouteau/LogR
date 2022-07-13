namespace LogRWebApp.Services
{
	public class LogRWebAppSettings
	{
		public string AdminKey { get; set; }
		public bool UseOvhGrayLogRPlugin { get; set; } = false;
		public bool SendFatalAndExceptionByEmail { get; set; } = false;
		public string EmailProviderName { get; set; }
		public string EmailTemplatesDirectoryName { get; set; } = "EmailTemplates";
		public string SenderEmail { get; set; }
		public string RecipientEmail { get; set; }
		public string SendGridApiKey { get; set; }
	}
}
