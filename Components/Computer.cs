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
			private set => this.cpu.CurrentProcess = value;
		}
		/// <summary>
		/// Tells if there's a process processing inside the CPU
		/// </summary>
		public bool IsProcessingInCpu {
			get => this.cpu.IsProcessing;
			private set => this.cpu.IsProcessing = value;
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
			this.cpu = new Cpu();
			this.memory = new Memory();
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
			if (this.CurrentProcessInCpu is Process currentProcessInCpu) {
				if (currentProcessInCpu.Lifespan > 0) {
					currentProcessInCpu.Iterate();
				} else {
					this.CurrentProcessInCpu = null;
				}
				this.Iteration++;
			}
			if (this.memory.CurrentRunningProcesses.Count != 0 && this.memory.CurrentRunningProcesses[0] is Process currentProcessInMemory) {
				this.CurrentProcessInCpu = currentProcessInMemory;
				this.memory.CurrentRunningProcesses.RemoveAt(0);
				this.Iteration++;
			}
		}
	}
}
