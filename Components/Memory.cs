using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the memory
	/// </summary>
	public sealed class Memory {
		/// <summary>
		/// System processes queue
		/// </summary>
		public Queue<Process> SystemProcesses {
			get; private set;
		}
		/// <summary>
		/// User processes queue
		/// </summary>
		public Queue<Process> UserProcesses {
			get; private set;
		}
		/// <summary>
		/// Pending user processes
		/// </summary>
		public Queue<Process> PendingUserProcesses {
			get; private set;
		}
		/// <summary>
		/// Memory size. It will always have fixed size
		/// </summary>
		public readonly int Size = 1024;
		/// <summary>
		/// Creates a memory
		/// </summary>
		public Memory() {
			this.SystemProcesses = new Queue<Process>();
			this.UserProcesses = new Queue<Process>();
			this.PendingUserProcesses = new Queue<Process>();
		}
	}
}
