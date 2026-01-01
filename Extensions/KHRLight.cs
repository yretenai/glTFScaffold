namespace GLTF.Scaffold.Extensions;

public class KHRLight : ChildOfRootProperty, IExtension {
	public const string EXT_NAME = "KHR_lights_punctual";
	public static string ExtensionName => EXT_NAME;

	public List<double>? Color { get; set; }
	public double? Intensity { get; set; }
	public string? Type { get; set; }
	public double? Range { get; set; }
	public double? InnerConeAngle { get; set; }
	public double? OuterConeAngle { get; set; }
}
