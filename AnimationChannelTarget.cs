﻿using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>The descriptor of the animated property.</summary>
public record AnimationChannelTarget : Property {
    /// <summary>The index of the node to animate. When undefined, the animated object <b>MAY</b> be defined by an extension.</summary>
    [JsonPropertyName("node")]
    public int? Node { get; set; }

    /// <summary>
    ///     The name of the node's TRS property to animate, or the `"weights"` of the Morph Targets it instantiates. For
    ///     the `"translation"` property, the values that are provided by the sampler are the translation along the X, Y, and Z
    ///     axes. For the `"rotation"` property, the values are a quaternion in the order (x, y, z, w), where w is the scalar.
    ///     For the `"scale"` property, the values are the scaling factors along the X, Y, and Z axes.
    /// </summary>
    [JsonPropertyName("path")]
    public AnimationChannelPath Path { get; set; }
}
