using System.Numerics;

namespace GLTF.Scaffold;

public static class ExtensionHelpers {
	public static List<double> ToGLTF(this Vector2 vector) {
		return [vector.X, vector.Y];
	}

	public static List<double> ToGLTF(this Vector3 vector) {
		return [vector.X, vector.Y, vector.Z];
	}

	public static List<double> ToGLTF(this Vector4 vector) {
		return [vector.X, vector.Y, vector.Z, vector.W];
	}

	public static List<double> ToGLTF(this Quaternion quaternion) {
		return [quaternion.X, quaternion.Y, quaternion.Z, quaternion.W];
	}
}
