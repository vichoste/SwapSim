using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the computer
	/// </summary>
	public sealed class Computer {
		/// <summary>
		/// Holds the CPU
		/// </summary>
		private Cpu cpu;
		/// <summary>
		/// Gets the current process inside the CPU
		/// </summary>
		public Process CurrentProcessInCpu {
			get => this.cpu.CurrentProcess;
			private set { }
		}
		/// <summary>
		/// Tells if there's a process processing inside the CPU
		/// </summary>
		public bool IsProcessingInCpu {
			get => this.cpu.IsProcessing;
			private set { }
		}
		/// <summary>
		/// Holds the memory
		/// </summary>
		private Memory memory;
		/// <summary>
		/// Holds the current running processes inside the memory
		/// </summary>
		public List<Process> CurrentRunningProcessesInMemory {
			get => this.memory.CurrentRunningProcesses.ToList();
			private set { }
		}
		/// <summary>
		/// Holds the pending processes in memory
		/// </summary>
		public List<Process> PendingProcessesInMemory {
			get => this.memory.PendingProcesses.ToList();
			private set { }
		}
		/// <summary>
		/// Tells the memory size
		/// </summary>
		public int SizeInMemory {
			get => this.memory.Size;
			private set { }
		}
		/// <summary>
		/// Creates a computer
		/// </summary>
		public Computer() {
			this.cpu = new Cpu();
			this.memory = new Memory();
		}
	}
}
