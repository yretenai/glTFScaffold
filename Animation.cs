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

	public (AnimationChannel Channel, int Id) CreateChannel(int samplerId, Node node, AnimationChannelPath path) => CreateChannel(samplerId, node.Id, path);

	public (AnimationChannel Channel, int Id) CreateChannel(int samplerId, int nodeId, AnimationChannelPath path) {
		var id = Channels.Count;
		var channel = new AnimationChannel {
			Sampler = samplerId,
			Target = new AnimationChannelTarget {
				Node = nodeId,
				Path = path,
			},
		};
		Channels.Add(channel);
		return (channel, id);
	}

	public (AnimationSampler Sampler, int Id) CreateSampler(int timeAccessor, AnimationInterpolation interpolation, int valuesAccessor) {
		var id = Samplers.Count;
		var sampler = new AnimationSampler {
			Input = timeAccessor,
			Interpolation = interpolation,
			Output = valuesAccessor,
		};
		Samplers.Add(sampler);
		return (sampler, id);
	}
}
