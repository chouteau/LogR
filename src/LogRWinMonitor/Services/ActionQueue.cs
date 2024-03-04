using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogWinRMonitor.Views
{
	public class ActionQueue
	{
		private BackgroundWorker m_BgWorker;
		private Queue<Action> m_Queue = new Queue<Action>();
		private System.Threading.ManualResetEvent m_NewAction = new System.Threading.ManualResetEvent(false);
		private System.Threading.ManualResetEvent m_Terminate = new System.Threading.ManualResetEvent(false);

		public ActionQueue()
		{
			this.m_BgWorker = new BackgroundWorker();
			this.m_BgWorker.WorkerReportsProgress = true;
			this.m_BgWorker.DoWork += new DoWorkEventHandler(this.DoWork);
			this.m_BgWorker.ProgressChanged += new ProgressChangedEventHandler(this.ProgressChanged);
		}

		public void Run()
		{
			this.m_BgWorker.RunWorkerAsync();
		}

		public void Stop()
		{
			this.m_Terminate.Set();
		}

		public void Add(Action action)
		{
			lock (this.m_Queue)
			{
				this.m_Queue.Enqueue(action);
			}
			this.m_NewAction.Set();
		}

		private void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			var action = e.UserState as Action;
			action();
		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				var waitHandles = new System.Threading.WaitHandle[] { this.m_Terminate, this.m_NewAction };
				int result = System.Threading.WaitHandle.WaitAny(waitHandles, 60 * 1000, true);
				if (result == 0)
				{
					break;
				}
				this.m_NewAction.Reset();
				if (this.m_Queue.Count != 0)
				{
					Queue<Action> copy;
					lock (this.m_Queue)
					{
						copy = new Queue<Action>(this.m_Queue);
						this.m_Queue.Clear();
					}
					foreach (Action action in copy)
					{
						try
						{
							this.m_BgWorker.ReportProgress(0, action);
						}
						catch
						{
						}
					}
				}
			}
		}
	}
}
