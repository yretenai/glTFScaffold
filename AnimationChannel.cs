// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>An animation channel combines an animation sampler with a target property being animated.</summary>
public class AnimationChannel : Property {
	/// <summary>
	///     The index of a sampler in this animation used to compute the value for the target, e.g., a node's translation,
	///     rotation, or scale (TRS).
	/// </summary>
	[JsonPropertyName("sampler")]
	public int Sampler { get; set; }

	/// <summary>The descriptor of the animated property.</summary>
	[JsonPropertyName("target")]
	public AnimationChannelTarget Target { get; set; } = new();
}
