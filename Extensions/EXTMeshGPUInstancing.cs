using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class EXTMeshGPUInstancing : Property, IExtension {
	public const string EXT_NAME = "EXT_mesh_gpu_instancing";
	public static string ExtensionName => EXT_NAME;

	[JsonPropertyName("attributes")]
	public Dictionary<string, int> Attributes { get; set; } = new();
}
