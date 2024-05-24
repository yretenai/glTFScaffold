using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>A keyframe animation.</summary>
public record Animation : ChildOfRootProperty {
    /// <summary>
    ///     An array of animation channels. An animation channel combines an animation sampler with a target property
    ///     being animated. Different channels of the same animation <b>MUST NOT</b> have the same targets.
    /// </summary>
    [JsonPropertyName("channels")]
    public List<AnimationChannel> Channels { get; set; } = [];

    /// <summary>
    ///     An array of animation samplers. An animation sampler combines timestamps with a sequence of output values and
    ///     defines an interpolation algorithm.
    /// </summary>
    [JsonPropertyName("samplers")]
    public List<AnimationSampler> Samplers { get; set; } = [];
}
