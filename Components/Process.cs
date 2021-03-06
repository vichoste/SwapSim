﻿using System;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the selected priority on a process
	/// </summary>
	public enum PriorityEnum {
		User,
		System
	}
	/// <summary>
	/// Represents the process
	/// </summary>
	public sealed class Process {
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
		private readonly PriorityEnum priority;
		/// <summary>
		/// Priority of the process
		/// </summary>
		public string Priority {
			get => this.priority == PriorityEnum.User ? "Usuario" : "Sistema";
			private set { }
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
		public Process(int id, bool isSystemPriority) {
			var random = new Random();
			this.Id = id;
			this.Size = random.Next(128, 512);
			this.priority = isSystemPriority ? PriorityEnum.System : PriorityEnum.User;
			this.Lifespan = random.Next(1, 10);
		}
		/// <summary>
		/// Decrements the lifespan of the process by one unit
		/// </summary>
		public void Iterate() => this.Lifespan--;
	}
}
