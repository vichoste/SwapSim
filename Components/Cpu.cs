﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapSim.Components {
	/// <summary>
	/// Represents the CPU
	/// </summary>
	sealed class Cpu {
		/// <summary>
		/// Tells if the CPU is currently processing a process
		/// </summary>
		public bool IsProcessing { get; set; }
		/// <summary>
		/// The current process of the CPU
		/// </summary>
		private Process process;
		/// <summary>
		/// The current process of the CPU
		/// </summary>
		public Process CurrentProcess {
			get => this.process;
			set {
				this.process = value;
				this.IsProcessing = true;
			}
		}
		/// <summary>
		/// Creates a CPU
		/// </summary>
		public Cpu() {
		}
	}
}