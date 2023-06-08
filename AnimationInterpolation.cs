using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnimationInterpolation {
    LINEAR,
    STEP,
    CUBICSPLINE,
}
