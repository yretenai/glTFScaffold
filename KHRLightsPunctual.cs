namespace GLTF.Scaffold;

public record KHRLightsPunctual {
	public const string EXT_NAME = "KHR_lights_punctual";
	public List<KHRLight>? Lights { get; set; }
	public int? Light { get; set; }

	public (KHRLight Light, int Id) CreateLight() {
		Lights ??= [];
		var id = Lights.Count;
		var light = new KHRLight();
		Lights.Add(light);
		return (light, id);
	}
}
