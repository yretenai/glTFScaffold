// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public class OcclusionTextureInfo : TextureInfo {
	/// <summary>
	///     A scalar parameter controlling the amount of occlusion applied. A value of `0.0` means no occlusion. A value
	///     of `1.0` means full occlusion. This value affects the final occlusion value as:
	///     <code>1.0 + strength * (&lt;sampled occlusion texture value&gt; - 1.0)</code>
	/// </summary>
	[JsonPropertyName("strength")]
	public double? Strength { get; set; } = 1D;
}
