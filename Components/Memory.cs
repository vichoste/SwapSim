using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the memory
	/// </summary>
	sealed class Memory {
		/// <summary>
		/// Stores the current running processes inside the memory
		/// </summary>
		public Queue<Process> CurrentRunningProcesses;
		/// <summary>
		/// Stores the moved processes due to priority
		/// </summary>
		public Queue<Process> PendingProcesses;
		/// <summary>
		/// Memory size. It will always have fixed size
		/// </summary>
		public readonly int Size = 1024;
		public Memory() {
		}
	}
}
