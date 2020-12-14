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
		private readonly Cpu cpu;
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
		private readonly Memory memory;
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
		/// Tells the current iteration of the simulation
		/// </summary>
		public int Iteration { get; private set; }
		/// <summary>
		/// Current ID process
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Creates a computer
		/// </summary>
		public Computer() {
			this.cpu = new Cpu();
			this.memory = new Memory();
		}
		/// <summary>
		/// Resets the computer
		/// </summary>
		public void Reset() {
			this.cpu.CurrentProcess = null;
			this.memory.CurrentRunningProcesses.Clear();
			this.memory.PendingProcesses.Clear();
			this.Iteration = this.Id = 0;
		}
		/// <summary>
		/// Adds a new process
		/// </summary>
		/// <param name="isSystemPriority">Sets if it's a system process</param>
		public void AddProcess(bool isSystemPriority) {
			var usedSize = 0;
			foreach (var proc in this.memory.CurrentRunningProcesses) {
				usedSize += proc.Size;
			}
			var newProcess = new Process(this.Id, isSystemPriority);
			if (newProcess.Size + usedSize <= memory.Size) {
				this.memory.CurrentRunningProcesses.Add(new Process(this.Id++, isSystemPriority));
			}
		}
		/// <summary>
		/// Runs the current iteration in the computer
		/// </summary>
		public void Run() {
			var systemProcessAmount = 0;
			if (this.cpu.IsProcessing) {
				var newLifespan = this.cpu.CurrentProcess.Lifespan - 1;
				if (newLifespan > 0) {
					this.cpu.CurrentProcess.Lifespan = newLifespan;
				} else {
					this.cpu.CurrentProcess = null;
				}
			}
			var currentRunningProcessesInMemory = this.CurrentRunningProcessesInMemory;
			if (currentRunningProcessesInMemory.Count > 0) {
				foreach (var process in currentRunningProcessesInMemory) {
					if (process.Priority.Equals("Sistema")) {
						systemProcessAmount++;
					}
				}
				if (systemProcessAmount > 0) {
					var index = 0;
					foreach (var process in currentRunningProcessesInMemory) {
						if (process.Priority.Equals("Sistema")) {
							if (this.cpu.IsProcessing && this.cpu.CurrentProcess.Priority.Equals("Usuario")) {
								this.PendingProcessesInMemory.Add(this.cpu.CurrentProcess);
								this.cpu.CurrentProcess = null;
							}
						}
					}
				}
			}
		}
	}
}
