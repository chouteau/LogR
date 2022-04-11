namespace LogRWebMonitorDemo
{
	public class FakeLogWriter : BackgroundService
	{
		public FakeLogWriter(ILogger<FakeLogWriter> logger)
		{
			this.Logger = logger;
		}

		protected ILogger Logger { get; set; }	
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Logger.LogInformation("Log writer ready");
			while (!stoppingToken.IsCancellationRequested)
			{
				Logger.LogCritical(new Exception("Critical1\nCritical2\n"), "Critical : {0}", DateTime.Now);
				var error = new Exception("Error1\nError2\n");
				error.Data.Add("Param1", "Value1");
				error.Data.Add("Param2", "Value2");
				Logger.LogError(error, "Error : {Now}", DateTime.Now);
				Logger.LogWarning("Warning : {0}", DateTime.Now);
				Logger.LogInformation("Info : {0}", DateTime.Now);
				Logger.LogDebug("Debug : {0}", DateTime.Now);
				Logger.LogTrace("Trace : {0}", DateTime.Now);
				await Task.Delay(5 * 1000);
			}
		}
	}
}
