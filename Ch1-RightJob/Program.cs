using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch1_RightJob
{
	/*
	Chap 1: Right Job
		This implements four methods for selecting optimal jobs.
		1. By Earliest Start Time: Hueristic logic to sort jobs by their start time and pick non overlapping jobs starting from the earliest.
		2. By Duration: Hueristic logic to sort jobs by their duration and pick non overlapping jobs starting from the shortest job.
		3. All Nonoverlapping Subset: Exhaustive search method that iterates through every possible subsets of jobs and pick a largest susbet
		4. By Earliest End Time: The optimal logic got by sorting the jobs by the end time and picking up non overlapping jobs starting from the job that finishes earliest.
	*/
	class Program
	{
		/*
		Runs  example through all the slection logic. Here is the output
			Testing GetByEarliestStartTime
			Jobs: The President's Algorist, Steiner's Tree, Programming Challenges, Calculated Bets, Count: 4
			Testing GetByShortJobs
			Jobs: "Discrete" Mathematics, Steiner's Tree, Calculated Bets, Count: 3
			Testing GetByEarliestEndTime
			Jobs: "Discrete" Mathematics, Steiner's Tree, Process Terminated, Calculated Bets, Count: 4
			Testing GetByExhaustiveSubsets
			Jobs: The President's Algorist, Steiner's Tree, Process Terminated, Calculated Bets, Count: 4
		 */
		static void Main(string[] args)
		{
			DateTime now = DateTime.Now;
			Job[][] jobs = new[] {
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
			TestJobSelection(jobs, GetByEarliestStartTime);
			TestJobSelection(jobs, GetByShortJobs);
			TestJobSelection(jobs, GetByAllNonOverlappingSubsets);
			TestJobSelection(jobs, GetByEarliestEndTime);
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

		static IEnumerable<Job> GetByAllNonOverlappingSubsets(IEnumerable<Job> jobs)
		{
			var subsets = new SubsetGenerator<Job>(jobs.ToArray());
			var largestSubset = subsets.Where(subset => HasNoOverlappingJob(subset)).Aggregate((subset1, subset2) => (subset1.Length > subset2.Length) ? subset1 : subset2);
			return largestSubset;
		}

		private static bool HasNoOverlappingJob(Job[] subset)
		{
			var sortedJobs = subset.OrderBy(job => job.StartTime);
			DateTime lastEndTime = DateTime.MinValue;
			foreach (var job in sortedJobs)
			{
				if (job.StartTime < lastEndTime) return false;
				lastEndTime = job.EndTime;
			}

			return true;
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

	class SubsetGenerator<T> : IEnumerable<T[]>
	{
		private T[] givenSet;

		public SubsetGenerator(T[] givenSet)
		{
			this.givenSet = givenSet;
		}

		public IEnumerator<T[]> GetEnumerator()
		{
			return new SubsetGeneratorImpl(givenSet);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SubsetGeneratorImpl(givenSet);
		}

		/*
		Subset generator uses a long integer mask to choose elements as part of subset. For each subset the mask is incremented by one
		until all bits of the mask are set.
		 */
		class SubsetGeneratorImpl : IEnumerator<T[]>
		{
			private T[] givenSet;
			private T[] currentSubset;
			private long currentSubsetMask;
			private int subsetCount;
			const int MAX_ITEMS = sizeof(long) * 8;
			public SubsetGeneratorImpl(T[] givenSet)
			{
				if (givenSet.Length > MAX_ITEMS)
				{
					throw new ArgumentException($"Subset generator does not support more {MAX_ITEMS}. Given set has {givenSet.Length} items ");
				}
				this.givenSet = givenSet;
				currentSubset = null;
				currentSubsetMask = 0;
				subsetCount = 1 << givenSet.Length;
			}

			public T[] Current => currentSubset;

			object IEnumerator.Current => currentSubset;

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (currentSubsetMask == subsetCount) return false;
				List<T> subset = new List<T>();
				long subsetMask = currentSubsetMask;
				int subsetIndex = 0;
				while (subsetMask != 0L)
				{
					if ((subsetMask & 1) == 1)
					{
						subset.Add(givenSet[subsetIndex]);
					}

					subsetMask >>= 1;
					subsetIndex++;
				}

				currentSubset = subset.ToArray();
				currentSubsetMask++;
				return true;
			}

			public void Reset()
			{
				currentSubset = null;
				currentSubsetMask = 0;
			}
		}
	}
}
