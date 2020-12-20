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
		#region Properties
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
		/// Holds the memory
		/// </summary>
		private Memory memory;
		/// <summary>
		/// Holds the user running processes inside the memory
		/// </summary>
		public List<Process> CurrentUserProcessesInMemory {
			get => this.memory.UserProcesses.ToList();
			private set { }
		}
		/// <summary>
		/// Holds the system processes in memory
		/// </summary>
		public List<Process> CurrentSystemProcessesInMemory {
			get => this.memory.SystemProcesses.ToList();
			private set { }
		}
		/// <summary>
		/// Holds the pending processes in memory
		/// </summary>
		public List<Process> CurrentPendingProcessesInMemory {
			get => this.memory.PendingUserProcesses.ToList();
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
		#endregion
		#region Constructor
		public Computer() {
			this.cpu = new Cpu();
			this.memory = new Memory();
		}
		#endregion
		#region Methods
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
			foreach (var proc in this.memory.SystemProcesses) {
				usedSize += proc.Size;
			}
			foreach (var proc in this.memory.UserProcesses) {
				usedSize += proc.Size;
			}
			var newProcess = new Process(this.Id, isSystemPriority);
			if (newProcess.Size + usedSize <= memory.Size) {
				if (isSystemPriority) {
					this.memory.SystemProcesses.Enqueue(newProcess);
				} else {
					this.memory.UserProcesses.Enqueue(newProcess);
				}
				this.Id++;
			}
		}
		/// <summary>
		/// Runs the current iteration in the computer
		/// </summary>
		public void Run() {
			if (this.CurrentProcessInCpu == null) {
				if (this.memory.SystemProcesses.Count > 0 && this.memory.SystemProcesses.Dequeue() is Process systemProcess) {
					this.CurrentProcessInCpu = systemProcess;
				} else if (this.memory.PendingUserProcesses.Count > 0 && this.memory.PendingUserProcesses.Dequeue() is Process pendingProcess) {
					this.CurrentProcessInCpu = pendingProcess;
				} else if (this.memory.UserProcesses.Count > 0 && this.memory.UserProcesses.Dequeue() is Process userProcess) {
					this.CurrentProcessInCpu = userProcess;
				}
			} else if (this.CurrentProcessInCpu is Process currentProcessInCpu) {
				if (this.memory.SystemProcesses.Count > 0 && this.memory.SystemProcesses.Peek() is Process systemProcess) {
					if (this.CurrentProcessInCpu.Priority.Equals("Usuario")) {
						this.memory.PendingUserProcesses.Enqueue(this.cpu.CurrentProcess);
						this.cpu.CurrentProcess = this.memory.SystemProcesses.Dequeue();
					}
				}
				if (currentProcessInCpu.Lifespan > 0) {
					currentProcessInCpu.Iterate();
				} else {
					this.CurrentProcessInCpu = null;
				}
				this.Iteration++;
			}
		}
		#endregion
	}
}
