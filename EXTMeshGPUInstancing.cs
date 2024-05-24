using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public record EXTMeshGPUInstancing : Property {
	public const string EXT_NAME = "EXT_mesh_gpu_instancing";

	[JsonPropertyName("attributes")]
	public Dictionary<string, int> Attributes { get; set; } = new();
}
