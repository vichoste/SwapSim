using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the CPU
	/// </summary>
	public sealed class Cpu {
		/// <summary>
		/// Tells if the CPU is currently processing a process
		/// </summary>
		public bool IsProcessing { get; set; }
		/// <summary>
		/// The current processes of the CPU
		/// </summary>
		public List<Process> CurrentProcesses;
		/// <summary>
		/// The current processes of the CPU
		/// </summary>
		public Process this[int i] {
			get => this.CurrentProcesses[i];
			set {
				if (value == null) {
					this.CurrentProcesses = null;
					this.IsProcessing = false;
					return;
				}
				this.CurrentProcesses[i] = value;
				this.IsProcessing = true;
			}
		}
		/// <summary>
		/// Creates a CPU
		/// </summary>
		public Cpu() => this.CurrentProcesses = new List<Process>();
	}
}
