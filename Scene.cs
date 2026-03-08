// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>The root nodes of a scene.</summary>
public class Scene : ChildOfRootProperty, INodeCreator {
	/// <summary>The indices of each root node.</summary>
	[JsonPropertyName("nodes")]
	public List<int> Nodes { get; set; } = [];

	public (Node Node, int Id) CreateNode(Root root, string name) {
		var node = new Node {
			Name = name,
		};
		root.Nodes ??= [];
		var id = root.Nodes.Count;
		root.Nodes.Add(node);
		Nodes.Add(id);
		node.Id = id;
		return (node, id);
	}
}
