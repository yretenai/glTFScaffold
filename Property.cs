// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

public class Property {
	/// <summary>JSON object with extension-specific objects.</summary>
	[JsonPropertyName("extensions")]
	public Dictionary<string, JsonValue>? Extensions { get; set; }

	/// <summary>
	///     Although `extras` <b>MAY</b> have any type, it is common for applications to store and access custom data as
	///     key/value pairs. Therefore, `extras` <b>SHOULD</b> be a JSON object rather than a primitive value for best
	///     portability.
	/// </summary>
	[JsonPropertyName("extras")]
	public Dictionary<string, JsonValue>? Extras { get; set; }

	public static JsonNodeOptions? GltfNodeOptions => new() {
		PropertyNameCaseInsensitive = true,
	};

	public T? GetExtension<T>() where T : IExtension {
		if (Extensions == null) {
			return default;
		}

		return !Extensions.TryGetValue(T.ExtensionName, out var extension) ? default : extension.GetValue<T>();
	}

	public void AddExtension<T>(T extension) where T : IExtension {
		Extensions ??= [];
		Extensions[T.ExtensionName] = JsonValue.Create(extension, GltfNodeOptions)!;
	}
}

public interface IExtension {
	static abstract string ExtensionName { get; }
}
