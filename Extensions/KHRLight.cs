// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

namespace GLTF.Scaffold.Extensions;

public class KHRLight : ChildOfRootProperty, IExtension {
	public static string ExtensionName => "KHR_lights_punctual";

	public List<double>? Color { get; set; }
	public double? Intensity { get; set; }
	public string? Type { get; set; }
	public double? Range { get; set; }
	public double? InnerConeAngle { get; set; }
	public double? OuterConeAngle { get; set; }
}
