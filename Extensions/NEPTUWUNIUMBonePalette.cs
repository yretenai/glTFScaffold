using System.Text.Json.Serialization;

namespace GLTF.Scaffold.Extensions;

public class NEPTUWUNIUMBonePalette : Property, IExtension {
	public const string EXT_NAME = "NEPTUWUNIUM_bone_palette";
	public static string ExtensionName => EXT_NAME;

	/// <summary>
	/// Index to palette accessor
	/// </summary>
	[JsonPropertyName("palette")]
	public int Palette { get; set; }
}
