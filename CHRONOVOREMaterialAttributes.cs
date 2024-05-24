using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public record CHRONOVOREMaterialAttributes : Property {
	public const string EXT_NAME = "CHRONOVORE_material_attributes";

	[JsonPropertyName("textures")]
	public Dictionary<string, TextureInfo>? Textures { get; set; }

	[JsonPropertyName("scalars")]
	public Dictionary<string, double>? Scalars { get; set; }

	[JsonPropertyName("colors")]
	public Dictionary<string, List<double>>? Colors { get; set; }

	[JsonPropertyName("workflow")]
	public string? Workflow { get; set; }
}
