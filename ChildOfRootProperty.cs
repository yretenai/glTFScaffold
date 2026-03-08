// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public class ChildOfRootProperty : Property {
	/// <summary>
	///     The user-defined name of this object. This is not necessarily unique, e.g., an accessor and a buffer could
	///     have the same name, or two accessors could even have the same name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string? Name { get; set; }
}
