using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the disk
	/// </summary>
	public sealed class Disk {
		/// <summary>
		/// Processes waiting to get into the memory
		/// </summary>
		public Queue<Process> Processes {
			get; private set;
		}
		/// <summary>
		/// Disk- size. It will always have fixed size
		/// </summary>
		public readonly int Size = 4096;
		/// <summary>
		/// Creates a disk
		/// </summary>
		public Disk() {
			this.Processes = new Queue<Process>();
		}
	}
}
