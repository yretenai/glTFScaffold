// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class NEPTUWUNIUMBonePalette : Property, IExtension {
	public static string ExtensionName => "NEPTUWUNIUM_bone_palette";

	/// <summary>
	/// Index to palette accessor
	/// </summary>
	[JsonPropertyName("palette")]
	public required int Palette { get; set; }
}
