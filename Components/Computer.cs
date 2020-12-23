using System.Collections.Generic;
using System.Linq;

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
		/// Holds the disk
		/// </summary>
		private Disk disk;
		/// <summary>
		/// Holds the current processes in disk
		/// </summary>
		public List<Process> CurrentProcessesInDisk {
			get => this.disk.Processes.ToList();
			private set { }
		}
		/// <summary>
		/// Tells the disk size
		/// </summary>
		public int SizeInDisk {
			get => this.disk.Size;
			private set { }
		}
		#endregion
		#region Constructor
		/// <summary>
		/// Creates a computer
		/// </summary>
		public Computer() {
			this.cpu = new Cpu();
			this.memory = new Memory();
			this.disk = new Disk();
		}
		#endregion
		#region Methods
		/// <summary>
		/// Resets the computer
		/// </summary>
		public void Reset() {
			this.cpu = new Cpu();
			this.memory = new Memory();
			this.disk = new Disk();
			this.Iteration = this.Id = 0;
		}
		/// <summary>
		/// Adds a new process
		/// </summary>
		/// <param name="isSystemPriority">Sets if it's a system process</param>
		public void AddProcess(bool isSystemPriority) {
			var usedSize = this.CalculateUsedSizeInMemory();
			var newProcess = new Process(this.Id, isSystemPriority);
			if (newProcess.Size + usedSize <= this.memory.Size) {
				if (isSystemPriority) {
					this.memory.SystemProcesses.Enqueue(newProcess);
				} else {
					this.memory.UserProcesses.Enqueue(newProcess);
				}
				this.Id++;
			} else {
				var usedSizeInDisk = 0;
				foreach (var proc in this.disk.Processes) {
					usedSizeInDisk += proc.Size;
				}
				if (newProcess.Size + usedSizeInDisk <= this.disk.Size) {
					this.disk.Processes.Enqueue(newProcess);
					this.Id++;
				}
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
				if (this.memory.SystemProcesses.Count > 0 && this.memory.SystemProcesses.Peek() is Process) {
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
			if (this.disk.Processes.Count > 0 && this.disk.Processes.Peek() is Process processInDisk) {
				var usedSize = this.CalculateUsedSizeInMemory();
				if (processInDisk.Size + usedSize <= this.memory.Size) {
					var processToMove = this.disk.Processes.Dequeue();
					if (processToMove.Priority.Equals("Usuario")) {
						this.memory.UserProcesses.Enqueue(processToMove);
					} else {
						this.memory.SystemProcesses.Enqueue(processToMove);
					}
					this.Iteration++;
				}
			}
		}
		/// <summary>
		/// Calculates how much memory is currently being used
		/// </summary>
		/// <returns></returns>
		private int CalculateUsedSizeInMemory() {
			var usedSize = 0;
			foreach (var proc in this.memory.SystemProcesses) {
				usedSize += proc.Size;
			}
			foreach (var proc in this.memory.UserProcesses) {
				usedSize += proc.Size;
			}
			return usedSize;
		}
		#endregion
	}
}
