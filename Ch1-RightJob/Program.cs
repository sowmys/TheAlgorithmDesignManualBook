using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch1_RightJob
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime now = DateTime.Now;
			Job[][] examplePoints = new[] {
				new[] {
					new Job("Tarjan of the Jungle", now.AddDays(3), TimeSpan.FromDays(4)),
					new Job("\"Discrete\" Mathematics", now.AddDays(2), TimeSpan.FromDays(3)),
					new Job("The President's Algorist", now.AddDays(1), TimeSpan.FromDays(4)),
					new Job("Halting State", now.AddDays(5), TimeSpan.FromDays(3)),
					new Job("Steiner’s Tree", now.AddDays(7), TimeSpan.FromDays(3)),
					new Job("The Four Volume Problem", now.AddDays(9), TimeSpan.FromDays(6)),
					new Job("Programming Challenges", now.AddDays(11), TimeSpan.FromDays(6)),
					new Job("Process Terminated", now.AddDays(12), TimeSpan.FromDays(4)),
					new Job("Calculated Bets", now.AddDays(18), TimeSpan.FromDays(3)),
				},
			};
			TestJobSelection(examplePoints, GetByEarliestStartTime);
			TestJobSelection(examplePoints, GetByShortJobs);
			TestJobSelection(examplePoints, GetByEarliestEndTime);

		}

		private static void TestJobSelection(Job[][] examples, Func<IEnumerable<Job>, IEnumerable<Job>> jobSelector)
		{
			Console.WriteLine("Testing " + jobSelector.Method.Name);
			foreach (var example in examples)
			{
				Job[] selectedJobs = jobSelector(example).ToArray(); ;
				Console.WriteLine("Jobs: {0}, Count: {1}", string.Join(", ", (IEnumerable<Job>)selectedJobs), selectedJobs.Length);
			}
		}

		static IEnumerable<Job> GetByEarliestStartTime(IEnumerable<Job> jobs)
		{
			return GetJobs(jobs.OrderBy(job => job.StartTime));
		}

		static IEnumerable<Job> GetByShortJobs(IEnumerable<Job> jobs)
		{
			return GetJobs(jobs.OrderBy(job => job.Duration));
		}

		static IEnumerable<Job> GetByEarliestEndTime(IEnumerable<Job> jobs)
		{
			return GetJobs(jobs.OrderBy(job => job.EndTime));
		}

		private static IEnumerable<Job> GetJobs(IEnumerable<Job> sortedJobs)
		{
			DateTime nextAvailableTime = DateTime.Now;
			foreach (var job in sortedJobs)
			{
				if (nextAvailableTime < job.StartTime)
				{
					nextAvailableTime = job.EndTime;
					yield return job;
				}
			}
		}
	}

	class Job
	{
		private readonly DateTime startTime;
		private readonly TimeSpan duration;
		private readonly string name;

		public Job(string name, DateTime startTime, TimeSpan duration)
		{
			this.name = name;
			this.startTime = startTime;
			this.duration = duration;
		}

		public DateTime StartTime => startTime;

		public TimeSpan Duration => duration;

		public DateTime EndTime => startTime + duration;

		public string Name => name;

		public override string ToString() => Name;
	}
}
