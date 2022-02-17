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
				Logger.LogCritical("Critical : {0}", DateTime.Now);
				Logger.LogError("Error : {0}", DateTime.Now);
				Logger.LogWarning("Warning : {0}", DateTime.Now);
				Logger.LogInformation("Info : {0}", DateTime.Now);
				Logger.LogDebug("Debug : {0}", DateTime.Now);
				Logger.LogTrace("Trace : {0}", DateTime.Now);
				await Task.Delay(5 * 1000);
			}
		}
	}
}
