using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CameraType {
    perspective,
    orthographic,
}
