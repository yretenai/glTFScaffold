// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>Sparse storage of accessor values that deviate from their initialization value.</summary>
public class Sparse : Property {
	/// <summary>Number of deviating accessor values stored in the sparse array.</summary>
	[JsonPropertyName("count")]
	public int Count { get; set; }

	/// <summary>
	///     An object pointing to a buffer view containing the indices of deviating accessor values. The number of indices
	///     is equal to `count`. Indices <b>MUST</b> strictly increase.
	/// </summary>
	[JsonPropertyName("indices")]
	public SparseIndex Indices { get; set; } = new();

	/// <summary>An object pointing to a buffer view containing the deviating accessor values.</summary>
	[JsonPropertyName("values")]
	public SparseValue Values { get; set; } = new();
}
