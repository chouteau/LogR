using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorLauncher
{
	public class DownloaderService
	{
		public DownloaderService()
		{
		}

		protected Settings Settings { get; private set; }
		public string DownloadFolder { get; private set; }
		public string LatestFolder { get; private set; }

		public void Initialize(Settings settings)
		{
			this.Settings = settings;
			DownloadFolder = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "download");
			if (!System.IO.Directory.Exists(DownloadFolder))
			{
				System.IO.Directory.CreateDirectory(DownloadFolder);
			}
			LatestFolder = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "latest");
			if (!System.IO.Directory.Exists(LatestFolder))
			{
				System.IO.Directory.CreateDirectory(LatestFolder);
			}
		}

		public bool HasUpdate()
		{
			var latestZipFileInfo = GetLatestZipFileInfo();
			var lastWriteTime = DateTime.MinValue;
			if (latestZipFileInfo.Exists)
			{
				lastWriteTime = latestZipFileInfo.LastWriteTime;
			}

			var httpClient = new HttpClient();
			var ci = new System.Globalization.CultureInfo("en-US");
			httpClient.DefaultRequestHeaders.Add("User-Agent", "Monitor Launcher AutoUpdate/1.0 (+https://github.com/chouteau/logr)");
			httpClient.DefaultRequestHeaders.Add("If-Modified-Since", string.Format(ci, "{0:ddd, dd MMM yyyy HH:mm:ss} GMT", lastWriteTime.ToUniversalTime()));

			var response = httpClient.GetAsync(Settings.MonitorUpdateUrl).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.NotModified)
			{
				return false;
			}

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return false;
			}

			return true;
		}

		public void DownloadLatest()
		{
			var latestZipFileInfo = GetLatestZipFileInfo();

			var httpClient = new HttpClient();
			var ci = new System.Globalization.CultureInfo("en-US");
			httpClient.DefaultRequestHeaders.Add("User-Agent", "Monitor Launcher AutoUpdate/1.0 (+https://github.com/chouteau/logr)");

			var response = httpClient.GetAsync(Settings.MonitorUpdateUrl).Result;

			if (latestZipFileInfo.Exists)
			{
				System.IO.File.Delete(latestZipFileInfo.FullName);
			}
			using (var fs = new System.IO.FileStream(latestZipFileInfo.FullName, System.IO.FileMode.Create))
			{
				var stream = response.Content.ReadAsStreamAsync().Result;
				int bufferSize = 1024;
				byte[] buffer = new byte[bufferSize];
				int pos = 0;
				while ((pos = stream.Read(buffer, 0, bufferSize)) > 0)
				{
					fs.Write(buffer, 0, pos);
				}
				fs.Close();
			}
		}

		public bool DeployLatest()
		{
			var latestZipFileInfo = GetLatestZipFileInfo();

			// Unzip
			using (var zip = System.IO.Compression.ZipFile.Open(latestZipFileInfo.FullName, System.IO.Compression.ZipArchiveMode.Read))
			{
				foreach (var item in zip.Entries)
				{
					var entry = System.IO.Path.Combine(LatestFolder, item.FullName);
					var result = DeleteFile(entry);
					if (!result)
					{
						return false;
					}
				}
			}

			System.Threading.Thread.Sleep(1 * 1000);
			System.IO.Compression.ZipFile.ExtractToDirectory(latestZipFileInfo.FullName, LatestFolder);

			return true;
		}

		private System.IO.FileInfo GetLatestZipFileInfo()
		{
			var latestZipFile = System.IO.Path.Combine(DownloadFolder, "LogRMon.zip");
			var latestZipFileInfo = new System.IO.FileInfo(latestZipFile);
			return latestZipFileInfo;
		}

		private bool DeleteFile(string fileName)
		{
			var deleted = false;
			var retryCount = 0;
			while (true)
			{
				try
				{
					System.IO.File.Delete(fileName);
					deleted = true;
					break;
				}
				catch (Exception ex)
				{
					retryCount++;
				}

				if (retryCount > 5)
				{
					break;
				}

				System.Threading.Thread.Sleep(5 * 1000);
			}
			return deleted;
		}
	}
}
