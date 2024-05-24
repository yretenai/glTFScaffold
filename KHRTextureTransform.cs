using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public record KHRTextureTransform : Property {
	public const string EXT_NAME = "KHR_texture_transform";

	[JsonPropertyName("offset")]
	public List<double>? Offset { get; set; }

	[JsonPropertyName("rotation")]
	public double? Rotation { get; set; }

	[JsonPropertyName("scale")]
	public List<double>? Scale { get; set; }

	[JsonPropertyName("texCoord")]
	public int? TexCoord { get; set; }
}
