namespace GLTF.Scaffold.Extensions;

public class KHRLightsPunctual : Property, IExtension {
	public const string EXT_NAME = "KHR_lights_punctual";
	public static string ExtensionName => EXT_NAME;

	public List<KHRLight>? Lights { get; set; }
	public int? Light { get; set; }

	public (KHRLight Light, int Id) CreateLight(string name) {
		Lights ??= [];
		var id = Lights.Count;
		var light = new KHRLight {
			Name = name,
		};
		Lights.Add(light);
		return (light, id);
	}
}
