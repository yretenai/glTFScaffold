using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class NEPTUWUNIUMVertexScale : Property, IExtension {
	public static string ExtensionName => "NEPTUWUNIUM_vertex_scale";

	[JsonPropertyName("offset")]
	public List<double>? Offset { get; set; }

	[JsonPropertyName("scale")]
	public List<double>? Scale { get; set; }

	[JsonPropertyName("component")]
	public int? scaleFromComponent { get; set; } = null;
}
