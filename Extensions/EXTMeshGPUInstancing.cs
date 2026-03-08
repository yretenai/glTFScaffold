// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class EXTMeshGPUInstancing : Property, IExtension {
	public static string ExtensionName => "EXT_mesh_gpu_instancing";

	[JsonPropertyName("attributes")]
	public Dictionary<string, int> Attributes { get; set; } = new();
}
