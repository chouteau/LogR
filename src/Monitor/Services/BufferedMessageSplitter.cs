using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogR.Monitor.Services
{
	public class BufferedMessageSplitter
	{
		private string m_Message;
		public Action<string> m_Received;
		private static object m_Lock = new object();

		public BufferedMessageSplitter(Action<string> receive)
		{
			m_Received = receive;
		}

		public void Add(byte[] buffer, int byteRead)
		{
			lock (m_Lock)
			{
				var content = System.Text.Encoding.UTF8.GetString(buffer, 0, byteRead);
				foreach (var item in content)
				{
					m_Message += item;
					if (item == '}')
					{
						if (m_Received != null)
						{
							m_Received(m_Message);
						}
						m_Message = string.Empty;
					}
				}
			}

		}
	}

}
