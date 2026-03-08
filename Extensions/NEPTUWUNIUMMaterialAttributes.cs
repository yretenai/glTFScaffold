// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class NEPTUWUNIUMMaterialAttributes : Property, IExtension {
	public static string ExtensionName => "NEPTUWUNIUM_material_attributes";

	[JsonPropertyName("textures")]
	public Dictionary<string, TextureInfo>? Textures { get; set; }

	[JsonPropertyName("scalars")]
	public Dictionary<string, double>? Scalars { get; set; }

	[JsonPropertyName("colors")]
	public Dictionary<string, List<double>>? Colors { get; set; }

	[JsonPropertyName("workflow")]
	public string? Workflow { get; set; }
}
