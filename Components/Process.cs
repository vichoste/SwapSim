using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the selected priority on a process
	/// </summary>
	enum Priority {
		User,
		System
	}
	/// <summary>
	/// Represents the process
	/// </summary>
	sealed class Process {
		/// <summary>
		/// Process id
		/// </summary>
		public byte Id {
			get; private set;
		}
		/// <summary>
		/// Size of the process
		/// </summary>
		public byte Size {
			get; private set;
		}
		/// <summary>
		/// Priority of the process
		/// </summary>
		public Priority Priority {
			get; private set;
		}
		/// <summary>
		/// Life span of the process
		/// </summary>
		public byte Lifespan {
			get; private set;
		}
		/// <summary>
		/// Creates a process
		/// </summary>
		/// <param name="id">Process ID</param>
		/// <param name="size">Process size</param>
		/// <param name="isSystemPriority">Tells if the process has a system priority</param>
		/// <param name="lifeSpan">Lifespan in iterations</param>
		public Process(byte id, byte size, bool isSystemPriority, byte lifeSpan) {
			this.Id = id;
			this.Size = size;
			this.Priority = isSystemPriority ? Priority.System : Priority.User;
			this.Lifespan = lifeSpan;
		}
	}
}
