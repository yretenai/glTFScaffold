// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class NEPTUWUNIUMVertexScale : Property, IExtension {
	public static string ExtensionName => "NEPTUWUNIUM_vertex_scale";

	[JsonPropertyName("offset")]
	public List<double>? Offset { get; set; }

	[JsonPropertyName("scale")]
	public List<double>? Scale { get; set; }

	[JsonPropertyName("component")]
	public int? Component { get; set; } = null;

	[JsonPropertyName("componentScale")]
	public double? ComponentScale { get; set; } = null;
}
