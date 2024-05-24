using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>A set of primitives to be rendered. Its global transform is defined by a node that references it.</summary>
public record Mesh : ChildOfRootProperty {
	/// <summary>An array of primitives, each defining geometry to be rendered.</summary>
	[JsonPropertyName("primitives")]
	public List<Primitive> Primitives { get; set; } = [];

	/// <summary>
	///     Array of weights to be applied to the morph targets. The number of array elements <b>MUST</b> match the number
	///     of morph targets.
	/// </summary>
	[JsonPropertyName("weights")]
	public List<double>? Weights { get; set; }
}
