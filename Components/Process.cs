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
		public int Id {
			get; private set;
		}
		/// <summary>
		/// Size of the process. Totally random
		/// </summary>
		public int Size {
			get; private set;
		}
		/// <summary>
		/// Priority of the process
		/// </summary>
		public Priority Priority {
			get; private set;
		}
		/// <summary>
		/// Life span of the process. Totally random
		/// </summary>
		public int Lifespan {
			get; private set;
		}
		/// <summary>
		/// Creates a process
		/// </summary>
		/// <param name="id">Process ID</param>
		/// <param name="isSystemPriority">Tells if the process has a system priority</param>
		/// <param name="lifeSpan">Lifespan in iterations</param>
		public Process(byte id, bool isSystemPriority) {
			var random = new Random();
			this.Id = id;
			this.Size = random.Next(128, 512);
			this.Priority = isSystemPriority ? Priority.System : Priority.User;
			this.Lifespan = random.Next(1, 10);
		}
	}
}
