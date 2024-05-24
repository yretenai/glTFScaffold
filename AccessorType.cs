using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccessorType {
	SCALAR,
	VEC2,
	VEC3,
	VEC4,
	MAT2,
	MAT3,
	MAT4,
}
